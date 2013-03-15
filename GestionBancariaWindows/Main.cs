using System.Windows.Forms;
using Entidades;
namespace GestionBancariaWindows
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        public Empleado EMPLEADO { get;set;}

        private void arqueoDeCajaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ArqueoCaja arqueoCajaForm = new ArqueoCaja();
            arqueoCajaForm.EMPLEADO = EMPLEADO;
            arqueoCajaForm.Show();
        }

        private void nuevoClienteToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            NuevoCliente nuevoClienteForm = new NuevoCliente();
            nuevoClienteForm.Show();
        }

        private void listarClientesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ListarClientes listarClientesForm = new ListarClientes();
            listarClientesForm.Show();
        }

        private void retiroToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            RetiroCuenta retiroCuentaForm = new RetiroCuenta();
            retiroCuentaForm.EMPLEADO = EMPLEADO;
            retiroCuentaForm.Show();
        }

        private void depositoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            DepositoCuenta depositoCuentaForm = new DepositoCuenta();
            depositoCuentaForm.EMPLEADO = EMPLEADO;
            depositoCuentaForm.Show();
        }

        private void nuevaCuentaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            NuevaCuenta nuevaCuentaForm = new NuevaCuenta();
            nuevaCuentaForm.Show();
        }

        private void listarCuentasToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ListarCuentas listarCuentasForm = new ListarCuentas();
            listarCuentasForm.Show();
        }

        private void nuevoEmpleadoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            NuevoEmpleado nuevoEmpleado = new NuevoEmpleado();
            nuevoEmpleado.Show();
        }

        private void listarEmpleadosToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ListarEmpleados listarEmpleados = new ListarEmpleados();
            listarEmpleados.Show();
        }

        private void nuevaCotizacionToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            NuevaCotizacion nuevaCotizacion = new NuevaCotizacion();
            nuevaCotizacion.Show();
        }

        private void listarCotizacionesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ListarCotizaciones listarCotiazaciones = new ListarCotizaciones();
            listarCotiazaciones.Show();
        }

        private void nuevoPrestamoToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            NuevoPrestamo nuevoPrestamoForm = new NuevoPrestamo();
            nuevoPrestamoForm.Show();
        }

        private void cancelacionToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            CancelarPrestamo cancelarPrestamoForm = new CancelarPrestamo();
            cancelarPrestamoForm.Show();
        }

        private void pagoCuotaToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            PagoCuota pagoCuotaForm = new PagoCuota();
            pagoCuotaForm.Show();
        }

        private void prestamosAtrasadosToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            PrestamosAtrasados prestamosAtrasadosForm = new PrestamosAtrasados();
            prestamosAtrasadosForm.EMPLEADO = EMPLEADO;
            prestamosAtrasadosForm.Show();
        }

        private void nuevaSucursalToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            NuevaSucursal nuevaSucursalForm = new NuevaSucursal();
            nuevaSucursalForm.Show();
        }

        private void listadoDeProductividadToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ListadoProductividad listadoProductividadForm = new ListadoProductividad();
            listadoProductividadForm.Show();
        }

        private void listarSucursalesToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            ListarSucursales ls = new ListarSucursales();
            ls.Show();
        }

       
    }
}
