using FinancieraSoft.CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Servicios
{
    public class PrestamoServicio
    {
        public void VerificarDatosPrestamo(Prestamo prestamo)
        {
            if(!prestamo.TieneMontoPrestadoValido())
            {
                throw new Exception("El Monto Prestado ingresado no es válido");
            }
            if(!prestamo.TieneTasaEfectivaAnualValido())
            {
                throw new Exception("La Tasa Efectiva Anual ingresada no es válida");
            }
            if(!prestamo.TieneTotalPeriodosPagoValido())
            {
                throw new Exception("El Total de Periodos de Pago no es válido");
            }
        }
    }
}
