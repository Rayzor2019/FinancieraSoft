using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Entidades.EstrategiaMora
{
    public class EstrategiaMoraCero : IEstrategiaMora
    {
        public double CalcularMora(Pago pago)
        {
            return 0;
        }
    }
}
