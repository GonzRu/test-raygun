using System;
using MonoTouch.UIKit;
using System.Drawing;
using Mindscape.Raygun4Net;

namespace testraygun
{
	public class MainScreenController : UIViewController
	{
		private static readonly RaygunClient _raygunClient = new RaygunClient("");

		#region Buttons
		private static readonly RectangleF Button1Frame = new RectangleF(20, 20, 200, 20);
		private static readonly RectangleF Button2Frame = new RectangleF(20, 60, 200, 20);
		private static readonly RectangleF Button3Frame = new RectangleF(20, 100, 200, 20);

		private static readonly string Button1Text = "throw Exception";
		private static readonly string Button2Text = "throw ArgumentNullException";
		private static readonly string Button3Text = "throw Exception";

		private UIButton _btn1;
		private UIButton _btn2;
		private UIButton _btn3;
		#endregion

		public MainScreenController ()
		{
			InitButtons();
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			View.BackgroundColor = UIColor.White;

			InitButtonHandlers();

			View.AddSubview(_btn1);
			View.AddSubview(_btn2);
			View.AddSubview(_btn3);
		}

		private void InitButtons()
		{
			_btn1 = new UIButton();
			_btn1.Frame = Button1Frame;		
			_btn1.SetTitle(Button1Text, UIControlState.Normal);
			_btn1.BackgroundColor = UIColor.Blue;

			_btn2 = new UIButton(UIButtonType.Custom);
			_btn2.Frame = Button2Frame;
			_btn2.SetTitle(Button2Text, UIControlState.Normal);
			_btn2.BackgroundColor = UIColor.Blue;

			_btn3 = new UIButton(UIButtonType.Custom);
			_btn3.Frame = Button3Frame;
			_btn3.SetTitle(Button3Text, UIControlState.Normal);
			_btn3.BackgroundColor = UIColor.Blue;
		}

		private void InitButtonHandlers()
		{
			_btn1.TouchUpInside += Button1Clicked;
			_btn2.TouchUpInside += Button2Clicked;
			_btn3.TouchUpInside += Button3Clicked;
		}

		#region Handlers
		private void Button1Clicked(object sender, EventArgs e)
		{
			try
			{
				throw new Exception("Exception from Button1");
			}
			catch (Exception ex)
			{
				_raygunClient.Send(ex);
			}
		}

		private void Button2Clicked(object sender, EventArgs e)
		{
			try
			{
				throw new ArgumentNullException("ArgumentNullException from Button2");
			}
			catch (Exception ex)
			{
				_raygunClient.Send(ex);
			}
		}

		private void Button3Clicked(object sender, EventArgs e)
		{
			try
			{
				throw new Exception("Exception from Button1");

			}
			catch (Exception ex)
			{
				_raygunClient.Send(ex);
			}
		}
		#endregion
	}
}

