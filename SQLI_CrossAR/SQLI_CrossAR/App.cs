using SQLI_CrossAR.CrossAR.Models;
using SQLI_CrossAR.CrossAR.Services;
using SQLI_CrossAR.CrossAR.Views;
using SQLI_CrossAR.Utils;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace SQLI_CrossAR
{

	public class App : Application
	{
        public static GoogleWebService Service = new GoogleWebService();
        public static GeoLocatorService Locator = new GeoLocatorService();
        public static PaletteDesign MyPalette = new PaletteDesign("red", "red");
        public static ObservableCollection<Place> CurrentPlaces = new ObservableCollection<Place>();
        public static string CurrentQuery;

        public App ()
		{
            
            //MainPage = new NavigationPage(new SplashView());
            //App.Current.MainPage = new NavigationPage(new PIDetailView(DAOPointInteret.GetDAOImplInstance().onePoint()));
            //MainPage = new NavigationPage(new PIListView());
            //MainPage = new NavigationPage(new NearbyView());
            MainPage = new NavigationPage(new SplashView());
        }

        protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
