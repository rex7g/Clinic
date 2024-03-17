using API_CLINICA.Data;

namespace API_CLINICA.Repository
{
    public interface IEmpleados
    {
      Task <Empleado> GetAsyncEmpleado(string Id);
      Task<IEnumerable<Empleado>>GetAsyncAllEmpleado();
      Task<Empleado> GetEmpleadoByName(string nombre);
      Task<Empleado> DeleteEmpleadoByCodigo(string codigo);
      Task<Empleado>UpdateEmpleadobyCodigo(string codigo,Empleado empleadoActualizado);
      Task<Empleado> CrearEmpleadonuevo(Empleado nuevoEmpleado);

    }
}
