using SQLI_CrossAR.CrossAR.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace SQLI_CrossAR.CrossAR.ViewModels
{
    public class BaseViewModel : RootViewModel
    {     

        public BaseViewModel()
        {
            NavigationCommand = new Command(() => {
                NavigationService.NavigateTo("TabPage",null);
            });
        }       

    }
}
