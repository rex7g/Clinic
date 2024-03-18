namespace Clinic.Paginas.AreaPacientes.Ajustes;

public partial class CuentaPage : ContentPage
{
	public CuentaPage()
	{
		InitializeComponent();
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

    private void UserFoto_Clicked(object sender, EventArgs e)
    {

    }
}