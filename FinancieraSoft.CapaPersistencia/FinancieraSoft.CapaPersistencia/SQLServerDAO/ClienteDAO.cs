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
            cliente.IdCliente = resultadoSQL.GetString(0);
            cliente.Nombre = resultadoSQL.GetString(1);
            cliente.Dni = resultadoSQL.GetString(2);
            cliente.ApellidoPaterno = resultadoSQL.GetString(3);
            cliente.ApellidoMaterno = resultadoSQL.GetString(4);
            cliente.FechaNacimiento = resultadoSQL.GetDateTime(5);
            cliente.Telefono = resultadoSQL.GetString(6);
            return cliente;
        }

    }
}
