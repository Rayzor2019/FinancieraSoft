using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FinancieraSoft.CapaDominio.Entidades;
using FinancieraSoft.CapaDominio.Contratos;

namespace FinancieraSoft.CapaPersistencia.SQLServerDAO
{
    class PagoDAO : IPagoDAO
    {

        private GestorDAO gestorDAO;

        public PagoDAO(IGestorDAO gestorDAO)
        {
            this.gestorDAO = (GestorDAO)gestorDAO;
        }

        public void Guardar(Pago pago)
        {
            // CREANDO LAS SENTENCIAS SQL
            string insertarPagoSQL;

            insertarPagoSQL = "SP_GuardarPago";

            string pagoID = GenerarPagoID();

            try
            {
                SqlCommand comando;
                // GUARDANDO EL OBJETO Pago
                comando = gestorDAO.ObtenerComandoDeProcedimiento(insertarPagoSQL);
                comando.Parameters.AddWithValue("@coutaID", pago.Cuota.CuotaID);
                comando.Parameters.AddWithValue("@idpago", pagoID);
                comando.Parameters.AddWithValue("@diasmora", pago.DiasMora);
                comando.Parameters.AddWithValue("@montototal", pago.MontoTotal);
                comando.ExecuteNonQuery();
                ActualizarEstadoCuota(pago.Cuota);
            }
            catch (Exception err)
            {
                throw new Exception("Ocurrio un problema al intentar guardar.", err);
            }
        }

        public void ActualizarEstadoCuota(Cuota cuota)
        {
            string updateSQL = "update cuota" +
                               "set estado = 'Pagado'" +
                               "where cuotaID = '"+ cuota.CuotaID + "'";
            try
            {
                SqlCommand comando;
                comando = gestorDAO.ObtenerComandoSQL(updateSQL);
                comando.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                throw new Exception("Ocurrio un problema al intentar actualizar la cuota", err);
            }
        }

        public string GenerarPagoID()
        {
            string pagoID = "";
            int total = 0;
            try
            {
                string consultaSQL = "Select count (*) from Pago";
                SqlDataReader resultadoSQL = gestorDAO.EjecutarConsulta(consultaSQL);
                if (resultadoSQL.Read())
                {
                    total = int.Parse(resultadoSQL.GetString(0));
                }
                resultadoSQL.Close();
                if (total < 10)
                {
                    pagoID = "PAGO-0000" + total;
                }
                if (total < 100)
                {
                    pagoID = "PAGO-000" + total;
                }
                if (total < 1000)
                {
                    pagoID = "PAGO-00" + total;
                }
                if (total < 10000)
                {
                    pagoID = "PAGO-0" + total;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Hubo un error al generar el pagoID", err);
            }
            return pagoID;
        }
    }
}
    
    

