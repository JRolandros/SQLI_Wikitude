using System;
using System.Collections.Generic;
using System.Text;

using Foundation;
using CoreLocation;


namespace Xamarin_AR.Algorithm
{
    class ARGeoCoordinate : ARCoordinate
    {
        public CLLocation geoLocation { get; set; }
        public UIView displayView;
        public double distanceFromOrigin { get; set; }

        public ARGeoCoordinate(CLLocation location, string titleOfLocation)
        {
            geoLocation = location;
            base.title = titleOfLocation;
        }

        public ARGeoCoordinate(CLLocation location, CLLocation origin)
        {
            calibrateUsingOrigin(origin);
        }

        public float angleFromCoordinate(CLLocationCoordinate2D first, CLLocationCoordinate2D second)
        {
            double longitudinalDifference = second.longitude - first.longitude;
            double latitudinalDifference = second.latitude - first.latitude;
            double possibleAzimuth = (Math.PI * .5f) - Math.Atan(latitudinalDifference / longitudinalDifference);

            if (longitudinalDifference > 0)
                return (float)possibleAzimuth;
            else if (longitudinalDifference < 0)
                return (float)(possibleAzimuth + Math.PI);
            else if (latitudinalDifference < 0)
                return (float)Math.PI;

            return 0.0f;
        }

        public void calibrateUsingOrigin(CLLocation origin)
        {
            if(geoLocation == null)
            {
                return;
            }

            distanceFromOrigin = origin.DistanceFrom(geoLocation);
            base.radialDistance = Math.Sqrt(Math.Pow((origin.Altitude - geoLocation.Altitude), 2) + Math.Pow(distanceFromOrigin, 2));

            float angle = Math.Sin(Math.Abs(origin.Altitude - geoLocation.Altitude) / base.radialDistance);

            if (origin.Altitude > geoLocation.Altitude)
            {
                angle = -angle;
            }

            base.inclination = angle;
            base.azimuth = angleFromCoordinate(origin.Coordinate, geoLocation.Coordinate);

            //NSLog(@"distance from %@ is %f, angle is %f, azimuth is %f",[self title], [self distanceFromOrigin], angle,[self azimuth]);
        }
    }
}
