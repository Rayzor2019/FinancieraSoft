using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Entidades
{
    public class Pago
    {
        private float montoAPagar;
        private float montoRecibido;
        private string pagoID;
        private float vuelto;

        public float MontoAPagar { get => montoAPagar; set => montoAPagar = value; }
        public float MontoRecibido { get => montoRecibido; set => montoRecibido = value; }
        public string PagoID { get => pagoID; set => pagoID = value; }
        public float Vuelto { get => vuelto; set => vuelto = value; }

        public Pago(Cuota cuota, float montoRecibido)
        {
            pagoID = cuota.CuotaID;
            montoAPagar = (float)cuota.MontoTotal;
            this.montoRecibido = montoRecibido;
            if (ValidarVuelto(CalcularVuelto(montoAPagar, montoRecibido)))
                vuelto = CalcularVuelto(montoAPagar, montoRecibido);
            cuota.Estado = "Pagado";
        }

        public float CalcularVuelto (float montoTotal, float montoRecibido)
        {
            return (montoRecibido - montoTotal);
        }
        public bool ValidarVuelto (float vuelto)
        {
            return (vuelto > 0);
        }
    }
}
