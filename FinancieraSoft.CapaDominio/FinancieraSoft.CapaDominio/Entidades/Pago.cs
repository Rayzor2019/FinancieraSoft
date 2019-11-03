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
            diasMora = CalcularDiasMora();
            montoTotal = CalcularMontoTotal();
            this.Cuota = cuota;
        }

        //REGLAS DE NEGOCIO
        //REGLA 1 - Implementada en Cuota.cs
        //REGLA 2 - El total de días de mora se calcula con la siguiente fórmula [DiasMora=FechaActual-FechaLimite].
        public int CalcularDiasMora()
        {
            TimeSpan tiempoDeMora = DateTime.Now - Cuota.FechaLimite;
            if (tiempoDeMora.Days <= 0)
                return 0;
            else
                return tiempoDeMora.Days;
        }

        //REGLA 3 - 3: Si la fecha actual excede a la fecha limite de pago de la cuota pendiente, entonces se
                     //aumentará el 0.5% de la cuota por día

        public double CalcularMora()
        {
            return diasMora * (Cuota.Prestamo.CuotaFijaMensual * 0.05);
        }

        //REGLA 4 - El cálculo del monto total a pagar se realizará con la formula 
                  //[MontoCalculadoTotal = MontoCuota + MontoMora].
        public double CalcularMontoTotal()
        {
            return Cuota.Prestamo.CuotaFijaMensual + CalcularMora();
        }

        //REGLA 5 - El vuelto se calculará con la fórmula: [Vuelto=MontoIngresado – MontoCalculadoTotal]
        public double CalcularVuelto (double montoRecibido)
        {
            return (montoRecibido - CalcularMontoTotal());
        }
    }
}