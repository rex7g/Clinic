using Clinic.Servicios;

namespace Clinic.Paginas.Login;

public partial class RecordarContraseñaPage : ContentPage
{
    public IApiService ApiService { get; set; }
	public RecordarContraseñaPage(IApiService service)
	{

		InitializeComponent();
        ApiService = service;
	}

    private async void Regresar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private void EnviarEmail_Clicked(object sender, EventArgs e)
    {
        var email = emailEntry.Text;
        var token = ApiService.GenerateToken();
        ApiService.SendPasswordResetEmail(email, token);
        ApiService.SaveTokenToSecureStorage(token);
        TokenEntry.IsVisible = true;
        NewPasswordEntry.IsVisible = true;
        (sender as Button).IsVisible = false;
        RestablecerCrontraseña.IsVisible = true;
    }

    private async void RestablecerCrontraseña_Clicked(object sender, EventArgs e)
    {
        var userInputToken = TokenEntry.Text;
        var email = emailEntry.Text.Trim();
        if (await ApiService.VerifyToken(userInputToken))
        {
            var newPassword = NewPasswordEntry.Text.Trim();
            await ApiService.UpdateUserPassword(email, newPassword);

            await Shell.Current.DisplayAlert("Contrase�a restablecida correctamente", "Su Contrase�a ha sido restablecida", "ok");
            await Navigation.PushModalAsync(new LoginPage(ApiService));
        }
        else
        {
            await Shell.Current.DisplayAlert("Error", "El token ingresado esta incorrecto", "ok");
        }

    }
}