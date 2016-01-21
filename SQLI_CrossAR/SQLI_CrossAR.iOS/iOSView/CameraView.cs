using AVFoundation;
using CoreFoundation;
using CoreMedia;
using CoreVideo;
using Foundation;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(SQLI_CrossAR.CrossAR.Views.CameraView), typeof(SQLI_CrossAR.iOS.iOSView.CameraView))]
namespace SQLI_CrossAR.iOS.iOSView
{
    public class CameraView : PageRenderer
    {
        #region Private Variables
        private NSError Error;
        #endregion

        #region Computed Properties
        //public override UIWindow Window { get; set; }
        public bool CameraAvailable { get; set; }
        public AVCaptureSession Session { get; set; }
        public AVCaptureDevice CaptureDevice { get; set; }
       public OutputRecorder Recorder { get; set; }
        public DispatchQueue Queue { get; set; }
        public AVCaptureDeviceInput Input { get; set; }
        #endregion

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            //imageView = new UIImageView(UIImage.FromFile("Background.png"));

           // Add(imageView);
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            try
            {
                
            }catch
            {

            }
         }


        public CameraView() : base()
        {
            //create a new capture session
            Session = new AVCaptureSession();
            Session.SessionPreset = AVCaptureSession.PresetMedium;

            // Create a device input
            CaptureDevice = AVCaptureDevice.DefaultDeviceWithMediaType(AVMediaType.Video);
            if (CaptureDevice == null)
            {
                // Video capture not supported, abort
                Console.WriteLine("Video recording not supported on this device");
                CameraAvailable = false;
                return;
            }

            // Prepare device for configuration
            CaptureDevice.LockForConfiguration(out Error);
            if (Error != null)
            {
                // There has been an issue, abort
                Console.WriteLine("Error: {0}", Error.LocalizedDescription);
                CaptureDevice.UnlockForConfiguration();
                return;
            }

            // Configure stream for 15 frames per second (fps)
            CaptureDevice.ActiveVideoMinFrameDuration = new CMTime(1, 15);

            // Unlock configuration
            CaptureDevice.UnlockForConfiguration();

            // Get input from capture device
            Input = AVCaptureDeviceInput.FromDevice(CaptureDevice);
            if (Input == null)
            {
                // Error, report and abort
                Console.WriteLine("Unable to gain input from capture device.");
                CameraAvailable = false;
                return;
            }

            // Attach input to session
            Session.AddInput(Input);


            // Create a new output
            var output = new AVCaptureVideoDataOutput();
            var settings = new AVVideoSettingsUncompressed();
            settings.PixelFormatType = CVPixelFormatType.CV32BGRA;
            output.WeakVideoSettings = settings.Dictionary;

            // IUView
            var layer = new AVCaptureVideoPreviewLayer(Session);
            //layer.LayerVideoGravity = AVLayerVideoGravity.ResizeAspectFill;
            //layer.VideoGravity = AVCaptureVideoPreviewLayer.GravityResizeAspectFill;

            var cameraView = new UIView();
            cameraView.Layer.AddSublayer(layer);

            // Configure and attach to the output to the session
            Queue = new DispatchQueue("ManCamQueue");
            Recorder = new OutputRecorder();
            output.SetSampleBufferDelegate(Recorder, Queue);
            Session.AddOutput(output);

            // Let tabs know that a camera is available
            CameraAvailable = true;

            Session.StartRunning();
        }
    }
}
