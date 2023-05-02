namespace AutomataCelular
{
    partial class FormAutomata
    {
		/// <summary>
		/// Variable del diseñador necesaria.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Limpiar los recursos que se estén usando.
		/// </summary>
		/// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
		/// el contenido de este método con el editor de código.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormAutomata));
            this.pbAutomata = new System.Windows.Forms.PictureBox();
            this.timerAutomata = new System.Windows.Forms.Timer(this.components);
            this.plGeneral = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.tbInfeccionesNecesarias = new System.Windows.Forms.TextBox();
            this.btnResumenEvolucion = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.tbDiasEvolucionVirus = new System.Windows.Forms.TextBox();
            this.cbTamPixel = new System.Windows.Forms.ComboBox();
            this.btnAvanzar = new System.Windows.Forms.Button();
            this.lblDia = new System.Windows.Forms.Label();
            this.btnPausarSimulacion = new System.Windows.Forms.Button();
            this.btnStartProcess = new System.Windows.Forms.Button();
            this.lblFallecidos = new System.Windows.Forms.Label();
            this.lblHospitalizados = new System.Windows.Forms.Label();
            this.lblInmunes = new System.Windows.Forms.Label();
            this.lblAsintomaticos = new System.Windows.Forms.Label();
            this.lblContagiados = new System.Windows.Forms.Label();
            this.lblSanos = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.btnPintarIniciales = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tbPorInmunesIniciales = new System.Windows.Forms.TextBox();
            this.tbPorFallecidosIniciales = new System.Windows.Forms.TextBox();
            this.tbPorUCIIniciales = new System.Windows.Forms.TextBox();
            this.tbProbabilidadHospitalizacion = new System.Windows.Forms.TextBox();
            this.tbProbabilidadMorir = new System.Windows.Forms.TextBox();
            this.tbPorAsintomaticosIniciales = new System.Windows.Forms.TextBox();
            this.tbPorContagiadosIniciales = new System.Windows.Forms.TextBox();
            this.tbPorSanosIniciales = new System.Windows.Forms.TextBox();
            this.plPictureBox = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.pbAutomata)).BeginInit();
            this.plGeneral.SuspendLayout();
            this.plPictureBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // pbAutomata
            // 
            this.pbAutomata.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.pbAutomata, "pbAutomata");
            this.pbAutomata.Name = "pbAutomata";
            this.pbAutomata.TabStop = false;
            // 
            // timerAutomata
            // 
            this.timerAutomata.Tick += new System.EventHandler(this.timerAutomata_Tick);
            // 
            // plGeneral
            // 
            resources.ApplyResources(this.plGeneral, "plGeneral");
            this.plGeneral.Controls.Add(this.label10);
            this.plGeneral.Controls.Add(this.tbInfeccionesNecesarias);
            this.plGeneral.Controls.Add(this.btnResumenEvolucion);
            this.plGeneral.Controls.Add(this.label1);
            this.plGeneral.Controls.Add(this.tbDiasEvolucionVirus);
            this.plGeneral.Controls.Add(this.cbTamPixel);
            this.plGeneral.Controls.Add(this.btnAvanzar);
            this.plGeneral.Controls.Add(this.lblDia);
            this.plGeneral.Controls.Add(this.btnPausarSimulacion);
            this.plGeneral.Controls.Add(this.btnStartProcess);
            this.plGeneral.Controls.Add(this.lblFallecidos);
            this.plGeneral.Controls.Add(this.lblHospitalizados);
            this.plGeneral.Controls.Add(this.lblInmunes);
            this.plGeneral.Controls.Add(this.lblAsintomaticos);
            this.plGeneral.Controls.Add(this.lblContagiados);
            this.plGeneral.Controls.Add(this.lblSanos);
            this.plGeneral.Controls.Add(this.label11);
            this.plGeneral.Controls.Add(this.btnPintarIniciales);
            this.plGeneral.Controls.Add(this.label9);
            this.plGeneral.Controls.Add(this.label8);
            this.plGeneral.Controls.Add(this.label7);
            this.plGeneral.Controls.Add(this.label6);
            this.plGeneral.Controls.Add(this.label5);
            this.plGeneral.Controls.Add(this.label4);
            this.plGeneral.Controls.Add(this.label3);
            this.plGeneral.Controls.Add(this.label2);
            this.plGeneral.Controls.Add(this.tbPorInmunesIniciales);
            this.plGeneral.Controls.Add(this.tbPorFallecidosIniciales);
            this.plGeneral.Controls.Add(this.tbPorUCIIniciales);
            this.plGeneral.Controls.Add(this.tbProbabilidadHospitalizacion);
            this.plGeneral.Controls.Add(this.tbProbabilidadMorir);
            this.plGeneral.Controls.Add(this.tbPorAsintomaticosIniciales);
            this.plGeneral.Controls.Add(this.tbPorContagiadosIniciales);
            this.plGeneral.Controls.Add(this.tbPorSanosIniciales);
            this.plGeneral.Controls.Add(this.plPictureBox);
            this.plGeneral.Name = "plGeneral";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // tbInfeccionesNecesarias
            // 
            resources.ApplyResources(this.tbInfeccionesNecesarias, "tbInfeccionesNecesarias");
            this.tbInfeccionesNecesarias.Name = "tbInfeccionesNecesarias";
            // 
            // btnResumenEvolucion
            // 
            resources.ApplyResources(this.btnResumenEvolucion, "btnResumenEvolucion");
            this.btnResumenEvolucion.Name = "btnResumenEvolucion";
            this.btnResumenEvolucion.UseVisualStyleBackColor = true;
            this.btnResumenEvolucion.Click += new System.EventHandler(this.btnResumenEvolucion_Click);
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // tbDiasEvolucionVirus
            // 
            resources.ApplyResources(this.tbDiasEvolucionVirus, "tbDiasEvolucionVirus");
            this.tbDiasEvolucionVirus.Name = "tbDiasEvolucionVirus";
            // 
            // cbTamPixel
            // 
            this.cbTamPixel.FormattingEnabled = true;
            this.cbTamPixel.Items.AddRange(new object[] {
            resources.GetString("cbTamPixel.Items"),
            resources.GetString("cbTamPixel.Items1"),
            resources.GetString("cbTamPixel.Items2"),
            resources.GetString("cbTamPixel.Items3"),
            resources.GetString("cbTamPixel.Items4")});
            resources.ApplyResources(this.cbTamPixel, "cbTamPixel");
            this.cbTamPixel.Name = "cbTamPixel";
            // 
            // btnAvanzar
            // 
            resources.ApplyResources(this.btnAvanzar, "btnAvanzar");
            this.btnAvanzar.Name = "btnAvanzar";
            this.btnAvanzar.UseVisualStyleBackColor = true;
            this.btnAvanzar.Click += new System.EventHandler(this.btnAvanzar_Click);
            // 
            // lblDia
            // 
            resources.ApplyResources(this.lblDia, "lblDia");
            this.lblDia.Name = "lblDia";
            // 
            // btnPausarSimulacion
            // 
            resources.ApplyResources(this.btnPausarSimulacion, "btnPausarSimulacion");
            this.btnPausarSimulacion.Name = "btnPausarSimulacion";
            this.btnPausarSimulacion.UseVisualStyleBackColor = true;
            this.btnPausarSimulacion.Click += new System.EventHandler(this.btnPausarSimulacion_Click);
            // 
            // btnStartProcess
            // 
            resources.ApplyResources(this.btnStartProcess, "btnStartProcess");
            this.btnStartProcess.Name = "btnStartProcess";
            this.btnStartProcess.UseVisualStyleBackColor = true;
            this.btnStartProcess.Click += new System.EventHandler(this.btnStartProcess_Click);
            // 
            // lblFallecidos
            // 
            resources.ApplyResources(this.lblFallecidos, "lblFallecidos");
            this.lblFallecidos.Name = "lblFallecidos";
            // 
            // lblHospitalizados
            // 
            resources.ApplyResources(this.lblHospitalizados, "lblHospitalizados");
            this.lblHospitalizados.Name = "lblHospitalizados";
            // 
            // lblInmunes
            // 
            resources.ApplyResources(this.lblInmunes, "lblInmunes");
            this.lblInmunes.Name = "lblInmunes";
            // 
            // lblAsintomaticos
            // 
            resources.ApplyResources(this.lblAsintomaticos, "lblAsintomaticos");
            this.lblAsintomaticos.Name = "lblAsintomaticos";
            // 
            // lblContagiados
            // 
            resources.ApplyResources(this.lblContagiados, "lblContagiados");
            this.lblContagiados.Name = "lblContagiados";
            // 
            // lblSanos
            // 
            resources.ApplyResources(this.lblSanos, "lblSanos");
            this.lblSanos.Name = "lblSanos";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // btnPintarIniciales
            // 
            resources.ApplyResources(this.btnPintarIniciales, "btnPintarIniciales");
            this.btnPintarIniciales.Name = "btnPintarIniciales";
            this.btnPintarIniciales.UseVisualStyleBackColor = true;
            this.btnPintarIniciales.Click += new System.EventHandler(this.btnPintarIniciales_Click);
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // tbPorInmunesIniciales
            // 
            resources.ApplyResources(this.tbPorInmunesIniciales, "tbPorInmunesIniciales");
            this.tbPorInmunesIniciales.Name = "tbPorInmunesIniciales";
            // 
            // tbPorFallecidosIniciales
            // 
            resources.ApplyResources(this.tbPorFallecidosIniciales, "tbPorFallecidosIniciales");
            this.tbPorFallecidosIniciales.Name = "tbPorFallecidosIniciales";
            // 
            // tbPorUCIIniciales
            // 
            resources.ApplyResources(this.tbPorUCIIniciales, "tbPorUCIIniciales");
            this.tbPorUCIIniciales.Name = "tbPorUCIIniciales";
            // 
            // tbProbabilidadHospitalizacion
            // 
            resources.ApplyResources(this.tbProbabilidadHospitalizacion, "tbProbabilidadHospitalizacion");
            this.tbProbabilidadHospitalizacion.Name = "tbProbabilidadHospitalizacion";
            // 
            // tbProbabilidadMorir
            // 
            resources.ApplyResources(this.tbProbabilidadMorir, "tbProbabilidadMorir");
            this.tbProbabilidadMorir.Name = "tbProbabilidadMorir";
            // 
            // tbPorAsintomaticosIniciales
            // 
            resources.ApplyResources(this.tbPorAsintomaticosIniciales, "tbPorAsintomaticosIniciales");
            this.tbPorAsintomaticosIniciales.Name = "tbPorAsintomaticosIniciales";
            // 
            // tbPorContagiadosIniciales
            // 
            resources.ApplyResources(this.tbPorContagiadosIniciales, "tbPorContagiadosIniciales");
            this.tbPorContagiadosIniciales.Name = "tbPorContagiadosIniciales";
            // 
            // tbPorSanosIniciales
            // 
            resources.ApplyResources(this.tbPorSanosIniciales, "tbPorSanosIniciales");
            this.tbPorSanosIniciales.Name = "tbPorSanosIniciales";
            // 
            // plPictureBox
            // 
            resources.ApplyResources(this.plPictureBox, "plPictureBox");
            this.plPictureBox.Controls.Add(this.pbAutomata);
            this.plPictureBox.Name = "plPictureBox";
            // 
            // FormAutomata
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.plGeneral);
            this.MaximizeBox = false;
            this.Name = "FormAutomata";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pbAutomata)).EndInit();
            this.plGeneral.ResumeLayout(false);
            this.plGeneral.PerformLayout();
            this.plPictureBox.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PictureBox pbAutomata;
		private System.Windows.Forms.Timer timerAutomata;
        private System.Windows.Forms.Panel plGeneral;
        private System.Windows.Forms.Panel plPictureBox;
        private System.Windows.Forms.TextBox tbPorContagiadosIniciales;
        private System.Windows.Forms.TextBox tbPorSanosIniciales;
        private System.Windows.Forms.TextBox tbPorAsintomaticosIniciales;
        private System.Windows.Forms.TextBox tbProbabilidadMorir;
        private System.Windows.Forms.TextBox tbPorUCIIniciales;
        private System.Windows.Forms.TextBox tbPorFallecidosIniciales;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Button btnPintarIniciales;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tbPorInmunesIniciales;
        private System.Windows.Forms.Label lblFallecidos;
        private System.Windows.Forms.Label lblHospitalizados;
        private System.Windows.Forms.Label lblInmunes;
        private System.Windows.Forms.Label lblAsintomaticos;
        private System.Windows.Forms.Label lblContagiados;
        private System.Windows.Forms.Label lblSanos;
        private System.Windows.Forms.Button btnStartProcess;
        private System.Windows.Forms.Button btnPausarSimulacion;
        private System.Windows.Forms.Label lblDia;
        private System.Windows.Forms.Button btnAvanzar;
        private System.Windows.Forms.ComboBox cbTamPixel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tbDiasEvolucionVirus;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbProbabilidadHospitalizacion;
        private System.Windows.Forms.Button btnResumenEvolucion;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox tbInfeccionesNecesarias;
    }
}

