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
        private DateTime fecha;
        private double amortizacion;
        private double interes;
        private DateTime fechaLimite;
        private string estado;
        public double Amortizacion { get => amortizacion; set => amortizacion = value; }
        public string CuotaID { get => cuotaID; set => cuotaID = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public double Interes { get => interes; set => interes = value; }
        public double Saldo { get => saldo; set => saldo = value; }
        public int Periodo { get => periodo; set => periodo = value; }
        public DateTime FechaLimite { get => fechaLimite; set => fechaLimite = value; }
        public string Estado { get => estado; set => estado = value; }
        public Cuota(int periodo, double saldo, DateTime fecha, double amortizacion, double interes)
        {
            //cuotaID = ; Autogenerar
            this.periodo = periodo;
            this.saldo = saldo;
            this.fecha = fecha;
            this.amortizacion = amortizacion;
            this.interes = interes;
            fechaLimite = CalcularFechaLimite();
            estado = "Pendiente";
        }

        public DateTime CalcularFechaLimite()
        {
            return fecha.AddDays(10);
        }

        
    }
}
