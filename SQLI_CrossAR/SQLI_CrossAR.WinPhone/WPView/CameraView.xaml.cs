using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Devices;
using System.Threading;

namespace SQLI_CrossAR.WinPhone.WPView
{
    public partial class CameraView : PhoneApplicationPage
    {
        // Variables 
        PhotoCamera myCamera;
        PageOrientation orientation;

        // Holds the current flash mode. 
        private string currentFlashMode;

        // Holds the current resolution index. 
        int currentResIndex = 0;

        public CameraView()
        {
            InitializeComponent();
            this.Orientation = Microsoft.Phone.Controls.PageOrientation.Landscape;
            //this.OrientationChanged += cam_OrientationChanged;
            ShowPreview();
        }

        void ShowPreview()
        {
            myCamera = new Microsoft.Devices.PhotoCamera(CameraType.Primary);

            myCamera.CaptureCompleted += new EventHandler<CameraOperationCompletedEventArgs>(camera_CaptureCompleted);

            // myCamera.CaptureImageAvailable += new EventHandler<Microsoft.Devices.ContentReadyEventArgs>(camera_CaptureImageAvailable);

            viewfinderBrush.SetSource(myCamera);
        }

        void camera_CaptureCompleted(object sender, CameraOperationCompletedEventArgs e)
        {
            Thread.Sleep(100);
            Deployment.Current.Dispatcher.BeginInvoke(delegate ()
            {
                MessageBox.Show("Capture Completed");

            });
        }
    }
}