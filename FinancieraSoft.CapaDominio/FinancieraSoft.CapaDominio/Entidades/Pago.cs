using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Entidades
{
    public class Pago
    {
        private string pagoID;
        private int diasMora;
        private double mora;
        private double montoTotal;
        private double montoRecibido;
        private double vuelto;
        private Cuota cuota;
        private Prestamo prestamo;

        public string PagoID { get => pagoID; set => pagoID = value; }
        public int DiasMora { get => diasMora; set => diasMora = value; }
        public double Mora { get => mora; set => mora = value; }
        public double MontoTotal { get => montoTotal; set => montoTotal = value; }
        public double MontoRecibido { get => montoRecibido; set => montoRecibido = value; }
        public double Vuelto { get => vuelto; set => vuelto = value; }
        public Cuota Cuota { get => cuota; set => cuota = value; }
        public Prestamo Prestamo { get => prestamo; set => prestamo = value; }

        public Pago(Cuota cuota, string pagoID, int diasMora, double mora, double montoTotal, double montoRecibido, double vuelto)
        {
            this.pagoID = pagoID;
            this.diasMora = diasMora;
            this.mora = mora;
            this.montoTotal = montoTotal;
            this.montoRecibido = montoRecibido;
            this.vuelto = vuelto;
            this.cuota = cuota;
        }

        public int CalcularDiasMora()
        {
            TimeSpan t = DateTime.Now - cuota.FechaLimite;
            if (t.Days < 0)
                return 0;
            else
                return t.Days;
        }

        public double CalcularMora()
        {
            return DiasMora * (prestamo.CuotaFijaMensual* 0.05);
        }

        public double CalcularMontoTotal()
        {
            return prestamo.CuotaFijaMensual + Mora;
        }

        public double CalcularVuelto ()
        {
            return (MontoRecibido - CalcularMontoTotal());
        }
        public bool ValidarVuelto (float vuelto)
        {
            return (vuelto > 0);
        }
    }
}