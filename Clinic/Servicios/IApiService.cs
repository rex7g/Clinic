using Clinic.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clinic.Servicios
{
    public interface IApiService
    {
        //AREA DE USUARIOS
        Task<Usuarios> GetUsuario(string usuario);
        Task<IEnumerable<Usuarios>> GetAllUsers();
        Task<Usuarios> UpdateUserPassword(string email, string newPassowrd);
        Task<Usuarios> CrearUsuario(Usuarios usuario);
       // Task<Usuarios> UpdateUsuario(string codigo);
        Task<bool> EliminarUsuario(string codigo);

        //AREA DE CITAS MEDICAS
        Task<IEnumerable<CitasMedicas>> GetCitasMedicas();
        Task<CitasMedicas> CrearCita(CitasMedicas nuevacita);
        Task<CitasMedicas> ActualizarCita(CitasMedicas cita);
        Task<CitasMedicas> EliminarCitas(CitasMedicas cita);
        Task<bool> ActualizarCitaMedica(string paciente);


        //AREA DE DOCTORES
        Task<IEnumerable<Doctores>> GetDoctores();

        //AREA DE EMPLEADOS
        Task<Empleados> GetEmpleadobyCode(string codigo);
        Task<Empleados> GetEmpleadobyName(string nombre);
        Task<Empleados> CrearEmpleado(Empleados nuevoEmp);
        Task<IEnumerable<Empleados>> GetListaEmpleados();

        //FIREBASE
        Task<string> ObtenerTokenFirebaseAsync();
        Task<string> SubirPdfAFirebaseAsync(FileResult pdf);
        Task<FileResult> GenerarPdf(string rutaStorage, CitasMedicas citas);


        //AREA DE RECORDAR CONTRASEÑA
        Task SaveTokenToSecureStorage(string token);
        Task<bool> VerifyToken(string userInputToken);

    }
}
