using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using SQLI_CrossAR.Droid.Algorithm;
using SQLI_CrossAR.CrossAR.Algorithm;
using Java.Util;
using Android.Hardware;

[assembly: Dependency(typeof(DroidOrientation))]
namespace SQLI_CrossAR.Droid.Algorithm
{
    public class DroidOrientation : Java.Lang.Object, IDeviceOrientation
    {

        #region properties

        //used in the gyroFunction
        private static float NS2S = 1.0f / 1000000000.0f;
        private float timestamp;
        private bool initState = true;

        public static int TIME_CONSTANT = 30;
        public static float FILTER_COEFFICIENT = 0.98f;
        private Timer fuseTimer = new Timer();

        //
        public static float EPSILON = 0.000000001f;
        // angular speeds from gyro
        private Vector3D gyro = new Vector3D();

        // rotation matrix from gyro data
        private static float[] gyroMatrix = new float[9];

        // orientation angles from gyro matrix
        private static Vector3D gyroOrientation = new Vector3D();

        // magnetic field vector
        private Vector3D magnet = new Vector3D();

        // accelerometer vector
        private Vector3D accel = new Vector3D();

        // orientation angles from accel and magnet
        private static Vector3D accMagOrientation = new Vector3D();

        // final orientation angles from sensor fusion
        public static Vector3D fusedOrientation = new Vector3D();

        // accelerometer and magnetometer based rotation matrix
        private float[] rotationMatrix = new float[9];
        #endregion
        private static long newTimestamp;

        public DroidOrientation()
        {
            gyroOrientation.X = 0.0f;
            gyroOrientation.Y = 0.0f;
            gyroOrientation.Z = 0.0f;

            // initialise gyroMatrix with identity matrix
            gyroMatrix[0] = 1.0f; gyroMatrix[1] = 0.0f; gyroMatrix[2] = 0.0f;
            gyroMatrix[3] = 0.0f; gyroMatrix[4] = 1.0f; gyroMatrix[5] = 0.0f;
            gyroMatrix[6] = 0.0f; gyroMatrix[7] = 0.0f; gyroMatrix[8] = 1.0f;



            // wait for one second until gyroscope and magnetometer/accelerometer
            // data is initialised then scedule the complementary filter task
            fuseTimer.ScheduleAtFixedRate(new calculateFusedOrientationTask(), 1000, TIME_CONSTANT);

        }

        public Vector3D getFusOrientation()
        {
            return fusedOrientation;
        }
        public void setAccelerometerParam(Vector3D values)
        {
            accel = values;
        }
        public void setMagnetometerParam(Vector3D values)
        {
            gyroOrientation = values;
        }

        //this method changes a Vector3D into a float[3]
        public float[] getVector(Vector3D v)
        {
            float[] vf = new float[3];
            vf[0] = (float)v.X;
            vf[1] = (float)v.Y;
            vf[2] = (float)v.Z;
            return vf;
        }

        //you have to set the accelerometer and gyroscope values before calling this.
        public void StartCalculating()
        {
            newTimestamp = new DateTime().Ticks;
            //collect data
            calculateAccMagOrientation();
            gyroFunction(gyroOrientation);

        }


        #region implement orientation calculations
        public void calculateAccMagOrientation()
        {
            //SensorManager.get
            if (SensorManager.GetRotationMatrix(rotationMatrix, null, getVector(accel), getVector(magnet)))
            {
                SensorManager.GetOrientation(rotationMatrix, getVector(accMagOrientation));
            }
        }




        public void gyroFunction(Vector3D values)
        {
            // don't start until first accelerometer/magnetometer orientation has been acquired
            if (accMagOrientation == null)
                return;

            // initialisation of the gyroscope based rotation matrix
            if (initState)
            {
                newTimestamp = new DateTime().Ticks;
                float[] initMatrix = new float[9];
                initMatrix = getRotationMatrixFromOrientation(accMagOrientation);
                float[] test = new float[3];
                SensorManager.GetOrientation(initMatrix, test); //return pur value of the camera orientation into test: azimuth or Yaw=test[0], pitch=test[1], roll=test[2]

                //here we start 
                gyroMatrix = matrixMultiplication(gyroMatrix, initMatrix);
                initState = false;
            }

            // copy the new gyro values into the gyro array
            // convert the raw gyro data into a rotation vector
            float[] deltaVector = new float[4];
            if (timestamp != 0)
            {
                float dT = (newTimestamp - timestamp) * NS2S;
                values = gyro;
                //System.Arraycopy(event.values, 0, gyro, 0, 3);
                getRotationVectorFromGyro(gyro, deltaVector, dT / 2.0f);
            }

            // measurement done, save current time for next interval
            timestamp = newTimestamp;

            // convert rotation vector into rotation matrix
            float[] deltaMatrix = new float[9];
            SensorManager.GetRotationMatrixFromVector(deltaMatrix, deltaVector);

            // apply the new rotation interval on the gyroscope based rotation matrix
            gyroMatrix = matrixMultiplication(gyroMatrix, deltaMatrix);

            // get the gyroscope based orientation from the rotation matrix
            SensorManager.GetOrientation(gyroMatrix, getVector(gyroOrientation));
        }


        //retrieves gyro rotation values
        private void getRotationVectorFromGyro(Vector3D gyroValues,
                                       float[] deltaRotationVector,
                                       float timeFactor)
        {
            Vector3D normValues = new Vector3D();

            // Calculate the angular speed of the sample
            float omegaMagnitude =
                (float)Math.Sqrt(gyroValues.X * gyroValues.X +
                gyroValues.Y * gyroValues.Y +
                gyroValues.Z * gyroValues.Z);

            // Normalize the rotation vector if it's big enough to get the axis
            if (omegaMagnitude > EPSILON)
            {
                normValues.X = gyroValues.X / omegaMagnitude;
                normValues.Y = gyroValues.Y / omegaMagnitude;
                normValues.Z = gyroValues.Z / omegaMagnitude;
            }


            // Integrate around this axis with the angular speed by the timestep
            // in order to get a delta rotation from this sample over the timestep
            // We will convert this axis-angle representation of the delta rotation
            // into a quaternion before turning it into the rotation matrix.
            float thetaOverTwo = omegaMagnitude * timeFactor;
            float sinThetaOverTwo = (float)Math.Sin(thetaOverTwo);
            float cosThetaOverTwo = (float)Math.Cos(thetaOverTwo);
            deltaRotationVector[0] = (float)(sinThetaOverTwo * normValues.X);
            deltaRotationVector[1] = (float)(sinThetaOverTwo * normValues.Y);
            deltaRotationVector[2] = (float)(sinThetaOverTwo * normValues.Z);
            deltaRotationVector[3] = cosThetaOverTwo;
        }
        private static float[] getRotationMatrixFromOrientation(Vector3D o)
        {
            float[] xM = new float[9];
            float[] yM = new float[9];
            float[] zM = new float[9];

            float sinX = (float)Math.Sin(o.Y);
            float cosX = (float)Math.Cos(o.Y);
            float sinY = (float)Math.Sin(o.Z);
            float cosY = (float)Math.Cos(o.Z);
            float sinZ = (float)Math.Sin(o.X);
            float cosZ = (float)Math.Cos(o.X);

            // rotation about x-axis (pitch)
            xM[0] = 1.0f; xM[1] = 0.0f; xM[2] = 0.0f;
            xM[3] = 0.0f; xM[4] = cosX; xM[5] = sinX;
            xM[6] = 0.0f; xM[7] = -sinX; xM[8] = cosX;

            // rotation about y-axis (roll)
            yM[0] = cosY; yM[1] = 0.0f; yM[2] = sinY;
            yM[3] = 0.0f; yM[4] = 1.0f; yM[5] = 0.0f;
            yM[6] = -sinY; yM[7] = 0.0f; yM[8] = cosY;

            // rotation about z-axis (azimuth)
            zM[0] = cosZ; zM[1] = sinZ; zM[2] = 0.0f;
            zM[3] = -sinZ; zM[4] = cosZ; zM[5] = 0.0f;
            zM[6] = 0.0f; zM[7] = 0.0f; zM[8] = 1.0f;

            // rotation order is y, x, z (roll, pitch, azimuth)
            float[] resultMatrix = matrixMultiplication(xM, yM);
            resultMatrix = matrixMultiplication(zM, resultMatrix);
            return resultMatrix;
        }

        private static float[] matrixMultiplication(float[] A, float[] B)
        {
            float[] result = new float[9];

            result[0] = A[0] * B[0] + A[1] * B[3] + A[2] * B[6];
            result[1] = A[0] * B[1] + A[1] * B[4] + A[2] * B[7];
            result[2] = A[0] * B[2] + A[1] * B[5] + A[2] * B[8];

            result[3] = A[3] * B[0] + A[4] * B[3] + A[5] * B[6];
            result[4] = A[3] * B[1] + A[4] * B[4] + A[5] * B[7];
            result[5] = A[3] * B[2] + A[4] * B[5] + A[5] * B[8];

            result[6] = A[6] * B[0] + A[7] * B[3] + A[8] * B[6];
            result[7] = A[6] * B[1] + A[7] * B[4] + A[8] * B[7];
            result[8] = A[6] * B[2] + A[7] * B[5] + A[8] * B[8];

            return result;
        }



        class calculateFusedOrientationTask : TimerTask
        {
            public override void Run()
            {
                float oneMinusCoeff = 1.0f - FILTER_COEFFICIENT;
                /*
             * Fix for 179° <--> -179° transition problem:
             * Check whether one of the two orientation angles (gyro or accMag) is negative while the other one is positive.
             * If so, add 360° (2 * math.PI) to the negative value, perform the sensor fusion, and remove the 360° from the result
             * if it is greater than 180°. This stabilizes the output in positive-to-negative-transition cases.
             */

                // azimuth
                if (gyroOrientation.X < -0.5 * Math.PI && accMagOrientation.X > 0.0)
                {
                    fusedOrientation.X = (float)(FILTER_COEFFICIENT * (gyroOrientation.X + 2.0 * Math.PI) + oneMinusCoeff * accMagOrientation.X);
                    fusedOrientation.X -= (fusedOrientation.X > Convert.ToSingle(Math.PI)) ? Convert.ToSingle(2.0 * Math.PI) : 0;
                }
                else if (accMagOrientation.X < -0.5 * Math.PI && gyroOrientation.X > 0.0)
                {
                    fusedOrientation.X = (float)(FILTER_COEFFICIENT * gyroOrientation.X + oneMinusCoeff * (accMagOrientation.X + 2.0 * Math.PI));
                    fusedOrientation.X -= (fusedOrientation.X > Math.PI) ? Convert.ToSingle(2.0 * Math.PI) : 0;
                }
                else
                {
                    fusedOrientation.X = FILTER_COEFFICIENT * gyroOrientation.X + oneMinusCoeff * accMagOrientation.X;
                }

                // pitch
                if (gyroOrientation.Y < -0.5 * Math.PI && accMagOrientation.Y > 0.0)
                {
                    fusedOrientation.Y = (float)(FILTER_COEFFICIENT * (gyroOrientation.Y + 2.0 * Math.PI) + oneMinusCoeff * accMagOrientation.Y);
                    fusedOrientation.Y -= (fusedOrientation.Y > Math.PI) ? Convert.ToSingle(2.0 * Math.PI) : 0;
                }
                else if (accMagOrientation.Y < -0.5 * Math.PI && gyroOrientation.Y > 0.0)
                {
                    fusedOrientation.Y = (float)(FILTER_COEFFICIENT * gyroOrientation.Y + oneMinusCoeff * (accMagOrientation.Y + 2.0 * Math.PI));
                    fusedOrientation.Y -= (fusedOrientation.Y > Math.PI) ? Convert.ToSingle(2.0 * Math.PI) : 0;
                }
                else
                {
                    fusedOrientation.Y = FILTER_COEFFICIENT * gyroOrientation.Y + oneMinusCoeff * accMagOrientation.Y;
                }

                // roll
                if (gyroOrientation.Z < -0.5 * Math.PI && accMagOrientation.Z > 0.0)
                {
                    fusedOrientation.Z = (float)(FILTER_COEFFICIENT * (gyroOrientation.Z + 2.0 * Math.PI) + oneMinusCoeff * accMagOrientation.Z);
                    fusedOrientation.Z -= (fusedOrientation.Z > Math.PI) ? Convert.ToSingle(2.0 * Math.PI) : 0;
                }
                else if (accMagOrientation.Z < -0.5 * Math.PI && gyroOrientation.Z > 0.0)
                {
                    fusedOrientation.Z = (float)(FILTER_COEFFICIENT * gyroOrientation.Z + oneMinusCoeff * (accMagOrientation.Z + 2.0 * Math.PI));
                    fusedOrientation.Z -= (fusedOrientation.Z > Math.PI) ? Convert.ToSingle(2.0 * Math.PI) : 0;
                }
                else
                {
                    fusedOrientation.Z = FILTER_COEFFICIENT * gyroOrientation.Z + oneMinusCoeff * accMagOrientation.Z;
                }

                // overwrite gyro matrix and orientation with fused orientation
                // to comensate gyro drift
                gyroMatrix = getRotationMatrixFromOrientation(fusedOrientation);
                fusedOrientation = gyroOrientation;
            }
        }
        #endregion

        public IDeviceOrientation setAccelerometerParam(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        IDeviceOrientation IDeviceOrientation.setAccelerometerParam(Vector3D values)
        {
            accel = values;
            return this;
        }

        public IDeviceOrientation setMagnetometerParam(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        IDeviceOrientation IDeviceOrientation.setMagnetometerParam(Vector3D values)
        {
            magnet = values;
            return this;
        }

        public float[] getRotationMatrix()
        {
            throw new NotImplementedException();
        }

        public Vector3D getOrientation()
        {
            throw new NotImplementedException();
        }

        public Vector3D getOrientation(float[] rotationMatrix)
        {
            //gyroOrientation = rotationMatrix;???

            StartCalculating();
            return fusedOrientation;

        }

        public void getRotationMatrix(float[] R, float[] I, Vector3D elem1, Vector3D elem2)
        {
            throw new NotImplementedException();
        }

    }
}