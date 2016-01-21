using SQLI_CrossAR.CrossAR.Algorithm;
using SQLI_CrossAR.iOS.Algorithm;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

[assembly: Dependency(typeof(iOSOrientation))]
namespace SQLI_CrossAR.iOS.Algorithm
{
    public class iOSOrientation : IDeviceOrientation
    {
        public void calculateAccMagOrientation()
        {
            throw new NotImplementedException();
        }

        public float[] getFusOrientation()
        {
            throw new NotImplementedException();
        }

        public Vector3D getOrientation()
        {
            throw new NotImplementedException();
        }

        public float[] getOrientation(float[] rotationMatrix)
        {
            throw new NotImplementedException();
        }

        public float[] getRotationMatrix()
        {
            throw new NotImplementedException();
        }

        public void getRotationMatrix(float[] R, float[] I, Vector3D elem1, Vector3D elem2)
        {
            throw new NotImplementedException();
        }

        public void getRotationMatrix(float[] R, float[] I, float[] elem1, float[] elem2)
        {
            throw new NotImplementedException();
        }

        public void gyroFunction(Vector3D values)
        {
            throw new NotImplementedException();
        }

        public void gyroFunction(float[] values)
        {
            throw new NotImplementedException();
        }

        public IDeviceOrientation setAccelerometerParam(Vector3D values)
        {
            throw new NotImplementedException();
        }

        public IDeviceOrientation setAccelerometerParam(float[] values)
        {
            throw new NotImplementedException();
        }

        public IDeviceOrientation setAccelerometerParam(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        public IDeviceOrientation setMagnetometerParam(Vector3D values)
        {
            throw new NotImplementedException();
        }

        public IDeviceOrientation setMagnetometerParam(float[] values)
        {
            throw new NotImplementedException();
        }

        public IDeviceOrientation setMagnetometerParam(float x, float y, float z)
        {
            throw new NotImplementedException();
        }

        Vector3D IDeviceOrientation.getFusOrientation()
        {
            throw new NotImplementedException();
        }

        Vector3D IDeviceOrientation.getOrientation(float[] rotationMatrix)
        {
            throw new NotImplementedException();
        }
    }
}
