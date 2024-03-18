using Clinic.Modelos;
using Clinic.Servicios;
using CommunityToolkit.Maui.Views;

namespace Clinic.Paginas.AreaPacientes.CitasMedicasPages;

public partial class ActualizarPopup : Popup
{ 
    public readonly IApiService ApiService;
    public DateTime FechaCitapopup { get; set; }
    public string NombrePacientePopup { get; set; }
    public string DoctorPopup { get; set; }
    public string MotivoPopup { get; set; }
    public ActualizarPopup(CitasMedicas citas,IApiService service)
	{
		InitializeComponent();
        BindingContext = citas;

        FechaCitapopup = citas.FechaCita;
        NombrePacientePopup = citas.Paciente;
        DoctorPopup = citas.Doctor;
        MotivoPopup = citas.Motivos;
        ApiService = service;
        LoadCitaSeleccionadaData();
    }

    private async void CancelarCita_Clicked(object sender, EventArgs e)
    {
        var cita = new CitasMedicas
        {
            FechaCita = FechaCitaEntry.Date,
            Doctor = DoctorEntry.Text,
            Paciente = NombrePacienteEntry.Text,
            Status = "Cancelada",
            Motivos = MotivoEntry.Text,

        };
        var citaActualizada = await ApiService.ActualizarCita(cita);

        if (citaActualizada != null)
        {
            await Shell.Current.DisplayAlert("", "La cita ha sido Cancelada", "Ok");

            this.Close();

        }
    }

    private async void ActualizarCita_Clicked(object sender, EventArgs e)
    {
        if (string.IsNullOrEmpty(NombrePacienteEntry.Text) || string.IsNullOrEmpty(DoctorEntry.Text) || string.IsNullOrEmpty(MotivoEntry.Text))
        {
            await Shell.Current.DisplayAlert("", "Es necesario Completar todos los formularios para actualizar la cita", "OK");

            return;
        }
        var cita = new CitasMedicas
        {
            FechaCita = FechaCitaEntry.Date,
            Doctor = DoctorEntry.Text,
            Paciente = NombrePacienteEntry.Text,
            Status = "Pendiente",
            Motivos = MotivoEntry.Text,
        };
        var citaActualizada = await ApiService.ActualizarCita(cita);
        if (citaActualizada != null)
        {
            this.Close();

        }
    }

    private void CerrarPopup_Clicked(object sender, EventArgs e)
    {
        this.Close();
    }
    private async void LoadCitaSeleccionadaData()
    {
        try
        {
            // Recuperar los datos del usuario
            var paciente = await SecureStorage.GetAsync("pacienteSeleccionado");
            var doctor = await SecureStorage.GetAsync("doctorSeleccionado");
            var comentario = await SecureStorage.GetAsync("comentarioCita");
            //var Hora = await SecureStorage.GetAsync("horaSeleccionada");
            var Fecha = await SecureStorage.GetAsync("fechaSeleccionada");

            // Asignar los datos a los campos
            DoctorEntry.Text = doctor;
            NombrePacienteEntry.Text = paciente;
            MotivoEntry.Text = comentario;
            //HoraCita.Time = TimeSpan.Parse(Hora);
            FechaCitaEntry.Date = Convert.ToDateTime(Fecha);
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Alerta", $"Error al intentar obtener el usuario comuniquese con soporte{ex}", "ok");

        }
    }
}