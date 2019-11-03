using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Contratos
{
    public interface IGestorDAO
    {
        void AbrirConexion();

        void CerrarConexion();

        void IniciarTransaccion();

        void TerminarTransaccion();

        void CancelarTransaccion();
    }
}
