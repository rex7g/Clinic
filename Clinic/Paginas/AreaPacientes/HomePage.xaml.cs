using Clinic.Paginas.AreaPacientes.Ajustes;
using Clinic.Paginas.AreaPacientes.CitasMedicasPages;
using Clinic.Paginas.AreaPacientes.HistoriaClinica;
using Clinic.Paginas.AreaPacientes.Medicos;
using Clinic.Servicios;

namespace Clinic.Paginas.AreaPacientes;

public partial class HomePage : ContentPage
{
    public readonly IApiService _ApiService;
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
       await Navigation.PushModalAsync(new ContactosMedicosPage(_ApiService));
    }

    private async void BtnAjustes_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new AjustesPage());
    }

    private async void BtnHistoria_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new HistoriaPage());
    }

    private async void BtnSalir_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
}