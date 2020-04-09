using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace color_1_a_2
{
    public partial class Form1 : Form
    {
        private static char[] dictionary = {    'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J',
                                                'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S',
                                                'T', 'U', 'V', 'W', 'X', 'Y', 'Z',

                                                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
                                                'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
                                                't', 'u', 'v', 'w', 'x', 'y', 'z',

                                                ' ', '1', '2', '3', '4', '5', '6', '7',
                                                '8', '9', '0',
        };
        private const  int countColor = 2;
        private static int[,] dictionaryColor = { { 0x00, 0x00, 0x00 }, { 0xff, 0xff, 0xff } };
        private static int[,] palletColor;
        private int lengthData = 0;
        public Form1()
        {
            InitializeComponent();
            genPallet();
        }
        private void genPallet()
        {
            palletColor = new int[countColor * dictionary.Length, 3];
            for (int i = 0; i < countColor; i++)
            {
                for (int j = 0; j < dictionary.Length; j++)
                {
                    if (i == 0)
                    {
                        palletColor[i * dictionary.Length + j, 0] = dictionaryColor[i, 0] + j;
                        palletColor[i * dictionary.Length + j, 1] = dictionaryColor[i, 1] + j;
                        palletColor[i * dictionary.Length + j, 2] = dictionaryColor[i, 1] + j;
                    }
                    else
                    {
                        palletColor[i * dictionary.Length + j, 0] = dictionaryColor[i, 0] - j;
                        palletColor[i * dictionary.Length + j, 1] = dictionaryColor[i, 1] - j;
                        palletColor[i * dictionary.Length + j, 2] = dictionaryColor[i, 1] - j;
                    }
                }
            }
        }
        private void writeInIgm(string message,string folder, string folderOut)
        {
            //открываем изображение
            Bitmap imageOrig = new Bitmap(@folder, true);

            lengthData = message.Length;
            for (int i = 0; i < message.Length; i++)
            {
                Color d = imageOrig.GetPixel(i, 1);

                if (d.R == 0 && d.G == 0 && d.B == 0)
                {
                    int colorR = palletColor[Array.IndexOf(dictionary, message[i]), 0];
                    int colorG = palletColor[Array.IndexOf(dictionary, message[i]), 1];
                    int colorB = palletColor[Array.IndexOf(dictionary, message[i]), 2];
                    imageOrig.SetPixel(i, 1, Color.FromArgb(colorR, colorG, colorB));
                }
                else
                {
                    int length = dictionary.Length;
                    int colorR = palletColor[Array.IndexOf(dictionary, message[i]) + length, 0];
                    int colorG = palletColor[Array.IndexOf(dictionary, message[i]) + length, 1];
                    int colorB = palletColor[Array.IndexOf(dictionary, message[i]) + length, 2];
                    imageOrig.SetPixel(i, 1, Color.FromArgb(colorR, colorG, colorB));
                }
            }
            imageOrig.Save(@folderOut);
            imageOrig.Dispose();
            textBox4.Text = lengthData.ToString();
        }
        private string readFromIgm(string @folder)
        {
            Bitmap imageOrig = new Bitmap(@folder, true);
            string message = "";
            for (int i = 0; i < lengthData; i++)
            {
                Color d = imageOrig.GetPixel(i, 1);
                int limitBlack = palletColor.Length / 3 / 2;
                Console.WriteLine(palletColor[limitBlack-1, 0]);
                if (d.R  <= palletColor[limitBlack-1, 0])
                {
                    message += dictionary[d.R];
                }
                else
                {
                    int index = 255 - d.R;
                     
                    message += dictionary[index];
                }
            }
            imageOrig.Dispose();
            return message;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                writeInIgm(textBox3.Text, textBox1.Text, textBox2.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                textBox7.Text = readFromIgm(textBox5.Text);
            }
            catch
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
