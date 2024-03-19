namespace Clinic.Paginas.AreaDoctores.Login;

public partial class RegistroDoctoresPage : ContentPage
{
	public RegistroDoctoresPage()
	{
		InitializeComponent();
	}

    private void Register_Clicked(object sender, EventArgs e)
    {

    }

    private async void Salir_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}