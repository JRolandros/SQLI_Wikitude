using SQLI_CrossAR.CrossAR.DataAccess.DAO;
using SQLI_CrossAR.CrossAR.DataAccess.DB;
using SQLI_CrossAR.CrossAR.Models;
using SQLI_CrossAR.CrossAR.Services;
using SQLI_CrossAR.CrossAR.ViewModels;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace SQLI_CrossAR.CrossAR.Views
{
    public class SplashView : ContentPage
    {
        ProgressBar progressbar;
        Label progressLabel;
        DAOPointInteret _dao;
        DBImpl _dbi;
        bool resultDB;
        private SplashViewModel viewModel
        {
            get { return BindingContext as SplashViewModel; }
            
        }

        public SplashView()
        {
            BindingContext = new SplashViewModel();
            BackgroundColor = App.MyPalette.WhiteBackground;
            progressLabel = new Label() { Text = "Progress status" , HorizontalOptions=LayoutOptions.Center, VerticalOptions=LayoutOptions.Center};
            progressLabel.TextColor = App.MyPalette.TextPrimary;
            progressLabel.FontSize = App.MyPalette.SubtitleFontSize;
            progressbar = new ProgressBar() { Progress = 0, WidthRequest = 100, HorizontalOptions=LayoutOptions.Center, VerticalOptions = LayoutOptions.CenterAndExpand};
            var layout = new StackLayout();
            layout.Children.Add(progressLabel);
            layout.Children.Add(progressbar);
            Content = layout;
            _dao = DAOPointInteret.GetDAOImplInstance();
            _dbi = DBImpl.GetDBImpl();      
            progress();
        }

        public async void progress()
        {
            progressLabel.Text = "Initialisation BDD ..";
            progressbar.ProgressTo(.10, 1000, Easing.Linear);
            await Task.Delay(1000);
            resultDB = _dao.InitDB();
            if (resultDB)
                progressLabel.Text = "Intialisation DB OK";
            else progressLabel.Text = "Initialisation DB KO";
            progressbar.ProgressTo(.30, 500, Easing.Linear);
            await Task.Delay(500);

            progressLabel.Text = "Geolocalisation ..";
            progressbar.ProgressTo(.6, 2000, Easing.Linear);
            await viewModel.ExecuteLoadPlacesCommand();
            if (App.CurrentPlaces == null)
                progressLabel.Text = "Geolocation KO.";
            else progressLabel.Text = "Geolocation OK.";
            progressbar.ProgressTo(.8, 1000, Easing.Linear);
            await Task.Delay(1000);

            progressLabel.Text = "Initialisation OK ..";
            progressbar.ProgressTo(1, 200, Easing.Linear);
            await Task.Delay(100);

            //Debug.WriteLine("_ _ _ _ Check App.CurrentPlaces");
            //PointInteret insertedPI = new PointInteret();
            //foreach (var pi in App.CurrentPlaces)
            //{
            //    insertedPI.Nom = pi.name;
            //    insertedPI.Adresse = pi.vicinity;
            //    insertedPI.ImageSource = pi.icon;
            //    insertedPI.Latitude = pi.geometry.location.lat;
            //    insertedPI.Longitude = pi.geometry.location.lng;
            //    insertedPI.Visible = false;
            //    insertedPI.Favoris = false;
            //    insertedPI.Categorie = "food";
            //    _dbi.InsertPI(insertedPI);
            //}

            NavigationService.NavigateTo("CameraView", null);
        }   
    
    }
}



