using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLI_CrossAR.CrossAR.Algorithm
{
   public class Quaternion
    {
            public double X;
            public double Y;
            public double Z;
            public double W;

            public Quaternion(double x, double y, double z, double w)
            {
                this.X = x;
                this.Y = y;
                this.Z = z;
                this.W = w;
            }

            public Quaternion(Vector3D vectorPart, double scalarPart)
            {
                this.X = vectorPart.X;
                this.Y = vectorPart.Y;
                this.Z = vectorPart.Z;
                this.W = scalarPart;
            }


            public Quaternion()
            {

            }
            public static Quaternion CreateFromYawPitchRoll(double yaw, double pitch, double roll)
            {
                Quaternion result=new Quaternion();

                double num9 = roll * 0.5f;
                double num6 = (double)Math.Sin((double)num9);
                double num5 = (double)Math.Cos((double)num9);
                double num8 = pitch * 0.5f;
                double num4 = (double)Math.Sin((double)num8);
                double num3 = (double)Math.Cos((double)num8);
                double num7 = yaw * 0.5f;
                double num2 = (double)Math.Sin((double)num7);
                double num = (double)Math.Cos((double)num7);
                result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
                result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
                result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
                result.W = ((num * num3) * num5) + ((num2 * num4) * num6);

                return result;
            }

            public static void CreateFromYawPitchRoll(double yaw, double pitch, double roll, Quaternion result)
            {
                 //Quaternion result = new Quaternion();
                double num9 = roll * 0.5f;
                double num6 = (double)Math.Sin((double)num9);
                double num5 = (double)Math.Cos((double)num9);
                double num8 = pitch * 0.5f;
                double num4 = (double)Math.Sin((double)num8);
                double num3 = (double)Math.Cos((double)num8);
                double num7 = yaw * 0.5f;
                double num2 = (double)Math.Sin((double)num7);
                double num = (double)Math.Cos((double)num7);
                result.X = ((num * num4) * num5) + ((num2 * num3) * num6);
                result.Y = ((num2 * num3) * num5) - ((num * num4) * num6);
                result.Z = ((num * num3) * num6) - ((num2 * num4) * num5);
                result.W = ((num * num3) * num5) + ((num2 * num4) * num6);
            }
        }
}
