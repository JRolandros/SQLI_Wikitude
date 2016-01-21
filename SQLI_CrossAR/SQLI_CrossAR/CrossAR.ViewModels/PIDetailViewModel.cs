
using SQLI_CrossAR.CrossAR.Algorithm;
using SQLI_CrossAR.CrossAR.Models;
using SQLI_CrossAR.CrossAR.Views;
using System.ComponentModel;

using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace SQLI_CrossAR.CrossAR.ViewModels
{
    public class PIDetailViewModel : RootViewModel
    {
        public MathsHelper _maths = new MathsHelper();
        public PointInteret _pi;

        public string TitleLabel { get; set; }
        public string Address { get; set; }
        public string GPSInfoLabel { get; set; }
        public string DetailImage { get; set; }
        public string ItemInfo1 { get; set; }
        public string ItemInfo2 { get; set; }
        public string ItemInfo3 { get; set; }
        //public string ItemInfo4 { get; set; }

        //public string ItemInfo5 { get; set; }
        public PIDetailViewModel(PointInteret PI)
        {
            _pi = PI;

            //Generic labels
            TitleLabel = _pi.Nom;
            Address = _pi.Adresse;
            GPSInfoLabel = "GPS. (" + string.Format("{0:0.00000}", _pi.Latitude) + " ; " + string.Format("{0:0.00000}", _pi.Longitude) + ")"; 
            DetailImage = _pi.ImageSource;

            //Binding distance with functions froms GPS Algo
            Position userposition;
            Position piPosition = new Position(_pi.Latitude, _pi.Longitude);
            if (App.Locator != null)
            {
                userposition = new Position(App.Locator.CurrentLatitude, App.Locator.CurrentLongitude);
                double distanceUserPI = _maths.ComputeDistanceBetweenPoints(userposition, piPosition);

                ItemInfo1 = "User Pos. (" + string.Format("{0:0.00000}", userposition.Latitude) + " ; " + string.Format("{0:0.00000}", userposition.Longitude) + ")"; 
                ItemInfo2 = "PI Pos. (" + string.Format("{0:0.00000}", piPosition.Latitude) + " ; " + string.Format("{0:0.00000}", piPosition.Longitude) + ")";
                ItemInfo3 = "Distance. " + string.Format("{0:0.000}", distanceUserPI) + "km";

            }
            else
            {
                ItemInfo1 = "User Pos. Can't collect user position info!";
            }
                           
        }
    }
}
