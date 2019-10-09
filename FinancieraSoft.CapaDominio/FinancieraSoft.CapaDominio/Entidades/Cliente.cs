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

    }
}
