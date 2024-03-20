namespace Clinic.Paginas.AreaPacientes.Ajustes;

public partial class PoliticasPrivacidadPage : ContentPage
{
	public PoliticasPrivacidadPage()
	{
		InitializeComponent();
	}

    private async void Regresar_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }
}