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
        private Prestamo prestamo;

        public double Amortizacion { get => amortizacion; set => amortizacion = value; }
        public string CuotaID { get => cuotaID; set => cuotaID = value; }
        public DateTime Fecha { get => fecha; set => fecha = value; }
        public double Interes { get => interes; set => interes = value; }
        public double Saldo { get => saldo; set => saldo = value; }
        public int Periodo { get => periodo; set => periodo = value; }
        public DateTime FechaLimite { get => fechaLimite; set => fechaLimite = value; }
        public string Estado { get => estado; set => estado = value; }
        public Prestamo Prestamo { get => prestamo; set => prestamo = value; }

        public Cuota(int periodo, double saldo, DateTime fecha, double amortizacion, double interes, Prestamo prestamo)
        {
            //cuotaID = ; Autogenerar
            this.periodo = periodo;
            this.saldo = saldo;
            this.fecha = fecha;
            this.amortizacion = amortizacion;
            this.interes = interes;
            this.Prestamo = prestamo;
            fechaLimite = CalcularFechaLimite();
            estado = "Pendiente";
        }

        //Constructor para obtener una cuota
        public Cuota(string cuotaID, int periodo, double saldo, DateTime fecha, double amortizacion, double interes, DateTime fechaLimite, string estado)
        {
            this.cuotaID = cuotaID;
            this.periodo = periodo;
            this.saldo = saldo;
            this.fecha = fecha;
            this.amortizacion = amortizacion;
            this.interes = interes;
            this.fechaLimite = fechaLimite;
            this.estado = estado;
        }

        //REGLAS DE NEGOCIO

        //REGLA 1 - La fecha limite es de 10 días a partir de la Fecha de pago que se muestra en el cronograma
        public DateTime CalcularFechaLimite()
        {
            return fecha.AddDays(10);
        }        
    }
}
