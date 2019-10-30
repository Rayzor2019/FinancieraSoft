using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FinancieraSoft.CapaDominio.Entidades;

namespace FinancieraSoft.CapaPersistencia.SQLServerDAO
{
    class PagoDAO
    {

        private GestorSQL gestorSQL;

        public PagoDAO(GestorSQL gestorSQL)
        {
            this.gestorSQL = gestorSQL;
        }

        public void guardar(Pago pago)
        {
            // CREANDO LAS SENTENCIAS SQL
            string insertarPagoSQL;

            insertarPagoSQL = "SP_GuardarPago";

            try
            {
                SqlCommand comando;
                // GUARDANDO EL OBJETO Pago
                comando = gestorSQL.obtenerComandoSQL(insertarPagoSQL);
                comando.Parameters.AddWithValue("@coutaID", pago.Cuota.CuotaID);
                comando.Parameters.AddWithValue("@idpago", pago.PagoID);
                comando.Parameters.AddWithValue("@diasmora", pago.DiasMora);
                comando.Parameters.AddWithValue("@montototal", pago.MontoTotal);
                comando.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                throw new Exception("Ocurrio un problema al intentar guardar.", err);
            }

        }
        public List<Pago> buscar(string pagoID)
        {
            List<Pago> listaDePagos = new List<Pago>();
            Pago pago;
            string consultaSQL = "select * from Pago where pagoID like '%" + pagoID + "%' order by pagoID";
            try
            {
                SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(consultaSQL);
                while (resultadoSQL.Read())
                {
                    pago = obtenerPago(resultadoSQL);

                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return listaDePagos;
        }

        public Pago ConsultarPorfecha(string fecha)
        {
            Pago pago;
            string consultaSQL = "select * from Couta where fecha = '" + fecha + "'";
            try
            {
                SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(consultaSQL);
                if (resultadoSQL.Read())
                {
                    pago = obtenerPago(resultadoSQL);
                }
                else
                {
                    throw new Exception("No existe el pago.");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return pago;
        }

        private Pago obtenerPago(SqlDataReader resultadoSQL)
        {
            Pago pago = new Pago();
            pago.PagoID = resultadoSQL.GetString(0);
            pago.fecha = resultadoSQL.GetDateTime(1);
            pago.DiasMora = resultadoSQL.GetInt32(2);
            pago.MontoRecibido = Double.Parse(resultadoSQL.GetDecimal(3).ToString());
            pago.MontoTotal = resultadoSQL.GetInt32(4);
            pago.Mora = resultadoSQL.GetDouble(5);

            return pago;
        }

    }
}
    
    

