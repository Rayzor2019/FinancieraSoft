using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Entidades
{
    public class Cuota
    {
        private string cuotaID;
        private int periodo;
        private double saldo;
        private DateTime fechaPago;
        private double amortizacion;
        private double interes;
        private DateTime fechaLimite;
        private int diasMora;
        private double mora;
        private double montoTotal;
        private string estado;

        public double Amortizacion { get => amortizacion; set => amortizacion = value; }
        public string CuotaID { get => cuotaID; set => cuotaID = value; }
        public DateTime FechaPago { get => fechaPago; set => fechaPago = value; }
        public double Interes { get => interes; set => interes = value; }
        public double Saldo { get => saldo; set => saldo = value; }
        public int Periodo { get => periodo; set => periodo = value; }
        public DateTime FechaLimite { get => fechaLimite; set => fechaLimite = value; }
        public int DiasMora { get => diasMora; set => diasMora = value; }
        public double Mora { get => mora; set => mora = value; }
        public double MontoTotal { get => montoTotal; set => montoTotal = value; }

        public string Estado { get => estado; set => estado = value; }
        

        public Cuota(Prestamo prestamo, int periodo, double saldo, DateTime fechaPago, double amortizacion, double interes)
        {
            cuotaID = prestamo.PrestamoID + periodo;
            this.periodo = periodo;
            this.saldo = saldo;
            this.fechaPago = fechaPago;
            this.amortizacion = amortizacion;
            this.interes = interes;
            fechaLimite = fechaPago.AddDays(10);
            estado = "Pendiente";
        }

        public List<Cuota> ListaCuotas(Prestamo prestamo)
        {
            List<Cuota> cuotas = new List<Cuota>();
            Cuota c;
            DateTime fechaInicial = DateTime.Now;
            DateTime fechaPago;
            double saldo = prestamo.MontoPrestado;
            double interes;
            double amortizacion;

            for (int i = 1; i <= prestamo.TotalPeriodosPago; i++)
            {
                interes = saldo * prestamo.TasaEfectivaMensual;
                amortizacion = prestamo.CuotaFijaMensual - interes;
                saldo = saldo - amortizacion;
                fechaPago = fechaInicial.AddMonths(i);
                c = new Cuota(prestamo, i, saldo, fechaPago, amortizacion, interes);

                cuotas.Add(c);
            }
            return cuotas;
        }

        public double CalcularAmortizacion(Prestamo prestamo, double interes)
        {
            return prestamo.CuotaFijaMensual - interes;
        }

        public double CalcularInteres(double saldoAnterior, Prestamo prestamo)
        {
            return saldoAnterior * prestamo.TasaEfectivaMensual;
        }

        public double CalcularSaldo(double saldoAnterior, double amortizacion)
        {
            return saldoAnterior - amortizacion;
        }

        public int CalcularDiasMora()
        {
            TimeSpan t = DateTime.Now - fechaLimite;
            return t.Days;
        }

        public double CalcularMora(Prestamo prestamo)
        {
            return diasMora * (prestamo.CuotaFijaMensual * 0.05);
        }

        public double CalcularMontoTotal(Prestamo prestamo)
        {
            return prestamo.CuotaFijaMensual + mora;
        }
    }
}
