using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinPhone;

[assembly: ExportRenderer(typeof(SQLI_CrossAR.CrossAR.Views.CameraView), typeof(SQLI_CrossAR.WinPhone.WPView.CameraViewRenderer))]
namespace SQLI_CrossAR.WinPhone.WPView
{
    public class CameraViewRenderer : VisualElementRenderer<Page, Microsoft.Phone.Controls.PhoneApplicationPage>
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
        {
            base.OnElementChanged(e);
            SetNativeControl(new CameraView());
        }
    }
}
