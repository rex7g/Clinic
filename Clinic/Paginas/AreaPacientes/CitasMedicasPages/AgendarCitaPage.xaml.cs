﻿using Clinic.Modelos.ConstantesAPI;
using Clinic.Servicios;
using MailKit.Net.Smtp;
using MimeKit;

namespace Clinic.Paginas.AreaPacientes.CitasMedicasPages;

public partial class AgendarCitaPage : ContentPage
{
    private readonly IApiService apiService;
    public AgendarCitaPage(IApiService service)
	{
		InitializeComponent();
        apiService = service;
        CargarDoctores();


    }

    private async void Agendar_Clicked(object sender, EventArgs e)
    {
        var DoctorCita = DoctorPicker.SelectedItem.ToString();
        var motivo = comentario.Text.Trim();
        var NombrePaciente = paciente.Text.Trim();
        var fechaSeleccionada = FechaCita.Date;
        var Hora = HoraCita.Time;

      

        if (string.IsNullOrEmpty(DoctorCita) && string.IsNullOrEmpty(motivo) && string.IsNullOrEmpty(NombrePaciente))
        {

            await Shell.Current.DisplayAlert("Alerta", "Para crear la cita debe comppletar todos los campos", "OK");

        }

        var nuevaCita = new Modelos.CitasMedicas
        {
            Doctor = DoctorCita,
            Motivos = motivo,
            Paciente = NombrePaciente,
            Status = "Pendiente",
            FechaCita = fechaSeleccionada,
            HoraCita = Hora


        };
        var citaCreada = await apiService.CrearCita(nuevaCita);
        if (citaCreada != null)
        {
            await Shell.Current.DisplayAlert("", "La cita ha sido Agendada Satisfact oriamente", "OK");
            string userEmail = await SecureStorage.GetAsync("UserEmail");

            var EnviarEmail = SendEmailAsync($"{userEmail}", "Cita pendiente Creada", $"Tiene una cita pendiente con el doctor:{DoctorCita}");
            var generarPDF= await apiService.GenerarPdf(Constantes.rutaStorage, nuevaCita);
            var PDFSubido= await apiService.SubirPdfAFirebaseAsync(generarPDF);


            await Navigation.PopModalAsync();
        }
        else
        {
            await Shell.Current.DisplayAlert("", "Hubo un error al agendar la cita.", "OK");
        }
    }
    public async Task SendEmailAsync(string recipientEmail, string subject, string message)
    {
        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Clinicapp", "noreply@cecayes.com.do"));
        email.To.Add(new MailboxAddress("Clinicapp", recipientEmail));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Plain);


        using var smtpClient = new SmtpClient();
        try
        {
            //Configurar servidor smtp real
            await smtpClient.ConnectAsync("localhost", 25, useSsl: false);
            //await smtpClient.AuthenticateAsync("noreply@cecayes.com.do", "password");
            await smtpClient.SendAsync(email);
            await smtpClient.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            // Manejo de excepciones aqu� si es necesario
        }
      
    }
    private async void CargarDoctores()
    {
        // Aqu� deber�as cargar la lista de doctores desde tu servicio o fuente de datos
        // Por ejemplo:
        var doctores = await apiService.GetDoctores();

        // Obtener lista de especialidades sin duplicados
        var especialidades = doctores.Select(d => d.Especialidad).Distinct().ToList();
        // Actualizar el picker de especialidades
        MainThread.BeginInvokeOnMainThread(() => {
            EspecialidadPicker.ItemsSource = especialidades;
        });
    }
    private async void Salir_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopModalAsync();
    }

    private async void EspecialidadPicker_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (EspecialidadPicker.SelectedIndex == -1) return;

        var especialidadSeleccionada = EspecialidadPicker.SelectedItem.ToString();
        var doctores = await apiService.GetDoctores();
        var doctoresDeEspecialidad = doctores.Where(d => d.Especialidad == especialidadSeleccionada).ToList();
        var nombresDeDoctores = doctoresDeEspecialidad.Select(d => d.Nombre).ToList();

        // Actualizar el picker de doctores con los doctores de la especialidad seleccionada
        MainThread.BeginInvokeOnMainThread(() => {
            DoctorPicker.ItemsSource = nombresDeDoctores;
        });
    }
}