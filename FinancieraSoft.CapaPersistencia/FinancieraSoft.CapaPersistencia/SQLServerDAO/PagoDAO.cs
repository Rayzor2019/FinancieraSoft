using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

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
            string insertarPago1SQL, insertarPago2SQL
;

            insertarPago1SQL = "SP_Pago1";

            insertarPago2SQL = "SP_Pago2";

            try
            {
                SqlCommand comando;
                Pago c;
                // GUARDANDO EL OBJETO Pago
                if (Pago.couta != null)
                {
                    comando = gestorSQL.obtenerComandoSQL(insertarPago1SQL);
                    comando.Parameters.AddWithValue("@coutaID", c.couta.CoutaID);
                }
                else
                {
                    comando = gestorSQL.obtenerComandoSQL(insertarPago2SQL);
                }
                comando.Parameters.AddWithValue("@idpago", c.PagoID);
                comando.Parameters.AddWithValue("@diasmora", c.DiasMora);
                comando.Parameters.AddWithValue("@montorecibido", c.MontoRecibido);
                comando.Parameters.AddWithValue("@montototal", c.MontoTotal);
                comando.Parameters.AddWithValue("@mora", c.Mora);
                comando.Parameters.AddWithValue("@vuelto", c.Vuelto);
                comando.ExecuteNonQuery();


            }
            catch (Exception err)
            {
                throw new Exception("Ocurrio un problema al intentar guardar.", err);
            }

        }
    }
}
