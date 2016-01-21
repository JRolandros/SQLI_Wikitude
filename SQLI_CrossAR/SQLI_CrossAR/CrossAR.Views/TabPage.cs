using SQLI_CrossAR.CrossAR.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace SQLI_CrossAR.CrossAR.Views
{
    public class TabPage : TabbedPage
    {
        public TabPage()
        {            
            Title = "TabPage";

            var nearbyView = new NearbyPage();
            var mapView = new MapPage();
            //var favorisView = new PIListView();
            var settingsView = new SettingsPage();
            
            Children.Add(nearbyView);
            Children.Add(mapView);
            //.Add(favorisView);
            Children.Add(settingsView);           
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            string title = this.CurrentPage.Title;
            switch (title)
            {
                case "Nearby":
                    if (NavigationService.navStack.Contains("nearbyview"))
                        NavigationService.navStack.Remove("nearbyview");
                    NavigationService.navStack.Add("nearbyview");
                    break;
                case "Map":
                    if (NavigationService.navStack.Contains("mappage"))
                        NavigationService.navStack.Remove("mappage");
                    NavigationService.navStack.Add("mappage");
                    break;                
                //case "Favoris":
                //    if (NavigationService.navStack.Contains("pilistview")) 
                //        NavigationService.navStack.Remove("pilistview");
                //    NavigationService.navStack.Add("pilistview");
                //    break;
                case "Settings":
                    if (NavigationService.navStack.Contains("settingsview"))
                        NavigationService.navStack.Remove("settingsview");
                    NavigationService.navStack.Add("settingsview");
                    break;
                default:
                    break;
            }
        }

        protected override bool OnBackButtonPressed()
        {
            NavigationService.GoBack();
            return true;
        }
    }
}
