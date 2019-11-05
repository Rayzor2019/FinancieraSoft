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
    public class PagarCuotaServicio
    {
        private IGestorDAO gestorDAO;
        private IClienteDAO clienteDAO;
        private IPrestamoDAO prestamoDAO;
        private IPagoDAO pagoDAO;

        public PagarCuotaServicio()
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

        public Prestamo BuscarPrestamo(Cliente cliente)
        {
            gestorDAO.AbrirConexion();
            Prestamo prestamo = prestamoDAO.BuscarPrestamo(cliente);
            gestorDAO.CerrarConexion();
            return prestamo;
        }

        public bool TieneDeudaPendiente(Prestamo prestamo)
        {
            bool tieneDeudaPendiente = false;
            gestorDAO.AbrirConexion();
            tieneDeudaPendiente = prestamoDAO.TieneDeudaPendiente(prestamo);
            gestorDAO.CerrarConexion();
            return tieneDeudaPendiente;
            
        }

        public void GuardarPago(Pago pago, double montoRecibido)
        {
            PagoServicio pagoServicio = new PagoServicio();
            pagoServicio.ValidarVuelto(pago,montoRecibido);
            gestorDAO.AbrirConexion();
            gestorDAO.IniciarTransaccion();
            pagoDAO.Guardar(pago);
            gestorDAO.TerminarTransaccion();
            gestorDAO.CerrarConexion();
        }
    }
}
