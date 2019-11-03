using FinancieraSoft.CapaDominio.Contratos;
using FinancieraSoft.CapaDominio.Entidades;
using FinancieraSoft.CapaDominio.Servicios;
using FinancieraSoft.CapaPersistencia.FabricaDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancieraSoft.CapaAplicacion.Servicios
{
    public class GenerarPrestamoServicio
    {
        private IGestorDAO gestorDAO;
        private IClienteDAO clienteDAO;
        private IPrestamoDAO prestamoDAO;
        private IPagoDAO pagoDAO;


        public GenerarPrestamoServicio()
        {
            FabricaAbstractaDAO fabricaDAO = FabricaAbstractaDAO.Instanciar();
            gestorDAO = fabricaDAO.CrearGestorDAO();
            prestamoDAO = fabricaDAO.CrearPrestamoDAO(gestorDAO);
            clienteDAO = fabricaDAO.CrearClienteDAO(gestorDAO);
            pagoDAO = fabricaDAO.CrearPagoDAO(gestorDAO);
        }

        public Cliente BuscarCliente(string dni)
        {
            gestorDAO.AbrirConexion();
            Cliente cliente = clienteDAO.Buscar(dni);
            gestorDAO.CerrarConexion();
            return cliente;
        }

        public void GenerarCronograma(double montoPrestado, double tasaEfectivaAnual, int totalPeriodosPago, Cliente cliente)
        {
            Prestamo prestamo = new Prestamo(montoPrestado, tasaEfectivaAnual, totalPeriodosPago, cliente);
            prestamo.GenerarCronograma();
        }

        public void GuardarPrestamo(Prestamo prestamo)
        {
            PrestamoServicio prestamoServicio = new PrestamoServicio();
            prestamoServicio.VerificarDatosPrestamo(prestamo);
            gestorDAO.IniciarTransaccion();
            prestamoDAO.Guardar(prestamo);
            gestorDAO.TerminarTransaccion();
        }
    }
}
