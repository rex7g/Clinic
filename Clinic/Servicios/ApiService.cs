using Clinic.Modelos.ConstantesAPI;
using iText.Kernel.XMP.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Clinic.Modelos;
using Firebase.Auth.Providers;
using Firebase.Auth;
using Firebase.Storage;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using iText.Kernel.Pdf;
using iText.Layout.Element;
using iText.Layout;


namespace Clinic.Servicios
{
    public class ApiService : IApiService
    {
        readonly HttpClient client;
        readonly JsonSerializerOptions serializerOptions;

        public ApiService()
        {
            client = new HttpClient();
            serializerOptions = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true
            };
        }
        #region Usuarios
        public async Task<Usuarios> GetUsuario(string usuario)
        {

            try
            {

                Uri uri = new(string.Format($"{Constantes.API_BASE_ADDRESS}/api/usuarios/BuscarUsuarioporNombre?nombre={usuario}"));
                var response = await client.GetAsync(uri);
                var prueba = response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    Usuarios usuarioObtenido = JsonConvert.DeserializeObject<Usuarios>(jsonString);
                    if (usuarioObtenido != null)
                    {
                        await SecureStorage.SetAsync("UserEmail", usuarioObtenido.Email);
                        await SecureStorage.SetAsync("UserName", usuarioObtenido.Nombre);
                        await SecureStorage.SetAsync("UserPassword", usuarioObtenido.Contraseña);
                        await SecureStorage.SetAsync("UserPhone", usuarioObtenido.Telefono);


                    }
                    return JsonConvert.DeserializeObject<Usuarios>(jsonString);
                }
                else
                {
                    return new Usuarios();
                }
            }
            catch (Exception ex)
            {

                Shell.Current.DisplayAlert("Alerta", $"No se ha podido conectar con la base de datos:{ex}", "OK");
                return null;
            }
        }
        public async Task<IEnumerable<Usuarios>> GetAllUsers()
        {
            try
            {
                Uri uri = new(string.Format($"{Constantes.API_BASE_ADDRESS}api/usuarios/ListaUsuario"));
                var response = await client.GetAsync(uri);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Usuarios>>(jsonString);
                }
                else
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Usuarios>>(jsonString);
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }
        public async Task<Usuarios> UpdateUserPassword(string email, string newPassowrd)
        {
            try
            {
                string ActualPassword = JsonConvert.SerializeObject(newPassowrd);
                HttpContent content = new StringContent(ActualPassword, Encoding.UTF8, "application/json");

                Uri uri = new(string.Format($"{Constantes.API_BASE_ADDRESS}api/usuarios/ActualizarContraseñaUsuario?email={email}&NewPassword={newPassowrd}"));
                var response = await client.PutAsync(uri, content);
                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Usuarios>(jsonString);
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Alerta", $"Error al  actualizar la contraseña:{ex} ", "ok");
            }
            return null;
        }
        public async Task<Usuarios> CrearUsuario(Usuarios usuario)
        {
            try
            {
                string usuarioJson = JsonConvert.SerializeObject(usuario);
                HttpContent content = new StringContent(usuarioJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{Constantes.API_BASE_ADDRESS}api/usuarios/CrearUsuario", content);

                if (response.IsSuccessStatusCode)
                {
                    // Lee la respuesta JSON del servidor
                    string respuestaJson = await response.Content.ReadAsStringAsync();

                    // Deserializa la respuesta JSON en un objeto CitasMedicas
                    Usuarios UsuarioCreado = JsonConvert.DeserializeObject<Usuarios>(respuestaJson);

                    return UsuarioCreado;
                }
                else
                {
                    throw new Exception("Error al crear el usuario: " + response.ReasonPhrase);
                }



            }
            catch (Exception ex)
            {
                throw new Exception("Error Error al crear el usuario: " + ex.Message);
            }
        }
        //public async Task<Usuarios> UpdateUsuario(string codigo)
        //{

        //    throw new NotImplementedException();
        //}
        public async Task<bool> EliminarUsuario(string codigo)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {

                    HttpResponseMessage response = await client.DeleteAsync(new Uri($"{Constantes.API_BASE_ADDRESS}api/usuarios/DeleteUsuario?codigo={codigo}"));
                    if (response.IsSuccessStatusCode)
                    {
                        return true; // Eliminación del usuario exitosa
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la cita médica: " + ex.Message);
            }
        }
        #endregion


        #region citasMedicas
        public async Task<IEnumerable<CitasMedicas>> GetCitasMedicas()
        {

            try
            {
                Uri uri = new(string.Format($"{Constantes.API_BASE_ADDRESS}/api/CitaMedica/ListaCitasMedicas"));
                var response = await client.GetAsync(uri);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<CitasMedicas>>(jsonString);
                }
                else
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<CitasMedicas>>(jsonString);
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }

        public async Task<CitasMedicas> CreateCitaMedica(CitasMedicas nuevaCita)
        {
            var cita = new CitasMedicas
            {

                Paciente = nuevaCita.Paciente,
                Doctor = nuevaCita.Doctor,
                FechaCita = nuevaCita.FechaCita,
                HoraCita = nuevaCita.HoraCita,
                Motivos = nuevaCita.Motivos,
                Status = nuevaCita.Status
            };

            Uri uri = new Uri($"{Constantes.API_BASE_ADDRESS}api/CitaMedica/CrearCita");

            // Serializa el objeto cita a JSON
            var jsonCita = JsonConvert.SerializeObject(cita);
            var content = new StringContent(jsonCita, Encoding.UTF8, "application/json");

            // Realiza la solicitud POST
            var response = await client.PostAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                // La solicitud fue exitosa
                Console.WriteLine("Cita médica enviada exitosamente.");
                return nuevaCita;
            }
            else
            {
                // Manejar errores aquí si es necesario
                Console.WriteLine($"Error al enviar la cita médica. Código de estado: {response.StatusCode}");
                return null;
            }
        }
        public async Task<CitasMedicas> CrearCita(CitasMedicas nuevacita)
        {
            try
            {
                string citaJson = JsonConvert.SerializeObject(nuevacita);
                HttpContent content = new StringContent(citaJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{Constantes.API_BASE_ADDRESS}/api/CitaMedica/CrearCita", content);
                if (response.IsSuccessStatusCode)
                {
                    // Lee la respuesta JSON del servidor
                    string respuestaJson = await response.Content.ReadAsStringAsync();

                    // Deserializa la respuesta JSON en un objeto CitasMedicas
                    CitasMedicas citaCreada = JsonConvert.DeserializeObject<CitasMedicas>(respuestaJson);

                    return citaCreada;
                }

                else
                {
                    throw new Exception("Error al crear la cita médica: " + response.ReasonPhrase);
                }


            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la cita médica: " + ex.Message);

            }

        }
        public async Task<CitasMedicas> ActualizarCita(CitasMedicas cita)
        {
            try
            {
                string citaJson = JsonConvert.SerializeObject(cita);
                HttpContent content = new StringContent(citaJson, Encoding.UTF8, "application/json");
                var respuesta = await client.PutAsync($"{Constantes.API_BASE_ADDRESS}api/CitaMedica/ActualizarCita?paciente={cita.Paciente}", content);

                var prueba = respuesta.Content;
                if (respuesta.IsSuccessStatusCode)
                {
                    // Lee la respuesta JSON del servidor
                    string respuestaJson = await respuesta.Content.ReadAsStringAsync();
                    string contentType = respuesta.Content.Headers.ContentType?.ToString();



                    // Deserializa la respuesta JSON en un objeto CitasMedicas
                    CitasMedicas citaActualizada = JsonConvert.DeserializeObject<CitasMedicas>(respuestaJson);

                    return citaActualizada;
                }
                else
                {
                    throw new Exception("Error al actualizar la cita médica: " + respuesta.ReasonPhrase);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<CitasMedicas> EliminarCitas(CitasMedicas cita)
        {
            try
            {
                string citaJson = JsonConvert.SerializeObject(cita);
                HttpContent content = new StringContent(citaJson, Encoding.UTF8, "application/json");
                var respuesta = await client.DeleteAsync($"{Constantes.API_BASE_ADDRESS}api/CitaMedica/ActualizarCita?paciente={cita.Paciente}");

                var prueba = respuesta.Content;
                if (respuesta.IsSuccessStatusCode)
                {
                    // Lee la respuesta JSON del servidor
                    string respuestaJson = await respuesta.Content.ReadAsStringAsync();
                    string contentType = respuesta.Content.Headers.ContentType?.ToString();



                    // Deserializa la respuesta JSON en un objeto CitasMedicas
                    CitasMedicas CitaEliminada = JsonConvert.DeserializeObject<CitasMedicas>(respuestaJson);

                    return CitaEliminada;
                }
                else
                {
                    throw new Exception("Error al actualizar la cita médica: " + respuesta.ReasonPhrase);
                }


            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<bool> ActualizarCitaMedica(string paciente)
        {
            Uri uri = new Uri($"{Constantes.API_BASE_ADDRESS}api/CitaMedica/ActualizarCita?paciente={paciente}");

            // Serializa el objeto citaActualizada a JSON
            var jsonCita = JsonConvert.SerializeObject(paciente);
            var content = new StringContent(jsonCita, Encoding.UTF8, "application/json");

            // Realiza la solicitud PUT
            var response = await client.PutAsync(uri, content);

            if (response.IsSuccessStatusCode)
            {
                // La solicitud fue exitosa
                Console.WriteLine("Cita médica actualizada exitosamente.");
                return true;
            }
            else
            {
                // Manejar errores aquí si es necesario
                Console.WriteLine($"Error al actualizar la cita médica. Código de estado: {response.StatusCode}");
                return false;
            }
        }
        #endregion

        #region Doctores
        public async Task<IEnumerable<Doctores>> GetDoctores()
        {
            try
            {

                Uri uri = new(string.Format($"{Constantes.API_BASE_ADDRESS}/api/Doctores/ListaDoctores"));
                var response = await client.GetAsync(uri);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Doctores>>(jsonString);
                }
                else
                {
                    Shell.Current.DisplayAlert("Error al obtener la lista de doctores: ", "", "ok");

                }
                return null;

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }
        #endregion
        #region Empleados
        public async Task<Empleados> GetEmpleadobyCode(string codigo)
        {
            try
            {

                Uri uri = new(string.Format($"{Constantes.API_BASE_ADDRESS}api/Empleados/BuscarEmpleadoporCodigo?codigo={codigo}", string.Empty));
                var response = await client.GetAsync(uri);
                var prueba = response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Empleados>(jsonString);
                }
                else
                {
                    return new Empleados();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }
        public async Task<Empleados> GetEmpleadobyName(string nombre)
        {
            try
            {

                Uri uri = new(string.Format($"{Constantes.API_BASE_ADDRESS}/api/Empleados/BuscarEmpleadoporNombre?nombre={nombre}", string.Empty));
                var response = await client.GetAsync(uri);
                var prueba = response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<Empleados>(jsonString);
                }
                else
                {
                    return new Empleados();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }
        }
        public async Task<Empleados> CrearEmpleado(Empleados nuevoEmpleado)
        {
            try
            {
                string nuevoEmpleadoJson = JsonConvert.SerializeObject(nuevoEmpleado);
                HttpContent content = new StringContent(nuevoEmpleadoJson, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await client.PostAsync($"{Constantes.API_BASE_ADDRESS}/api/Empleados/CrearEmpleado", content);
                if (response.IsSuccessStatusCode)
                {
                    string respuestaEmpleado = await response.Content.ReadAsStringAsync();
                    Empleados empleadoCreado = JsonConvert.DeserializeObject<Empleados>(respuestaEmpleado);
                    return empleadoCreado;
                }
                else
                {
                    throw new Exception("Su solicitud no ha sido procesada correctamente: " + response.ReasonPhrase);

                }
            }
            catch
            {
                throw new Exception("Error al creaer el empleado ");

            }
        }
        public async Task<IEnumerable<Empleados>> GetListaEmpleados()
        {
            try
            {


                Uri uri = new(string.Format($"{Constantes.API_BASE_ADDRESS}api/Empleados/ListaEmpleados"));
                var response = await client.GetAsync(uri);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var jsonString = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Empleados>>(jsonString);
                }
                else
                {
                    throw new Exception("Error al obtener la lista de doctores: " + response.ReasonPhrase);

                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);

            }

        }
        #endregion
        #region Firebase
        public async Task<string> ObtenerTokenFirebaseAsync()
        {
            var client = new FirebaseAuthClient(new FirebaseAuthConfig()
            {
                ApiKey = Constantes.apiKey,
                AuthDomain = Constantes.authDomain,
                Providers = new FirebaseAuthProvider[]
                {
                    new EmailProvider()
                }
            });
            var credenciales = await client.SignInWithEmailAndPasswordAsync(Constantes.email, Constantes.passWord);
            Constantes.token = await credenciales.User.GetIdTokenAsync();

            return Constantes.token;

        }
        public async Task< string> SubirPdfAFirebaseAsync(FileResult pdf)
        {
            string rutaStorage = Constantes.rutaStorage; // Obtener la ruta de almacenamiento de Firebase
            string token = await ObtenerTokenFirebaseAsync(); // Obtener el token de autenticación

            try
            {
                var historiaGuardada = new FirebaseStorage(rutaStorage, new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(token),
                    ThrowOnCancel = true
                })
                .Child("Historia_Clinica")
                .Child(pdf.FileName)
                .PutAsync(await pdf.OpenReadAsync());

                return await historiaGuardada;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Ha ocurrido un error al subir la historia clínica: " + ex.Message, "OK");
                return null;
            }
        }
        public string GenerateToken()
        {
            // Genera un token criptográficamente seguro
            using (var rng = new RNGCryptoServiceProvider())
            {
                var tokenData = new byte[32];
                rng.GetBytes(tokenData);
                return Convert.ToBase64String(tokenData);
            }
        }
        #endregion

        #region ResetPassword
        public void SendPasswordResetEmail(string email, string token)
        {
            var smtpClient = new SmtpClient() // Reemplaza con tu SMTP host
            {
                Host = "localhost",
                Port = 25,
                Credentials = new NetworkCredential("username@example.com", "password"), // Reemplaza con tus credenciales
                EnableSsl = false,
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress("noreply@example.com"),
                Subject = "Restablecimiento de contraseña",
                Body = $"Tu código para restablecer la contraseña es: {token}",
                IsBodyHtml = false,
            };

            mailMessage.To.Add(email);

            smtpClient.Send(mailMessage);
        }

        // Método para guardar el token en SecureStorage
        public async Task SaveTokenToSecureStorage(string token)
        {
            await SecureStorage.SetAsync("password_reset_token", token);
        }

        // Método para verificar el token desde SecureStorage
        public async Task<bool> VerifyToken(string userInputToken)
        {
            var savedToken = await SecureStorage.GetAsync("password_reset_token");
            return savedToken == userInputToken;
        }
        #endregion
        #region PDF
        public async Task<FileResult> GenerarPdf(string rutaStorage,CitasMedicas cita)
        {
            var pdfStream = new MemoryStream();
            var writer = new PdfWriter(pdfStream);
            var pdf = new PdfDocument(writer);
            var document = new Document(pdf);

            document.Add(new Paragraph("Cita Médica"));
            document.Add(new Paragraph($"Paciente: {cita.Paciente}"));
            document.Add(new Paragraph($"Doctor: {cita.Doctor}"));
            document.Add(new Paragraph($"Fecha: {cita.FechaCita}"));
            document.Add(new Paragraph($"Hora: {cita.HoraCita}"));
            // Agregar más información si es necesario

            document.Close();

            pdfStream.Position = 0;

            // Guardar el PDF en el almacenamiento local temporal
            var fileName = "cita_medica.pdf";
            var filePath = Path.Combine(rutaStorage, fileName);
            using (var fileStream = File.Create(filePath))
            {
                pdfStream.CopyTo(fileStream);
            }

            // Devolver el resultado del archivo
            return new FileResult(filePath);
        }

        public async Task<bool> GuardarFotosUsuario(string codigo , byte[] foto)
        {
            //try
            //{
            //    var formData = new MultipartFormDataContent();
            //    formData.Add(new StringContent(codigo), "codigo"); // Añade el código como texto
            //    formData.Add(new ByteArrayContent(foto), "archivo", "foto.jpg"); // Añade la foto como archivo

            //    var NuevaFoto = new Fotos
            //    {
            //        codigo=codigo,
            //        Foto=foto,
            //    };

            //    Uri uri = new Uri($"{Constantes.API_BASE_ADDRESS}/api/Fotos/GuardarFoto");

            //    // Serializa el objeto cita a JSON
            //    var jsonCita = JsonConvert.SerializeObject(NuevaFoto);
            //    var content = new StringContent(jsonCita, Encoding.UTF8, "application/json");

            //    // Realiza la solicitud POST
            //    var response = await client.PostAsync(uri, formData);

            //    if (response.IsSuccessStatusCode)
            //    {
            //        // Si la solicitud fue exitosa, devuelve true
            //        return true;
            //    }
            //    else
            //    {
            //        // Si la solicitud no fue exitosa, lanza una excepción con el mensaje de error
            //        string errorMessage = await response.Content.ReadAsStringAsync();
            //        return false;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Error al crear la cita médica: " + ex.Message);

            //}
            try
            {
                // Crear el contenido del formulario multipart
                var formData = new MultipartFormDataContent();
                formData.Add(new StringContent(codigo), "codigo"); // Añade el código como texto
                formData.Add(new ByteArrayContent(foto), "archivo", "foto.jpg"); // Añade la foto como archivo

                // Realiza la solicitud POST
                var response = await client.PostAsync($"{Constantes.API_BASE_ADDRESS}/api/Fotos/GuardarFoto", formData);

                if (response.IsSuccessStatusCode)
                {
                    // Si la solicitud fue exitosa, devuelve true
                    return true;
                }
                else
                {
                    // Si la solicitud no fue exitosa, lanza una excepción con el mensaje de error
                    string errorMessage = await response.Content.ReadAsStringAsync();
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la cita médica: " + ex.Message);
            }

        }
        #endregion

        public class httpServices
        {
            readonly HttpClient client;
            readonly JsonSerializerOptions serializerOptions;

            public httpServices()
            {
                client = new HttpClient();
                serializerOptions = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                    WriteIndented = true
                };
            }
            

            // Método para cambiar la contraseña (este método debe implementarse de acuerdo a tu lógica de negocio)
          


        }
    }
}
