using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLI_CrossAR.CrossAR.Algorithm
{
    public class Vector3D
    {
        public static readonly Vector3D Zero = new Vector3D();

        public static readonly Vector3D Up = new Vector3D(0.0, 1.0, 0.0);
        public static readonly Vector3D Down = new Vector3D(0.0, -1.0, 0.0);

        public static readonly Vector3D Left = new Vector3D(-1.0, 0.0, 0.0);
        public static readonly Vector3D Right = new Vector3D(1.0, 0.0, 0.0);


        public static readonly Vector3D Forward = new Vector3D(0.0, 0.0, -1.0);
        public static readonly Vector3D Backward = new Vector3D(0.0, 0.0, 1.0);



        //correspondant rotation is roll
        public double X { get; set; }

        //correspondant rotation is pitch
        public double Y { get; set; }

        //correspondant rotation is yaw
        public double Z { get; set; }


        public Vector3D()
        {

        }
        public Vector3D(double x,double y,double z)
        {
            X = x;
            Z = z;
            Y = y;
        }

        //vector length sqaured or the intensity squared of the vector
        public double LengthSquared()
        {
            return (X * X + Y * Y + Z * Z);
        }

        //get the vector lengh or the intensity
        public double Length()
        {
            return (double)Math.Sqrt((double)LengthSquared());
        }

        /// <summary>
        /// Performs vector normalization process, so at the end
        /// vector's len = 1.0f; Note, does not support zero vectors
        /// this return value like x/|x| for each axis
        /// </summary>
        public void Normalize()
        {
            double factor = 1.0f / Length();
            X = X * factor;
            Y = Y * factor;
            Z = Z * factor;
        }

        public override bool Equals(object obj)
        {
            if (object.ReferenceEquals(this, obj)) return true;
            if (obj is Vector3D)
            {
                return this == (Vector3D)obj;
            }
            return false;
        }

        public override int GetHashCode()
        {
            // HACK: Is this the right way to compute a haschode for three doubles?
            // I don't like casting to int, but the ^ operator does not work on double.
            return ((int)X) ^ ((int)Y) ^ ((int)Z);
        }
        
        public static Vector3D Normalize(Vector3D value)
        {
            Vector3D result=new Vector3D();
            double factor = 1.0f / value.Length();
            result.X = value.X * factor;
            result.Y = value.Y * factor;
            result.Z = value.Z * factor;
            return result;
        }

        public static Vector3D operator -(Vector3D vector)
        {
            Vector3D result=new Vector3D();

            result.X = -vector.X;
            result.Y = -vector.Y;
            result.Z = -vector.Z;

            return result;
        }

        /// <summary>
        /// Subtracts vectorB from vectorA
        /// </summary>
        /// <param name="vectorA"></param>
        /// <param name="vectorB"></param>
        /// <returns></returns>
        public static Vector3D operator -(Vector3D vectorA, Vector3D vectorB)
        {
            Vector3D result=new Vector3D();

            result.X = vectorA.X - vectorB.X;
            result.Y = vectorA.Y - vectorB.Y;
            result.Z = vectorA.Z - vectorB.Z;

            return result;
        }

        public static Vector3D operator +(Vector3D vectorA, Vector3D vectorB)
        {
            Vector3D result=new Vector3D();

            result.X = vectorA.X + vectorB.X;
            result.Y = vectorA.Y + vectorB.Y;
            result.Z = vectorA.Z + vectorB.Z;

            return result;
        }

        public static Vector3D operator *(Vector3D vector, double scaleFactor)
        {
            Vector3D result=new Vector3D();

            result.X = vector.X * scaleFactor;
            result.Y = vector.Y * scaleFactor;
            result.Z = vector.Z * scaleFactor;

            return result;
        }

        public static Vector3D operator *(double scaleFactor, Vector3D vector)
        {
            return (vector * scaleFactor);
        }

        public static Vector3D operator /(Vector3D vector, double divider)
        {
            Vector3D result;
            double invertedScale = 1.0f / divider;

            result = vector * invertedScale;

            return result;
        }

        /// <summary>
        /// Do not use this method if two parameters(vectorA, vectorB) don't have the same direction
        /// </summary>
        /// <param name="vectorA"></param>
        /// <param name="vectorB"></param>
        /// <returns></returns>
        public static Vector3D operator /(Vector3D vectorA, Vector3D vectorB)
        {
            Vector3D result=new Vector3D();

            result.X = vectorA.X / vectorB.X;
            result.Y = vectorA.Y / vectorB.Y;
            result.Z = vectorA.Z / vectorB.Z;

            return result;
        }

        public static bool operator ==(Vector3D vectorA, Vector3D vectorB)
        {
            return ((vectorA.X == vectorB.X) && (vectorA.Y == vectorB.Y) && (vectorA.Z == vectorB.Z));
        }

        public static bool operator !=(Vector3D vectorA, Vector3D vectorB)
        {
            return !((vectorA.X == vectorB.X) && (vectorA.Y == vectorB.Y) && (vectorA.Z == vectorB.Z));
        }

        //calculate the vector product in french produit vectoriel. A^B
        public static Vector3D Cross(Vector3D vectorA, Vector3D vectorB)
        {
            Vector3D result =new Vector3D();

            result.X = (vectorA.Y * vectorB.Z) - (vectorA.Z * vectorB.Y);
            result.Y = (vectorA.Z * vectorB.X) - (vectorA.X * vectorB.Z);
            result.Z = (vectorA.X * vectorB.Y) - (vectorA.Y * vectorB.X);

            return result;
        }

        //calculate scalor product in french produit scalaire vectA.vectB
        public static double Dot(Vector3D vectorA, Vector3D vectorB)
        {
            double result;

            result = ((vectorA.X * vectorB.X) + (vectorA.Y * vectorB.Y)) + (vectorA.Z * vectorB.Z);

            return result;
        }

        //calculate a multiplication of a matrice and a vector. Mat*Vect
        public static Vector3D Transform(Vector3D position, Matrix matrix)
        {
            Vector3D result =new Vector3D();

            result.X = (((position.X * matrix.M11) + (position.Y * matrix.M21)) + (position.Z * matrix.M31)) + matrix.M41;
            result.Y = (((position.X * matrix.M12) + (position.Y * matrix.M22)) + (position.Z * matrix.M32)) + matrix.M42;
            result.Z = (((position.X * matrix.M13) + (position.Y * matrix.M23)) + (position.Z * matrix.M33)) + matrix.M43;

            return result;
        }

        public string toString()
        {
            return "[ X:"+X+" Y:"+Y+" Z:"+Z+" ]";
        }
    }
}
