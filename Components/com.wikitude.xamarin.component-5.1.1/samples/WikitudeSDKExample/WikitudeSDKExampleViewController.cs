using System;
using CoreGraphics;

using Foundation;
using UIKit;

using Wikitude.Architect;

namespace WikitudeSDKExample
{
	public partial class WikitudeSDKExampleViewController : UIViewController
	{

		private WTArchitectView architectView;

		static bool UserInterfaceIdiomIsPhone {
			get { return UIDevice.CurrentDevice.UserInterfaceIdiom == UIUserInterfaceIdiom.Phone; }
		}

		public WikitudeSDKExampleViewController (IntPtr handle) : base (handle)
		{
		}

		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}

		public void StartAR()
		{
			if ( !architectView.IsRunning ) {
				architectView.Start (null, null);
				Console.WriteLine ("Wikitude SDK version " + Wikitude.Architect.WTArchitectView.SDKVersion + " is running.");
			}
		}

		public void StopAR()
		{
			if ( architectView.IsRunning ) {
				architectView.Stop ();
			}
		}

		#region View lifecycle

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();			
			// Perform any additional setup after loading the view, typically from a nib.

			this.architectView = new Wikitude.Architect.WTArchitectView ();
			this.View.AddSubview (this.architectView);
			this.architectView.TranslatesAutoresizingMaskIntoConstraints = false;

			NSDictionary views = new NSDictionary (new NSString ("architectView"), architectView);
			this.View.AddConstraints (NSLayoutConstraint.FromVisualFormat("|[architectView]|", 0, null, views));
			this.View.AddConstraints (NSLayoutConstraint.FromVisualFormat("V:|[architectView]|", 0, null, views));

			architectView.SetLicenseKey ("TE2aBzgPu/Gw3LkVsGtiVM6PHjD9hMWnJKzoqsZUwMZFj02Qck7lpgFR+fEy4CVQhUSUbOLJH3S9UITCq3SW8B6KrrAeJLzghONILBXStJMNXwSNy4jz9HyQZTtlcL3vYUB8qLW9A2xs6VGO3T1Kxhp88kG/NCIi2uyGbdX7Fm1TYWx0ZWRfX2LmEsNhIN48hzc/w+7m3K2STcpca74vxhkTBJ4AnyoMuIEQ0P9xcxDUzcKRktg1mbU+n+a92+jo8MtJ4td2r392NiUgjaiQkWjLDKkadQqc++5JhQurSIHZqF2dpTZblHFKtw5E2FNn2BWrYo44U/WSYxkfqdJX2OilCePHcaFO+G7/1wP8JGu4myrk/z3cLlYhLUR1smfDByEfHKYE7h+scmlpSpBMus7dFTSOwaedln9sViYzuCAgojK8ScJPGObtBje4AwajdCjw1AgxseSFH6m1Ox3PNLNk1z8sJLWOZtIjpsLTLtRAWy0k0725qO2IJgnJ367kqmCTXlxLbKz3A4y2vws3DzWBkIb7HRRrZKsu5HXAXIWz3fZik01mMSKYnshp33lAIW/5Gz1pPd1yb+mjQgZAdUQAaHqxgVi65GKRnroFwjJEwabM+/dyYl0DRt2WL/pnTluc9/G+GIkGsbwH/0ASG9gMhlxMkPxQylAYhoXLWwx2p3nzMBKQbia6mgyefnpoOHdSw69xYVmPEC9mnspehpwqkE0tgUcc+OIiF6bcRmRJxeI1KW1yCSzOy4ReS3G1GZxyGtmRyS601nOJQPqm6g==");

			var path = NSBundle.MainBundle.BundleUrl.AbsoluteString + "1_ImageRecognition_1_ImageOnTarget/index.html";
			architectView.LoadArchitectWorldFromURL (NSUrl.FromString (path), Wikitude.Architect.WTFeatures.WTFeature_2DTracking);
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			StartAR ();
		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);
		}

		public override void ViewWillDisappear (bool animated)
		{
			base.ViewWillDisappear (animated);
		}

		public override void ViewDidDisappear (bool animated)
		{
			base.ViewDidDisappear (animated);

			StopAR ();
		}

		#endregion

		#region Rotation

		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate (toInterfaceOrientation, duration);

			architectView.SetShouldRotateToInterfaceOrientation (true, toInterfaceOrientation);
		}

		public override bool ShouldAutorotate()
		{
			return true;
		}

		public override UIInterfaceOrientationMask GetSupportedInterfaceOrientations ()
		{
			return UIInterfaceOrientationMask.All;
		}

		#endregion
	}
}

