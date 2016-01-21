using System;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using DeviceMotion.Plugin;

using SQLI_CrossAR.CrossAR.Algorithm;
using SQLI_CrossAR.CrossAR.ViewModels;


namespace SQLI_CrossAR.CrossAR.Views
{
    public partial class BaseView : ContentPage
    {
        //Round Button Constante
        private const int BUTTON_BORDER_WIDTH = 1;
        private const int BUTTON_HEIGHT = 66;
        private const int BUTTON_HEIGHT_WP = 108;
        private const int BUTTON_HALF_HEIGHT = 33;
        private const int BUTTON_HALF_HEIGHT_WP = 54;
        private const int BUTTON_WIDTH = 66;
        private const int BUTTON_WIDTH_WP = 108;


        double x_position = 0.35;
        private OrientationViewModel vm;
        private GetDeviceOrientation deviceOrientation;
        private Rectangle rect, rect2;

        public ObservableCollection<ToolbarItem> LeftToolbarItems { get; set; }

        public BaseView()
        {
            vm = new OrientationViewModel();
            InitializeComponent();
            //deviceOrientation = new GetDeviceOrientation();
            //initViewModel();

            LeftToolbarItems = new ObservableCollection<ToolbarItem>();

            //Ajout des Objets supperposés à la caméra

            //The boxview
            BoxView bv1 = new BoxView();
            bv1.Color = Color.Green;
            rect = new Rectangle(.35, .5, 50, 50);
            AbsoluteLayout.SetLayoutBounds(bv1, rect);
            AbsoluteLayout.SetLayoutFlags(bv1, AbsoluteLayoutFlags.PositionProportional);
            al.Children.Add(bv1);

            //Round Button
            Button roundButton = new Button
            {
                HorizontalOptions = LayoutOptions.Center,
                BackgroundColor = App.MyPalette.AccentColor,
                BorderColor = App.MyPalette.PrimaryColor,
                TextColor = Color.White,
                BorderWidth = BUTTON_BORDER_WIDTH,
                BorderRadius = Device.OnPlatform(BUTTON_HALF_HEIGHT, BUTTON_HALF_HEIGHT, BUTTON_HALF_HEIGHT_WP),
                HeightRequest = Device.OnPlatform(BUTTON_HEIGHT, BUTTON_HEIGHT, BUTTON_HEIGHT_WP),
                MinimumHeightRequest = Device.OnPlatform(BUTTON_HEIGHT, BUTTON_HEIGHT, BUTTON_HEIGHT_WP),
                WidthRequest = Device.OnPlatform(BUTTON_WIDTH, BUTTON_WIDTH, BUTTON_WIDTH_WP),
                MinimumWidthRequest = Device.OnPlatform(BUTTON_WIDTH, BUTTON_WIDTH, BUTTON_WIDTH_WP),
                Text = "Menu"
            };
            AbsoluteLayout.SetLayoutFlags(roundButton, AbsoluteLayoutFlags.PositionProportional);
            Rectangle rect3 = new Rectangle(1f, 1f, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize);
            AbsoluteLayout.SetLayoutBounds(roundButton, rect3);
            al.Children.Add(roundButton);
            roundButton.SetBinding(Button.CommandProperty, "NavigationCommand");

            //Trigerring motion sensors
            CrossDeviceMotion.Current.SensorValueChanged += (s, a) =>
            {
                //deviceOrientation.sensorRegistration(s, a);
                //initViewModel();

                //x_position = deviceOrientation.distanceMoveX(vm.RotationX);
                // x_position -= 0.5 * vm.ValueX * Math.Pow(deviceOrientation.dt, 2);
                //y_position -= 0.5 * vm.ValueY * Math.Pow(deviceOrientation.dt, 2);

                //x_position = vm.RotationX;
                //x_pos.Text = x_position.ToString();
                AbsoluteLayout.SetLayoutBounds(bv1, new Rectangle(x_position, .5, 50, 50));
            };

            this.BindingContext = new BaseViewModel();
        }

        ////To get the initial values for the orientation
        //private void initViewModel()
        //{
        //    vm.TextContent = deviceOrientation.str;
        //    vm.ValueX = deviceOrientation.degree.X;
        //    vm.ValueY = deviceOrientation.degree.Y;
        //    vm.ValueZ = deviceOrientation.degree.Z;
        //    vm.Rotation = -deviceOrientation.degree.X;
        //    vm.RotationX = -deviceOrientation.degree.Y;
        //    vm.RotationY = -deviceOrientation.degree.Z;
        //}

        private double getModuleAccel()
        {
            return Math.Sqrt(Math.Pow(vm.ValueX, 2) + Math.Pow(vm.ValueY, 2) + Math.Pow(vm.ValueZ, 2));
        }
    }
}
