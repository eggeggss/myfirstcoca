// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace myfirstcoca
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField labeltime { get; set; }

		[Outlet]
		AppKit.NSButton mybtnstart { get; set; }

		[Outlet]
		AppKit.NSLayoutConstraint txtmin { get; set; }

		[Outlet]
		AppKit.NSTextField txtminutes { get; set; }

		[Outlet]
		AppKit.NSTextField txtRemain { get; set; }

		[Action ("btnStart:")]
		partial void btnStart (Foundation.NSObject sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (txtRemain != null) {
				txtRemain.Dispose ();
				txtRemain = null;
			}

			if (labeltime != null) {
				labeltime.Dispose ();
				labeltime = null;
			}

			if (mybtnstart != null) {
				mybtnstart.Dispose ();
				mybtnstart = null;
			}

			if (txtmin != null) {
				txtmin.Dispose ();
				txtmin = null;
			}

			if (txtminutes != null) {
				txtminutes.Dispose ();
				txtminutes = null;
			}
		}
	}
}
