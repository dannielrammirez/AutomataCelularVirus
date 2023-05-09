using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomataCelular
{
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void test_Load(object sender, EventArgs e)
        {
            string pathImage = @"D:\Descargas\Croquis_Colombia.jpg";

            Bitmap image = new Bitmap(pathImage);

            // Asigna la imagen al PictureBox
            pictureBox1.Image = image;
            int[,] array = new int[800,800];


            // Recorrer cada pixel de la imagen
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    // Obtener el color del pixel en la posición (x, y)
                    Color pixelColor = image.GetPixel(x, y);
                    array[x, y] = pixelColor.A == 255 ? 0 : 1;

                    // Aquí puedes hacer lo que necesites con el color del pixel, como imprimirlo en la consola:
                    //Console.WriteLine("Color del pixel ({0}, {1}): {2}", x, y, pixelColor);
                }
            }

            //PatronInicial.Custom = array;

            //// Array de ejemplo
            //string[] lines = { "nueva linea 1", "nueva linea 2", "nueva linea 3" };

            //// Ruta del archivo de texto
            //string filePath = @"C:\Users\danni\OneDrive\Documentos\Array.txt";

            //for(int i = 0 ; i >= 800 ; i++)
            //{
            //    // Agrega las líneas al archivo de texto
            //    File.AppendAllLines(filePath, array[x]);
            //}
        }
    }
}
