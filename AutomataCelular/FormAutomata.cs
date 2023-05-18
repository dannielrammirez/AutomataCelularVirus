using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using static System.Windows.Forms.LinkLabel;

namespace AutomataCelular
{
    public partial class FormAutomata : Form
    {
        private static FormAutomata instance = null;
        public static FormAutomata Instance { get { return instance; } }

        private int longitudPixel = 0;
        private double factorMultiplicador;
        private double _numPoblacion = 0;
        private double _numSanos = 0;
        private double _numContagiados = 0;
        private double _numAsintomaticos = 0;
        private double _numInmunes = 0;
        private double _numHospitalizados = 0;
        private double _numFallecidos = 0;
        public int longitud = 0;
        public int countDias = 0;
        public int[,] arrayPersonas;
        public double _probabilidadMorir = 0;
        public double _probabilidadMovimiento = 0;
        public double _probabilidadHospitalizacion = 0;
        public int _VecinasNecesariasParaInfeccion = 0;
        public int _diasEvolucionVirus = 0;
        public double _probabilidadInfeccion = 0;

        public Persona[,] PrevObjpersonas;
        public Persona[,] Objpersonas;
        public Automata _objAutomata = null;
        public List<ResumenDia> _resumenDia;
        private Bitmap bmp = null;

        public FormAutomata()
        {
            InitializeComponent();
            if (instance == null) instance = this;
            _objAutomata = new Automata();
            _resumenDia = new List<ResumenDia>();
        }

        private void ReiniciarRejilla()
        {
            for (int i = 0; i < longitud; i++)
                for (int j = 0; j < longitud; j++)
                {
                    Objpersonas[i, j] = new Persona(i, j);
                    arrayPersonas[i, j] = 0;
                }

            PintarMatriz();
        }

        private void TimerAutomata_Tick(object sender, EventArgs e)
        {
            ProcessEvolucion();
        }

        private bool ValidarInformacionInicial()
        {
            try
            {
                _numSanos = GetValFromPorc(_numPoblacion, tbPorSanosIniciales.Text) * factorMultiplicador;
                _numContagiados = GetValFromPorc(_numPoblacion, tbPorContagiadosIniciales.Text) * factorMultiplicador;
                _numAsintomaticos = GetValFromPorc(_numPoblacion, tbPorAsintomaticosIniciales.Text) * factorMultiplicador;
                _numInmunes = GetValFromPorc(_numPoblacion, tbPorInmunesIniciales.Text) * factorMultiplicador;
                _numHospitalizados = GetValFromPorc(_numPoblacion, tbPorUCIIniciales.Text) * factorMultiplicador;
                _numFallecidos = GetValFromPorc(_numPoblacion, tbPorFallecidosIniciales.Text) * factorMultiplicador;
                _probabilidadMorir = !string.IsNullOrEmpty(tbProbabilidadMorir.Text) ? int.Parse(tbProbabilidadMorir.Text) : 0;
                _probabilidadHospitalizacion = !string.IsNullOrEmpty(tbProbabilidadHospitalizacion.Text) ? int.Parse(tbProbabilidadHospitalizacion.Text) : 0;
                _probabilidadInfeccion = !string.IsNullOrEmpty(tbProbabilidadInfeccion.Text) ? int.Parse(tbProbabilidadInfeccion.Text) : 50;
                _probabilidadMovimiento = !string.IsNullOrEmpty(tbProbabilidadMovimiento.Text) ? int.Parse(tbProbabilidadMovimiento.Text) : 50;
                _diasEvolucionVirus = !string.IsNullOrEmpty(tbDiasEvolucionVirus.Text) ? int.Parse(tbDiasEvolucionVirus.Text) : 0;

                if (string.IsNullOrEmpty(tbInfeccionesNecesarias.Text)) return false;

                _VecinasNecesariasParaInfeccion = int.Parse(tbInfeccionesNecesarias.Text);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Excepcion: FormAutomara->ValidarInformacionInicial: {ex}");
                return false;
            }
        }

        private double GetValFromPorc(double totalPoblacion, string prmStrValPorcentaje)
        {
            double response = 0;

            if (string.IsNullOrEmpty(prmStrValPorcentaje))
                response = 0;
            else
            {
                bool isValid = int.TryParse(prmStrValPorcentaje, out int vlrPorcentaje);
                if (isValid)
                    return (vlrPorcentaje * totalPoblacion) / 100;
            }

            return response;
        }

        private void PindarDatos()
        {
            int totalPoblacion = 0;
            int totalLimite = 0;
            int totalIndeterminados = 0;

            int grupoPixeles;
            if (checkUsarMapa.Checked)
            {
                grupoPixeles = 1;
                arrayPersonas = PatronInicial.MapaColombia;

                for (int x = 0; x < longitud; x++)
                {
                    for (int y = 0; y < longitud; y++)
                    {
                        if (arrayPersonas[x, y] == 0)
                        {
                            Objpersonas[x, y].Estado = EnumEstado.INDETERMINADO;
                            totalIndeterminados++;
                        }
                        else if (arrayPersonas[x, y] == 3)
                        {
                            Objpersonas[x, y].Estado = EnumEstado.LIMITE;
                            totalLimite++;
                        }
                        else
                        {
                            Objpersonas[x, y].Estado = EnumEstado.VACIO;
                            totalPoblacion++;
                        }

                    }
                }
                _numSanos = GetValFromPorc(totalPoblacion, tbPorSanosIniciales.Text);
                _numContagiados = GetValFromPorc(totalPoblacion, tbPorContagiadosIniciales.Text);


                //int cant

                PintarPixelAleatorio(_numSanos, EnumEstado.SANO, grupoPixeles);
                PintarPixelAleatorio(_numContagiados, EnumEstado.CONTAGIADO, grupoPixeles);
            }
            else
            {
                grupoPixeles = 0;
                PintarPixelAleatorio(_numSanos, EnumEstado.SANO, grupoPixeles);
                PintarPixelAleatorio(_numContagiados, EnumEstado.CONTAGIADO, grupoPixeles);
                PintarPixelAleatorio(_numAsintomaticos, EnumEstado.ASINTOMATICO, grupoPixeles);
                PintarPixelAleatorio(_numInmunes, EnumEstado.INMUNE, grupoPixeles);
                PintarPixelAleatorio(_numHospitalizados, EnumEstado.UCI, grupoPixeles);
                PintarPixelAleatorio(_numFallecidos, EnumEstado.FALLECIDO, grupoPixeles);
            }

            PintarMatriz();
        }

        private void PintarPixelAleatorio(double numMaxPintados, EnumEstado estadoPintar, int grupoPixeles)
        {
            int numPintados = 0;
            Random random = new Random();

            while (numPintados < (int)numMaxPintados)
            {
                int ejeX = random.Next(0, longitud);
                int ejeY = random.Next(0, longitud);

                if (arrayPersonas[ejeX, ejeY] == grupoPixeles && !Objpersonas[ejeX, ejeY].IsPaint)
                {
                    Objpersonas[ejeX, ejeY].Estado = estadoPintar;
                    Objpersonas[ejeX, ejeY].IsPaint = true;
                    arrayPersonas[ejeX, ejeY] = 1;
                    numPintados++;
                }
            }
        }

        public int NumPersonasPorEstado(EnumEstado prmEstado)
        {
            int response = 0;

            Persona[] unidimensional = Objpersonas.Cast<Persona>().ToArray();

            var newObject = unidimensional.Count(x => x.Estado == prmEstado);

            response = newObject;

            return response;
        }

        private void PintarMatriz()
        {
            bmp = new Bitmap(pbAutomata.Width, pbAutomata.Height);

            for (int x = 0; x < longitud; x++)
                for (int y = 0; y < longitud; y++)
                {
                    var tempPersona = Objpersonas[x, y];
                    var tempColor = GetColorPixel(tempPersona.Estado);
                    Objpersonas[x, y].IsModified = false;
                    PintarPixel(bmp, x, y, tempColor);
                }
        }

        private void PintarPixel(Bitmap bmp, int x, int y, Color prmColor)
        {
            for (int _x = 0; _x < longitudPixel; _x++)
                for (int _y = 0; _y < longitudPixel; _y++)
                {
                    var ejeX = _x + (x * longitudPixel);
                    var ejeY = _y + (y * longitudPixel);

                    bmp.SetPixel(ejeX, ejeY, prmColor);
                }
        }

        private Color GetColorPixel(EnumEstado prmEstado)
        {
            _ = new Color();
            Color response;
            switch (prmEstado)
            {
                case EnumEstado.VACIO:
                    response = Color.White;
                    break;
                case EnumEstado.SANO:
                    response = Color.LightGreen;
                    break;
                case EnumEstado.CONTAGIADO:
                    response = Color.Red;
                    break;
                case EnumEstado.ASINTOMATICO:
                    response = Color.Green;
                    break;
                case EnumEstado.INMUNE:
                    response = Color.DarkGreen;
                    break;
                case EnumEstado.UCI:
                    response = Color.Fuchsia;
                    break;
                case EnumEstado.FALLECIDO:
                    response = Color.Brown;
                    break;
                case EnumEstado.LIMITE:
                    response = Color.Black;
                    break;
                default:
                    response = Color.White;
                    break;
            }

            return response;
        }

        private void BtnPintarIniciales_Click(object sender, EventArgs e)
        {
            int sumaPorcentajes = GetSumaPorcentajes();

            if (sumaPorcentajes > 100)
            {
                MessageBox.Show("La suma de los porcentajes es mayor a 100, verifique la información e intente nuevamente.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            countDias = 0;
            _resumenDia.Clear();
            _numPoblacion = 0;
            CargarTamanios();

            arrayPersonas = new int[longitud, longitud];
            Objpersonas = new Persona[longitud, longitud];
            bmp = new Bitmap(pbAutomata.Width, pbAutomata.Height);

            bool isValid = ValidarInformacionInicial();

            if (isValid)
            {
                ReiniciarRejilla();
                PindarDatos();
                ShowResumen();
                btnStartProcess.Enabled = true;
                btnAvanzar.Enabled = true;

                pbAutomata.Image = bmp;
            }
            else
                MessageBox.Show("Datos incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            stopwatch.Stop();
            tSStatusLabel.Text = $"Pintar Patron Inicial - Tiempo de ejecución: (m/s) {stopwatch.ElapsedMilliseconds}";
        }

        private int GetSumaPorcentajes()
        {
            int response = GetValorInput(tbPorSanosIniciales.Text) + GetValorInput(tbPorContagiadosIniciales.Text) + GetValorInput(tbPorAsintomaticosIniciales.Text);
            response += GetValorInput(tbPorInmunesIniciales.Text) + GetValorInput(tbPorUCIIniciales.Text) + GetValorInput(tbPorFallecidosIniciales.Text);

            return response;
        }

        private int GetValorInput(string prmValorInput)
        {
            int response = string.IsNullOrEmpty(prmValorInput) ? 0 : int.Parse(prmValorInput);
            return response;
        }

        private void CargarTamanios()
        {
            if(cbTamPixel.Text == "Seleccionar...")
            {
                MessageBox.Show("Ingrese un valor para el tamaño del pixel.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var tempTamanio = cbTamPixel.Text.Replace("px", "");
            int tamPixel = int.Parse(tempTamanio);

            switch (tamPixel)
            {
                case 1:
                    longitud = 800;
                    longitudPixel = 1;
                    factorMultiplicador = 1;
                    _numPoblacion = _numPoblacion == 0 ? longitud * longitud : _numPoblacion;
                    break;
                case 2:
                    longitud = 400;
                    longitudPixel = 2;
                    factorMultiplicador = 1;
                    _numPoblacion = _numPoblacion == 0 ? longitud * longitud : _numPoblacion;
                    break;
                case 4:
                    longitud = 200;
                    longitudPixel = 4;
                    factorMultiplicador = 1;
                    _numPoblacion = _numPoblacion == 0 ? longitud * longitud : _numPoblacion;
                    break;
                case 8:
                    longitud = 100;
                    longitudPixel = 8;
                    factorMultiplicador = 0.25;
                    _numPoblacion = _numPoblacion == 0 ? (longitud * longitud) * 4 : _numPoblacion;
                    break;
                default:
                    longitud = 50;
                    longitudPixel = 16;
                    factorMultiplicador = 0.125;
                    _numPoblacion = _numPoblacion == 0 ? (longitud * longitud) * 8 : _numPoblacion;
                    break;
            }
        }

        private void ShowResumen()
        {
            int numSanos = NumPersonasPorEstado(EnumEstado.SANO);
            int numContagiados = NumPersonasPorEstado(EnumEstado.CONTAGIADO);
            int numAsintomaticos = NumPersonasPorEstado(EnumEstado.ASINTOMATICO);
            int numInmunes = NumPersonasPorEstado(EnumEstado.INMUNE);
            int numHospitalizados = NumPersonasPorEstado(EnumEstado.UCI);
            int numFallecidos = NumPersonasPorEstado(EnumEstado.FALLECIDO);
            int numVacios = NumPersonasPorEstado(EnumEstado.VACIO);
            countDias++;

            var objResumenDia = new ResumenDia()
            {
                Dia = countDias,
                NumCasosSanos = numSanos,
                NumCasosContagiados = numContagiados,
                NumCasosAsintomaticos = numAsintomaticos,
                NumCasosInmunes = numInmunes,
                NumCasosHospitalizados = numHospitalizados,
                NumCasosFallecidos = numFallecidos
            };

            _resumenDia.Add(objResumenDia);

            lblDia.Text = $"Dias: {countDias}";
            lblSanos.Text = $"Sanos: {numSanos}";
            lblContagiados.Text = $"Contagiados: {numContagiados}";
            lblAsintomaticos.Text = $"Asintomaticos: {numAsintomaticos}";
            lblInmunes.Text = $"Inmunes: {numInmunes}";
            lblHospitalizados.Text = $"Hospitalizados: {numHospitalizados}";
            lblFallecidos.Text = $"Fallecidos: {numFallecidos}";
            lblVacios.Text = $"Vacios: {numVacios}";
        }

        private void BtnStartProcess_Click(object sender, EventArgs e)
        {
            timerAutomata.Enabled = true;
            btnPausarSimulacion.Enabled = true;
            btnStartProcess.Enabled = false;
        }

        private Persona[,] GetClonArrayObject(Persona[,] arrayOriginal)
        {
            Persona[,] arrayClonado = new Persona[longitud, longitud];

            for (int i = 0; i < longitud; i++)
                for (int j = 0; j < longitud; j++)
                    arrayClonado[i, j] = (Persona)((ICloneable)arrayOriginal[i, j]).Clone();

            return arrayClonado;
        }

        private void ProcessEvolucion()
        {
            _objAutomata._longitud = longitud;
            _objAutomata._objPersonas = Objpersonas;
            _objAutomata._arrayPersonas = arrayPersonas;
            PrevObjpersonas = GetClonArrayObject(Objpersonas);
            btnRetroceder.Enabled = true;
            btnResumenEvolucion.Enabled = true;
            Persona[,] newObjectpersonas;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();


            if (checkInfeccion.Checked)
            {
                newObjectpersonas = _objAutomata.Process();
                Objpersonas = newObjectpersonas;
                PintarMatriz();
                ShowResumen();
                pbAutomata.Image = bmp;
                _objAutomata._objPersonas = Objpersonas;
                pbAutomata.Refresh();
            }

            if (checkMovimiento.Checked)
            {
                newObjectpersonas = _objAutomata.Movimiento();
                Objpersonas = newObjectpersonas;
                PintarMatriz();
                pbAutomata.Image = bmp;
                pbAutomata.Refresh();
            }

            stopwatch.Stop();

            tSStatusLabel.Text = $"Tiempo de ejecución: (m/s) {stopwatch.ElapsedMilliseconds}";
        }

        private void BtnPausarSimulacion_Click(object sender, EventArgs e)
        {
            btnPausarSimulacion.Enabled = false;
            btnStartProcess.Enabled = true;
            timerAutomata.Enabled = false;
        }

        private void BtnAvanzar_Click(object sender, EventArgs e)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ProcessEvolucion();

            stopwatch.Stop();
            tSStatusLabel.Text = $"Avanzar 1 dia - Tiempo de ejecución: (m/s) {stopwatch.ElapsedMilliseconds}";
        }

        private void BtnResumenEvolucion_Click(object sender, EventArgs e)
        {
            var objFormResumen = new ResumenAutomata(_resumenDia);
            objFormResumen.ShowDialog();
        }

        private void BtnRetroceder_Click(object sender, EventArgs e)
        {
            countDias -= 2;
            ShowResumen();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            Objpersonas = PrevObjpersonas;

            PintarMatriz();
            ShowResumen();

            pbAutomata.Image = bmp;

            stopwatch.Stop();
            btnRetroceder.Enabled = false;

            tSStatusLabel.Text = $"Retroceder - Tiempo de ejecución: (m/s) {stopwatch.ElapsedMilliseconds}";
        }

        private void CbTamPixel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var tempTamanio = cbTamPixel.Text.Replace("px", "");
            int tamPixel = int.Parse(tempTamanio);

            if (tamPixel != 1)
            {
                checkUsarMapa.Visible = false;
                checkUsarMapa.Checked = false;
            }
            else
            {
                checkUsarMapa.Visible = true;
            }
        }
    }
}