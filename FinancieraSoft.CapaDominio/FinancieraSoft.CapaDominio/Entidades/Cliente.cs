using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaDominio.Entidades
{
    public class Cliente
    {
        private string apellidos;
        private string codigo;
        private string dni;
        private string nombres;

        public string Apellidos { get => apellidos; set => apellidos = value; }
        public string Codigo { get => codigo; set => codigo = value; }
        public string Dni { get => dni; set => dni = value; }
        public string Nombres { get => nombres; set => nombres = value; }

        public Cliente(string dni, string apellidos, string nombres)
        {
            this.codigo = "AX20" + dni;
            this.dni = dni;
            this.apellidos = apellidos;
            this.nombres = nombres;
        }

        /* Ejemplo Consola
        public Cliente()
        {
            Console.Write("Ingrese dni: ");
            dni = Console.ReadLine();
            Console.Write("Ingrese apellidos: ");
            apellidos = Console.ReadLine();
            Console.Write("Ingrese nombres: ");
            nombres = Console.ReadLine();
            codigo = dni + "AX20";
        }
        
        public void Imprimir()
        {
            Console.WriteLine("Codigo: " + codigo);
            Console.WriteLine("Apellidos: " + apellidos);
            Console.WriteLine("Nombres: " + nombres);
        }
        */
    }
}
