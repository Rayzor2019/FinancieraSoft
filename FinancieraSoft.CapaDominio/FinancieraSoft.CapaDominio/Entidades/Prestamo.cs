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
        private double montoPrestado;
        private double tasaEfectivaAnual;
        private int totalPeriodosPago;
        private double tasaEfectivaMensual;
        private DateTime fechaPrestamo;
        private double cuotaFijaMensual;
        private Cliente cliente;
        private List<Cuota> listaCuotas;

        public string PrestamoID { get => prestamoID; set => prestamoID = value; }
        public double MontoPrestado { get => montoPrestado; set => montoPrestado = value; }
        public double TasaEfectivaAnual { get => tasaEfectivaAnual; set => tasaEfectivaAnual = value; }
        public int TotalPeriodosPago { get => totalPeriodosPago; set => totalPeriodosPago = value; }
        public double TasaEfectivaMensual { get => tasaEfectivaMensual; set => tasaEfectivaMensual = value; }
        public DateTime FechaPrestamo { get => fechaPrestamo; set => fechaPrestamo = value; }
        public double CuotaFijaMensual { get => cuotaFijaMensual; set => cuotaFijaMensual = value; }
        public Cliente Cliente { get => cliente; set => cliente = value; }
        public List<Cuota> ListaCuotas { get => listaCuotas; set => listaCuotas = value; }

        //Constructor de crear un prestamo

        public Prestamo(double montoPrestado, double tasaEfectivaAnual, int totalPeriodosPago, Cliente cliente)
        {
            //prestamoID = prestamoID; Autogenerar (con un metodo)
            this.montoPrestado = montoPrestado;
            this.tasaEfectivaAnual = tasaEfectivaAnual;
            this.totalPeriodosPago = totalPeriodosPago;
            this.cliente = cliente;
            fechaPrestamo = DateTime.Now;
            tasaEfectivaMensual = calcularTasaEfectivaMensual();
            cuotaFijaMensual = calcularCuotaFijaMensual();
            listaCuotas = new List<Cuota>();
        }

        //Constructor de obtener un prestamo
        public Prestamo(string prestamoID, double montoPrestado, double tasaEfectivaAnual, int totalPeriodosPago, double tasaEfectivaMensual, DateTime fechaPrestamo, double cuotaFijaMensual, Cliente cliente, List<Cuota> listaCuotas)
        {
            this.prestamoID = prestamoID;
            this.montoPrestado = montoPrestado;
            this.tasaEfectivaAnual = tasaEfectivaAnual;
            this.totalPeriodosPago = totalPeriodosPago;
            this.tasaEfectivaMensual = tasaEfectivaMensual;
            this.fechaPrestamo = fechaPrestamo;
            this.cuotaFijaMensual = cuotaFijaMensual;
            this.cliente = cliente;
            this.listaCuotas = listaCuotas;
        }

        public void generarCronograma ()
        {
            DateTime fecha;
            double saldo;
            double interes;
            double amortizacion;

            for (int i = 1; i <= totalPeriodosPago; i++)
            {
                interes = calcularInteres();
                amortizacion = calcularAmortizacion();
                saldo = calcularSaldo();
                fecha = fechaPrestamo.AddMonths(i);
                Cuota cuota = new Cuota(i, saldo, fecha, amortizacion, interes, this);
                agregarCuota(cuota);
            }
        }

        public void agregarCuota(Cuota cuota)
        {
            listaCuotas.Add(cuota);
        }

        public bool validarMontoPrestado(double montoPrestado)
        {
            return (montoPrestado >= 1000 && montoPrestado <= 30000);
        }

        public bool validarTasaEfectivaAnual(double tasaEfectivaAnual)
        {
            return (tasaEfectivaAnual >= 0.1 && tasaEfectivaAnual <= 0.20);
        }

        public bool validarTotalPeriodosPago(int totalPeriodosPago)
        {
            return (totalPeriodosPago >= 3 && totalPeriodosPago <= 24);
        }

        //calcularTEM antiguo, ver en consola si arroja el mismo resultado con el update
        /*
        public double calcularTasaEfectivaMensual()
        {
            double numero = (1 + tasaEfectivaAnual);
            decimal exponente = 1 / 12M;
            double a = Math.Pow(numero, (double)exponente);
            return a - 1;
        }*/

        public double calcularTasaEfectivaMensual()
        {
            return Math.Pow((1 + tasaEfectivaAnual), (double)(1 / 12M)) - 1;
        }

        public double calcularCuotaFijaMensual()
        {
            return (calcularTasaEfectivaMensual() * montoPrestado) 
                 / (1 - Math.Pow(1 + calcularTasaEfectivaMensual(), (totalPeriodosPago * -1)));
        }

        public double obtenerUltimoSaldo()
        {
            if (listaCuotas.Count==0)
                return montoPrestado;
            else
            {
                return listaCuotas.Last().Saldo;
            }
        }

        public double calcularInteres()
        {
            return obtenerUltimoSaldo() * tasaEfectivaMensual;
        }

        public double calcularAmortizacion()
        {
            return calcularCuotaFijaMensual() - calcularInteres();
        }

        public double calcularSaldo()
        {
            return obtenerUltimoSaldo() - calcularAmortizacion();
        }
    }
}
