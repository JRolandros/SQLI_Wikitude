using System;
using System.Collections.Generic;
using System.Text;

namespace SQLI_CrossAR.CrossAR.Algorithm
{
    public class Matrix
    {
        public double M11;
        public double M12;
        public double M13;
        public double M14;

        public double M21;
        public double M22;
        public double M23;
        public double M24;

        public double M31;
        public double M32;
        public double M33;
        public double M34;

        public double M41;
        public double M42;
        public double M43;
        public double M44;

        public Matrix(double m11, double m12, double m13, double m14, double m21, double m22, double m23, double m24, double m31, double m32, double m33, double m34, double m41, double m42, double m43, double m44)
        {
            this.M11 = m11;
            this.M12 = m12;
            this.M13 = m13;
            this.M14 = m14;

            this.M21 = m21;
            this.M22 = m22;
            this.M23 = m23;
            this.M24 = m24;

            this.M31 = m31;
            this.M32 = m32;
            this.M33 = m33;
            this.M34 = m34;

            this.M41 = m41;
            this.M42 = m42;
            this.M43 = m43;
            this.M44 = m44;
        }

        public Matrix()
        {

        }
        public static Matrix CreatePerspectiveFieldOfView(double fieldOfView, double aspectRatio, double nearPlaneDistance, double farPlaneDistance)
        {
            Matrix result = new Matrix();

            // TODO: sanity checks here

            double fovRatio = 1.0f / ((double)Math.Tan((double)(fieldOfView * 0.5f)));

            result.M11 = fovRatio / aspectRatio;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;

            result.M21 = 0.0f;
            result.M22 = fovRatio;
            result.M23 = 0.0f;
            result.M24 = 0.0f;

            result.M31 = 0.0f;
            result.M32 = 0.0f;
            result.M33 = farPlaneDistance / (nearPlaneDistance - farPlaneDistance);
            result.M34 = -1.0f;

            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = (nearPlaneDistance * farPlaneDistance) / (nearPlaneDistance - farPlaneDistance);
            result.M44 = 0.0f;

            return result;
        }

        public static Matrix CreateLookAt(Vector3D camPosition, Vector3D camTarget, Vector3D camUp)
        {
            Matrix result = new Matrix();

            Vector3D vector = Vector3D.Normalize(camPosition - camTarget);
            Vector3D vector2 = Vector3D.Normalize(Vector3D.Cross(camUp, vector));
            Vector3D vector3 = Vector3D.Cross(vector, vector2);

            result.M11 = vector2.X;
            result.M12 = vector3.X;
            result.M13 = vector.X;
            result.M14 = 0.0f;

            result.M21 = vector2.Y;
            result.M22 = vector3.Y;
            result.M23 = vector.Y;
            result.M24 = 0.0f;

            result.M31 = vector2.Z;
            result.M32 = vector3.Z;
            result.M33 = vector.Z;
            result.M34 = 0.0f;

            result.M41 = -Vector3D.Dot(vector2, camPosition);
            result.M42 = -Vector3D.Dot(vector3, camPosition);
            result.M43 = -Vector3D.Dot(vector, camPosition);
            result.M44 = 1.0f;

            return result;
        }


        public static Matrix CreateRotationX(double angleInRadians)
        {
            Matrix result = new Matrix();

            double cosine = (double)Math.Cos((double)angleInRadians);
            double sine = (double)Math.Sin((double)angleInRadians);

            result.M11 = 1.0f;
            result.M12 = 0.0f;
            result.M13 = 0.0f;
            result.M14 = 0.0f;

            result.M21 = 0.0f;
            result.M22 = cosine;
            result.M23 = sine;
            result.M24 = 0.0f;

            result.M31 = 0.0f;
            result.M32 = -sine;
            result.M33 = cosine;
            result.M34 = 0.0f;

            result.M41 = 0.0f;
            result.M42 = 0.0f;
            result.M43 = 0.0f;
            result.M44 = 1.0f;

            return result;
        }

        public static Matrix CreateWorld(Vector3D position, Vector3D forward, Vector3D up)
        {
            Matrix result = new Matrix();
            Vector3D vector = Vector3D.Normalize(-forward);
            Vector3D vector2 = Vector3D.Normalize(Vector3D.Cross(up, vector));
            Vector3D vector3 = Vector3D.Cross(vector, vector2);

            result.M11 = vector2.X;
            result.M12 = vector2.Y;
            result.M13 = vector2.Z;
            result.M14 = 0.0f;

            result.M21 = vector3.X;
            result.M22 = vector3.Y;
            result.M23 = vector3.Z;
            result.M24 = 0.0f;

            result.M31 = vector.X;
            result.M32 = vector.Y;
            result.M33 = vector.Z;
            result.M34 = 0.0f;

            result.M41 = position.X;
            result.M42 = position.Y;
            result.M43 = position.Z;
            result.M44 = 1.0f;

            return result;
        }


        public static Matrix Multiply(Matrix matrixA, Matrix matrixB)
        {
            Matrix result = new Matrix();

            result.M11 = (((matrixA.M11 * matrixB.M11) + (matrixA.M12 * matrixB.M21)) + (matrixA.M13 * matrixB.M31)) + (matrixA.M14 * matrixB.M41);
            result.M12 = (((matrixA.M11 * matrixB.M12) + (matrixA.M12 * matrixB.M22)) + (matrixA.M13 * matrixB.M32)) + (matrixA.M14 * matrixB.M42);
            result.M13 = (((matrixA.M11 * matrixB.M13) + (matrixA.M12 * matrixB.M23)) + (matrixA.M13 * matrixB.M33)) + (matrixA.M14 * matrixB.M43);
            result.M14 = (((matrixA.M11 * matrixB.M14) + (matrixA.M12 * matrixB.M24)) + (matrixA.M13 * matrixB.M34)) + (matrixA.M14 * matrixB.M44);

            result.M21 = (((matrixA.M21 * matrixB.M11) + (matrixA.M22 * matrixB.M21)) + (matrixA.M23 * matrixB.M31)) + (matrixA.M24 * matrixB.M41);
            result.M22 = (((matrixA.M21 * matrixB.M12) + (matrixA.M22 * matrixB.M22)) + (matrixA.M23 * matrixB.M32)) + (matrixA.M24 * matrixB.M42);
            result.M23 = (((matrixA.M21 * matrixB.M13) + (matrixA.M22 * matrixB.M23)) + (matrixA.M23 * matrixB.M33)) + (matrixA.M24 * matrixB.M43);
            result.M24 = (((matrixA.M21 * matrixB.M14) + (matrixA.M22 * matrixB.M24)) + (matrixA.M23 * matrixB.M34)) + (matrixA.M24 * matrixB.M44);

            result.M31 = (((matrixA.M31 * matrixB.M11) + (matrixA.M32 * matrixB.M21)) + (matrixA.M33 * matrixB.M31)) + (matrixA.M34 * matrixB.M41);
            result.M32 = (((matrixA.M31 * matrixB.M12) + (matrixA.M32 * matrixB.M22)) + (matrixA.M33 * matrixB.M32)) + (matrixA.M34 * matrixB.M42);
            result.M33 = (((matrixA.M31 * matrixB.M13) + (matrixA.M32 * matrixB.M23)) + (matrixA.M33 * matrixB.M33)) + (matrixA.M34 * matrixB.M43);
            result.M34 = (((matrixA.M31 * matrixB.M14) + (matrixA.M32 * matrixB.M24)) + (matrixA.M33 * matrixB.M34)) + (matrixA.M34 * matrixB.M44);

            result.M41 = (((matrixA.M41 * matrixB.M11) + (matrixA.M42 * matrixB.M21)) + (matrixA.M43 * matrixB.M31)) + (matrixA.M44 * matrixB.M41);
            result.M42 = (((matrixA.M41 * matrixB.M12) + (matrixA.M42 * matrixB.M22)) + (matrixA.M43 * matrixB.M32)) + (matrixA.M44 * matrixB.M42);
            result.M43 = (((matrixA.M41 * matrixB.M13) + (matrixA.M42 * matrixB.M23)) + (matrixA.M43 * matrixB.M33)) + (matrixA.M44 * matrixB.M43);
            result.M44 = (((matrixA.M41 * matrixB.M14) + (matrixA.M42 * matrixB.M24)) + (matrixA.M43 * matrixB.M34)) + (matrixA.M44 * matrixB.M44);

            return result;
        }

        public static Matrix Invert(Matrix source)
        {
            Matrix result = new Matrix();

            double num5 = source.M11;
            double num4 = source.M12;
            double num3 = source.M13;
            double num2 = source.M14;
            double num9 = source.M21;
            double num8 = source.M22;
            double num7 = source.M23;
            double num6 = source.M24;
            double num17 = source.M31;
            double num16 = source.M32;
            double num15 = source.M33;
            double num14 = source.M34;
            double num13 = source.M41;
            double num12 = source.M42;
            double num11 = source.M43;
            double num10 = source.M44;

            double num23 = (num15 * num10) - (num14 * num11);
            double num22 = (num16 * num10) - (num14 * num12);
            double num21 = (num16 * num11) - (num15 * num12);
            double num20 = (num17 * num10) - (num14 * num13);
            double num19 = (num17 * num11) - (num15 * num13);
            double num18 = (num17 * num12) - (num16 * num13);
            double num39 = ((num8 * num23) - (num7 * num22)) + (num6 * num21);
            double num38 = -(((num9 * num23) - (num7 * num20)) + (num6 * num19));
            double num37 = ((num9 * num22) - (num8 * num20)) + (num6 * num18);
            double num36 = -(((num9 * num21) - (num8 * num19)) + (num7 * num18));
            double num = 1.0f / ((((num5 * num39) + (num4 * num38)) + (num3 * num37)) + (num2 * num36));

            result.M11 = num39 * num;
            result.M21 = num38 * num;
            result.M31 = num37 * num;
            result.M41 = num36 * num;

            result.M12 = -(((num4 * num23) - (num3 * num22)) + (num2 * num21)) * num;
            result.M22 = (((num5 * num23) - (num3 * num20)) + (num2 * num19)) * num;
            result.M32 = -(((num5 * num22) - (num4 * num20)) + (num2 * num18)) * num;
            result.M42 = (((num5 * num21) - (num4 * num19)) + (num3 * num18)) * num;

            double num35 = (num7 * num10) - (num6 * num11);
            double num34 = (num8 * num10) - (num6 * num12);
            double num33 = (num8 * num11) - (num7 * num12);
            double num32 = (num9 * num10) - (num6 * num13);
            double num31 = (num9 * num11) - (num7 * num13);
            double num30 = (num9 * num12) - (num8 * num13);

            result.M13 = (((num4 * num35) - (num3 * num34)) + (num2 * num33)) * num;
            result.M23 = -(((num5 * num35) - (num3 * num32)) + (num2 * num31)) * num;
            result.M33 = (((num5 * num34) - (num4 * num32)) + (num2 * num30)) * num;
            result.M43 = -(((num5 * num33) - (num4 * num31)) + (num3 * num30)) * num;

            double num29 = (num7 * num14) - (num6 * num15);
            double num28 = (num8 * num14) - (num6 * num16);
            double num27 = (num8 * num15) - (num7 * num16);
            double num26 = (num9 * num14) - (num6 * num17);
            double num25 = (num9 * num15) - (num7 * num17);
            double num24 = (num9 * num16) - (num8 * num17);

            result.M14 = -(((num4 * num29) - (num3 * num28)) + (num2 * num27)) * num;
            result.M24 = (((num5 * num29) - (num3 * num26)) + (num2 * num25)) * num;
            result.M34 = -(((num5 * num28) - (num4 * num26)) + (num2 * num24)) * num;
            result.M44 = (((num5 * num27) - (num4 * num25)) + (num3 * num24)) * num;

            return result;
        }

        public static Matrix operator *(Matrix matrixA, Matrix matrixB)
        {
            Matrix result = new Matrix();

            result.M11 = (((matrixA.M11 * matrixB.M11) + (matrixA.M12 * matrixB.M21)) + (matrixA.M13 * matrixB.M31)) + (matrixA.M14 * matrixB.M41);
            result.M12 = (((matrixA.M11 * matrixB.M12) + (matrixA.M12 * matrixB.M22)) + (matrixA.M13 * matrixB.M32)) + (matrixA.M14 * matrixB.M42);
            result.M13 = (((matrixA.M11 * matrixB.M13) + (matrixA.M12 * matrixB.M23)) + (matrixA.M13 * matrixB.M33)) + (matrixA.M14 * matrixB.M43);
            result.M14 = (((matrixA.M11 * matrixB.M14) + (matrixA.M12 * matrixB.M24)) + (matrixA.M13 * matrixB.M34)) + (matrixA.M14 * matrixB.M44);

            result.M21 = (((matrixA.M21 * matrixB.M11) + (matrixA.M22 * matrixB.M21)) + (matrixA.M23 * matrixB.M31)) + (matrixA.M24 * matrixB.M41);
            result.M22 = (((matrixA.M21 * matrixB.M12) + (matrixA.M22 * matrixB.M22)) + (matrixA.M23 * matrixB.M32)) + (matrixA.M24 * matrixB.M42);
            result.M23 = (((matrixA.M21 * matrixB.M13) + (matrixA.M22 * matrixB.M23)) + (matrixA.M23 * matrixB.M33)) + (matrixA.M24 * matrixB.M43);
            result.M24 = (((matrixA.M21 * matrixB.M14) + (matrixA.M22 * matrixB.M24)) + (matrixA.M23 * matrixB.M34)) + (matrixA.M24 * matrixB.M44);

            result.M31 = (((matrixA.M31 * matrixB.M11) + (matrixA.M32 * matrixB.M21)) + (matrixA.M33 * matrixB.M31)) + (matrixA.M34 * matrixB.M41);
            result.M32 = (((matrixA.M31 * matrixB.M12) + (matrixA.M32 * matrixB.M22)) + (matrixA.M33 * matrixB.M32)) + (matrixA.M34 * matrixB.M42);
            result.M33 = (((matrixA.M31 * matrixB.M13) + (matrixA.M32 * matrixB.M23)) + (matrixA.M33 * matrixB.M33)) + (matrixA.M34 * matrixB.M43);
            result.M34 = (((matrixA.M31 * matrixB.M14) + (matrixA.M32 * matrixB.M24)) + (matrixA.M33 * matrixB.M34)) + (matrixA.M34 * matrixB.M44);

            result.M41 = (((matrixA.M41 * matrixB.M11) + (matrixA.M42 * matrixB.M21)) + (matrixA.M43 * matrixB.M31)) + (matrixA.M44 * matrixB.M41);
            result.M42 = (((matrixA.M41 * matrixB.M12) + (matrixA.M42 * matrixB.M22)) + (matrixA.M43 * matrixB.M32)) + (matrixA.M44 * matrixB.M42);
            result.M43 = (((matrixA.M41 * matrixB.M13) + (matrixA.M42 * matrixB.M23)) + (matrixA.M43 * matrixB.M33)) + (matrixA.M44 * matrixB.M43);
            result.M44 = (((matrixA.M41 * matrixB.M14) + (matrixA.M42 * matrixB.M24)) + (matrixA.M43 * matrixB.M34)) + (matrixA.M44 * matrixB.M44);

            return result;
        }


        public static Matrix CreateFromYawPitchRoll(double yaw, double pitch, double roll)
        {
            Matrix matrix = new Matrix();
            Quaternion quaternion = new Quaternion();
            Quaternion.CreateFromYawPitchRoll(yaw, pitch, roll, quaternion);
            CreateFromQuaternion(quaternion, matrix);
            return matrix;
        }

        public static Matrix CreateFromQuaternion(Quaternion quaternion)
        {
            Matrix matrix = new Matrix();
            double num9 = quaternion.X * quaternion.X;
            double num8 = quaternion.Y * quaternion.Y;
            double num7 = quaternion.Z * quaternion.Z;
            double num6 = quaternion.X * quaternion.Y;
            double num5 = quaternion.Z * quaternion.W;
            double num4 = quaternion.Z * quaternion.X;
            double num3 = quaternion.Y * quaternion.W;
            double num2 = quaternion.Y * quaternion.Z;
            double num = quaternion.X * quaternion.W;
            matrix.M11 = 1f - (2f * (num8 + num7));
            matrix.M12 = 2f * (num6 + num5);
            matrix.M13 = 2f * (num4 - num3);
            matrix.M14 = 0f;
            matrix.M21 = 2f * (num6 - num5);
            matrix.M22 = 1f - (2f * (num7 + num9));
            matrix.M23 = 2f * (num2 + num);
            matrix.M24 = 0f;
            matrix.M31 = 2f * (num4 + num3);
            matrix.M32 = 2f * (num2 - num);
            matrix.M33 = 1f - (2f * (num8 + num9));
            matrix.M34 = 0f;
            matrix.M41 = 0f;
            matrix.M42 = 0f;
            matrix.M43 = 0f;
            matrix.M44 = 1f;
            return matrix;
        }

        public static void CreateFromQuaternion(Quaternion quaternion, Matrix result)
        {
            double num9 = quaternion.X * quaternion.X;
            double num8 = quaternion.Y * quaternion.Y;
            double num7 = quaternion.Z * quaternion.Z;
            double num6 = quaternion.X * quaternion.Y;
            double num5 = quaternion.Z * quaternion.W;
            double num4 = quaternion.Z * quaternion.X;
            double num3 = quaternion.Y * quaternion.W;
            double num2 = quaternion.Y * quaternion.Z;
            double num = quaternion.X * quaternion.W;
            result.M11 = 1f - (2f * (num8 + num7));
            result.M12 = 2f * (num6 + num5);
            result.M13 = 2f * (num4 - num3);
            result.M14 = 0f;
            result.M21 = 2f * (num6 - num5);
            result.M22 = 1f - (2f * (num7 + num9));
            result.M23 = 2f * (num2 + num);
            result.M24 = 0f;
            result.M31 = 2f * (num4 + num3);
            result.M32 = 2f * (num2 - num);
            result.M33 = 1f - (2f * (num8 + num9));
            result.M34 = 0f;
            result.M41 = 0f;
            result.M42 = 0f;
            result.M43 = 0f;
            result.M44 = 1f;
        }

    }
}
