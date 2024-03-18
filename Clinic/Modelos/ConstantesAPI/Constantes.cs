using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Modelos.ConstantesAPI
{
    internal class Constantes
    {
        //Desarrollo
        //public static string API_BASE_ADDRESS = "https://localhost:7151/";
        //public static string API_BASE_ADDRESS = "https://127.0.0.1:7151/";
        //Produccion
        public static string API_BASE_ADDRESS = "https://ApiClinica.somee.com";

        //FirebaseAPI CONSTANTES
        public static string authDomain = "clinic-f3d50.firebaseapp.com";
        public static string apiKey = "AIzaSyCp08TVqvdB0vbh0HL5tRrjYa4KHD9bh70";
        public static string email = "clinic@gmail.com";
        public static string passWord = "clinic123";
        public static string token = string.Empty;
        public static string rutaStorage = "clinic-f3d50.appspot.com";
    }
}
