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

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Arquivos de Imagem|*.jpg;*.jpeg;*.png;*.bmp";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string caminhoImagem = dialog.FileName;
                pictureBox1.Image = Image.FromFile(caminhoImagem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image == null)
            {
                MessageBox.Show("Não há imagem para salvar.");
                return;
            }

            string caminhoSalvar = @"D:\teste\WindowsFormsApp1\imagem";
            string nomeArquivo = "imagem.png";
            string caminhoCompleto = Path.Combine(caminhoSalvar, nomeArquivo);

            try
            {
                pictureBox1.Image.Save(caminhoCompleto);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao salvar a imagem: " + ex.Message);
            }

            Bitmap image = new Bitmap(caminhoCompleto);

            // Detecta a cor da pele na imagem
            Color skinColor = DetectSkinColor(image);
            MessageBox.Show("Cor da pele detectada: " + skinColor.ToString());

        }
        public static Color DetectSkinColor(Bitmap image)
        {
            int skinPixels = 0;
            int totalPixels = 0;

            // Percorre cada pixel da imagem
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    Color pixelColor = image.GetPixel(x, y);
                    totalPixels++;

                    // Verifica se o pixel está dentro do intervalo de cor da pele
                    if (IsSkinColor(pixelColor))
                    {
                        skinPixels++;
                    }
                }
            }

            // Calcula a porcentagem de pixels de cor da pele
            double skinPercentage = (double)skinPixels / totalPixels;

            // Define uma cor baseada na porcentagem de pixels de cor da pele
            if (skinPercentage >= 0.5)
            {
                return Color.LightPink; // Exemplo: cor da pele clara
            }
            else
            {
                return Color.Brown; // Exemplo: cor da pele escura
            }
        }

        public static bool IsSkinColor(Color color)
        {
            // Implemente aqui a lógica para verificar se a cor está dentro do intervalo de cor da pele
            // Pode ser feito através de comparação de valores RGB ou utilizando modelos de cor diferentes, como YCbCr ou HSV

            // Exemplo básico: verifica se a intensidade do vermelho está dentro de um intervalo pré-definido
            int minRed = 100;
            int maxRed = 255;
            if (color.R >= minRed && color.R <= maxRed)
            {
                return true;
            }

            return false;
        }

    }
}
