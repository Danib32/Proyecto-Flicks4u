using LGNflicks.DTOS;
using LGNflicks.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// La plantilla de elemento Página en blanco está documentada en https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0xc0a

namespace LGNflicks
{
    /// <summary>
    /// Página vacía que se puede usar de forma independiente o a la que se puede navegar dentro de un objeto Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static string nomusser;
        public static int idusser;
        public static string LoginString;
        public MainPage()
        {
            this.InitializeComponent();
           
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            List<Usuarios> usuarios = new List<Usuarios>();
            var HttpHeadrer = new HttpClientHandler();
            var solicitud = new HttpRequestMessage();
            solicitud.RequestUri = new Uri("https://localhost:44379/api/Usuarios");
            solicitud.Method = HttpMethod.Get;
            solicitud.Headers.Add("Accept", "application/json");

            var Client = new HttpClient(HttpHeadrer);
            HttpResponseMessage respuesta = await Client.SendAsync(solicitud);
            string contenido = await respuesta.Content.ReadAsStringAsync();
            var data = JsonConvert.DeserializeObject<List<Usuarios>>(contenido);
            usuarios = data;
            string user = Txt_Usuariolog.Text;
            string pass = Txt_Password.Password.ToString();
            nomusser = user;
            foreach (Usuarios aux1 in usuarios)
            {
                
                if(user==null && pass== null) 
                {
                    mext.Text = "existen campos vacios";
                }
               
                if (aux1.CorreoElectronico==user && aux1.Contrasenia==pass)
                {
                    if (aux1.tipoSuscripcion == 2)
                    {
                        mext.Text = "No se permite el ingreso de administradores";
                        break;
                    }
                    Frame.Navigate(typeof(Ingreso));
                    break;
                }
                
                else
                {
                    mext.Text = "Ingreso de usuario o contrasenia incorrectos";
                }
            }

        }
       
    }
}
