using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Threading.Tasks;

namespace SQLI_CrossAR.CrossAR.Models
{
    public class PointInteret
    {
        //Déclarations
        //public int Id { get; set; }
        public string ImageSource { get; set; }
        public string Nom { get; set; }
        public string Adresse { get; set; }
        public string Categorie { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public bool Favoris { get; set; }
        public bool Visible { get; set; }


        //method to convert latitude and longitude to a physic address name
        public async Task<string> GetAddressByCoordinate(double latitude, double longitude)
        {           
            var pos = new Position(latitude, longitude);
            Geocoder geoCoder = new Geocoder();
            var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(pos);
            return possibleAddresses.FirstOrDefault();
        }
        
        //this method has to be called before getting attribute adresse
        public async void GetAddressByPosition()
        {
                var pos = new Position(Latitude, Longitude);
                Geocoder geoCoder = new Geocoder();
                var possibleAddresses = await geoCoder.GetAddressesForPositionAsync(pos);
                Adresse = possibleAddresses.FirstOrDefault();
        }
    }
}
