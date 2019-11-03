using FinancieraSoft.CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Contratos
{
    public interface IPagoDAO
    {
        void Guardar(Pago pago);

        void ActualizarEstadoCuota(Cuota cuota);
    }
}
