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
	[Register ("PlanificadorController")]
	partial class PlanificadorController
	{
		[Outlet]
		UIKit.UIButton btnActualizarEventos { get; set; }

		[Outlet]
		UIKit.UIButton btnEditarEvento { get; set; }

		[Outlet]
		UIKit.UIButton btnExportaPDF { get; set; }

		[Outlet]
		UIKit.UIButton btnNuevoEvento { get; set; }

		[Outlet]
		UIKit.UITableView tblEventos { get; set; }

		[Outlet]
		UIKit.UITextField txtbusquedatarea { get; set; }

		[Outlet]
		UIKit.UIView viewPanelBottom { get; set; }

		[Outlet]
		UIKit.UIView viewPanelTop { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (btnActualizarEventos != null) {
				btnActualizarEventos.Dispose ();
				btnActualizarEventos = null;
			}

			if (btnEditarEvento != null) {
				btnEditarEvento.Dispose ();
				btnEditarEvento = null;
			}

			if (btnExportaPDF != null) {
				btnExportaPDF.Dispose ();
				btnExportaPDF = null;
			}

			if (btnNuevoEvento != null) {
				btnNuevoEvento.Dispose ();
				btnNuevoEvento = null;
			}

			if (tblEventos != null) {
				tblEventos.Dispose ();
				tblEventos = null;
			}

			if (txtbusquedatarea != null) {
				txtbusquedatarea.Dispose ();
				txtbusquedatarea = null;
			}

			if (viewPanelBottom != null) {
				viewPanelBottom.Dispose ();
				viewPanelBottom = null;
			}

			if (viewPanelTop != null) {
				viewPanelTop.Dispose ();
				viewPanelTop = null;
			}
		}
	}
}
