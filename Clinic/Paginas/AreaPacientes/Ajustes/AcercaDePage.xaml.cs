namespace Clinic.Paginas.AreaPacientes.Ajustes;

public partial class AcercaDePage : ContentPage
{
	public AcercaDePage()
	{
		InitializeComponent();
	}

    private async void Regresar_Clicked(object sender, EventArgs e)
    {
		await Navigation.PopModalAsync();
    }
}