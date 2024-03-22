using Clinic.Modelos;
using Clinic.Servicios;

namespace Clinic.Paginas.AreaPacientes.Medicos;

public partial class DetalleContactosPage : ContentPage
{
	public readonly IApiService apiService;
	public DetalleContactosPage(IApiService service,Doctores doctores)
	{
		InitializeComponent();
		apiService = service;
        LoadDetallesDoctores(doctores);
    }

    private void contactar_Clicked(object sender, EventArgs e)
    {
        string mensaje = "hola";
        string numero = TelefonoDoctor.Text;
        EnviarWhatsapp(numero, mensaje);
    }

    private async void Regresar_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }
    public async void EnviarWhatsapp(string numero, string mensaje)
    {
        bool mensajeEnviado = await Launcher.CanOpenAsync($"whatsapp://send?phone=+{numero}&text={mensaje}");

        if (mensajeEnviado == true)
        {
            // await Launcher.OpenAsync($"whatsapp://send?phone=+{numero}&text={mensaje}");
            await Launcher.OpenAsync($"whatsapp://send?phone=+{numero}");
        }
        else
        {
            await Shell.Current.DisplayAlert("Aviso", "No se ha podido contactar con el doctor", $"El numero del doctor es:{numero}");
        }


    }
    public  void LoadDetallesDoctores(Doctores doctore)
    {
        NombreDoctor.Text = doctore.Nombre;
        EspecialidadDoctor.Text = doctore.Especialidad;
        TelefonoDoctor.Text = doctore.Telefono;
        SexoDoctor.Text = doctore.Sexo;
        JornadaDoctor.Text = doctore.Jornada;
        EdadDoctor.Text = doctore.Edad.ToString();
        ConsultorioDoctor.Text = doctore.Consultorio;


    }
}