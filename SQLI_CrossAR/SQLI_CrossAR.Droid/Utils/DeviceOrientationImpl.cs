using System;
using Android.Content;
using Android.Runtime;
using Android.Views;
using SQLI_CrossAR.CrossAR.Services;
using SQLI_CrossAR.Droid.Utils;

[assembly: Xamarin.Forms.Dependency (typeof(DeviceOrientationImpl))]
namespace SQLI_CrossAR.Droid.Utils
{

    public class DeviceOrientationImpl : IOrientationDependancyService
    {
        public DeviceOrientationImpl() { }
        public static void Init() { }

        public DeviceOrientations GetOrientation()
        {
            IWindowManager windowManager = Android.App.Application.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();

            var rotation = windowManager.DefaultDisplay.Rotation;
            bool isLandscape = rotation == SurfaceOrientation.Rotation90 || rotation == SurfaceOrientation.Rotation270;
            return isLandscape ? DeviceOrientations.Landscape : DeviceOrientations.Portrait;
        }

        public void SetOrientationLandscape()
        {
            throw new NotImplementedException();
        }

        public void SetOrientationPortrait()
        {
            throw new NotImplementedException();
        }
    }
}