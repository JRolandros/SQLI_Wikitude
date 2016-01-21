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
using Android.Hardware;
using Xamarin.Forms.Platform.Android;
using Android.Content.PM;

[assembly: ExportRenderer(typeof(SQLI_CrossAR.CrossAR.Views.CameraView), typeof(SQLI_CrossAR.Droid.DroidView.CameraView))]
namespace SQLI_CrossAR.Droid.DroidView
{
    
    public class CameraView : PageRenderer, TextureView.ISurfaceTextureListener
    {
        Camera _camera;
        TextureView _textureView;
        global::Android.Views.View view;
        CameraFacing cameraType;


        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || Element == null)
                return;

            try
            {
                var activity = this.Context as Activity;
                activity.RequestedOrientation = ScreenOrientation.Landscape;
                view = activity.LayoutInflater.Inflate(Resource.Layout.CameraLayout, this, false);
                _textureView = view.FindViewById<TextureView>(Resource.Id.textureView);
                _textureView.SurfaceTextureListener = this;
                cameraType = CameraFacing.Back;
                AddView(view);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);

            var msw = MeasureSpec.MakeMeasureSpec(r - l, MeasureSpecMode.Exactly);
            var msh = MeasureSpec.MakeMeasureSpec(b - t, MeasureSpecMode.Exactly);

            view.Measure(msw, msh);
            view.Layout(0, 0, r - l, b - t);
        }

        public void OnSurfaceTextureAvailable(Android.Graphics.SurfaceTexture surface, int width, int height)
        {
            // var camInfo = new Camera.CameraInfo();

            _camera = Camera.Open(0);

            // _camera = Camera.Open();
            _textureView.LayoutParameters = new FrameLayout.LayoutParams(width, height);
            try
            {
                _camera.SetPreviewTexture(surface);
                _camera.StartPreview();
            }
            catch (Java.IO.IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public bool OnSurfaceTextureDestroyed(Android.Graphics.SurfaceTexture surface)
        {
            _camera.StopPreview();
            _camera.Release();
            return true;
        }

        public void OnSurfaceTextureSizeChanged(Android.Graphics.SurfaceTexture surface, int width, int height)
        {

        }

        public void OnSurfaceTextureUpdated(Android.Graphics.SurfaceTexture surface)
        {

        }
    }
}