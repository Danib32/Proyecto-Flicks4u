using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace LGNflicks.Tools
{
    
        public static class Herramientas
        {

            public static async Task<BitmapImage> ArrayToBmI(byte[] imagen)
            {
                BitmapImage imagentransformada = new BitmapImage();
                using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
                {
                    await stream.WriteAsync(imagen.AsBuffer());
                    stream.Seek(0);
                    await imagentransformada.SetSourceAsync(stream);
                }
                return imagentransformada;
            }

        }
    }

