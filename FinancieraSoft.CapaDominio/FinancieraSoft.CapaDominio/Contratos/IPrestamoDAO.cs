using FinancieraSoft.CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Contratos
{
    public interface IPrestamoDAO
    {

        void Guardar(Prestamo prestamo);

        bool TieneDeudaPendiente(Prestamo prestamo);

        Prestamo BuscarPrestamo(Cliente cliente);

        Prestamo ObtenerPrestamo(SqlDataReader resultadoSQL, Cliente cliente);

        Cuota ObtenerCuota(SqlDataReader resultadoConsultaSQL);
    }
}
