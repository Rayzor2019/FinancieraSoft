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
        private double montoTotal;
        private Cuota cuota;

        public string PagoID { get => pagoID; set => pagoID = value; }
        public int DiasMora { get => diasMora; set => diasMora = value; }
        public double MontoTotal { get => montoTotal; set => montoTotal = value; }
        public Cuota Cuota { get => cuota; set => cuota = value; }

        public Pago(Cuota cuota)
        {
            //this.pagoID = pagoID; Autogenerar
            diasMora = calcularDiasMora();
            montoTotal = calcularMontoTotal();
            this.Cuota = cuota;
        }

        public int calcularDiasMora()
        {
            TimeSpan tiempoDeMora = DateTime.Now - Cuota.FechaLimite;
            if (tiempoDeMora.Days <= 0)
                return 0;
            else
                return tiempoDeMora.Days;
        }

        public double calcularMontoTotal()
        {
            return Cuota.Prestamo.CuotaFijaMensual 
                 + diasMora * (Cuota.Prestamo.CuotaFijaMensual * 0.05);
        }

        public double calcularVuelto (double montoRecibido)
        {
            return (montoRecibido - calcularMontoTotal());
        }
    }
}