using System.Collections.Generic;
using System.Windows.Forms;

namespace AutomataCelular
{
    public partial class ResumenAutomata : Form
    {
        List<ResumenDia> _resumenEvolucion;
        public ResumenAutomata(List<ResumenDia> resumenEvolucion)
        {
            InitializeComponent();
            _resumenEvolucion = resumenEvolucion;
        }

        private void ResumenAutomata_Load(object sender, System.EventArgs e)
        {
            fillChart();
        }

        private void fillChart()
        {
            foreach (var item in _resumenEvolucion)
            {
                chartResumen.Series["CasosSanos"].Points.AddXY(item.Dia, item.NumCasosSanos);
                chartResumen.Series["CasosAsintomaticos"].Points.AddXY(item.Dia, item.NumCasosAsintomaticos);
                chartResumen.Series["CasosInfectados"].Points.AddXY(item.Dia, item.NumCasosContagiados);
                chartResumen.Series["CasosHospitalizados"].Points.AddXY(item.Dia, item.NumCasosHospitalizados);
                chartResumen.Series["CasosFallecidos"].Points.AddXY(item.Dia, item.NumCasosFallecidos);
                chartResumen.Series["CasosInmunes"].Points.AddXY(item.Dia, item.NumCasosInmunes);
            }
            
            chartResumen.Titles.Add("Numero de casos");
        }
    }
}