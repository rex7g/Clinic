using Clinic.Servicios;

namespace Clinic.Paginas.AreaPacientes.CitasMedicasPages;

public partial class ListaCitasPage : ContentPage
{
    public IApiService ApiService;
	public ListaCitasPage(IApiService service)
	{
		InitializeComponent();
        ApiService = service;
        LoadCitasPendientes();

    }

    private async void Salir_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private void BuscarCita_SearchButtonPressed(object sender, EventArgs e)
    {

    }

    private void citasListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {

    }
    private async void LoadCitasPendientes()
    {
        try
        {
            CargandoCitas.IsVisible = true; // Mostrar el ActivityIndicator
            CargandoCitas.IsRunning = true;

            var citasPendientes = await ApiService.GetCitasMedicas();
            string usuario = await SecureStorage.GetAsync("UserName");
            //Filtra las citas por el estado pendiente y por el nombre del paciente 
            var citasDelUsuario = citasPendientes.Where(c => c.Status == "Pendiente" && c.Paciente == usuario).ToList();



            if (citasDelUsuario.Any())
            {
                citasListview.ItemsSource = citasDelUsuario;
            }
            else
            {
                await DisplayAlert("Alerta", "No tiene citas pendientes", "OK");
            }

        }
        catch (Exception)
        {
            await DisplayAlert("Error", "No se pudieron cargar las citas pendientes.", "OK");
            await Navigation.PushModalAsync(new HomePage());
        }
        finally
        {
            CargandoCitas.IsVisible = false;
            CargandoCitas.IsRunning = false;
        }
    }


}