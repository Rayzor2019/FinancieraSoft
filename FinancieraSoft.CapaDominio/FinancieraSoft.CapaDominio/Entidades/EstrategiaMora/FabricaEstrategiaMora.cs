using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Entidades.EstrategiaMora
{
    public class FabricaEstrategiaMora
    {
        public static IEstrategiaMora crearInstancia()
        {
            Type tipoClaseEstrategiaMora = Type.GetType(ClaseEstrategiaMora.Default.NombreDeClaseMora);
            IEstrategiaMora estrategiaMora = (IEstrategiaMora)Activator.CreateInstance(tipoClaseEstrategiaMora);
            return estrategiaMora;
        }
    }
}
