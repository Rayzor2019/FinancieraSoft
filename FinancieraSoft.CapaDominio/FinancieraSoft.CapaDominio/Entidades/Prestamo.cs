using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Entidades
{
    public class Prestamo
    {
        private float montoPrestado;
        private string prestamoID;
        private double tasaEfectivaAnual;
        private double tasaEfectivaMensual;
        private int totalPeriodosPago;
        private double cuotaFijaMensual;

        public float MontoPrestado { get => montoPrestado; set => montoPrestado = value; }
        public string PrestamoID { get => prestamoID; set => prestamoID = value; }
        public double TasaEfectivaAnual { get => tasaEfectivaAnual; set => tasaEfectivaAnual = value; }
        public double TasaEfectivaMensual { get => tasaEfectivaMensual; set => tasaEfectivaMensual = value; }
        public int TotalPeriodosPago { get => totalPeriodosPago; set => totalPeriodosPago = value; }
        public double CuotaFijaMensual { get => cuotaFijaMensual; set => cuotaFijaMensual = value; }

        public Prestamo(Cliente cliente, float montoPrestado, double tasaEfectivaAnual, int totalPeriodosPago)
        {
            prestamoID = cliente.Codigo;
            if (ValidarMontoPrestado(montoPrestado))
                this.montoPrestado = montoPrestado;
            if (ValidarTasaEfectivaAnual(tasaEfectivaAnual))
                this.tasaEfectivaAnual = tasaEfectivaAnual;
            if (ValidarTotalPeriodosPago(totalPeriodosPago))
                this.totalPeriodosPago = totalPeriodosPago;
            tasaEfectivaMensual = CalcularTasaEfectivaMensual();
            cuotaFijaMensual = CalcularCuotaFijaMensual();
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
            double n = totalPeriodosPago * -1;
            double dividendo = tasaEfectivaMensual * montoPrestado;
            double divisor = 1 - Math.Pow(1 + tasaEfectivaMensual, n);
            return dividendo / divisor;
        }
    }
}
