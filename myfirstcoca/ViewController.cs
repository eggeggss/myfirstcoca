using System;
using System.Net;
using System.Timers;
using AppKit;
using CoreAnimation;
using CoreGraphics;
using Foundation;

namespace myfirstcoca
{
    public partial class ViewController : NSViewController, INSWindowDelegate
    {
        private Timer MainTimer { set; get; }
        private Timer SecondTimer { set; get; }
        private Timer thirdTimer { set; get; }
        private int orignTimeLeft;
        private int TimeLeft = 0;
        private int imagenumber = 0;
        private int snowpos = 0;


        public ViewController(IntPtr handle) : base(handle)
        {

        }



        private void ChangeBackground(int number)
        {

            NSImage image = NSImage.ImageNamed($"1_{number}.jpg");
            NSColor color = NSColor.FromPatternImage(image);


            this.View.Window.BackgroundColor = color;
            //NSApplication.SharedApplication.KeyWindow.BackgroundColor = color;
        }

        public override void ViewDidAppear()
        {
            base.ViewDidAppear();


            ChangeBackground((6));


            this.View.Window.Title = "remember to take a break ~ ";

            this.View.Window.Delegate = this;

            ShowSnow(0);
            //ShowFire();
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            this.labeltime.Alignment = NSTextAlignment.Center;
            this.txtRemain.StringValue = "";
            this.txtRemain.BackgroundColor = NSColor.FromRgb(135 / 255f, 206 / 255f, 250 / 255f);
            this.txtRemain.Alignment = NSTextAlignment.Center;
            this.txtminutes.Alignment = NSTextAlignment.Right;

            SecondTimer = new Timer(1000);
            SecondTimer.Elapsed += (sender, e) =>
            {
                TimeLeft--;
                TimeSpan time = TimeSpan.FromSeconds(TimeLeft);

                InvokeOnMainThread(() =>
                {
                    this.txtRemain.StringValue = $"剩:{TimeLeft / 60} 分鐘 ";
                });

                if (TimeLeft == 0)
                {
                    // Stop the timer and reset
                    //SecondTimer.Stop();
                    TimeLeft = this.orignTimeLeft;

                    InvokeOnMainThread(() =>
                    {
                        String path = NSBundle.MainBundle.PathForResource("fb", "mp3");

                        NSSound sound = new NSSound(path, true);

                        sound.Play();

                        NSAlert alert = new NSAlert();
                        // Set the style and message text
                        alert.AlertStyle = NSAlertStyle.Informational;
                        alert.MessageText = "起來休息啦!!";
                        //NSImage image = NSImage.ImageNamed("clock.ico");
                        // Display the NSAlert from the current view
                        alert.BeginSheet(View.Window);
                        NSTimer.CreateScheduledTimer(TimeSpan.FromSeconds(5),(a)=>{

                            this.View.Window.EndSheet(alert.Window);
                        });

                    });
                }

            };


            MainTimer = new Timer(1000);
            MainTimer.Elapsed += (sender, e) =>
            {

                DateTime time = DateTime.Now;
                string timeString = time.ToString(@"HH\:mm\:ss");
                InvokeOnMainThread(() =>
                {

                    this.labeltime.StringValue = timeString;

                });

            };

            MainTimer.Start();

            thirdTimer = new Timer(3000);
            thirdTimer.Elapsed += (sender, e) =>
              {
                  imagenumber++;

                  InvokeOnMainThread(() =>
                  {
                      ChangeBackground((imagenumber));
					  
                      NextSnowStartPosition();
					  //this.HideLayer();

				  });


                  if (imagenumber == 6)
                      imagenumber = 1;

                 
                  
                  
              };

            thirdTimer.Start();
        }


        public override NSObject RepresentedObject
        {
            get
            {
                return base.RepresentedObject;
            }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }

        partial void btnStart(NSObject sender)
        {
            int result;


            if (!Int32.TryParse(this.txtminutes.StringValue, out result))
            {

                NSAlert alert = new NSAlert();
                // Set the style and message text
                alert.AlertStyle = NSAlertStyle.Informational;
                alert.MessageText = "please input number";
                // Display the NSAlert from the current view
                alert.BeginSheet(View.Window);


            }
            else
            {
                this.TimeLeft = result * 60;
                this.orignTimeLeft = this.TimeLeft;
            }


            if (SecondTimer.Enabled)
            {
                SecondTimer.Stop();
                mybtnstart.Title = "Start";
                txtminutes.Enabled = true;
            }
            else
            {
                SecondTimer.Start();
                mybtnstart.Title = "Stop";
                txtminutes.Enabled = false;


            }

        }

        [Export("windowShouldClose:")]
        public bool WindowShouldClose(NSObject sender)
        {
            NSApplication.SharedApplication.Terminate(this);
            return true;
        }

        private void NextSnowStartPosition()
        {
           

            this.HideLayer();

            //int x=snowsx[snowpos];
            switch(snowpos)
            {
                case(0):
                    ShowSnow(0);
                    break;
                case(1):
                    
                    ShowFire();
                    break;

                case(2):
                    ShowSnow(640);
                    break;
                default:
                    break;
            }


            if (snowpos >= 2)
            {
                snowpos = 0;
            }else
            {
				snowpos++;

            }
                

        }

        public CAEmitterLayer Layer { set; get; }
         

        private void ShowSnow(int x)
        {
            this.Layer = new CAEmitterLayer();
            this.Layer.Position = new CGPoint(x, 320);

            CAEmitterCell cell = new CAEmitterCell();
            cell.BirthRate = 100;
            cell.LifeTime = 10;
            cell.Velocity = 100;
            cell.Scale = 0.4f;
            cell.EmissionRange = 3.14f * 2;
            cell.Contents = NSImage.ImageNamed("snow.png").CGImage;

            this.Layer.Cells = new CAEmitterCell[1]{cell};
            this.View.Layer.AddSublayer(this.Layer);

		}

        private void ShowFire()
        {
            CGRect viewBounds = this.View.Layer.Bounds;
            this.Layer = new CAEmitterLayer();

            //this.Layer.Position= new CGPoint(0, 320);

			//this.Layer.Position = new CGPoint(viewBounds.Size.Width / 2.0, viewBounds.Size.Height / 2.0);

			this.Layer.Position = new CGPoint(0, viewBounds.Size.Height / 2.0);

			this.Layer.Size = new CGSize(viewBounds.Size.Width / 2.0, 0.0);

            this.Layer.Mode = "kCAEmitterLayerOutline";

            this.Layer.Shape = "kCAEmitterLayerLine";

            this.Layer.RenderMode = "kCAEmitterLayerAdditive";

            Random rad = new Random();
            int radnum=rad.Next(0,100);

            this.Layer.Seed = radnum + 1;

            //1.rocket
            CAEmitterCell rocket = new CAEmitterCell();
            rocket.BirthRate = 1.0f;
            rocket.EmissionRange = 0.25f * 3.14f;  // some variation in angle
			rocket.Velocity = 380;
			rocket.VelocityRange = 100;
            rocket.AccelerationY = 75;
            rocket.LifeTime = 1.02f;    // we cannot set the birthrate < 1.0 for the burst
            rocket.Contents = NSImage.ImageNamed("snow.png").CGImage;
            rocket.Scale = 0.2f;
            rocket.Color = new CGColor(1,0,0);
            rocket.GreenRange = 1.0f;        // different colors
            rocket.RedRange = 1.0f;
            rocket.BlueRange = 1.0f;
            rocket.SpinRange = 3.14f;

            //2.burst
            CAEmitterCell burst = new CAEmitterCell();

            burst.BirthRate = 1.0f;        // at the end of travel
            burst.Velocity = 0;
            burst.Scale = 2.5f;
            burst.RedSpeed = -1.5f;        // shifting
            burst.BlueSpeed = +1.5f;        // shifting
            burst.GreenSpeed = +1.0f;        // shifting
            burst.LifeTime = 0.35f;

            //3.spark
            CAEmitterCell spark = new CAEmitterCell();

            spark.BirthRate = 400f;
            spark.Velocity = 125f;
            spark.EmissionRange = 2f * 3.14f;    // 360 deg
            spark.AccelerationY = 75f;        // gravity
            spark.LifeTime = 3f;

            spark.Contents = NSImage.ImageNamed("snow.png").CGImage;
            spark.ScaleSpeed = -0.2f;
            spark.GreenSpeed = -0.1f;
            spark.RedSpeed = 0.4f;
            spark.BlueSpeed = -0.1f;
            spark.AlphaSpeed = -0.25f;
            spark.Spin = 1f * 3.14f;
            spark.SpinRange = 2f * 3.14f;


            this.Layer.Cells = new CAEmitterCell[1] { rocket };
            rocket.Cells = new CAEmitterCell[1] { burst };
            burst.Cells = new CAEmitterCell[1] { spark };
            this.View.Layer.AddSublayer(this.Layer);
	        
            //remar3333


		}



        private void HideLayer()
        {
            this.Layer.RemoveFromSuperLayer();

        }


        #region
        private void Faddin(NSView view)
        {
            NSAnimationContext.BeginGrouping();
            NSAnimationContext.CurrentContext.Duration = 2;
            this.View.AddSubview(view);
            NSAnimationContext.EndGrouping();

        }

        private void Faddout(NSView view)
        {
            NSAnimationContext.BeginGrouping();
            NSAnimationContext.CurrentContext.Duration = 2;
            view.RemoveFromSuperview();
            NSAnimationContext.EndGrouping();

        }
        #endregion
    }
}
