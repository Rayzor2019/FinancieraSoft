using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancieraSoft.CapaDominio.Entidades;
using System.Data.SqlClient;

namespace FinancieraSoft.CapaPersistencia.SQLServerDAO
{
    class ClienteDAO
    {
        private GestorSQL gestorSQL;
        public ClienteDAO(GestorSQL gestorSQL)
        {
            this.gestorSQL = gestorSQL;
        }
        public Cliente buscarPorDni(string dni)
        {
            Cliente cliente;
            string consultaSQL = "select * from cliente where dni ='" + dni + "'";
            try
            {
                SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(consultaSQL);
                if (resultadoSQL.Read())
                {
                    cliente = obtenerCliente(resultadoSQL);
                }
                else
                {
                    throw new Exception("No existe el Cliente");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return cliente;
        }

        public Cliente obtenerCliente(SqlDataReader resultadoSQL)
        {
            Cliente cliente = new Cliente();
            cliente.Codigo = resultadoSQL.GetString(0);
            cliente.Apellidos = resultadoSQL.GetString(1);
            cliente.Nombres = resultadoSQL.GetString(2);
            return cliente;
        }
    }
}
