using FinancieraSoft.CapaDominio.Contratos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaPersistencia.FabricaDAO
{
    public abstract class FabricaAbstractaDAO
    {
        public static FabricaAbstractaDAO Instanciar()
        {
            Type tipoClaseFabricaDAO = Type.GetType(ClaseFabricaDAO.Default.NombreDeClaseFabricaDAO);
            FabricaAbstractaDAO fabricaAbstractaDAO = (FabricaAbstractaDAO)Activator.CreateInstance(tipoClaseFabricaDAO);
            return fabricaAbstractaDAO;
        }

        public abstract IGestorDAO CrearGestorDAO();
        public abstract IClienteDAO CrearClienteDAO(IGestorDAO gestorDAO);
        public abstract IPrestamoDAO CrearPrestamoDAO(IGestorDAO gestorDAO);
        public abstract IPagoDAO CrearPagoDAO(IGestorDAO gestorDAO);

    }
}
