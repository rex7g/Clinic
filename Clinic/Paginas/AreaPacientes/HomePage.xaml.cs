using Clinic.Paginas.AreaPacientes.Ajustes;
using Clinic.Paginas.AreaPacientes.CitasMedicasPages;
using Clinic.Servicios;

namespace Clinic.Paginas.AreaPacientes;

public partial class HomePage : ContentPage
{
    public IApiService _ApiService { get; set; }

	public HomePage(IApiService service)
	{
		InitializeComponent();
        _ApiService = service;
	}

    private async void BtnCitas_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AgendarCitaPage(_ApiService));
    }

    private async void ListaCitas_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new ListaCitasPage(_ApiService));
    }

    private async void BtnDoctor_Clicked(object sender, EventArgs e)
    {
       
    }

    private async void BtnAjustes_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AjustesPage());
    }

    private void BtnHistoria_Clicked(object sender, EventArgs e)
    {

    }

    private void BtnSalir_Clicked(object sender, EventArgs e)
    {

    }
}