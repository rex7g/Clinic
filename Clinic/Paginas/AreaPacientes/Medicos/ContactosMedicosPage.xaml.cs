using Clinic.Modelos;
using Clinic.Servicios;

namespace Clinic.Paginas.AreaPacientes.Medicos;

public partial class ContactosMedicosPage : ContentPage
{
    public IApiService ApiService { get; set; }
	public ContactosMedicosPage(IApiService api)
	{
		InitializeComponent();
        ApiService= api;
        LoadDoctores();

    }

    private async void Salir_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void DoctorListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedDoctor = e.SelectedItem as Doctores;
        if (selectedDoctor != null)
        {
            await Navigation.PushModalAsync(new DetalleContactosPage(ApiService,selectedDoctor));
            ((ListView)sender).SelectedItem = null;


        }
    }


    private async void LoadDoctores()
    {
        try
        {
            CargandoDoctores.IsVisible = true; // Mostrar el ActivityIndicator
            CargandoDoctores.IsRunning = true;

            //ActivityIndicator activityIndicator = new ActivityIndicator { IsVisible = true, IsRunning = true };

            var Doctores = await ApiService.GetDoctores();
            string usuario = await SecureStorage.GetAsync("UserName");

            if (Doctores.Any())
            {
                DoctorListView.ItemsSource = Doctores;
            }
            else
            {
                await DisplayAlert("Alerta", $"No hay doctores disponibles en esta fecha: {DateTime.Now.Date}", "OK");
            }
            //activityIndicator.IsVisible = false;

        }
        catch (Exception)
        {
            await DisplayAlert("Error", "No se pudieron cargar los doctores.", "OK");
            await Navigation.PushModalAsync(new HomePage(ApiService));
        }
        finally
        {
            CargandoDoctores.IsVisible = false; // Mostrar el ActivityIndicator
            CargandoDoctores.IsRunning = false;
        }
    }
}