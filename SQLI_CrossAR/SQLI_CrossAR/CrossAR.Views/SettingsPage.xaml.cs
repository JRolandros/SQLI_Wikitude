using SQLI_CrossAR.CrossAR.Services;
using SQLI_CrossAR.CrossAR.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace SQLI_CrossAR.CrossAR.Views
{
	public partial class SettingsPage : ContentPage
	{
        public double value;
        public SettingsViewModel viewModel = new SettingsViewModel();
		public SettingsPage ()
		{
            Title = "Settings";
			InitializeComponent ();
            initRayon();
            this.BindingContext = viewModel;
            sliderRayon.ValueChanged += SliderRayon_ValueChanged;
            designSetups();
		}
        public void initRayon()
        {
            viewModel.Rayon = ((int) Math.Round(sliderRayon.Value * 1000));
        }
        private void SliderRayon_ValueChanged(object sender, ValueChangedEventArgs e)
        {            
            viewModel.Rayon = ((int)Math.Round(e.NewValue * 1000));
            Utils.Settings.Rayon = viewModel.Rayon;                                   
            Debug.WriteLine("\n_ _ _ _Nouvelle valeur du slider SQLI_CrossAR.Utils.Settings.Rayon : " + SQLI_CrossAR.Utils.Settings.Rayon + "m");
        }

        protected override bool OnBackButtonPressed()
        {
            NavigationService.GoBack();
            return true;
        }

        public void designSetups()
        {            
            BackgroundColor = App.MyPalette.WhiteBackground;
            settingsRayTxt.TextColor = App.MyPalette.TextPrimary;
            settingsRayTxt.FontSize = App.MyPalette.TitleFontSize;
            settingsCatTxt.TextColor = App.MyPalette.TextPrimary;
            settingsCatTxt.FontSize = App.MyPalette.TitleFontSize;
            pickerCat.BackgroundColor = App.MyPalette.WhiteBackground;            
        }
    }
}
