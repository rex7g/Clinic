using Clinic.Paginas.Login;
using Clinic.Servicios;
using ZXing;

namespace Clinic.Paginas.AreaPacientes.Ajustes;

public partial class CuentaPage : ContentPage
{
    public IApiService ApiService { get; set; }
	public CuentaPage(IApiService service)
	{
		InitializeComponent();
        ApiService = service;
        LoadUserData();

    }

    private async void Regresar_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.DisplayAlert("Aviso","Los cambios que ha realizados no han sido guardados desea guardar?","Si","No");
        await Navigation.PopModalAsync();
    }

    private void ActualizarDatosCuenta_Clicked(object sender, EventArgs e)
    {

    }

    private async void EliminarDatosCuenta_Clicked(object sender, EventArgs e)
    {
        var emailCuenta = await SecureStorage.GetAsync("Email");
        bool cuentaEliminada = await ApiService.EliminarUsuario(emailCuenta);

        if(cuentaEliminada ==true)
        {

            await Navigation.PushModalAsync(new LoginPage(ApiService));
        }

    }

    private async void UserFoto_Clicked(object sender, EventArgs e)
    {
        var foto_usuario = await FilePicker.PickAsync(new PickOptions
        {
            PickerTitle = "Actualizar Foto",
            FileTypes = FilePickerFileType.Images
        });

        if (foto_usuario == null) return;
        var stream = await foto_usuario.OpenReadAsync();
        var codigoUsuario = await SecureStorage.GetAsync("NombreUsuario");
        UserFoto.Source = ImageSource.FromStream(() => stream);
        var userfoto= ImageSource.FromStream(() => stream);

        var imagen = await ConvertirImagenABytesAsync(userfoto);

        var GuardarFoto = await ApiService.GuardarFotosUsuario(codigoUsuario, imagen);
        
        
        // Llamar al método para guardar la foto en el servidor
       // var FotoGuardada = await ApiService.GuardarFotosUsuario(codigoUsuario, fotoBytes);

    }

    public async Task<byte[]> ConvertirImagenABytesAsync(ImageSource imagen)
    {
        try
        {
            // Convierte la imagen a un flujo de memoria
            MemoryStream memoryStream = new MemoryStream();
            var stream = await ((StreamImageSource)imagen).Stream(CancellationToken.None);
            await stream.CopyToAsync(memoryStream);

            // Convierte el flujo de memoria a un arreglo de bytes
            byte[] bytes = memoryStream.ToArray();

            return bytes;
        }
        catch (Exception ex)
        {
            // Manejar cualquier error que ocurra durante la conversión
            Console.WriteLine("Error al convertir la imagen a bytes: " + ex.Message);
            return null;
        }
    }




    private async void LoadUserData()
    {
        try
        {
            EmailEntry.Text = await SecureStorage.GetAsync("Email");
            PasswordEntry.Text = await SecureStorage.GetAsync("Userpassword");
            TelefonoEntry.Text = await SecureStorage.GetAsync("telefono");
            usuarioEnrty.Text = await SecureStorage.GetAsync("NombreUsuario");


        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

}