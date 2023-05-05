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
                chartResumen.Series["Sanos"].Points.AddXY(item.Dia, (item.NumCasosSanos + item.NumCasosInmunes));
                chartResumen.Series["Asintomaticos"].Points.AddXY(item.Dia, item.NumCasosAsintomaticos);
                chartResumen.Series["Infectados"].Points.AddXY(item.Dia, item.NumCasosContagiados);
                chartResumen.Series["Hospitalizados"].Points.AddXY(item.Dia, item.NumCasosHospitalizados);
                chartResumen.Series["Fallecidos"].Points.AddXY(item.Dia, item.NumCasosFallecidos);
                chartResumen.Series["Inmunes"].Points.AddXY(item.Dia, item.NumCasosInmunes);
            }
            
            chartResumen.Titles.Add("Numero de casos");
        }
    }
}