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

using SQLI_CrossAR.CrossAR.Algorithm;

namespace SQLI_CrossAR.Droid.Algorithm
{
    class DroidOrientationCal : IOrientationCal
    {
        public float calAngleBetweenOrientationAndPI(float[] orientation, Position pi)
        {
            // orientation vector
            float axis_x = orientation[0];
            float axis_y = orientation[1];
            float axis_z = orientation[2];

            // get current location

        }
    }
}