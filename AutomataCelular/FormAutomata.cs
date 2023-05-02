using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace AutomataCelular
{
    public partial class FormAutomata : Form
    {
        private static FormAutomata instance = null;
        public static FormAutomata Instance { get { return instance; } }

        public int longitud = 0;
        public int countDias = 0;
        private int longitudPixel = 0;
        private double factorMultiplicador;
        public int[,] arrayPersonas;
        public Persona[,] Objpersonas;
        private double _numPoblacion = 0;
        private double _numSanos = 0;
        private double _numContagiados = 0;
        private double _numAsintomaticos = 0;
        private double _numInmunes = 0;
        private double _numHospitalizados = 0;
        private double _numFallecidos = 0;
        public double _probabilidadMorir = 0;
        public double _probabilidadHospitalizacion = 0;
        public int _VecinasNecesariasParaInfeccion = 0;
        public int _diasEvolucionVirus = 0;
        public List<ResumenDia> _resumenDia;

        Bitmap bmp = null;

        string reglaSeleccionada = string.Empty;
        public Automata _objAutomata = null;
        public FormAutomata()
        {
            InitializeComponent();
            if (instance == null) instance = this;
            _objAutomata = new Automata();
            _resumenDia = new List<ResumenDia>();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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

        private void timerAutomata_Tick(object sender, EventArgs e)
        {
            ProcessEvoluciuon();
        }

        private bool ValidarInformacionInicial()
        {
            bool response = false;

            try
            {
                _numSanos = getValFromPorc(tbPorSanosIniciales.Text) * factorMultiplicador;
                _numContagiados = getValFromPorc(tbPorContagiadosIniciales.Text) * factorMultiplicador;
                _numAsintomaticos = getValFromPorc(tbPorAsintomaticosIniciales.Text) * factorMultiplicador;
                _numInmunes = getValFromPorc(tbPorInmunesIniciales.Text) * factorMultiplicador;
                _numHospitalizados = getValFromPorc(tbPorUCIIniciales.Text) * factorMultiplicador;
                _numFallecidos = getValFromPorc(tbPorFallecidosIniciales.Text) * factorMultiplicador;
                _probabilidadMorir = !string.IsNullOrEmpty(tbProbabilidadMorir.Text) ? int.Parse(tbProbabilidadMorir.Text) : 0;
                _probabilidadHospitalizacion = !string.IsNullOrEmpty(tbProbabilidadHospitalizacion.Text) ? int.Parse(tbProbabilidadHospitalizacion.Text) : 0;
                _diasEvolucionVirus = !string.IsNullOrEmpty(tbDiasEvolucionVirus.Text) ? int.Parse(tbDiasEvolucionVirus.Text) : 0;

                if (string.IsNullOrEmpty(tbInfeccionesNecesarias.Text)) return false;

                _VecinasNecesariasParaInfeccion = int.Parse(tbInfeccionesNecesarias.Text);

                response = true;
            }
            catch (Exception ex)
            {
                response = false;
            }

            return response;
        }

        private double getValFromPorc(string prmStrValPorcentaje)
        {
            double response = 0;
            int vlrPorcentaje = 0;
            int strValPorcentaje = 0;
            bool isValid = false;

            if (string.IsNullOrEmpty(prmStrValPorcentaje))
            {
                strValPorcentaje = 0;
                response = 0;
            }
            else
            {
                isValid = int.TryParse(prmStrValPorcentaje, out vlrPorcentaje);

                if (isValid)
                {
                    response = (vlrPorcentaje * _numPoblacion) / 100;
                }
            }

            return response;
        }

        private void PindarDatos()
        {
            PintarPixelAleatorio(_numSanos, EnumEstado.SANO);
            PintarPixelAleatorio(_numContagiados, EnumEstado.CONTAGIADO);
            PintarPixelAleatorio(_numAsintomaticos, EnumEstado.ASINTOMATICO);
            PintarPixelAleatorio(_numInmunes, EnumEstado.INMUNE);
            PintarPixelAleatorio(_numHospitalizados, EnumEstado.UCI);
            PintarPixelAleatorio(_numFallecidos, EnumEstado.FALLECIDO);
            PintarMatriz();
        }

        private void PintarPixelAleatorio(double numMaxPintados, EnumEstado estadoPintar)
        {
            int numPintados = 0;
            Random random = new Random();

            while (numPintados < numMaxPintados)
            {
                int ejeX = random.Next(0, longitud);
                int ejeY = random.Next(0, longitud);

                if (arrayPersonas[ejeX, ejeY] == 0)
                {
                    Objpersonas[ejeX, ejeY].Estado = estadoPintar;
                    arrayPersonas[ejeX, ejeY] = 1;
                    numPintados++;
                }
            }
        }

        private int NumPersonasPorEstado(EnumEstado prmEstado)
        {
            int response = 0;

            Persona[] unidimensional = Objpersonas.Cast<Persona>().ToArray();

            var newObject = unidimensional.Count(x => x.Estado == prmEstado);

            response = newObject;

            return response;
        }

        private void PintarMatriz()
        {
            //bmp = new Bitmap(pbAutomata.Width, pbAutomata.Height);

            for (int x = 0; x < longitud; x++)
            {
                for (int y = 0; y < longitud; y++)
                {
                    //PintarPixel(bmp, x, y, personas[x, y] == 0);
                    var tempPersona = Objpersonas[x, y];
                    var tempColor = getColorPixel(tempPersona.Estado);

                    PintarPixel(bmp, x, y, tempColor);
                }
            }
        }

        private void PintarPixel(Bitmap bmp, int x, int y, Color prmColor)
        {
            for (int _x = 0; _x < longitudPixel; _x++)
            {
                for (int _y = 0; _y < longitudPixel; _y++)
                {
                    var ejeX = _x + (x * longitudPixel);
                    var ejeY = _y + (y * longitudPixel);

                    bmp.SetPixel(ejeX, ejeY, prmColor);
                }
            }
        }

        private Color getColorPixel(EnumEstado prmEstado)
        {
            var response = new Color();

            switch (prmEstado)
            {
                case EnumEstado.VACIO:
                    response = Color.White;
                    break;
                case EnumEstado.SANO:
                    response = Color.DarkGreen;
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
                    response = Color.Black;
                    break;
                default:
                    response = Color.White;
                    break;
            }

            return response;
        }

        private void btnPintarIniciales_Click(object sender, EventArgs e)
        {
            countDias = 0;
            _resumenDia.Clear();
            //_numPoblacion = !string.IsNullOrEmpty(tbDimensionRejilla.Text) ? int.Parse(tbDimensionRejilla.Text) : 0;
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
            {
                MessageBox.Show("Datos incorrectos");
            }
        }

        private void CargarTamanios()
        {
            var tempTamanio = cbTamPixel.Text.Replace("px", "");
            int tamPixel;

            var isValid = int.TryParse(tempTamanio, out tamPixel);

            tamPixel = isValid ? tamPixel : 2;

            if (tamPixel == 1)
            {
                longitud = 800;
                longitudPixel = 1;
                factorMultiplicador = 1;
                _numPoblacion = _numPoblacion == 0 ? longitud * longitud : _numPoblacion;
            }
            else if (tamPixel == 2)
            {
                longitud = 400;
                longitudPixel = 2;
                factorMultiplicador = 1;
                _numPoblacion = _numPoblacion == 0 ? longitud * longitud : _numPoblacion;
            }
            else if (tamPixel == 4)
            {
                longitud = 200;
                longitudPixel = 4;
                factorMultiplicador = 1;
                _numPoblacion = _numPoblacion == 0 ? longitud * longitud : _numPoblacion;
            }
            else if (tamPixel == 8)
            {
                longitud = 100;
                longitudPixel = 8;
                factorMultiplicador = 0.25;
                _numPoblacion = _numPoblacion == 0 ? (longitud * longitud) * 4 : _numPoblacion;
            }
            else if (tamPixel == 16)
            {
                longitud = 50;
                longitudPixel = 16;
                factorMultiplicador = 0.125;
                _numPoblacion = _numPoblacion == 0 ? (longitud * longitud) * 8 : _numPoblacion;
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
        }

        private void btnStartProcess_Click(object sender, EventArgs e)
        {
            timerAutomata.Enabled = true;
            btnPausarSimulacion.Enabled = true;
            btnStartProcess.Enabled = false;
        }

        private void ProcessEvoluciuon()
        {
            _objAutomata._longitud = longitud;
            _objAutomata._objPersonas = Objpersonas;
            _objAutomata._arrayPersonas = arrayPersonas;
            var newObjectpersonas = _objAutomata.Process();
            Objpersonas = newObjectpersonas;

            PintarMatriz();
            ShowResumen();

            pbAutomata.Image = bmp;
        }

        private void btnPausarSimulacion_Click(object sender, EventArgs e)
        {
            btnPausarSimulacion.Enabled = false;
            btnStartProcess.Enabled = true;
            timerAutomata.Enabled = false;
        }

        private void btnAvanzar_Click(object sender, EventArgs e)
        {
            ProcessEvoluciuon();
        }

        private void btnResumenEvolucion_Click(object sender, EventArgs e)
        {
            var objFormResumen = new ResumenAutomata(_resumenDia);
            objFormResumen.ShowDialog();
        }
    }
}