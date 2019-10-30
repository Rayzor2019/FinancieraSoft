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

        public void guardar(Prestamo prestamo)
        {
            // CREANDO LAS SENTENCIAS SQL
            string insertarPrestamoSQL, insertarCuotaSQL;

            insertarPrestamoSQL = "SP_GenerarPrestamo";

            insertarCuotaSQL = "SP_InsertarCuota";

            try
            {
                SqlCommand comando;
                // GUARDANDO EL OBJETO Prestamo               
                comando = gestorSQL.obtenerComandoSQL(insertarPrestamoSQL);
                comando.Parameters.AddWithValue("@codigoCliente", prestamo.Cliente.Codigo);
                comando.Parameters.AddWithValue("@montoPrestado", prestamo.MontoPrestado);
                comando.Parameters.AddWithValue("@tasaEfectivaAnual", prestamo.TasaEfectivaAnual);
                comando.Parameters.AddWithValue("@totalPeriodosPago", prestamo.TotalPeriodosPago);
                comando.Parameters.AddWithValue("@tasaEfectivaMensual", prestamo.TasaEfectivaMensual);
                comando.Parameters.AddWithValue("@fechaPrestado", prestamo.FechaPrestamo);
                comando.Parameters.AddWithValue("@cuotaFijaMensual", prestamo.TasaEfectivaAnual);
                comando.ExecuteNonQuery();

                // GUARDANDO LOS OBJETOS Cuotas
                foreach (Cuota cuota in prestamo.ListaCuotas)
                {
                    // Agregando una couta del Prestamo
                    comando = gestorSQL.obtenerComandoSQL(insertarCuotaSQL);
                    comando.Parameters.AddWithValue("@prestamoID", cuota.Prestamo.PrestamoID);
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

        public Prestamo buscarPrestamo(Cliente cliente)
        {
            Prestamo prestamo;
            string consultaSQL = "select * from prestamo where codigoCliente ='" + cliente.Codigo + "'";
            try
            {
                SqlDataReader resultadoSQL = gestorSQL.ejecutarConsulta(consultaSQL);
                if (resultadoSQL.Read())
                {
                    prestamo = obtenerPrestamo(resultadoSQL,cliente);
                }
                else
                {
                    throw new Exception("El cliente no tiene un prestamo pendiente");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return prestamo;
        }

        public Prestamo obtenerPrestamo(SqlDataReader resultadoSQL, Cliente cliente)
        {
            //Creando variables locales para instanciar el objeto Prestamo al buscarlo en la DB;
            string prestamoID;
            double montoPrestado, tasaEfectivaAnual, tasaEfectivaMensual, cuotaFijaMensual;
            int totalPeriodosPago;            
            DateTime fechaPrestamo;
            Prestamo prestamo;
            List<Cuota> listaDeCuotas = new List<Cuota>();

            //Asignando valores del prestamo (DB) a las variables locales
            prestamoID = resultadoSQL.GetString(0);
            montoPrestado = resultadoSQL.GetDouble(1);
            tasaEfectivaAnual = resultadoSQL.GetDouble(2);
            totalPeriodosPago = int.Parse(resultadoSQL.GetString(3));
            tasaEfectivaMensual = resultadoSQL.GetDouble(4);
            fechaPrestamo = resultadoSQL.GetDateTime(5);
            cuotaFijaMensual = resultadoSQL.GetDouble(6);

            //Buscando las cuotas del préstamo
            Cuota cuota;
            string consultaSQL = "select * from couta where prestamoid ='" + prestamoID + "'";
            try
            {
                SqlDataReader resultadoConsultaSQL = gestorSQL.ejecutarConsulta(consultaSQL);
                while(resultadoConsultaSQL.HasRows)
                {
                    if (resultadoConsultaSQL.Read())
                    {
                        cuota = obtenerCuota(resultadoConsultaSQL);
                        listaDeCuotas.Add(cuota);
                    }
                    else
                    {
                        throw new Exception("Hubo un error al procesar una de las cuotas");
                    }
                }
            }
            catch (Exception err)
            {
                throw err;
            }

            //Instanciar el OBJETO prestamo
            prestamo = new Prestamo(prestamoID, montoPrestado, tasaEfectivaAnual, totalPeriodosPago, tasaEfectivaMensual, fechaPrestamo, cuotaFijaMensual, cliente, listaCuotas);
            
            return prestamo;
        }

        public Cuota obtenerCuota(SqlDataReader resultadoConsultaSQL)
        {
            Cuota cuota;

            //Creando variables locales para leer la cuota

            string cuotaID, estado;
            int periodo;
            double saldo, amortizacion, interes;
            DateTime fecha, fechaLimite;

            //Asignando los valores de la cuota

            cuotaID = resultadoConsultaSQL.GetString(0);
            periodo = int.Parse(resultadoConsultaSQL.GetString(1));
            saldo = resultadoConsultaSQL.GetDouble(2);
            fecha = resultadoConsultaSQL.GetDateTime(3);
            amortizacion = resultadoConsultaSQL.GetDouble(4);
            interes = resultadoConsultaSQL.GetDouble(5);
            fechaLimite = resultadoConsultaSQL.GetDateTime(6);
            estado = resultadoConsultaSQL.GetString(7);

            cuota = new Cuota(cuotaID, periodo, saldo, fecha, amortizacion, interes, fechaLimite, estado);

            return cuota;
        }
    }
}
