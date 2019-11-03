using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Entidades.EstrategiaMora
{
    public class EstrategiaMoraPorDia : IEstrategiaMora
    {
		public double CalcularMora(Pago pago)
        {
            return (pago.DiasMora * (pago.Cuota.Prestamo.CuotaFijaMensual * 0.05));
        }

    }
}
