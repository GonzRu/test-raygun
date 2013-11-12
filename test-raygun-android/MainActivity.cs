using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Threading.Tasks;

namespace testraygunandroid
{
	[Activity (Label = "test-raygun-android", MainLauncher = true)]
	public class MainActivity : Activity
	{
		private Button _btn1;
		private Button _btn2;
		private Button _btn3;
		private Button _btn4;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate(bundle);

			var layot = new LinearLayout(this);
			layot.Orientation = Orientation.Vertical;

			_btn1 = new Button(this);
			_btn1.SetText(Resource.String.Button1Text);
			_btn1.Click += OnClickButton1;

			_btn2 = new Button(this);
			_btn2.SetText(Resource.String.Button2Text);
			_btn2.Click += OnClickButton2;

			_btn3 = new Button(this);
			_btn3.SetText(Resource.String.Button3Text);
			_btn3.Click += OnClickButton3;

			_btn4 = new Button(this);
			_btn4.SetText(Resource.String.Button4Text);
			_btn4.Click += OnClickButton4;

			layot.AddView(_btn1);
			layot.AddView(_btn2);
			layot.AddView(_btn3);
			layot.AddView(_btn4);
			SetContentView(layot);
		}

		#region Button Handlers
		private void OnClickButton1(object sender, EventArgs e)
		{
			try
			{
				throw new Exception("Exception from Button1");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Catch Exception:" + ex.Message);
			}
		}

		private void OnClickButton2(object sender, EventArgs e)
		{
			try
			{
				throw new ArgumentNullException("ArgumentNullException from Button2");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Catch Exception:" + ex.Message);
			}
		}

		private void OnClickButton3(object sender, EventArgs e)
		{
			try
			{
				throw new OutOfMemoryException("OutOfMemoryException from Button3");
			}
			catch (Exception ex)
			{
				Console.WriteLine("Catch Exception:" + ex.Message);
			}
		}

		private void OnClickButton4(object sender, EventArgs e)
		{
			Task.Run( () => {
				try
				{
					throw new Exception("Exception from Button4 from new thread");
				}
				catch (Exception ex)
				{
					Console.WriteLine("Catch Exception:" + ex.Message);
				}
			});
		}
		#endregion
	}
}


