using SQLI_CrossAR.CrossAR.ViewModels;
using SQLI_CrossAR.CrossAR.Views;
using SQLI_CrossAR.Utils;
using System;
using System.Linq;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SQLI_CrossAR.CrossAR.Services
{
   
    public static class NavigationService
    {
        public static List<string> navStack = new List<string>();
        public static string previousPage = null;
        public static string currentPage = null;
        public static TabPage tabPage=null;

        public enum ListPages
        {
            baseview=1,
            caemeraview=2,
            pilistview,
            tabpage,
            nearbypage,
            mappage,
            pidetailview,
            settings
        };

        public static void NavigateTo(string namePage,object param)
        {
            switch (namePage.ToLower())
            {
                case "baseview":
                    App.Current.MainPage = new BaseView();
                    currentPage = "baseview";
                    break;

                case "cameraview":
                    App.Current.MainPage = new CameraView();
                    currentPage = "cameraview";
                    navStack.Add(currentPage);
                    break;
                
                case "tabpage": 
                    tabPage= new TabPage();
                    App.Current.MainPage = tabPage;
                    //when going to the tabPage, the stack list of pages will be populated inside the tabpage with the current page object.
                    break;
                
                case "pidetailview": 
                    if (param != null)
                    {
                        PIDetailViewModel vm = new PIDetailViewModel((Models.PointInteret)param);
                        App.Current.MainPage = new NavigationPage(new PIDetailView(vm));
                        currentPage = "pidetailview";
                        navStack.Add(currentPage);
                    }                       
                    break;
               
                default:
                    throw new InvalidOperationException(
                            "No suitable page found for this name" + namePage);
                    break;
            }
        }

        public static void GoBack()
        {
            navStack.RemoveAt(navStack.Count - 1);
            previousPage = navStack.ElementAt(navStack.Count - 1);

            switch (previousPage)
            {
                //case "baseview":
                //    App.Current.MainPage = new BaseView();
                //    currentPage = "baseview";
                //    break;
                case "cameraview":
                    App.Current.MainPage = new CameraView();
                    currentPage = "cameraview";
                    break;

                //case "pilistview":
                //    tabPage.CurrentPage = tabPage.Children[0];
                //    App.Current.MainPage = tabPage;
                //    break;

                case "nearbypage":
                    //App.Current.MainPage = new BaseView();
                    //currentPage = "baseview";
                    tabPage.CurrentPage = tabPage.Children[0];
                    App.Current.MainPage = tabPage;
                    break;
                case "mappage":
                    tabPage.CurrentPage = tabPage.Children[1];
                    App.Current.MainPage = tabPage;
                    break;

                case "pidetailview":
                    //tabPage.CurrentPage = tabPage.Children[0];
                    App.Current.MainPage = tabPage;
                    break;

                case "settingspage":
                    tabPage.CurrentPage = tabPage.Children[2];
                    App.Current.MainPage = tabPage;
                    break;

                default:
                    App.Current.MainPage = new CameraView();
                    //if (Device.OS == TargetPlatform.Android)
                    //{
                    //    //DependencyService.Get<IAppLifeCycle>().ExitApp();
                    //}
                    break;
            }
        }
    }
}
