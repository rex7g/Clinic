using API_CLINICA.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;


namespace API_CLINICA.Repository
{
    public class EmpleadosRepository : IEmpleados
    {
       
        private readonly CLINICAContext _context;

        public EmpleadosRepository(CLINICAContext cLINICAContext)
        {
             _context = cLINICAContext;
        }
        public async Task<Empleado> DeleteEmpleadoByCodigo(string codigo)
        {
            var EmpleadoEliminado = await _context.Empleados.FirstOrDefaultAsync(e => e.Codigo == codigo);
            if (EmpleadoEliminado != null)
            {
                _context.Empleados.Remove(EmpleadoEliminado);
                await _context.SaveChangesAsync();
            }
            return EmpleadoEliminado;
        }

        public async Task<IEnumerable<Empleado>> GetAsyncAllEmpleado()
        {

            var listaEmpleados = await _context.Empleados.ToListAsync();
            return listaEmpleados;
        }

        public async Task<Empleado> GetAsyncEmpleado(string codigo)
        {
           var EmpleadoPorCodigo = await _context.Empleados.FirstOrDefaultAsync(e => e.Codigo==codigo);
           return EmpleadoPorCodigo;
           
        }

        public async Task<Empleado> GetEmpleadoByName(string nombre)
        {
            var EmpleadoPorNombre= await _context.Empleados.FirstOrDefaultAsync(e=>e.Nombre==nombre);
            return EmpleadoPorNombre;
        }

        public async Task<Empleado> UpdateEmpleadobyCodigo(string codigo, Empleado empleadoActualizado)
        {
            var EmpleadoActual = await _context.Empleados.FirstOrDefaultAsync(e => e.Codigo == codigo);

            if (EmpleadoActual != null)
            {
                // Actualiza los campos necesarios del empleado con los valores del empleadoActualizado
                EmpleadoActual.Nombre = empleadoActualizado.Nombre;
                EmpleadoActual.Apellido = empleadoActualizado.Apellido;
                EmpleadoActual.puesto = empleadoActualizado.puesto;
                EmpleadoActual.Status = empleadoActualizado.Status;
                EmpleadoActual.Email= empleadoActualizado.Email;
                EmpleadoActual.Telefono = empleadoActualizado.Telefono;
                EmpleadoActual.Contraseña = empleadoActualizado.Contraseña;
                // Actualiza otros campos según sea necesario

                _context.Empleados.Update(EmpleadoActual);
                await _context.SaveChangesAsync();
            }

            return EmpleadoActual;
        }
        public async Task<Empleado> CrearEmpleadonuevo(Empleado nuevoEmpleado)
        {
            _context.Empleados.AddAsync(nuevoEmpleado);
            await _context.SaveChangesAsync();

            return nuevoEmpleado;
        }

    }
}
