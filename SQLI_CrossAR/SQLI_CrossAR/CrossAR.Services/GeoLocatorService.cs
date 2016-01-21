using System;
using System.Collections.Generic;
using System.Text;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System.Threading.Tasks;
using SQLI_CrossAR.CrossAR.ViewModels;

namespace SQLI_CrossAR.CrossAR.Services
{
    public class GeoLocatorService : RootViewModel
    {
        private readonly IGeolocator _locator;
        private int _timeout = 10000;
        private double _latitude = 0;
        private double _longitude = 0;
        private DateTime _timestamp = DateTime.MinValue;

        public GeoLocatorService(double accuracy = 50)
        {
            _locator = CrossGeolocator.Current;
            _locator.DesiredAccuracy = accuracy;
        }

        public int Timeout
        {
            get { return _timeout; }
            set { SetProperty(ref _timeout, value); }
        }
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set { SetProperty(ref _timestamp, value); }
        }

        public double CurrentLatitude
        {
            get { return _latitude; }
            set { SetProperty(ref _latitude, value); }
        }

        public double CurrentLongitude
        {
            get { return _longitude; }
            set { SetProperty(ref _longitude, value); }
        }

        public async Task<bool> GetLocation()
        {
            Position currentpos = null;

            try
            {
                currentpos = await _locator.GetPositionAsync(_timeout);
            }
            catch (Exception e)
            {
                Console.WriteLine("Geolocator.GetLocation error: " + e.Message);
            }

            if (currentpos != null)
            {
                CurrentLatitude = currentpos.Latitude;
                CurrentLongitude = currentpos.Longitude;
                Timestamp = currentpos.Timestamp.DateTime;

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}