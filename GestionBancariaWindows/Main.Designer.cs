namespace GestionBancariaWindows
{
    partial class Main
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.clientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoClienteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listarClientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cuentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.retiroToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.depositoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevaCuentaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listarCuentasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.empleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoEmpleadoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listarEmpleadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cotizacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevaCotizacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listarCotizacionesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prestamosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevoPrestamoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelacionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pagoCuotaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.prestamosAtrasadosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sucursalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nuevaSucursalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.arqueoDeCajaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listadoDeProductividadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listarSucursalesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.clientesToolStripMenuItem,
            this.cuentasToolStripMenuItem,
            this.empleadosToolStripMenuItem,
            this.cotizacionesToolStripMenuItem,
            this.prestamosToolStripMenuItem,
            this.sucursalesToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(614, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // clientesToolStripMenuItem
            // 
            this.clientesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoClienteToolStripMenuItem,
            this.listarClientesToolStripMenuItem});
            this.clientesToolStripMenuItem.Name = "clientesToolStripMenuItem";
            this.clientesToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.clientesToolStripMenuItem.Text = "Clientes";
            // 
            // nuevoClienteToolStripMenuItem
            // 
            this.nuevoClienteToolStripMenuItem.Name = "nuevoClienteToolStripMenuItem";
            this.nuevoClienteToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.nuevoClienteToolStripMenuItem.Text = "Nuevo Cliente";
            this.nuevoClienteToolStripMenuItem.Click += new System.EventHandler(this.nuevoClienteToolStripMenuItem_Click);
            // 
            // listarClientesToolStripMenuItem
            // 
            this.listarClientesToolStripMenuItem.Name = "listarClientesToolStripMenuItem";
            this.listarClientesToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.listarClientesToolStripMenuItem.Text = "Listar Clientes";
            this.listarClientesToolStripMenuItem.Click += new System.EventHandler(this.listarClientesToolStripMenuItem_Click);
            // 
            // cuentasToolStripMenuItem
            // 
            this.cuentasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.retiroToolStripMenuItem,
            this.depositoToolStripMenuItem,
            this.nuevaCuentaToolStripMenuItem,
            this.listarCuentasToolStripMenuItem});
            this.cuentasToolStripMenuItem.Name = "cuentasToolStripMenuItem";
            this.cuentasToolStripMenuItem.Size = new System.Drawing.Size(62, 20);
            this.cuentasToolStripMenuItem.Text = "Cuentas";
            // 
            // retiroToolStripMenuItem
            // 
            this.retiroToolStripMenuItem.Name = "retiroToolStripMenuItem";
            this.retiroToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.retiroToolStripMenuItem.Text = "Retiro";
            this.retiroToolStripMenuItem.Click += new System.EventHandler(this.retiroToolStripMenuItem_Click);
            // 
            // depositoToolStripMenuItem
            // 
            this.depositoToolStripMenuItem.Name = "depositoToolStripMenuItem";
            this.depositoToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.depositoToolStripMenuItem.Text = "Deposito";
            this.depositoToolStripMenuItem.Click += new System.EventHandler(this.depositoToolStripMenuItem_Click);
            // 
            // nuevaCuentaToolStripMenuItem
            // 
            this.nuevaCuentaToolStripMenuItem.Name = "nuevaCuentaToolStripMenuItem";
            this.nuevaCuentaToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.nuevaCuentaToolStripMenuItem.Text = "Nueva Cuenta";
            this.nuevaCuentaToolStripMenuItem.Click += new System.EventHandler(this.nuevaCuentaToolStripMenuItem_Click);
            // 
            // listarCuentasToolStripMenuItem
            // 
            this.listarCuentasToolStripMenuItem.Name = "listarCuentasToolStripMenuItem";
            this.listarCuentasToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.listarCuentasToolStripMenuItem.Text = "Listar Cuentas";
            this.listarCuentasToolStripMenuItem.Click += new System.EventHandler(this.listarCuentasToolStripMenuItem_Click);
            // 
            // empleadosToolStripMenuItem
            // 
            this.empleadosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoEmpleadoToolStripMenuItem,
            this.listarEmpleadosToolStripMenuItem});
            this.empleadosToolStripMenuItem.Name = "empleadosToolStripMenuItem";
            this.empleadosToolStripMenuItem.Size = new System.Drawing.Size(77, 20);
            this.empleadosToolStripMenuItem.Text = "Empleados";
            // 
            // nuevoEmpleadoToolStripMenuItem
            // 
            this.nuevoEmpleadoToolStripMenuItem.Name = "nuevoEmpleadoToolStripMenuItem";
            this.nuevoEmpleadoToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.nuevoEmpleadoToolStripMenuItem.Text = "Nuevo Empleado";
            this.nuevoEmpleadoToolStripMenuItem.Click += new System.EventHandler(this.nuevoEmpleadoToolStripMenuItem_Click);
            // 
            // listarEmpleadosToolStripMenuItem
            // 
            this.listarEmpleadosToolStripMenuItem.Name = "listarEmpleadosToolStripMenuItem";
            this.listarEmpleadosToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.listarEmpleadosToolStripMenuItem.Text = "Listar Empleados";
            this.listarEmpleadosToolStripMenuItem.Click += new System.EventHandler(this.listarEmpleadosToolStripMenuItem_Click);
            // 
            // cotizacionesToolStripMenuItem
            // 
            this.cotizacionesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevaCotizacionToolStripMenuItem,
            this.listarCotizacionesToolStripMenuItem});
            this.cotizacionesToolStripMenuItem.Name = "cotizacionesToolStripMenuItem";
            this.cotizacionesToolStripMenuItem.Size = new System.Drawing.Size(86, 20);
            this.cotizacionesToolStripMenuItem.Text = "Cotizaciones";
            // 
            // nuevaCotizacionToolStripMenuItem
            // 
            this.nuevaCotizacionToolStripMenuItem.Name = "nuevaCotizacionToolStripMenuItem";
            this.nuevaCotizacionToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.nuevaCotizacionToolStripMenuItem.Text = "Nueva Cotizacion";
            this.nuevaCotizacionToolStripMenuItem.Click += new System.EventHandler(this.nuevaCotizacionToolStripMenuItem_Click);
            // 
            // listarCotizacionesToolStripMenuItem
            // 
            this.listarCotizacionesToolStripMenuItem.Name = "listarCotizacionesToolStripMenuItem";
            this.listarCotizacionesToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.listarCotizacionesToolStripMenuItem.Text = "Listar Cotizaciones";
            this.listarCotizacionesToolStripMenuItem.Click += new System.EventHandler(this.listarCotizacionesToolStripMenuItem_Click);
            // 
            // prestamosToolStripMenuItem
            // 
            this.prestamosToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevoPrestamoToolStripMenuItem,
            this.cancelacionToolStripMenuItem,
            this.pagoCuotaToolStripMenuItem,
            this.prestamosAtrasadosToolStripMenuItem});
            this.prestamosToolStripMenuItem.Name = "prestamosToolStripMenuItem";
            this.prestamosToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.prestamosToolStripMenuItem.Text = "Prestamos";
            // 
            // nuevoPrestamoToolStripMenuItem
            // 
            this.nuevoPrestamoToolStripMenuItem.Name = "nuevoPrestamoToolStripMenuItem";
            this.nuevoPrestamoToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.nuevoPrestamoToolStripMenuItem.Text = "Nuevo Prestamo";
            this.nuevoPrestamoToolStripMenuItem.Click += new System.EventHandler(this.nuevoPrestamoToolStripMenuItem_Click);
            // 
            // cancelacionToolStripMenuItem
            // 
            this.cancelacionToolStripMenuItem.Name = "cancelacionToolStripMenuItem";
            this.cancelacionToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.cancelacionToolStripMenuItem.Text = "Cancelacion";
            this.cancelacionToolStripMenuItem.Click += new System.EventHandler(this.cancelacionToolStripMenuItem_Click);
            // 
            // pagoCuotaToolStripMenuItem
            // 
            this.pagoCuotaToolStripMenuItem.Name = "pagoCuotaToolStripMenuItem";
            this.pagoCuotaToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.pagoCuotaToolStripMenuItem.Text = "Pago Cuota";
            this.pagoCuotaToolStripMenuItem.Click += new System.EventHandler(this.pagoCuotaToolStripMenuItem_Click);
            // 
            // prestamosAtrasadosToolStripMenuItem
            // 
            this.prestamosAtrasadosToolStripMenuItem.Name = "prestamosAtrasadosToolStripMenuItem";
            this.prestamosAtrasadosToolStripMenuItem.Size = new System.Drawing.Size(184, 22);
            this.prestamosAtrasadosToolStripMenuItem.Text = "Prestamos Atrasados";
            this.prestamosAtrasadosToolStripMenuItem.Click += new System.EventHandler(this.prestamosAtrasadosToolStripMenuItem_Click);
            // 
            // sucursalesToolStripMenuItem
            // 
            this.sucursalesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nuevaSucursalToolStripMenuItem,
            this.arqueoDeCajaToolStripMenuItem,
            this.listadoDeProductividadToolStripMenuItem,
            this.listarSucursalesToolStripMenuItem});
            this.sucursalesToolStripMenuItem.Name = "sucursalesToolStripMenuItem";
            this.sucursalesToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
            this.sucursalesToolStripMenuItem.Text = "Sucursales";
            // 
            // nuevaSucursalToolStripMenuItem
            // 
            this.nuevaSucursalToolStripMenuItem.Name = "nuevaSucursalToolStripMenuItem";
            this.nuevaSucursalToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.nuevaSucursalToolStripMenuItem.Text = "Nueva Sucursal";
            this.nuevaSucursalToolStripMenuItem.Click += new System.EventHandler(this.nuevaSucursalToolStripMenuItem_Click);
            // 
            // arqueoDeCajaToolStripMenuItem
            // 
            this.arqueoDeCajaToolStripMenuItem.Name = "arqueoDeCajaToolStripMenuItem";
            this.arqueoDeCajaToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.arqueoDeCajaToolStripMenuItem.Text = "Arqueo de Caja";
            this.arqueoDeCajaToolStripMenuItem.Click += new System.EventHandler(this.arqueoDeCajaToolStripMenuItem_Click);
            // 
            // listadoDeProductividadToolStripMenuItem
            // 
            this.listadoDeProductividadToolStripMenuItem.Name = "listadoDeProductividadToolStripMenuItem";
            this.listadoDeProductividadToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.listadoDeProductividadToolStripMenuItem.Text = "Listado de Productividad";
            this.listadoDeProductividadToolStripMenuItem.Click += new System.EventHandler(this.listadoDeProductividadToolStripMenuItem_Click);
            // 
            // listarSucursalesToolStripMenuItem
            // 
            this.listarSucursalesToolStripMenuItem.Name = "listarSucursalesToolStripMenuItem";
            this.listarSucursalesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
            this.listarSucursalesToolStripMenuItem.Text = "Listar Sucursales";
            this.listarSucursalesToolStripMenuItem.Click += new System.EventHandler(this.listarSucursalesToolStripMenuItem_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 347);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Main";
            this.Text = "Bienvenido - Sistema de Gestion Bancaria";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem sucursalesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem empleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cuentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cotizacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoClienteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem retiroToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem depositoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevaCuentaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoEmpleadoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevaCotizacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prestamosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevoPrestamoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cancelacionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pagoCuotaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem prestamosAtrasadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nuevaSucursalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem arqueoDeCajaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listadoDeProductividadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listarClientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listarCuentasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listarEmpleadosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listarCotizacionesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listarSucursalesToolStripMenuItem;
    }
}

