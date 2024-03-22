using Clinic.Paginas.AreaPacientes;
using Clinic.Servicios;

namespace Clinic.Paginas.Login;

public partial class LoginPage : ContentPage
{
    private readonly IApiService _apiService;
	public LoginPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
	}

    private async void BtnInicio_Clicked(object sender, EventArgs e)
    {
        var usuario = user.Text.Trim();
        var Password = password.Text.Trim();
        var ValidaUsuario = await _apiService.GetUsuario(usuario);
        var email= ValidaUsuario.Email.Trim();
        var telefono=ValidaUsuario.Telefono.Trim();
        var fechaNacimiento = ValidaUsuario.FechaNacimiento.ToString();
        var Userpassword=ValidaUsuario.Contrase�a.Trim();


        await SecureStorage.SetAsync("NombreUsuario", usuario);
        await SecureStorage.SetAsync("rol", ValidaUsuario.Codigo);
        await SecureStorage.SetAsync("telefono", telefono);
        await SecureStorage.SetAsync("fechaNacimiento", fechaNacimiento);
        await SecureStorage.SetAsync("Userpassword", Userpassword);
        await SecureStorage.SetAsync("Email", email);


        if (ValidaUsuario!= null)
        {
            await Navigation.PushModalAsync(new HomePage(_apiService));
        }


        //if (ValidaUsuario != null && ValidaUsuario.Nombre == usuario && !ValidaUsuario.Codigo.Contains("DOC") && !ValidaUsuario.Codigo.Contains("CL"))
        //{
        //    if (ValidaUsuario.Contrase�a == Password)
        //    {
        //        await Navigation.PushModalAsync(new HomePage(_apiService));
        //    }
        //}


    }

    private void ForgetPassword_Clicked(object sender, EventArgs e)
    {

    }

    private void Register_Clicked(object sender, EventArgs e)
    {

    }
}