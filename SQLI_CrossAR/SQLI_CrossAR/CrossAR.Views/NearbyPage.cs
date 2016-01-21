using SQLI_CrossAR.CrossAR.DataAccess.DAO;
using SQLI_CrossAR.CrossAR.Models;
using SQLI_CrossAR.CrossAR.Services;
using SQLI_CrossAR.CrossAR.ViewModels;
using Xamarin.Forms;

namespace SQLI_CrossAR.CrossAR.Views
{
    public class NearbyPage : ContentPage
    {
        public DAOPointInteret _dao;
        public NearbyViewModel ViewModel
        {
            get { return BindingContext as NearbyViewModel; }
        }

        public NearbyPage()
        {
            _dao = DAOPointInteret.GetDAOImplInstance();
            Title = "Nearby";
            BindingContext = new NearbyViewModel();

            var main_stack = new StackLayout()
            {
                BackgroundColor = App.MyPalette.WhiteBackground
            };

            var header_label = new Label()
            {
                Text = "Places",
                FontSize = App.MyPalette.TitleFontSize,
                BackgroundColor = App.MyPalette.AccentColor,
                TextColor = App.MyPalette.TextIcons,
                HeightRequest = 40,
                XAlign = TextAlignment.Center,
                YAlign = TextAlignment.Center
            };

            var places_list = new ListView()
            {
                ItemsSource = App.CurrentPlaces
                //IsPullToRefreshEnabled = true,
                //RefreshCommand = ViewModel.LoadPlacesCommand
                
            };
            places_list.SetBinding(ListView.ItemsSourceProperty, "Places");
            places_list.SetBinding(ListView.IsRefreshingProperty, "IsBusy");

            var cell = new DataTemplate(typeof(ImageCell));
            cell.SetBinding(TextCell.TextProperty, "name");
            cell.SetValue(TextCell.TextColorProperty, App.MyPalette.TextPrimary);
            cell.SetBinding(ImageCell.ImageSourceProperty, "icon");

            places_list.ItemTemplate = cell;
            places_list.SeparatorColor = App.MyPalette.DividerColor;

            var lat_label = new Label()
            {
                FontSize          = App.MyPalette.TextLargeFontSize,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor   = App.MyPalette.AccentColor,
                TextColor         = App.MyPalette.TextIcons,
                HeightRequest     = 25,
                XAlign            = TextAlignment.Center,
                YAlign            = TextAlignment.Center
            };
            lat_label.BindingContext = App.Locator;
            lat_label.SetBinding(Label.TextProperty, "CurrentLatitude");

            var lon_label = new Label()
            {
                FontSize          = App.MyPalette.TextLargeFontSize,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor   = App.MyPalette.AccentColor,
                TextColor         = App.MyPalette.TextIcons,
                HeightRequest     = 25,
                XAlign            = TextAlignment.Center,
                YAlign            = TextAlignment.Center
            };
            lon_label.BindingContext = App.Locator;
            lon_label.SetBinding(Label.TextProperty, "CurrentLongitude");

            var coordinates_stack = new StackLayout()
            {
                Orientation       = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Padding           = new Thickness(0),
                Children          = { lat_label, lon_label }
            };

            main_stack.Children.Add(header_label);
            main_stack.Children.Add(coordinates_stack);
            main_stack.Children.Add(places_list);

            Content = main_stack;

            ViewModel.LoadPlacesCommand.Execute(null);
            places_list.ItemTapped += (sender,e)=> 
            {
                Place place = (Place)places_list.SelectedItem;
                PointInteret pi = new PointInteret()
                {
                    Latitude = place.geometry.location.lat,
                    Longitude = place.geometry.location.lng,
                    Nom = place.name,
                    Adresse = place.vicinity,
                    ImageSource = place.icon
                };
                pi.GetAddressByPosition();
                NavigationService.NavigateTo("PIDetailView", pi);
            };
        }

        protected override bool OnBackButtonPressed()
        {
            NavigationService.GoBack();
            return true;
        }
    }
}