using FinancieraSoft.CapaDominio.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Contratos
{
    public interface IClienteDAO
    {
        Cliente Buscar(string dni);
    }
}
