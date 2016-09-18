﻿using System;
using System.Collections.Generic;
using UIKit;
using System.Threading.Tasks;
using Foundation;
using icom.globales;
using System.Net.Http;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Linq;

namespace icom
{
	public partial class PlanificadorController : UIViewController
	{
		public PlanificadorController() : base("PlanificadorController", null)
		{
		}
		LoadingOverlay loadPop;
		HttpClient client;
		public int idcategoria { get; set; }

		private List<clsEvento> lstEventos; 
		NSDateFormatter dateFormatterFecha = new NSDateFormatter() { DateFormat = "EEEE, MMMM dd, yy" };
		NSLocale locale = NSLocale.FromLocaleIdentifier("es_MX");

		public async override void ViewDidLoad()
		{
			base.ViewDidLoad();
			lstEventos = new List<clsEvento>();
			DateTime fechaAct = DateTime.Now;
			dateFormatterFecha.Locale = locale;
			//lblfecha.Text = dateFormatterFecha.ToString(funciones.ConvertDateTimeToNSDate(fechaAct));

			//UITapGestureRecognizer tgrLabel = new UITapGestureRecognizer(() =>{	DatePickerFechaInicio(); });
			//lblfecha.UserInteractionEnabled = true;
			//lblfecha.AddGestureRecognizer(tgrLabel);


			/*clsEvento objev1 = new clsEvento
			{
				titulo = "Evento 1",
				totalhoras = "25",
				lapso = "de 12:00 a 12:00",
				porcentajeavance = 27
			};

			clsEvento objev2 = new clsEvento
			{
				titulo = "Evento 2",
				totalhoras = "25",
				lapso = "de 12:00 a 12:00",
				porcentajeavance = 40
			};

			clsEvento objev3 = new clsEvento
			{
				titulo = "Evento 3",
				totalhoras = "25",
				lapso = "de 12:00 a 12:00",
				porcentajeavance = 85
			};

			clsEvento objev4 = new clsEvento
			{
				titulo = "Evento 4",
				totalhoras = "25",
				lapso = "de 12:00 a 12:00",
				porcentajeavance = 36
			};

			clsEvento objev5 = new clsEvento
			{
				titulo = "Evento 5",
				totalhoras = "25",
				lapso = "de 12:00 a 12:00",
				porcentajeavance = 95
			};

			lstEventos.Add(objev1);
			lstEventos.Add(objev2);
			lstEventos.Add(objev3);
			lstEventos.Add(objev4);
			lstEventos.Add(objev5);*/

			tblEventos.Source = new FuenteTablaEventos(this, lstEventos);
			Boolean resp = await GetTareas();
			if (resp) {
				tblEventos.ReloadData();
			}

			btnNuevoEvento.TouchUpInside += delegate {
				NuevaTareaController viewnt = new NuevaTareaController();
				viewnt.idcategoria = idcategoria;

				viewnt.Title = "Nueva Tarea";
				this.NavigationController.PushViewController(viewnt, false);
				UIView.BeginAnimations(null);
				UIView.SetAnimationDuration(0.7);
				UIView.SetAnimationTransition(UIViewAnimationTransition.CurlUp, NavigationController.View, true);
				UIView.CommitAnimations();
			};

			btnEditarEvento.TouchUpInside += delegate {
				ModifciarTareaController viewmt = new ModifciarTareaController();


				viewmt.Title = "Modificar Tarea";
				this.NavigationController.PushViewController(viewmt, false);
				UIView.BeginAnimations(null);
				UIView.SetAnimationDuration(0.7);
				UIView.SetAnimationTransition(UIViewAnimationTransition.CurlUp, NavigationController.View, true);
				UIView.CommitAnimations();
			};



			btnExportaPDF.TouchUpInside += delegate
			{
				ExportarTareasController viewe = new ExportarTareasController();


				viewe.Title = "Exportacion PDF";
				this.NavigationController.PushViewController(viewe, false);
				UIView.BeginAnimations(null);
				UIView.SetAnimationDuration(0.7);
				UIView.SetAnimationTransition(UIViewAnimationTransition.CurlUp, NavigationController.View, true);
				UIView.CommitAnimations();
			};
		}

		public async void recargarListado()
		{

			lstEventos = new List<clsEvento>();
			tblEventos.Source = new FuenteTablaEventos(this, lstEventos);
			Boolean resp = await GetTareas();

			if (resp)
			{
				loadPop.Hide();
				tblEventos.ReloadData();
			}
		}

		public async Task<Boolean> GetTareas()
		{

			var bounds = UIScreen.MainScreen.Bounds;
			loadPop = new LoadingOverlay(bounds, "Buscando Tareas...");
			View.Add(loadPop);
			client = new HttpClient();
			client.Timeout = new System.TimeSpan(0, 0, 0, 10, 0);
			string url = Consts.ulrserv + "controldeobras/getTareasPlanificadorByIdCategoria";
			var uri = new Uri(string.Format(url));

			Dictionary<string, string> pet = new Dictionary<string, string>();

			pet.Add("idcategoria", idcategoria.ToString());

			var json = JsonConvert.SerializeObject(pet);

			string responseString = string.Empty;
			responseString = await funciones.llamadaRest(client, uri, loadPop, json, Consts.token);


			if (responseString.Equals("-1"))
			{
				funciones.SalirSesion(this);
			}

			JArray jrarray;
			try
			{
				var objectjson = JObject.Parse(responseString);

				var result = objectjson["result"];
				if (result != null)
				{
					loadPop.Hide();
					if (result.ToString().Equals("0")) { funciones.MessageBox("Aviso", "No existen tareas actualmente"); }
					else { funciones.MessageBox("Error", objectjson["error"].ToString()); }
					return false;
				}

				jrarray = JArray.Parse(objectjson["tareas"].ToString());
			}
			catch (Exception e)
			{
				loadPop.Hide();
				var jsonresponse = JObject.Parse(responseString);
				string mensaje = "al transformar el arreglo" + e.HResult;
				var jtokenerror = jsonresponse["error"];
				if (jtokenerror != null)
				{
					mensaje = jtokenerror.ToString();
				}
				funciones.MessageBox("Error", mensaje);
				return false;
			}

			foreach (var tar in jrarray)
			{
				clsEvento objev = getObjEvento(tar);
				lstEventos.Add(objev);
			}
			loadPop.Hide();
			return true;
		}



		public clsEvento getObjEvento(Object varjson)
		{

			JObject json = (JObject)varjson;
			clsEvento obj = new clsEvento
			{
				titulo = json["titulo"].ToString(),
				totalhoras = json["horas"].ToString(),
				lapso = json["lapso"].ToString(),
				porcentajeavance = double.Parse(json["porcentaje"].ToString())
			};

			return obj;
		}

		async void buscarTareas(object sender, EventArgs e)
		{

			Boolean resp;
			if (txtbusquedatarea.Equals(""))
				resp = await GetTareas();
			else
				resp = await searchTareas();

			if (resp)
			{
				tblEventos.Source = new FuenteTablaEventos(this, lstEventos);
				tblEventos.ReloadData();
			}
		}

		public async Task<Boolean> searchTareas()
		{

			var bounds = UIScreen.MainScreen.Bounds;
			loadPop = new LoadingOverlay(bounds, "Buscando Tareas...");
			View.Add(loadPop);
			client = new HttpClient();
			client.Timeout = new System.TimeSpan(0, 0, 0, 10, 0);
			string url = Consts.ulrserv + "controldeobras/busquedaTareasPlanificador";
			var uri = new Uri(string.Format(url));

			Dictionary<string, string> pet = new Dictionary<string, string>();

			pet.Add("idcategoria", idcategoria.ToString());
			pet.Add("strBusqueda", idcategoria.ToString());

			var json = JsonConvert.SerializeObject(pet);

			string responseString = string.Empty;
			responseString = await funciones.llamadaRest(client, uri, loadPop, json, Consts.token);


			if (responseString.Equals("-1"))
			{
				funciones.SalirSesion(this);
			}

			JArray jrarray;
			try
			{
				var objectjson = JObject.Parse(responseString);

				var result = objectjson["result"];
				if (result != null)
				{
					loadPop.Hide();
					if (result.ToString().Equals("0")) { funciones.MessageBox("Aviso", "No se encontraron coincidencias "); }
					else { funciones.MessageBox("Error", objectjson["error"].ToString()); }
					return false;
				}

				jrarray = JArray.Parse(objectjson["tareas"].ToString());
			}
			catch (Exception e)
			{
				loadPop.Hide();
				var jsonresponse = JObject.Parse(responseString);
				string mensaje = "al transformar el arreglo" + e.HResult;
				var jtokenerror = jsonresponse["error"];
				if (jtokenerror != null)
				{
					mensaje = jtokenerror.ToString();
				}
				funciones.MessageBox("Error", mensaje);
				return false;
			}

			foreach (var tar in jrarray)
			{
				clsEvento objev = getObjEvento(tar);
				lstEventos.Add(objev);
			}
			loadPop.Hide();
			return true;
		}

	}
}


