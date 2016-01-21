using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms.Maps;


namespace SQLI_CrossAR.CrossAR.Algorithm
{
    public class MathsHelper
    {
        /// <summary>
        /// Calcul de la distance entre deux points gps
        /// Formule : A(latA, longA) B(latB, longB)
        ///     distance angulaire en radians drad = longB - longA
        ///     distance en km d(A>B) = arccos( sin(latA)sin(latB) + cos(latA)cos(latB)cos(drad) )
        /// </summary>

        public double ComputeDistanceBetweenPoints(Position positionA, Position positionB)
        {
            double dist_angulaire_en_radian = positionB.Longitude - positionA.Longitude;
            double dist = Math.Sin(deg2rad(positionA.Latitude)) * Math.Sin(deg2rad(positionB.Latitude)) 
                + Math.Cos(deg2rad(positionA.Latitude)) * Math.Cos(deg2rad(positionB.Latitude)) * Math.Cos(deg2rad(dist_angulaire_en_radian));
            dist = rad2deg(Math.Acos(dist)) * 1.609344 * 60 * 1.1515; 
            return (dist);
        }


        /// <summary>
        ///     Method who can decide whether to display a pi on screen or not
        /// </summary>
        /// <param name="deltaAzimuthInDeg">Angle returned by (public double AngleBetweenDeviceOrientationAndPI(Vector3D orient, Position pi))</param>
        /// <param name="cameraRangeInDeg">Camera range of device</param>
        /// <returns></returns>
        public bool shouldDisplayPi(double deltaAzimuthInDeg, double cameraRangeInDeg)
        {
            return (Math.Abs(deltaAzimuthInDeg) <= Math.Abs(cameraRangeInDeg / 2)) ? true : false;
        }


        /// <summary>
        ///     For the first version we only consider the azimuth
        /// </summary>
        /// <param name="orient">Vector3D of which the values are in [-180,180]</param>
        /// <param name="pi">GPS position of PI</param>
        /// <returns>
        ///     Return angle between camera facing orientation and the orientation pointing to PI,
        ///     if angle is in [0,90], that means PI is on our right-handed side,
        ///     if angle is in [-90,0], that means PI is on our left-handed side.
        ///     After using this method, user should check the return value,
        ///     if the return value is greater than 90 or less than -90, that means the pi should be hidden on the screen.
        /// </returns>
        public double AngleBetweenDeviceOrientationAndPI(Vector3D orient, Position pi)
        {
            // Get current GPS position
            Position current = new Position(App.Locator.CurrentLatitude, App.Locator.CurrentLongitude);

            // Get camera azimuth orientation in deg (-180 -- 180)
            double currentAzimuth = orient.Z;

            // [-180,180] -> [0,360]
            currentAzimuth = (currentAzimuth + 360) % 360;

            // Get gps orientation (in deg) from current position to pi (0 -- 360)
            double piAzimuth = getPiAzimuthInDeg(current, pi);

            // Get delta orientation (-360 -- 360)
            double deltaAzimuth = piAzimuth - currentAzimuth;

            // We want to keep the values in [0,90] and [270,360] because of the screen scale
            if(deltaAzimuth > -360 && deltaAzimuth < -270)
            {
                deltaAzimuth += 360; // [-360,-270] -> [0,90]
            }
            else if (deltaAzimuth > 270 && deltaAzimuth < 360)
            {
                deltaAzimuth -= 360; // [270,360] -> [-90,0]
            }

            return deltaAzimuth;
        }


        /// <summary>
        ///     Returns pointing orientation from start position to end position
        /// </summary>
        /// <param name="start">GPS position of starting point</param>
        /// <param name="end">GPS position of ending point</param>
        /// <returns>
        ///     Value in degree (0 -- 360). 0 means north, 90 means east, 180 means south and 270 means west.
        /// </returns>
        private double getPiAzimuthInDeg(Position start, Position end)
        {
            // Get start lat and lon in rad
            double startLat = deg2rad(start.Latitude);
            double startLong = deg2rad(start.Longitude);

            // Get end lat and lon in rad
            double endLat = deg2rad(end.Latitude);
            double endLong = deg2rad(end.Longitude);

            // get the delta values between start and end coordinates
            double dLong = endLong - startLong;

            // Calculate delta phi
            double dPhi = Math.Log(Math.Tan(endLat / 2 + Math.PI / 4) / Math.Tan(startLat / 2 + Math.PI / 4));

            // Set dLong between -2PI and 2PI
            if (Math.Abs(dLong) > Math.PI)
            {
                dLong = dLong > 0 ? -(2 * Math.PI - dLong) : (2 * Math.PI + dLong);
            }

            return (rad2deg(Math.Atan2(dLong, dPhi)) + 360) % 360;
        }


        private double getPiAzimuthInRad(Position start, Position end)
        {
            return deg2rad(getPiAzimuthInDeg(start, end));
        }



        private double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
    }
}
