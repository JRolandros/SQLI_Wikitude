using Android.App;
using Android.Content.PM;
using Android.OS;

namespace SQLI_CrossAR.Droid
{
	[Activity (Label = "SQLI_CrossAR", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation/*, Theme = "@android:style/Theme.NoTitleBar"*/)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);
            this.Window.AddFlags(Android.Views.WindowManagerFlags.Fullscreen);
			global::Xamarin.Forms.Forms.Init (this, bundle);
            global::Xamarin.FormsMaps.Init(this,bundle);
            LoadApplication (new SQLI_CrossAR.App ());
		}
	}
}

