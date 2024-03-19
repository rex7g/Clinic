using Clinic.Paginas.AreaDoctores.Login;

namespace Clinic.Paginas.AreaDoctores;

public partial class LoginDoctoresPage : ContentPage
{
	public LoginDoctoresPage()
	{
		InitializeComponent();
	}

    private void BtnInicioMedico_Clicked(object sender, EventArgs e)
    {

    }

    private async void RegistroMedico_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new RegistroDoctoresPage());
    }

    private async void ResetPasswordMedico_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ResetContraseñaDoctores());
    }
}