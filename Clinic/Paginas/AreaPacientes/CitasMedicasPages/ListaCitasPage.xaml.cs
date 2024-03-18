using Clinic.Modelos;
using Clinic.Servicios;
using CommunityToolkit.Maui.Views;
using Org.BouncyCastle.Utilities.Collections;
using System.Collections;
using static Clinic.Servicios.ApiService;

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

    private async void BuscarCita_SearchButtonPressed(object sender, EventArgs e)
    {
        var NombrePaciente = SecureStorage.GetAsync("NombreUsuario");
        string searchText = BuscarCita.Text;
       
        var listaCitasMedicas = await ApiService.GetCitasMedicas();
        if (listaCitasMedicas == null) return;

        var citasFiltradas = listaCitasMedicas.Where(a => a.Doctor.Contains(searchText, StringComparison.OrdinalIgnoreCase)
        && a.Paciente== NombrePaciente.ToString())
            
            
            .ToList();
       // && a.Paciente == NombrePaciente).ToList();
        citasListview.ItemsSource= citasFiltradas;

        if (citasListview.ItemsSource == null || ((IEnumerable)citasListview.ItemsSource).Cast<object>().Count() == 0)
        {
            citasListview.ItemsSource = listaCitasMedicas;
        }

    }

    private async void citasListview_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var selectedCita = e.SelectedItem as CitasMedicas;

        if (selectedCita != null)
        {
            await SecureStorage.SetAsync("pacienteSeleccionado", selectedCita.Paciente);
            await SecureStorage.SetAsync("doctorSeleccionado", selectedCita.Doctor);
            await SecureStorage.SetAsync("fechaSeleccionada", selectedCita.FechaCita.ToString());
            await SecureStorage.SetAsync("comentarioCita", selectedCita.Motivos);
            // Crear el popup y pasar los datos necesarios
            var popup = new ActualizarPopup(selectedCita, ApiService);
            await this.ShowPopupAsync(popup);


            ((ListView)sender).SelectedItem = null;
            // Mostrar el popup
        }

    }
    private async void LoadCitasPendientes()
    {
        try
        {
            CargandoCitas.IsVisible = true; // Mostrar el ActivityIndicator
            CargandoCitas.IsRunning = true;
           
            var citasPendientes = await ApiService.GetCitasMedicas();
            string usuario = await SecureStorage.GetAsync("NombreUsuario");
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
            await Navigation.PushModalAsync(new HomePage(ApiService));
        }
        finally
        {
            CargandoCitas.IsVisible = false;
            CargandoCitas.IsRunning = false;
        }
    }


}