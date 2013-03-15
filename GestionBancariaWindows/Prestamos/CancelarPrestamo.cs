﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestionBancariaWindows.GestionBancariaWS;


namespace GestionBancariaWindows
{
    public partial class CancelarPrestamo : Form
    {
        public CancelarPrestamo()
        {
            InitializeComponent();
        }

        public Prestamo PRESTAMO { get; set; }

        private void CancelarPrestamo_Load(object sender, EventArgs e)
        {
            try
            {
                ServiceGestionBancaria serv = new ServiceGestionBancaria();
               Sucursal[] sucursales = serv.ListarSucursales();

                SucursalbindingSource.DataSource = sucursales;
            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }


        private void btnBuscar_Click(object sender, EventArgs e)
        {
            try
            {
                Prestamo p = new Prestamo();
                int idPrestamo;
                if (Int32.TryParse(txtNumPrestamo.Text, out idPrestamo))
                {
                    p.IDPRESTAMO = idPrestamo;
                    Sucursal s = new Sucursal();
                    s.IDSUCURSAL = Convert.ToInt32(cmbSucursal.SelectedValue);
                    p.SUCURSAL = s;

                    ServiceGestionBancaria serv = new ServiceGestionBancaria();
                    Pago[] pagos = serv.IsPrestamoCancelado(ref p);

                    PRESTAMO = p;

                    if (p.CANCELADO)
                    {
                        btnAceptar.Enabled = true;
                    }
                    else
                    {
                        btnAceptar.Enabled = false;
                    }

                    PagosbindingSource.DataSource = pagos;
                    lvPagos.DataSource = PagosbindingSource;
                    bindingNavigator1.BindingSource = PagosbindingSource;
                }
            }
            catch (Exception ex)
            {

                lblInfo.Text = ex.Message;
            }
        }


        private void btnAceptar_Click(object sender, EventArgs e)
        {
            try
            {
                ServiceGestionBancaria serv = new ServiceGestionBancaria();
                serv.CancelarPrestamo(PRESTAMO);

                lblInfo.Text = "El prestamo fue cancelado.";
                btnAceptar.Visible = false;
            }
            catch (Exception ex)
            {
                lblInfo.Text = ex.Message;
            }
        }

    }
}
