using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using System.Collections.ObjectModel;
using SQLI_CrossAR.CrossAR.Models;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Diagnostics;
using SQLI_CrossAR.CrossAR.Services;

namespace SQLI_CrossAR.CrossAR.ViewModels
{
    public class RootViewModel : INotifyPropertyChanged
    {

        private ObservableCollection<Place> places;
        public ObservableCollection<Place> Places
        {
            get { return places; }
            private set { places = value; }
        }

        public ICommand NavigationCommand { get; set; }

        private bool _IsBusy;
        public bool IsBusy
        {
            get { return _IsBusy; }
            set { SetProperty(ref _IsBusy, value); }
        }

        public RootViewModel()
        {
            places = new ObservableCollection<Place>();
        }

        #region INotifyPropertyChanged implementation
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        protected void SetProperty<T>(ref T backingStore, T value, [CallerMemberName]string propertyName = null, Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return;

            backingStore = value;

            if (onChanged != null)
                onChanged();

            OnPropertyChanged(propertyName);
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
        
        private Command _LoadPlacesCommand;
        public Command LoadPlacesCommand
        {
            get
            {
                return _LoadPlacesCommand ?? (_LoadPlacesCommand = new Command(async () => await ExecuteLoadPlacesCommand(), () => !IsBusy));
            }
        }

        public async Task ExecuteLoadPlacesCommand()
        {
            await Task.Yield();

            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                bool DidGetLocation = await App.Locator.GetLocation();
                if (DidGetLocation)
                {
                    GoogleWebService googleService = new GoogleWebService();
                    List<Place> query = (await googleService.GetPlacesForCoordinates(App.Locator.CurrentLatitude, App.Locator.CurrentLongitude));

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (query != null)
                        {
                            if (places != null)
                            {
                                App.CurrentPlaces.Clear();
                                Places.Clear();
                            }                                
                            else
                                places = new ObservableCollection<Place>();
                            foreach (Place p in query)
                            {
                                App.CurrentPlaces.Add(p);
                                places.Add(p);
                            }
                        }
                    });
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("Error loading place: ", e.Message);
            }
            finally
            {
                IsBusy = false;
                LoadPlacesCommand.ChangeCanExecute();
            }
        }
    }
}
