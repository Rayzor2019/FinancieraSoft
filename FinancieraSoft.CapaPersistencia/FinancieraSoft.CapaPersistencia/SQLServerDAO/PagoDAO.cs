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
    }
}
