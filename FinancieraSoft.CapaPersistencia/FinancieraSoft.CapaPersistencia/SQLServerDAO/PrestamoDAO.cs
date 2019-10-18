using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace FinancieraSoft.CapaPersistencia.SQLServerDAO
{
    class PrestamoDAO
    {
        private GestorSQL gestorSQL;

        public PrestamoDAO(GestorSQL gestorSQL)
        {
            this.gestorSQL = gestorSQL;
        }
        public void generaPrestamo(Prestamo prestamo)
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
                //GUARDANDO EL OBJETO Prestamo
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

        public List<Prestamo> buscar(string prestamoID)
        {
            List<Prestamo> listaDePrestamos = new List<Prestamo>();
            Prestamo prestamo;
            string consultaSQL = "select * from Prestamo where prestamoID like '%" + prestamoID + "%' order by prestamoID";
            try
            {
                SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(consultaSQL);
                while (resultadoSQL.Read())
                {
                    prestamo = obtenerPrestamo(resultadoSQL);

                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return listaDePrestamos;
        }

        public Prestamo consultarPorDni(string Dni)
        {
            Prestamo prestamo;
            string consultaSQL = "select * from Cliente where dni = '" + Dni + "'";
            try
            {
                SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(consultaSQL);
                if (resultadoSQL.Read())
                {
                    prestamo = obtenerPrestamo(resultadoSQL);
                }
                else
                {
                    throw new Exception("No existe el cliente.");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return prestamo;
        }

        private Prestamo obtenerPrestamo(SqlDataReader resultadoSQL)
        {
            Prestamo prestamo = new Prestamo();
            prestamo.PrestamoID = resultadoSQL.GetString(0);
            prestamo.Dni = resultadoSQL.GetString(1);
            prestamo.TotalPeriodosPago = resultadoSQL.GetInt32(2);
            prestamo.FechaPrestamo = resultadoSQL.GetDateTime(3);
            prestamo.MontoPrestado = resultadoSQL.GetFloat(4);
            prestamo.TasaEfectivaAnual = Double.Parse(resultadoSQL.GetDecimal(5).ToString());


            return prestamo;
        }

    }
}




    

