using SQLI_CrossAR.CrossAR.Models;
using SQLI_CrossAR.CrossAR.Services;
using SQLI_CrossAR.CrossAR.ViewModels;
using Xamarin.Forms;

namespace SQLI_CrossAR.CrossAR.Views
{
	public partial class PIListView : ContentPage
	{
        public PIListView ()
		{
            Title = "Favoris";
			InitializeComponent ();
            this.BindingContext = new PIListViewModel();
            designSetups();
            myListView.ItemTapped += (sender, e) => 
            {
                PointInteret listItem = (PointInteret)myListView.SelectedItem;
                NavigationService.NavigateTo("PIDetailView",listItem);
            };
        }

        protected override bool OnBackButtonPressed()
        {
            NavigationService.GoBack();
            return true;
        }

        public void designSetups()
        {
            mainStack.BackgroundColor = App.MyPalette.WhiteBackground;
            searchBar.BackgroundColor = App.MyPalette.AccentColor;
            searchBar.TextColor = App.MyPalette.TextIcons;
        }
    }
}
