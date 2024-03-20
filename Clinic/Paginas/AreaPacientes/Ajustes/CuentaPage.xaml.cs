using Clinic.Servicios;

namespace Clinic.Paginas.AreaPacientes.Ajustes;

public partial class CuentaPage : ContentPage
{
    public IApiService ApiService { get; set; }
	public CuentaPage(IApiService service)
	{
		InitializeComponent();
        ApiService = service;
	}

    private async void Regresar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void ActualizarDatosCuenta_Clicked(object sender, EventArgs e)
    {

    }

    private void EliminarDatosCuenta_Clicked(object sender, EventArgs e)
    {

    }

    private async void UserFoto_Clicked(object sender, EventArgs e)
    {
        var foto_usuario = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Sube la foto porfavor",
            FileTypes = FilePickerFileType.Images
        });

        if (foto_usuario == null) return;

        // Convertir la imagen en un arreglo de bytes en base64
        byte[] fotoBytes;
        using (var memoryStream = new MemoryStream())
        using (var stream = await foto_usuario.OpenReadAsync())
        {
            await stream.CopyToAsync(memoryStream);
            fotoBytes = memoryStream.ToArray();
        }

        // Convertir la imagen en un arreglo de bytes en base64
        string fotoBase64 = Convert.ToBase64String(fotoBytes);
         

        // Llamar al método para guardar la foto en el servidor
        var codigoUsuario = usuarioEnrty.Text;
        var FotoGuardada = await ApiService.GuardarFotosUsuario(codigoUsuario, fotoBytes);

    }

}