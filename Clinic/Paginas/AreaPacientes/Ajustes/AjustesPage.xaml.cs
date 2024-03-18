namespace Clinic.Paginas.AreaPacientes.Ajustes;

public partial class AjustesPage : ContentPage
{
	public AjustesPage()
	{
		InitializeComponent();
	}

    private async void Cuenta_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new CuentaPage());
    }

    private async void Privacidad_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new PoliticasPrivacidadPage());
    }

    private void Ayuda_Clicked(object sender, EventArgs e)
    {
        var telefono = SecureStorage.GetAsync("TelefonoClinica").ToString();
        var Mensaje = SecureStorage.GetAsync("mensajeInicial").ToString();
        EnviarWhatsapp(telefono, Mensaje);
    }
    public async void EnviarWhatsapp(string numero, string mensaje)
    {
        bool mensajeEnviado = await Launcher.CanOpenAsync($"whatsapp://send?phone=+{numero}&text={mensaje}");

        if (mensajeEnviado == true)
        {
            // await Launcher.OpenAsync($"whatsapp://send?phone=+{numero}&text={mensaje}");
            await Launcher.OpenAsync($"whatsapp://send?phone=+{numero}");
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "No se ha podido contactar con soporte", $"El numero de contacto es:{numero.ToString()}");
        }


    }
    private async void About_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AcercaDePage());
    }

    private async void Salir_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}