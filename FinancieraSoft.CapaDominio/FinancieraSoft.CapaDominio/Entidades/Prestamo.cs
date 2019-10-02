using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Entidades
{
    public class Prestamo
    {
        private string prestamoID;
        private float montoPrestado;
        private double tasaEfectivaAnual;
        private int totalPeriodosPago;
        private double tasaEfectivaMensual;
        private DateTime fechaPrestamo;
        private double cuotaFijaMensual;
        private Cliente cliente;
        private List<Cuota> listaCuotas;

        public string PrestamoID { get => prestamoID; set => prestamoID = value; }
        public float MontoPrestado { get => montoPrestado; set => montoPrestado = value; }
        public double TasaEfectivaAnual { get => tasaEfectivaAnual; set => tasaEfectivaAnual = value; }
        public int TotalPeriodosPago { get => totalPeriodosPago; set => totalPeriodosPago = value; }
        public double TasaEfectivaMensual { get => tasaEfectivaMensual; set => tasaEfectivaMensual = value; }
        public DateTime FechaPrestamo { get => fechaPrestamo; set => fechaPrestamo = value; }
        public double CuotaFijaMensual { get => cuotaFijaMensual; set => cuotaFijaMensual = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public List<Cuota> ListaCuotas { get => listaCuotas; set => listaCuotas = value; }

        public Prestamo(Cliente cliente, float montoPrestado, double tasaEfectivaAnual, int totalPeriodosPago, DateTime fechaPrestamo)
        {
            this.Cliente = cliente;
            this.MontoPrestado = montoPrestado;
            this.TasaEfectivaAnual = tasaEfectivaAnual;
            this.TotalPeriodosPago = totalPeriodosPago;
            this.fechaPrestamo = fechaPrestamo;
            List<Cuota> cuotas = new List<Cuota>();
        }

        public Prestamo(Cliente cliente, string prestamoID, float montoPrestado, double tasaEfectivaAnual, int totalPeriodosPago, double tasaEfectivaMensual, double cuotaFijaMensual, DateTime fechaPrestamo)
        {
            this.Cliente = cliente;
            this.MontoPrestado = montoPrestado;
            this.PrestamoID = prestamoID;
            this.TasaEfectivaAnual = tasaEfectivaAnual;
            this.TasaEfectivaMensual = tasaEfectivaMensual;
            this.TotalPeriodosPago = totalPeriodosPago;
            this.CuotaFijaMensual = cuotaFijaMensual;
            this.fechaPrestamo = fechaPrestamo;
        }

        public void GenerarCronograma ()
        {
            fechaPrestamo = DateTime.Now;
            DateTime fecha;
            double saldo;
            double interes;
            double amortizacion;

            for (int i = 1; i <= totalPeriodosPago; i++)
            {
                interes = CalcularInteres();
                amortizacion = CalcularAmortizacion();
                saldo = CalcularSaldo();
                fecha = fechaPrestamo.AddMonths(i);
                Cuota cuota = new Cuota(i, saldo, fecha, amortizacion, interes);
                AgregarCuota(cuota);
            }
        }

        public void AgregarCuota(Cuota cuota)
        {
            listaCuotas.Add(cuota);
        }

        public bool ValidarMontoPrestado(float montoPrestado)
        {
            return (montoPrestado >= 1000 && montoPrestado <= 30000);
        }

        public bool ValidarTasaEfectivaAnual(double tasaEfectivaAnual)
        {
            return (tasaEfectivaAnual >= 0.1 && tasaEfectivaAnual <= 0.20);
        }

        public bool ValidarTotalPeriodosPago(int totalPeriodosPago)
        {
            return (totalPeriodosPago >= 3 && totalPeriodosPago <= 24);
        }
        public double CalcularTasaEfectivaMensual()
        {
            double numero = 1 + tasaEfectivaAnual;
            decimal exponente = 1 / 12M;
            double a = Math.Pow(numero, (double)exponente);
            return a - 1;
        }
        public double CalcularCuotaFijaMensual()
        {
            double tem = CalcularTasaEfectivaMensual();
            double n = totalPeriodosPago * -1;
            double dividendo = tem * montoPrestado;
            double divisor = 1 - Math.Pow(1 + tem, n);
            return dividendo / divisor;
        }

        public double ObtenerSaldoAnterior()
        {
            if (listaCuotas.Count==0)
                return montoPrestado;
            else
            {
                return listaCuotas.Last().Saldo;
            }
        }

        public double CalcularInteres()
        {
            return ObtenerSaldoAnterior() * tasaEfectivaMensual;
        }

        public double CalcularAmortizacion()
        {
            return CalcularCuotaFijaMensual() - CalcularInteres();
        }

        public double CalcularSaldo()
        {
            return ObtenerSaldoAnterior() - CalcularAmortizacion();
        }
    }
}
