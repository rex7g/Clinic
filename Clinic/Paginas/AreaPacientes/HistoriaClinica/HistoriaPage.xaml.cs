using Clinic.Modelos;
using Clinic.Modelos.ConstantesAPI;
using Firebase.Storage;
using iText.Barcodes.Qrcode;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser.ClipperLib;
using iText.Layout;
using iText.Layout.Element;
using Microsoft.Maui.ApplicationModel.Communication;
using MimeKit.Encodings;
using Newtonsoft.Json;
using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using ZXing;
using ZXing.Mobile;
using ZXing.QrCode;

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
            throw ex;
        }
    }

    private async void BtnGuardarHistoria_Clicked(object sender, EventArgs e)
    {
        string diagnostico = DiagnosticoAnteriorEntry.Text;
        string religion = ReligionEntry.Text;
        string ocupacion = OcupacionEntry.Text;
        string lugarResidencia = LugarResidenciaEntry.Text;
        string padecimientoActual = TextAreaPadecimiento.Text;
        string fechaIngreso = FechaIngresoEntry.Date.ToString("dd/MM/yyyy");

        MemoryStream pdfStream = await GeneratePdf(diagnostico, religion, ocupacion, lugarResidencia, padecimientoActual, fechaIngreso);

    }
    private async Task<MemoryStream> GeneratePdf(string diagnostico, string religion, string ocupacion, string lugarResidencia, string padecimientoActual, string fechaIngreso)
    {
        MemoryStream pdfStream = new MemoryStream();
        PdfWriter writer = new PdfWriter(pdfStream);
        PdfDocument pdf = new PdfDocument(writer);
        Document document = new Document(pdf);

        // Agregar contenido al PDF
        document.Add(new Paragraph("Historia Clínica"));
        document.Add(new Paragraph($"Diagnóstico: {diagnostico}"));
        document.Add(new Paragraph($"Religión: {religion}"));
        document.Add(new Paragraph($"Ocupación: {ocupacion}"));
        document.Add(new Paragraph($"Lugar de Residencia: {lugarResidencia}"));
        document.Add(new Paragraph($"Padecimiento Actual: {padecimientoActual}"));
        document.Add(new Paragraph($"Fecha de Ingreso: {fechaIngreso}"));

        // Cerrar el documento
        document.Close();

        // Volver al principio del stream
        pdfStream.Position = 0;
        if (pdfStream != null)
        {
            try
            {
                string fileName = "historia_clinica.pdf";
                var storage = new FirebaseStorage(Constantes.rutaStorage, new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(Constantes.token),
                    ThrowOnCancel = true
                });
                var task = storage
                    .Child("Historias_Clinicas")
                    .Child(fileName)
                    .PutAsync(pdfStream);
                var url = await task;

                // Generar el QR con la URL de descarga
                 //Bitmap qrCodeBitmap = await GenerateQRCodeAsync(url);

                // Aquí debes mostrar o guardar el qrCodeBitmap según tus necesidades

                // Mostrar un mensaje de éxito
                await Shell.Current.DisplayAlert("Éxito", "La historia clínica se ha guardado correctamente", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"No se pudo guardar la historia clínica: {ex.Message}", "OK");
            }
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No se pudo generar el PDF de la historia clínica", "OK");
        }
        return pdfStream;
    }

    //private void Task<Bitmap> GenerateQRCodeAsync(string url)
    //{
    //    QRCodeGenerator qrGenerator = new QRCodeGenerator();
    //    QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.L);
    //    PngByteQRCode qRCode = new PngByteQRCode(qrCodeData);
    //    byte[] qrCodeBytes = qRCode.GetGraphic(20);
    //    hcimagen.Source = ImageSource.FromStream(() => new MemoryStream(qrCodeBytes));
    //}

    private async void Salir_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}