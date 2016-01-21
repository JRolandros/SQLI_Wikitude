using Xamarin.Forms;
using SQLI_CrossAR.CrossAR.Models;
using SQLI_CrossAR.CrossAR.ViewModels;
using SQLI_CrossAR.CrossAR.DataAccess.DAO;
using Xamarin.Forms.Maps;
using SQLI_CrossAR.CrossAR.Services;
using System;
using System.Diagnostics;

namespace SQLI_CrossAR.CrossAR.Views
{
	public partial class PIDetailView : ContentPage
	{
        //Round Button Constante
        private const int BUTTON_BORDER_WIDTH = 1;
        private const int BUTTON_HEIGHT = 66;
        private const int BUTTON_HEIGHT_WP = 108;
        private const int BUTTON_HALF_HEIGHT = 33;
        private const int BUTTON_HALF_HEIGHT_WP = 54;
        private const int BUTTON_WIDTH = 66;
        private const int BUTTON_WIDTH_WP = 108;

        public PointInteret _pi;
        DAOPointInteret _dao;
        PIDetailViewModel _vm = null;
        public PIDetailView (PIDetailViewModel vm)
		{
            _vm = vm;
            _pi = _vm._pi;
            _dao = DAOPointInteret.GetDAOImplInstance();
            Title = "Detail";
			InitializeComponent ();
            Position pinPosition = new Position(vm._pi.Latitude, vm._pi.Longitude);
            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(pinPosition, Distance.FromMeters(1000)));
            var pin = new Pin
            {
                Type = PinType.Place,
                Position = pinPosition,
                Label = _pi.Nom,
                Address = _pi.Adresse
            };
            MyMap.Pins.Add(pin);
            designSetups();
            btnFavoris.Clicked += OnButtonClicked;
            this.BindingContext = _vm;

            
        }

        protected override bool OnBackButtonPressed()
        {
            NavigationService.GoBack();
            return true;
        }

        void OnButtonClicked(object sender, EventArgs e)
        {
            bool insertPI = _dao.InsertTablePI(_pi);
            if (insertPI)
                Debug.WriteLine("_ _ __OK__ _ _");   
        }

        public void designSetups()
        {            
            mainStack.BackgroundColor = App.MyPalette.WhiteBackground;
            imageTextStack .BackgroundColor = App.MyPalette.PrimaryColor;
            textStack.Padding = new Thickness(20, 0, 0, 0);
            detailTitleLabel.TextColor = App.MyPalette.TextIcons;
            detailTitleLabel.FontSize = App.MyPalette.TitleFontSize;
            detailTitleLabel.FontAttributes = FontAttributes.Bold;
            detailMessageLabel.TextColor = App.MyPalette.PrimaryColorLight;
            detailMessageLabel.FontSize = App.MyPalette.SubtitleFontSize;
            detailMessageLabel.FontAttributes = FontAttributes.Italic;
            detailGPSInfoLabel.TextColor = App.MyPalette.PrimaryColorLight;
            detailGPSInfoLabel.FontSize = App.MyPalette.TextLargeFontSize;
            detailGPSInfoLabel.FontAttributes = FontAttributes.Italic;
            infoStack.BackgroundColor = App.MyPalette.WhiteBackground;
            infoStack.Padding = new Thickness(10, 0, 10, 10);
            detailAutreInfos.TextColor = App.MyPalette.TextPrimary;
            detailAutreInfos.FontSize = App.MyPalette.TextMediumFontSize;
            detailAutreInfos.FontAttributes = FontAttributes.Bold;
            detailItemInfo1.TextColor = App.MyPalette.TextSecondary;
            detailItemInfo1.FontSize = App.MyPalette.TextSmallFontSize;
            detailItemInfo2.TextColor = App.MyPalette.TextSecondary;
            detailItemInfo2.FontSize = App.MyPalette.TextSmallFontSize;
            detailItemInfo3.TextColor = App.MyPalette.TextSecondary;
            detailItemInfo3.FontSize = App.MyPalette.TextSmallFontSize;
            //detailItemInfo4.TextColor = App.MyPalette.TextSecondary;
            //detailItemInfo4.FontSize = App.MyPalette.TextSmallFontSize;
            //detailItemInfo5.TextColor = App.MyPalette.TextSecondary;
            //detailItemInfo5.FontSize = App.MyPalette.TextTinyFontSize;
            btnFavoris.HorizontalOptions = LayoutOptions.Center;
            btnFavoris.BackgroundColor = App.MyPalette.AccentColor;
            btnFavoris.BorderColor = App.MyPalette.PrimaryColor;
            btnFavoris.BorderWidth = BUTTON_BORDER_WIDTH;
            btnFavoris.BorderRadius = Device.OnPlatform(BUTTON_HALF_HEIGHT, BUTTON_HALF_HEIGHT, BUTTON_HALF_HEIGHT_WP);
            btnFavoris.HeightRequest = Device.OnPlatform(BUTTON_HEIGHT, BUTTON_HEIGHT, BUTTON_HEIGHT_WP);
            btnFavoris.MinimumHeightRequest = Device.OnPlatform(BUTTON_HEIGHT, BUTTON_HEIGHT, BUTTON_HEIGHT_WP);
            btnFavoris.WidthRequest = Device.OnPlatform(BUTTON_WIDTH, BUTTON_WIDTH, BUTTON_WIDTH_WP);
            btnFavoris.MinimumWidthRequest = Device.OnPlatform(BUTTON_WIDTH, BUTTON_WIDTH, BUTTON_WIDTH_WP);
            
        }

        
    }
}
