using System;
using AppKit;
using Foundation;

namespace myfirstcoca
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate,INSWindowDelegate
    {
        public NSWindow MyWindow { set; get; }


        public AppDelegate()
        {

        }

       

        public override void DidFinishLaunching(NSNotification notification)
        {
            /*
            this.MyWindow = NSApplication.SharedApplication.KeyWindow;

            NSImage image = NSImage.ImageNamed("1_3.jpg");
            NSColor color = NSColor.FromPatternImage(image);

            NSApplication.SharedApplication.KeyWindow.BackgroundColor = color;
            */
            //NSApplication.SharedApplication.KeyWindow.Title = "remember to take a break ~ ";

            // Insert code here to initialize your application
        }



        public override void WillTerminate(NSNotification notification)
        {
            //NSApplication.SharedApplication.Terminate(this);
            // Insert code here to tear down your application
        }
    }



}