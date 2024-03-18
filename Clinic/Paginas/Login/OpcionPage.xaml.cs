using Clinic.Paginas.AreaDoctores;
using Clinic.Servicios;

namespace Clinic.Paginas.Login;

public partial class OpcionPage : ContentPage
{
    private readonly IApiService _apiService;

    public OpcionPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
    }

    private async void BtnPaciente_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new LoginPage(_apiService));
    }

    private async void BtnMedico_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushModalAsync(new LoginDoctoresPage());
    }
}