namespace GestionBancariaWindows
{
    partial class ArqueoCaja
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblArqueoHeader = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblInfo = new System.Windows.Forms.Label();
            this.btnAceptar = new System.Windows.Forms.Button();
            this.panel3 = new System.Windows.Forms.Panel();
            this.txtCantidadCuotasPrestamos = new System.Windows.Forms.TextBox();
            this.lblCantidadTotalPrestamos = new System.Windows.Forms.Label();
            this.txtCantidadRetiros = new System.Windows.Forms.TextBox();
            this.lblCantidadRetiros = new System.Windows.Forms.Label();
            this.txtSaldoPesos = new System.Windows.Forms.TextBox();
            this.lblSaldoPesos = new System.Windows.Forms.Label();
            this.txtCantidadDepositos = new System.Windows.Forms.TextBox();
            this.lblCantidadDepositos = new System.Windows.Forms.Label();
            this.txtSaldoDolares = new System.Windows.Forms.TextBox();
            this.lblSaldoDolares = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblArqueoHeader);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(398, 53);
            this.panel1.TabIndex = 0;
            // 
            // lblArqueoHeader
            // 
            this.lblArqueoHeader.AutoSize = true;
            this.lblArqueoHeader.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblArqueoHeader.Location = new System.Drawing.Point(13, 16);
            this.lblArqueoHeader.Name = "lblArqueoHeader";
            this.lblArqueoHeader.Size = new System.Drawing.Size(156, 24);
            this.lblArqueoHeader.TabIndex = 0;
            this.lblArqueoHeader.Text = "Arqueo de Caja";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnAceptar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 235);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(398, 38);
            this.panel2.TabIndex = 1;
            // 
            // lblInfo
            // 
            this.lblInfo.AutoSize = true;
            this.lblInfo.Location = new System.Drawing.Point(10, 156);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(0, 13);
            this.lblInfo.TabIndex = 1;
            // 
            // btnAceptar
            // 
            this.btnAceptar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnAceptar.Location = new System.Drawing.Point(289, 0);
            this.btnAceptar.Name = "btnAceptar";
            this.btnAceptar.Size = new System.Drawing.Size(109, 38);
            this.btnAceptar.TabIndex = 0;
            this.btnAceptar.Text = "Aceptar";
            this.btnAceptar.UseVisualStyleBackColor = true;
            this.btnAceptar.Click += new System.EventHandler(this.btnAceptar_Click);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.lblInfo);
            this.panel3.Controls.Add(this.txtCantidadCuotasPrestamos);
            this.panel3.Controls.Add(this.lblCantidadTotalPrestamos);
            this.panel3.Controls.Add(this.txtCantidadRetiros);
            this.panel3.Controls.Add(this.lblCantidadRetiros);
            this.panel3.Controls.Add(this.txtSaldoPesos);
            this.panel3.Controls.Add(this.lblSaldoPesos);
            this.panel3.Controls.Add(this.txtCantidadDepositos);
            this.panel3.Controls.Add(this.lblCantidadDepositos);
            this.panel3.Controls.Add(this.txtSaldoDolares);
            this.panel3.Controls.Add(this.lblSaldoDolares);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 53);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(398, 182);
            this.panel3.TabIndex = 2;
            // 
            // txtCantidadCuotasPrestamos
            // 
            this.txtCantidadCuotasPrestamos.Location = new System.Drawing.Point(138, 83);
            this.txtCantidadCuotasPrestamos.Name = "txtCantidadCuotasPrestamos";
            this.txtCantidadCuotasPrestamos.Size = new System.Drawing.Size(42, 20);
            this.txtCantidadCuotasPrestamos.TabIndex = 9;
            // 
            // lblCantidadTotalPrestamos
            // 
            this.lblCantidadTotalPrestamos.AutoSize = true;
            this.lblCantidadTotalPrestamos.Location = new System.Drawing.Point(10, 86);
            this.lblCantidadTotalPrestamos.Name = "lblCantidadTotalPrestamos";
            this.lblCantidadTotalPrestamos.Size = new System.Drawing.Size(119, 13);
            this.lblCantidadTotalPrestamos.TabIndex = 8;
            this.lblCantidadTotalPrestamos.Text = "Total Cuotas Prestamos";
            // 
            // txtCantidadRetiros
            // 
            this.txtCantidadRetiros.Location = new System.Drawing.Point(321, 54);
            this.txtCantidadRetiros.Name = "txtCantidadRetiros";
            this.txtCantidadRetiros.Size = new System.Drawing.Size(42, 20);
            this.txtCantidadRetiros.TabIndex = 7;
            // 
            // lblCantidadRetiros
            // 
            this.lblCantidadRetiros.AutoSize = true;
            this.lblCantidadRetiros.Location = new System.Drawing.Point(230, 57);
            this.lblCantidadRetiros.Name = "lblCantidadRetiros";
            this.lblCantidadRetiros.Size = new System.Drawing.Size(85, 13);
            this.lblCantidadRetiros.TabIndex = 6;
            this.lblCantidadRetiros.Text = "Cantidad Retiros";
            // 
            // txtSaldoPesos
            // 
            this.txtSaldoPesos.Location = new System.Drawing.Point(321, 28);
            this.txtSaldoPesos.Name = "txtSaldoPesos";
            this.txtSaldoPesos.Size = new System.Drawing.Size(42, 20);
            this.txtSaldoPesos.TabIndex = 5;
            // 
            // lblSaldoPesos
            // 
            this.lblSaldoPesos.AutoSize = true;
            this.lblSaldoPesos.Location = new System.Drawing.Point(239, 31);
            this.lblSaldoPesos.Name = "lblSaldoPesos";
            this.lblSaldoPesos.Size = new System.Drawing.Size(66, 13);
            this.lblSaldoPesos.TabIndex = 4;
            this.lblSaldoPesos.Text = "Saldo Pesos";
            // 
            // txtCantidadDepositos
            // 
            this.txtCantidadDepositos.Location = new System.Drawing.Point(138, 54);
            this.txtCantidadDepositos.Name = "txtCantidadDepositos";
            this.txtCantidadDepositos.Size = new System.Drawing.Size(42, 20);
            this.txtCantidadDepositos.TabIndex = 3;
            // 
            // lblCantidadDepositos
            // 
            this.lblCantidadDepositos.AutoSize = true;
            this.lblCantidadDepositos.Location = new System.Drawing.Point(30, 61);
            this.lblCantidadDepositos.Name = "lblCantidadDepositos";
            this.lblCantidadDepositos.Size = new System.Drawing.Size(99, 13);
            this.lblCantidadDepositos.TabIndex = 2;
            this.lblCantidadDepositos.Text = "Cantidad Depositos";
            // 
            // txtSaldoDolares
            // 
            this.txtSaldoDolares.Location = new System.Drawing.Point(138, 28);
            this.txtSaldoDolares.Name = "txtSaldoDolares";
            this.txtSaldoDolares.Size = new System.Drawing.Size(42, 20);
            this.txtSaldoDolares.TabIndex = 1;
            // 
            // lblSaldoDolares
            // 
            this.lblSaldoDolares.AutoSize = true;
            this.lblSaldoDolares.Location = new System.Drawing.Point(56, 31);
            this.lblSaldoDolares.Name = "lblSaldoDolares";
            this.lblSaldoDolares.Size = new System.Drawing.Size(73, 13);
            this.lblSaldoDolares.TabIndex = 0;
            this.lblSaldoDolares.Text = "Saldo Dolares";
            // 
            // ArqueoCaja
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 273);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "ArqueoCaja";
            this.Text = "Arqueo de Caja";
            this.Load += new System.EventHandler(this.NuevaSucursal_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label lblArqueoHeader;
        private System.Windows.Forms.Button btnAceptar;
        private System.Windows.Forms.TextBox txtCantidadDepositos;
        private System.Windows.Forms.Label lblCantidadDepositos;
        private System.Windows.Forms.Label lblSaldoDolares;
        private System.Windows.Forms.TextBox txtSaldoDolares;
        private System.Windows.Forms.TextBox txtSaldoPesos;
        private System.Windows.Forms.Label lblSaldoPesos;
        private System.Windows.Forms.TextBox txtCantidadRetiros;
        private System.Windows.Forms.Label lblCantidadRetiros;
        private System.Windows.Forms.TextBox txtCantidadCuotasPrestamos;
        private System.Windows.Forms.Label lblCantidadTotalPrestamos;
        private System.Windows.Forms.Label lblInfo;
    }
}