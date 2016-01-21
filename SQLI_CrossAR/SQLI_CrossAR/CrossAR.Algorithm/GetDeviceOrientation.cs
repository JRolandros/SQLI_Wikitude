using DeviceMotion.Plugin;
using DeviceMotion.Plugin.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SQLI_CrossAR.CrossAR.Algorithm
{
    public class GetDeviceOrientation
    {
        public string str;
        //Accelerometer vector
        public Vector3D gravity = new Vector3D();
        //Magnetic vector
        public Vector3D orientation = new Vector3D();
        public Vector3D degree = new Vector3D();


        //Test part for camera orientation rolan
        public Vector3D acc = new Vector3D();
        public Vector3D magneto = new Vector3D();
        //end camera test


        private IDeviceOrientation orienGetter = DependencyService.Get<IDeviceOrientation>();

        public GetDeviceOrientation()
        {
        }

        public void sensorRegistration(Object s, SensorValueChangedEventArgs a)
        {
            CrossDeviceMotion.Current.Start(MotionSensorType.Accelerometer, MotionSensorDelay.Fastest);
            CrossDeviceMotion.Current.Start(MotionSensorType.Magnetometer, MotionSensorDelay.Fastest);
            CrossDeviceMotion.Current.Start(MotionSensorType.Gyroscope, MotionSensorDelay.Fastest);

            //TODO explain each line of code here 
            Vector3D values = new Vector3D();
            Vector3D valuesGyro = new Vector3D();
            switch (a.SensorType)
            {
                case MotionSensorType.Accelerometer:
                    //for camera test  roland
                    acc.X = ((MotionVector)a.Value).X;
                    acc.Y = ((MotionVector)a.Value).Y;
                    acc.Z = ((MotionVector)a.Value).Z;
                    //end camera test

                    values.X = (float)((MotionVector)a.Value).X;
                    values.Y = (float)((MotionVector)a.Value).Y;
                    values.Z = (float)((MotionVector)a.Value).Z;
                    filterLowPass(values, gravity, 0.98f);
                    orienGetter.setAccelerometerParam(gravity);
                    orienGetter.calculateAccMagOrientation();
                    degree = orienGetter.getFusOrientation();
                    //degree = orienGetter.getOrientation(valuesGyro);
                    break;

                case MotionSensorType.Magnetometer:
                    values.X = (float)((MotionVector)a.Value).X;
                    values.Y = (float)((MotionVector)a.Value).Y;
                    values.Z = (float)((MotionVector)a.Value).Z;
                    orienGetter.setMagnetometerParam(values);

                    // test jiyuan
                    orienGetter.calculateAccMagOrientation();
                    degree = orienGetter.getFusOrientation();
                    // end test

                    //filterLowPass(orienGetter.getOrientation(), orientation, 0.98f);

                    //for (int i = 0; i < 3; i++)
                    //    degree[i] = (float)(orientation[i] * 180 / Math.PI);

                    //give a text title to current direction.
                    //str = directionEstimate(degree[0]);

                    //Console.WriteLine(str);

                    //transfer radian to degree
                    //for (int i = 0; i < 3; i++)
                    //{
                    //    if (degree[i] < 0)
                    //        degree[i] += 360;
                    //}




                    //Debug.WriteLine("Values: {0}, {1}, {2}, {3}", orientation[0], orientation[1], orientation[2], str);

                    break;

                case MotionSensorType.Gyroscope:
                    valuesGyro.X = (float)((MotionVector)a.Value).X;
                    valuesGyro.Y = (float)((MotionVector)a.Value).Y;
                    valuesGyro.Z = (float)((MotionVector)a.Value).Z;
                    orienGetter.gyroFunction(valuesGyro);
                    degree = orienGetter.getFusOrientation();
                    break;

                default:
                    break;
            };
        }

        public void filterLowPass(Vector3D arrin, Vector3D arrout, float alpha)
        {
            //Debug.WriteLine("arrin.length={0}, sizeof float={1}, len={2}", arrin.Length, sizeof(float), len);
            arrout.X = alpha * arrout.X + (1 - alpha) * arrin.X;
            arrout.Y = alpha * arrout.Y + (1 - alpha) * arrin.Y;
            arrout.Z = alpha * arrout.Z + (1 - alpha) * arrin.Z;
        }

        public string directionEstimate(float d)
        {
            string result = "Result can not be found.";
            if (d >= -30 && d < 30)
                result = "Nord";
            else if (d >= 30 && d < 60)
                result = "Nord-Est";
            else if (d >= 60 && d < 120)
                result = "Est";
            else if (d >= 120 && d < 150)
                result = "Sud-Est";
            else if ((d >= 150 && d <= 180) || (d >= -180 && d < -150))
                result = "Sud";
            else if (d >= -150 && d < -120)
                result = "Sud-West";
            else if (d >= -120 && d < -60)
                result = "West";
            else if (d >= -60 && d < -30)
                result = "Nord-West";

            return result;
        }

        public double distanceMoveX(double angle)
        {
            return 2 * Math.Sin(angle / 2);
        }

        public double distanceMoveY(double angle)
        {
            return 1 - Math.Cos(angle / 2);
        }
    }
}
