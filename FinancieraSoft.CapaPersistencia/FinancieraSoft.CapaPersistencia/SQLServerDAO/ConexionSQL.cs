using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FinancieraSoft.CapaPersistencia.SQLServerDAO
{
    class ConexionSQL
    {
        public SqlConnection EstablecerConexion()
        {
            string cadenaDeConexion;
            cadenaDeConexion = "Data Source=(local);" +
                "Initial Catalog = FiancieraSoft;" +
                "Integrated Security=True";

            SqlConnection conexion = new SqlConnection(cadenaDeConexion);

            return conexion;
        }
    }
}
