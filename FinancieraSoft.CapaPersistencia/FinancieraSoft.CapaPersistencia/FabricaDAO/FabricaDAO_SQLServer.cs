using FinancieraSoft.CapaDominio.Contratos;
using FinancieraSoft.CapaPersistencia.SQLServerDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaPersistencia.FabricaDAO
{
    public class FabricaDAO_SQLServer : FabricaAbstractaDAO
    {
        public override IClienteDAO CrearClienteDAO(IGestorDAO gestorDAO)
        {
            return new ClienteDAO(gestorDAO);
        }

        public override IGestorDAO CrearGestorDAO()
        {
            return new GestorDAO();
        }

        public override IPrestamoDAO CrearPrestamoDAO(IGestorDAO gestorDAO)
        {
            return new PrestamoDAO(gestorDAO);
        }

        public override IPagoDAO CrearPagoDAO(IGestorDAO gestorDAO)
        {
            return new PagoDAO(gestorDAO);
        }
    }
}
