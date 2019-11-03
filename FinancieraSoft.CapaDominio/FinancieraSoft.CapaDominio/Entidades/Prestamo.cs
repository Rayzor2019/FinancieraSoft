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
            tasaEfectivaMensual = CalcularTasaEfectivaMensual();
            cuotaFijaMensual = CalcularCuotaFijaMensual();
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

        public void GenerarCronograma ()
        {
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
                Cuota cuota = new Cuota(i, saldo, fecha, amortizacion, interes, this);
                AgregarCuota(cuota);
            }
        }

        public void AgregarCuota(Cuota cuota)
        {
            listaCuotas.Add(cuota);
        }

        //REGLAS DE NEGOCIO

        //REGLA 2 - El monto prestado debe ser mayor o igual a 10000 y menor o igual a 30000
        public bool TieneMontoPrestadoValido()
        {
            return (montoPrestado >= 1000 && montoPrestado <= 30000);
        }

        //REGLA 3 - La TEA debe ser mayor o igual a 0.10 y menor o igual a 0.20 
        public bool TieneTasaEfectivaAnualValido()
        {
            return (tasaEfectivaAnual >= 0.10 && tasaEfectivaAnual <= 0.20);
        }

        //REGLA 4 - El total de periodos de pago tiene que ser mayor o igual a 3 y menor o igual a 24
        public bool TieneTotalPeriodosPagoValido()
        {
            return (totalPeriodosPago >= 3 && totalPeriodosPago <= 24);
        }

        //REGLA 5 -  La TEM se calcula mediante la formula: i = (1+TEA)^(1/12)-1

            /* Donde:
             * i = TEM
             * TEA = Tasa Efectiva Anual /*

        //CalcularTEM antiguo, ver en consola si arroja el mismo resultado con el update descartar
        /*
        public double CalcularTasaEfectivaMensual()
        {
            double numero = (1 + tasaEfectivaAnual);
            decimal exponente = 1 / 12M;
            double a = Math.Pow(numero, (double)exponente);
            return a - 1;
        }*/

        public double CalcularTasaEfectivaMensual()
        {
            return Math.Pow((1 + tasaEfectivaAnual), (double)(1 / 12M)) - 1;
        }

        //REGLA 6 - La cuota fija mensual (A) : P = (A/i)*[1-(1+i)^-n] Se debe despejar A

            /*Donde:
             * P = MontoPrestado
             * i = TasaEfectivaMensual
             * n = total de Periodos de Pago */

        public double CalcularCuotaFijaMensual()
        {
            return (CalcularTasaEfectivaMensual() * montoPrestado) 
                 / (1 - Math.Pow(1 + CalcularTasaEfectivaMensual(), (totalPeriodosPago * -1)));
        }

        //REGLA 7 - Calcular Interes: Saldo del periodo anterior * i
            //Instanciamos un método para obtener el ultimo saldo
        public double ObtenerUltimoSaldo()
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
            return ObtenerUltimoSaldo() * tasaEfectivaMensual;
        }

        //REGLA 8 - Amortizacion = A - interés
        public double CalcularAmortizacion()
        {
            return CalcularCuotaFijaMensual() - CalcularInteres();
        }

        //REGLA 9 - Saldo = Saldo del periodo anterior - Amortizacion
        public double CalcularSaldo()
        {
            return ObtenerUltimoSaldo() - CalcularAmortizacion();
        }
    }
}
