// WARNING
//
// This file has been generated automatically by Xamarin Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace icom
{
	[Register ("AgendaController")]
	partial class AgendaController
	{
		[Outlet]
		UIKit.UIButton btnNuevoEvento { get; set; }

		[Outlet]
		UIKit.UITableView lstAgenda { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnNuevoEvento != null) {
				btnNuevoEvento.Dispose ();
				btnNuevoEvento = null;
			}

			if (lstAgenda != null) {
				lstAgenda.Dispose ();
				lstAgenda = null;
			}
		}
	}
}