using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

/// <summary>
/// Código hecho por Héctor de León
/// www.hdeleon.net
/// </summary>
namespace AutomataCelular
{
    
    public partial class FormAutomata : Form
    {
        private int longitud = 200;
        private int longitudPixel = 4;
        int[,] celulas;
        string reglaSeleccionada = string.Empty;
        public FormAutomata()
        {
            InitializeComponent();

            //inicializamos
            celulas = new int[longitud, longitud];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //PintarMatriz();
        }

        private void PintarMatriz()
        {
            Bitmap bmp = new Bitmap(pbAutomata.Width,pbAutomata.Height);
            for (int x=0;x<longitud;x++)
            {
                for (int y=0;y<longitud;y++)
                {
                    PintarPixel(bmp, x, y, celulas[x, y] == 0);
                }
            }

            pbAutomata.Image = bmp;
        }
        private void PintarPixel(Bitmap bmp, int x, int y, bool pintar)
        {
            var color = !pintar ? Color.Black : Color.White;

            for (int _x = 0 ; _x < longitudPixel ; _x++)
            {
                for(int _y = 0 ; _y < longitudPixel ; _y++)
                {
                    var ejeX = _x + (x * longitudPixel);
                    var ejeY = _y + (y * longitudPixel);

                    bmp.SetPixel(ejeX, ejeY, color);
                }
            }
        }

        private void Process()
        {
            int[,] celulasTemp = new int[longitud, longitud];
            for (int x = 0; x < longitud; x++)
            {
                for (int y = 0; y < longitud; y++)
                {
                    bool estaViva = celulas[x, y] != 0;

                    celulasTemp[x, y] = ProcessRegla(reglaSeleccionada, x, y, estaViva);
                }
            }

            celulas = celulasTemp;

        }

        public int ProcessRegla(string nombreRegla, int x, int y, bool estaViva)
        {
            switch (nombreRegla)
            {
                case "Juego de la vida":
                    return ReglaJuegoVida(x, y, estaViva);
                case "Personalizado_Daniel":
                    return ReglaDaniel(x, y, estaViva);
                default:
                    return ReglaJuegoVida(x, y, estaViva);
            }
        }

        private int AnalizarVecinas(int x, int y)
        {
            int VecinasVivas = 0;

            //vecina 1
            if (x > 0 && y > 0)
                if (celulas[x - 1, y - 1] == 1)
                    VecinasVivas++;

            //vecina 2
            if (y > 0)
                if (celulas[x, y - 1] == 1)
                    VecinasVivas++;

            //vecina 3
            if (x < longitud - 1 && y > 0)
                if (celulas[x + 1, y - 1] == 1)
                    VecinasVivas++;

            //vecina 4
            if (x > 0)
                if (celulas[x - 1, y] == 1)
                    VecinasVivas++;

            //vecina 5
            if (x < longitud - 1)
                if (celulas[x + 1, y] == 1)
                    VecinasVivas++;

            //vecina 6
            if (x > 0 && y < longitud - 1)
                if (celulas[x - 1, y + 1] == 1)
                    VecinasVivas++;

            //vecina 7
            if (y < longitud - 1)
                if (celulas[x, y + 1] == 1)
                    VecinasVivas++;


            //vecina 8
            if (x < longitud - 1 && y < longitud - 1)
                if (celulas[x + 1, y + 1] == 1)
                    VecinasVivas++;

            return VecinasVivas;
        }

        private int ReglaJuegoVida(int x, int y, bool EsViva)
        {
            int VecinasVivas = AnalizarVecinas(x, y);
            int newStatus;

            if (EsViva)
            {
                newStatus = (VecinasVivas == 2 || VecinasVivas == 3) ? 1 : 0;
            }
            else
            {
                newStatus = VecinasVivas == 2 ? 1 : 0;
            }

            //if (EsViva)
            //{
            //    newStatus = (VecinasVivas == 2 || VecinasVivas == 3) ? 1 : 0;
            //}
            //else
            //{
            //    newStatus = VecinasVivas == 3 ? 1 : 0;
            //}

            return newStatus;
        }

        private int ReglaDaniel(int x, int y, bool EsViva)
        {
            int VecinasVivas = AnalizarVecinas(x, y);
            int newStatus = 0;
            int v = 0;

            //vecina 1
            if (x > 0 && y > 0)
                if (celulas[x - 1, y - 1] == 1)
                    v++;

            //vecina 3
            if (x < longitud - 1 && y > 0)
                if (celulas[x + 1, y - 1] == 1)
                    v++;


            if (v > 0) newStatus = 1;

            if (EsViva)
            {
                newStatus = 1;
            }
            else
            {
                newStatus = v > 0 ? 1 : 0;
            }

            return newStatus;
        }

        private void btnRandom_Click(object sender, EventArgs e)
        {
            //reiniciamos
            for (int i = 0; i < longitud; i++)
                for (int j = 0; j < longitud; j++)
                    celulas[i, j] = 0;

            Random r = new Random();
            //llenamos random
            for (int i = 0; i < longitud; i++)
                for (int j = 0; j < longitud; j++)
                    celulas[i, j] = r.Next(0,2);

            PintarMatriz();
        }

        private void btnIniciar_Click(object sender, EventArgs e)
        {
            timerAutomata.Enabled = true;
        }

        private void btnPausar_Click(object sender, EventArgs e)
        {
            timerAutomata.Enabled = false;
        }

        private void timerAutomata_Tick(object sender, EventArgs e)
        {
            Process();
            PintarMatriz();
        }

        private void cbReglas_SelectedIndexChanged(object sender, EventArgs e)
        {
            reglaSeleccionada = cbReglas.Text;
        }

        private void cbPatronInicial_SelectedIndexChanged(object sender, EventArgs e)
        {
            string itemSelected = cbPatronInicial.Text;

            switch (itemSelected)
            {
                case "Personalizado":
                    celulas = new int[longitud, longitud];
                    break;
                case "Gosper Glider Gun":
                    celulas = PatronInicial.GosperGliderGun;
                    break;
                case "Personalizado_Daniel":
                    celulas = PatronInicial.Personalizado;
                    break;
                default:
                    celulas = PatronInicial.GosperGliderGun;
                    break;
            }

            PintarMatriz();
        }

        private void tbSpeed_Scroll(object sender, EventArgs e)
        {
            timerAutomata.Interval = tbSpeed.Value * 10;
        }

        private void pbAutomata_Click(object sender, EventArgs e)
        {
            Point relativePoint = pbAutomata.PointToClient(Cursor.Position);

            try
            {
                int ejeX = relativePoint.X / longitudPixel;// - relativePoint.X * longitudPixel;
                int ejeY = relativePoint.Y / longitudPixel;// - relativePoint.Y * longitudPixel;

                celulas[ejeX, ejeY] = 1;

                PintarMatriz();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
