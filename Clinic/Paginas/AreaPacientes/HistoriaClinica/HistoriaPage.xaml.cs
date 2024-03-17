using Clinic.Modelos;
using Clinic.Modelos.ConstantesAPI;
using Firebase.Storage;
using MimeKit.Encodings;
using Newtonsoft.Json;

namespace Clinic.Paginas.AreaPacientes.HistoriaClinica;

public partial class HistoriaPage : ContentPage
{
	public HistoriaPage()
	{
		InitializeComponent();
	}

    private async void SubirHistoria_Clicked(object sender, EventArgs e)
    {
        var Historia = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Sube la historia clinica por favor",
            FileTypes = FilePickerFileType.Pdf
        });

        if (Historia == null) return;
        try
        {
            bool resultado = await Shell.Current.DisplayAlert("Aviso", "Se guardara su historia clinica esta de acuerdo?", "SI", "NO");

            //Guarda el pdf en firebase storage
            var HIstoriaGuardada = new FirebaseStorage(Constantes.rutaStorage, new FirebaseStorageOptions
            {
                AuthTokenAsyncFactory = () => Task.FromResult(Constantes.token),
                ThrowOnCancel = true
            })
               .Child("Historia_Clinica")
               .Child(Historia.FileName)
               .PutAsync(await Historia.OpenReadAsync());
            //genera el link de descarga para 
            var urlDescarga = await HIstoriaGuardada;
            string nombreUsuario = await SecureStorage.GetAsync("UserName");
            string email = await SecureStorage.GetAsync("UserEmail");

            if (resultado)
            {
                var codigoHistoria = await SecureStorage.GetAsync("UserPhone");
                var historiaClinica = new HistoriasClinicas
                {
                    NombreUsuario = nombreUsuario,
                    Telefono = codigoHistoria,
                    Email = email,
                    Foto = urlDescarga.ToString(),
                    Diagnostico = DiagnosticoAnteriorEntry.Text,
                    Religion = ReligionEntry.Text,
                    Ocupacion = OcupacionEntry.Text,
                    LugarR = LugarResidenciaEntry.Text,
                    PadecimientoActual = TextAreaPadecimiento.Text,
                    FechaIngreso = FechaIngresoEntry.Date.ToString(),

                };
                string jsonHistoriaClinica = JsonConvert.SerializeObject(historiaClinica);
                //var qrGenerator = new QEncoder.QRCodeGenerator();
                //var qrCodeData = qrGenerator.CreateQrCode(jsonHistoriaClinica, QRCoder.QRCodeGenerator.ECCLevel.Q);
                //var qrCode = new PngByteQRCode(qrCodeData);
                //byte[] qrCodeBytes = qrCode.GetGraphic(20);
                //ImageSource qrImageSource = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
                //await Shell.Current.DisplayAlert("Aviso", "Se ha guardado la historia correctamente", "OK");

                //hcimagen.Source = qrImageSource;
            }

        
        }
        catch (Exception ex)
        {

        }
    }

    private void BtnGuardarHistoria_Clicked(object sender, EventArgs e)
    {

    }

    private void Salir_Clicked(object sender, EventArgs e)
    {

    }
}