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
using Wikitude.Architect;
using Wikitude.Tools.Services;
using SQLI_CrossAR.Utils;

namespace SQLI_CrossAR.Droid.DroidView
{
    [Activity(Label = "Wikitude")]
    public class Wikitude : Activity
    {
        
        private ArchitectView architectView;

        
        //wikitude world
        private const string AR_WORLD_URL = "ARWorld/4_PointOfInterest_2_PoiWithLabel/index.html";

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.CameraLayout);

            Title = "Witude ro!";
            //Find your Architect view from the layout you just used
            architectView = FindViewById<ArchitectView>(Resource.Id.architectView);

            //addind POI
            architectView.SetLocation(49.41838, 1.04542, 100.0f);

            //Activate the ArchitectView with your license and set the feature type
            StartupConfiguration startupConfiguration = new StartupConfiguration(Constantes.WIKITUDE_SDK_KEY, StartupConfiguration.Features.Tracking2D);

            //check if you config is supported by the target device
            int requiredFeatures = StartupConfiguration.Features.Tracking2D;
            if ((ArchitectView.getSupportedFeaturesForDevice(this.ApplicationContext) & requiredFeatures) == requiredFeatures)
            {
                architectView.OnCreate(startupConfiguration);               
            }
        }


        protected override void OnResume()
        {
            base.OnResume();

            if (architectView != null)
                architectView.OnResume();
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (architectView != null)
                architectView.OnPause();
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();

            if (architectView != null)
            {
                architectView.OnDestroy();
            }
        }

        public override void OnLowMemory()
        {
            base.OnLowMemory();

            if (architectView != null)
                architectView.OnLowMemory();
        }

        protected override void OnPostCreate(Bundle savedInstanceState)
        {
            base.OnPostCreate(savedInstanceState);

            if (architectView != null)
            {
                architectView.OnPostCreate();

                try
                {
                    architectView.Load(AR_WORLD_URL);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("WIKITUDE_SAMPLE", ex.ToString());
                }
            }
        }
    }
}