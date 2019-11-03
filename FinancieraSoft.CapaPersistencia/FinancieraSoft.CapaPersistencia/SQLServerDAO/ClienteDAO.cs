using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinancieraSoft.CapaDominio.Entidades;
using System.Data.SqlClient;
using FinancieraSoft.CapaDominio.Contratos;

namespace FinancieraSoft.CapaPersistencia.SQLServerDAO
{
    class ClienteDAO : IClienteDAO
    {
        private GestorDAO gestorDAO;
        public ClienteDAO(IGestorDAO gestorDAO)
        {
            this.gestorDAO = (GestorDAO)gestorDAO;
        }
        public Cliente Buscar(string dni)
        {
            Cliente cliente;
            string consultaSQL = "select * from cliente where dni ='" + dni + "'";
            try
            {
                SqlDataReader resultadoSQL = gestorDAO.EjecutarConsulta(consultaSQL);
                if (resultadoSQL.Read())
                {
                    cliente = ObtenerCliente(resultadoSQL);
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

        public Cliente ObtenerCliente(SqlDataReader resultadoSQL)
        {
            Cliente cliente = new Cliente();
            cliente.Codigo = resultadoSQL.GetString(0);
            cliente.Apellidos = resultadoSQL.GetString(1);
            cliente.Nombres = resultadoSQL.GetString(2);
            return cliente;
        }
    }
}
