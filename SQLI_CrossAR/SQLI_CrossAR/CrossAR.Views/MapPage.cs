using SQLI_CrossAR.CrossAR.DataAccess.DAO;
using SQLI_CrossAR.CrossAR.Models;
using SQLI_CrossAR.CrossAR.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SQLI_CrossAR.CrossAR.Views
{
    public class MapPage : ContentPage
    {
        private readonly StackLayout main_stack;
        private object NavigationService;
        //public DAOPointInteret _dao;
        private NearbyViewModel viewModel
        {
            get { return BindingContext as NearbyViewModel; }
        }
        public MapPage()
        {
            //set the title of the tabbar item
            Title = "Map";

            //Assign out ViewModel to the page's binding context
            BindingContext = new NearbyViewModel();

            //load places
            //viewModel.LoadPlacesCommand.Execute(null);

            //create main stack panel for page
            main_stack = new StackLayout();

            //setup and display the map
             SetupMap();

            //assign content to page
            Content = main_stack;

            
        }

        private async void SetupMap()
        {
            //execute load places through our viewmodel (which also gets current position)
            await viewModel.ExecuteLoadPlacesCommand();

            if (App.Locator != null)
            {
                //create a new Position object with our current coordinates
                var my_position = new Position(App.Locator.CurrentLatitude, App.Locator.CurrentLongitude);
                //initialize the map object
                var map = new Map(MapSpan.FromCenterAndRadius(my_position, Distance.FromKilometers(2)))
                {
                    IsShowingUser = true,
                    HeightRequest = 100,
                    WidthRequest = 960,
                    VerticalOptions = LayoutOptions.FillAndExpand
                };
                map.MapType = MapType.Hybrid;
                
                //loop on the places list and create pins
                foreach (Place p in viewModel.Places)
                {
                    var pin = new Pin
                    {
                        Type = PinType.Place,
                        Position = new Position(p.geometry.location.lat, p.geometry.location.lng),
                        Label = p.name,
                        Address = p.vicinity
                    };

                    //ad place pin to map
                    map.Pins.Add(pin);
                }

                //add map to existing stack layout assigned to page
                main_stack.Children.Add(map);
            }
        }


        protected override bool OnBackButtonPressed()
        {
            Services.NavigationService.GoBack();
            return true;
        }
    }
}

