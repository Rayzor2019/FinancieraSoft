﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FinancieraSoft.CapaDominio.Entidades;
using FinancieraSoft.CapaDominio.Contratos;

namespace FinancieraSoft.CapaPersistencia.SQLServerDAO
{
    class PrestamoDAO : IPrestamoDAO
    {
        private GestorDAO gestorDAO;

        public PrestamoDAO(IGestorDAO gestorDAO)
        {
            this.gestorDAO = (GestorDAO)gestorDAO;
        }

        public void Guardar(Prestamo prestamo)
        {
            // CREANDO LAS SENTENCIAS SQL
            string insertarPrestamoSQL, insertarCuotaSQL;

            insertarPrestamoSQL = "SP_GenerarPrestamo";

            insertarCuotaSQL = "SP_InsertarCuota";

            string prestamoID = GenerarPrestamoID();
            string cuotaID = GenerarCuotaID();

            try
            {
                SqlCommand comando;
                // GUARDANDO EL OBJETO Prestamo               
                comando = gestorDAO.ObtenerComandoDeProcedimiento(insertarPrestamoSQL);
                comando.Parameters.AddWithValue("@codigoCliente", prestamo.Cliente.Codigo);
                comando.Parameters.AddWithValue("@prestamoID", prestamoID;
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
                    // Agregando la couta
                    comando = gestorDAO.ObtenerComandoDeProcedimiento(insertarCuotaSQL);
                    comando.Parameters.AddWithValue("@prestamoID", prestamoID);
                    comando.Parameters.AddWithValue("@cuotaID", cuotaID);
                    comando.Parameters.AddWithValue("@periodo", cuota.Periodo);
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

        public bool TieneDeudaPendiente(Prestamo prestamo)
        {
            string consultaSQL = "select estado from cuota where prestamoID ='" + prestamo.PrestamoID + "'";
            SqlDataReader resultadoSQL = gestorDAO.EjecutarConsulta(consultaSQL);
            string estado;
            bool tieneDeuda = true;
            while (resultadoSQL.Read())
            {
                estado = resultadoSQL["estado"].ToString();
                if (estado != "Pagado")
                {
                    tieneDeuda = false;
                }
            }
            return tieneDeuda;
        }

        public Prestamo BuscarPrestamo(Cliente cliente)
        {
            Prestamo prestamo;
            string consultaSQL = "select top 1 * from prestamo where codigoCliente ='" + cliente.Codigo + "'"
                                + "ORDER BY fechaPrestamo DESC";
            try
            {
                SqlDataReader resultadoSQL = gestorDAO.EjecutarConsulta(consultaSQL);
                if (resultadoSQL.Read())
                {
                    prestamo = ObtenerPrestamo(resultadoSQL, cliente);
                }
                else
                {
                    throw new Exception("El cliente no tiene un historial de prestamos");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
            return prestamo;
        }

        public Prestamo ObtenerPrestamo(SqlDataReader resultadoSQL, Cliente cliente)
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
                SqlDataReader resultadoConsultaSQL = gestorDAO.EjecutarConsulta(consultaSQL);
                while (resultadoConsultaSQL.Read())
                {
                    cuota = ObtenerCuota(resultadoConsultaSQL);
                    listaDeCuotas.Add(cuota);
                }
            }
            catch (Exception err)
            {
                throw new Exception("Hubo un error al procesar una de las cuotas", err);
            }

            //Instanciar el OBJETO prestamo
            prestamo = new Prestamo(prestamoID, montoPrestado, tasaEfectivaAnual, totalPeriodosPago, tasaEfectivaMensual, fechaPrestamo, cuotaFijaMensual, cliente, listaDeCuotas);

            return prestamo;
        }

        public Cuota ObtenerCuota(SqlDataReader resultadoConsultaSQL)
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

        public string GenerarPrestamoID()
        {
            string prestamoID = "";
            int total = 0;
            try
            {
                string consultaSQL = "Select count (*) from Prestamo";
                SqlDataReader resultadoSQL = gestorDAO.EjecutarConsulta(consultaSQL);
                if (resultadoSQL.Read())
                {
                    total = int.Parse(resultadoSQL.GetString(0));
                }
                resultadoSQL.Close();
                if (total < 10)
                {
                    prestamoID = "PRES-000" + total;
                }
                if (total < 100)
                {
                    prestamoID = "PRES-00" + total;
                }
                if (total < 1000)
                {
                    prestamoID = "PRES-0" + total;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Hubo un error al generar el prestamoID", err);
            }
            return prestamoID;
        }

        public string GenerarCuotaID()
        {
            string cuotaID = "";
            int total = 0;
            try
            {
                string consultaSQL = "Select count (*) from Cuota";
                SqlDataReader resultadoSQL = gestorDAO.EjecutarConsulta(consultaSQL);
                if (resultadoSQL.Read())
                {
                    total = int.Parse(resultadoSQL.GetString(0));
                }
                resultadoSQL.Close();
                if (total < 10)
                {
                    cuotaID = "CUOTA-0000" + total;
                }
                if (total < 100)
                {
                    cuotaID = "CUOTA-000" + total;
                }
                if (total < 1000)
                {
                    cuotaID = "CUOTA-00" + total;
                }
                if (total < 10000)
                {
                    cuotaID = "CUOTA-0" + total;
                }
            }
            catch (Exception err)
            {
                throw new Exception("Hubo un error al generar la cuotaID", err);
            }
            return cuotaID;
        }
    }
}
