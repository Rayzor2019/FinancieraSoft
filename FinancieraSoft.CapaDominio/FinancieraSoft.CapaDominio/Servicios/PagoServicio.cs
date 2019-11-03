using FinancieraSoft.CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Servicios
{
    class PagoServicio
    {
        public void ValidarVuelto(Pago pago, double montoRecibido)
        {
            if(pago.CalcularVuelto(montoRecibido)<0)
            {
                throw new Exception("El monto recibido es menor que el monto total");
            }
        }
    }
}
