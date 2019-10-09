using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FinancieraSoft.CapaDominio.Entidades;

namespace FinancieraSoft.CapaPersistencia.SQLServerDAO
{
    class PrestamoDAO
    {
        private GestorSQL gestorSQL;

        public PrestamoDAO(GestorSQL gestorSQL)
        {
            this.gestorSQL = gestorSQL;
        }
        public void generar(Prestamo prestamo)
        {
            // CREANDO LAS SENTENCIAS SQL
            string insertarPrestamo1SQL, insertarPrestamo2SQL, insertarCuotaSQL;

            insertarPrestamo1SQL = "SP_GenerarPrestamo";

            insertarPrestamo2SQL = "SP_GenerarPrestamo2";

            insertarCuotaSQL = "SP_InsertarCuota";

            try
            {
                SqlCommand comando;
                Cuota cuota;
                // GUARDANDO EL OBJETO Prestamo
                if (prestamo.Cliente != null)
                {
                    comando = gestorSQL.obtenerComandoSQL(insertarPrestamo1SQL);
                    comando.Parameters.AddWithValue("@codigoCliente", prestamo.Cliente.Codigo);
                }
                else
                {
                    comando = gestorSQL.obtenerComandoSQL(insertarPrestamo2SQL);
                }
                comando.Parameters.AddWithValue("@montoPrestado", prestamo.MontoPrestado);
                comando.Parameters.AddWithValue("@tasaEfectivaAnual", prestamo.TasaEfectivaAnual);
                comando.Parameters.AddWithValue("@totalPeriodosPago", prestamo.TotalPeriodosPago);
                comando.Parameters.AddWithValue("@tasaEfectivaMensual", prestamo.TasaEfectivaMensual);
                comando.Parameters.AddWithValue("@fechaPrestado", prestamo.FechaPrestamo);
                comando.Parameters.AddWithValue("@cuotaFijaMensual", prestamo.TasaEfectivaAnual);
                comando.ExecuteNonQuery();

                // GUARDANDO LOS OBJETOS Lista de Cuotas
                foreach (Cuota c in prestamo.ListaCuotas)
                {
                    cuota = c;
                    // Agregando una couta del Prestamo
                    comando = gestorSQL.obtenerComandoSQL(insertarCuotaSQL);
                    comando.Parameters.AddWithValue("@prestamoID", prestamo.PrestamoID);
                    comando.Parameters.AddWithValue("@saldo", cuota.Saldo);
                    comando.Parameters.AddWithValue("@fecha", cuota.Fecha);
                    comando.Parameters.AddWithValue("@amortizacion", cuota.Amortizacion);
                    comando.Parameters.AddWithValue("@interes", cuota.Interes);
                    comando.Parameters.AddWithValue("@fechaLimite", cuota.FechaLimite);
                    comando.Parameters.AddWithValue("@estado", cuota.Estado);
                    comando.ExecuteNonQuery();
                }
            }
            catch (Exception err)
            {
                throw new Exception("Ocurrio un problema al intentar guardar.", err);
            }
        }
    }
}
