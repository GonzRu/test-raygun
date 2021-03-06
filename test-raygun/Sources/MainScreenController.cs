using System;
using MonoTouch.UIKit;
using System.Drawing;
using Mindscape.Raygun4Net;
using System.Threading.Tasks;

namespace testraygun
{
	public class MainScreenController : UIViewController
	{
		private static readonly RaygunClient _raygunClient = new RaygunClient("");

		#region Buttons
		private static readonly RectangleF Button1Frame = new RectangleF(20, 20, 280, 20);
		private static readonly RectangleF Button2Frame = new RectangleF(20, 60, 280, 20);
		private static readonly RectangleF Button3Frame = new RectangleF(20, 100, 280, 20);
		private static readonly RectangleF Button4Frame = new RectangleF(20, 140, 280, 20);

		private static readonly string Button1Text = "throw Exception";
		private static readonly string Button2Text = "throw ArgumentNullException";
		private static readonly string Button3Text = "throw OutOfMemoryException";
		private static readonly string Button4Text = "throw from another thread";

		private UIButton _btn1;
		private UIButton _btn2;
		private UIButton _btn3;
		private UIButton _btn4;
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
			View.AddSubview(_btn4);
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

			_btn4 = new UIButton(UIButtonType.Custom);
			_btn4.Frame = Button4Frame;
			_btn4.SetTitle(Button4Text, UIControlState.Normal);
			_btn4.BackgroundColor = UIColor.Blue;
		}

		private void InitButtonHandlers()
		{
			_btn1.TouchUpInside += Button1Clicked;
			_btn2.TouchUpInside += Button2Clicked;
			_btn3.TouchUpInside += Button3Clicked;
			_btn4.TouchUpInside += Button4Clicked;
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
				_raygunClient.Send(ex, null, "1.1");
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
				_raygunClient.Send(ex, null, "1.0");
			}
		}

		private void Button3Clicked(object sender, EventArgs e)
		{
			try
			{
				throw new OutOfMemoryException("Exception from Button3");

			}
			catch (Exception ex)
			{
				_raygunClient.Send(ex, null, "1.5");
			}
		}

		private void Button4Clicked(object sender, EventArgs e)
		{
			Task.Run (() => {
				try {
					throw new OutOfMemoryException ("Exception from Button4 from Task");
				} catch (Exception ex) {
					_raygunClient.Send (ex, null, "1.5");
				}
			});
		}
		#endregion
	}
}

