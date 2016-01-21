using System;
using System.Collections.Generic;
using System.Text;

namespace SQLI_CrossAR.CrossAR.Algorithm
{
    public interface IDeviceOrientation
    {
        IDeviceOrientation setAccelerometerParam(float x, float y, float z);
        IDeviceOrientation setAccelerometerParam(Vector3D values);
        IDeviceOrientation setMagnetometerParam(float x, float y, float z);
        IDeviceOrientation setMagnetometerParam(Vector3D values);
        float[] getRotationMatrix();
        Vector3D getOrientation();
        Vector3D getOrientation(float[] rotationMatrix);
        void getRotationMatrix(float[] R, float[] I, Vector3D elem1, Vector3D elem2);
        void gyroFunction(Vector3D values);
        void calculateAccMagOrientation();
        Vector3D getFusOrientation();
    }

}
