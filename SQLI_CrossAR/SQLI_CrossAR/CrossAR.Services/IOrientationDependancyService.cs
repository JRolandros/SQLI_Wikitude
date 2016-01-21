namespace SQLI_CrossAR.CrossAR.Services
{
    public enum DeviceOrientations
    {
        Undefined,
        Landscape,
        Portrait
    }

    public interface IOrientationDependancyService
    {
        DeviceOrientations GetOrientation();

        void SetOrientationPortrait();

        void SetOrientationLandscape();
    }
}


//CameraPage : Landscape (default) 
//Mappage : Portrait (default) ou Landscape
//NearbyView : Portrait (default) ou Landscape
//PIDetailView : Portrait (default) 
//PIListView : Portrait (default) 
//SettingsView : Portrait (default) ou Landscape
//SplashView : Landscape (default) ou Portrait
//TabPage : Portrait (default) ou Landscape