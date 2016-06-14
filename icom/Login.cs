﻿using System;

using UIKit;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Text;
using System.IO;
using icom.globales;
namespace icom
{
	public partial class Login : UIViewController
	{
		public Login () : base ("Login", null)
		{
		}
		HttpClient client;
		string token;
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			btnLogin.TouchUpInside += loginboton;

		}

		async void loginboton(object sender, EventArgs e)
		{
			Boolean resp = await login();

			if (resp)
			{
				
				Principal viewprin = new Principal();
				viewprin.strusuario = txtUsuario.Text;
				viewprin.strpass = txtPass.Text;
				viewprin.token = token;

				viewprin.Title = "I.C.O.M";
				this.NavigationController.PushViewController(viewprin, false);
				UIView.BeginAnimations(null);
				UIView.SetAnimationDuration(0.7);
				UIView.SetAnimationTransition(UIViewAnimationTransition.CurlUp, NavigationController.View,true);
				UIView.CommitAnimations();
			}
		}

		public async Task<Boolean> login()
		{
			String usuario = txtUsuario.Text;
			String pass = txtPass.Text;

			if (usuario.Equals(""))
			{
				funciones.MessageBox("Error", "Debe de ingresar usuario");
				return false;
			}

			if (pass.Equals(""))
			{
				funciones.MessageBox("Error", "Debe de ingresar password");
				return false;
			}

			client = new HttpClient();

			var uri = new Uri(string.Format(Consts.urltoken));


            var content = new StringContent("username="+usuario+"&password="+pass+"&grant_type=password&client_id=099153c2625149bc8ecb3e85e03f0022", Encoding.UTF8, "application/x-www-form-urlencoded");

			HttpResponseMessage response = null;

			try
			{
				response = await client.PostAsync(uri, content);
			}
			catch (Exception e)
			{
				funciones.MessageBox("Error", "No se ha podido hacer conexion con el servicio, verfiquelo con su administrador TI");
				return false;
			}

			if (response == null)
			{
				funciones.MessageBox("Error", "No se ha podido hacer conexion con el servicio, verfiquelo con su administrador TI");
				return false;
			}

			string responseString = string.Empty;
			responseString = await response.Content.ReadAsStringAsync();

			var jsonrequest = JObject.Parse(responseString);

			var jtokenerror = jsonrequest["error_description"];


			if (jtokenerror != null)
			{
				string error = jtokenerror.ToString();
				funciones.MessageBox("Error", error);
				return false;
			}

			token = jsonrequest["access_token"].ToString();
			return true;
		}



		public override void DidReceiveMemoryWarning ()
		{
			base.DidReceiveMemoryWarning ();
			// Release any cached data, images, etc that aren't in use.
		}


	}
}

