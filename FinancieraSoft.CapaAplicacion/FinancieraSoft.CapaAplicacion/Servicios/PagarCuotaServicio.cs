using FinancieraSoft.CapaDominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaAplicacion.Servicios
{
    public class PagarCuotaServicio
    {
        private IGestorDAO gestorDAO;
        private IClienteDAO clienteDAO;
        private IPrestamoDAO prestamoDAO;
        private IPagoDAO pagoDAO;
    }
}
