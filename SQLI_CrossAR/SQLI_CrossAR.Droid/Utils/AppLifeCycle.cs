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
using SQLI_CrossAR.Utils;
using SQLI_CrossAR.Droid.Utils;

[assembly: Xamarin.Forms.Dependency(typeof(AppLifeCycle))]
namespace SQLI_CrossAR.Droid.Utils
{
    public class AppLifeCycle : IAppLifeCycle
    {
        public void ExitApp()
        {
            Android.OS.Process.KillProcess(Android.OS.Process.MyPid());
        }
    }
}