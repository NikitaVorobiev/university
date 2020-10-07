using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace AIlab2
{
    class NetworkLayers
    {
        Random rnd = new Random();

        double[] enters;
        double outer;
        double[] weights;
        double studCoef = 0.1; //коэффициент обучения

        double[,] examples = new double[100, 2305];

        int number;
        public NetworkLayers(int number)
        {
            this.number = number;

            enters = new double[10];

            weights = new double[enters.Length];
            for (int i = 0; i < enters.Length; i++)
            {
                weights[i] = rnd.NextDouble() * 0.2 + 0.1;
            }
        }

        public double countOuter()
        {
            outer = 0;
            for (int i = 0; i < enters.Length; i++)
                outer += enters[i] * weights[i];
            return outer;
        }

        public void study()
        {
            double gError = 0.0;

            int num = 0;//кол-во итераций

            do
            {
                gError = 0.0;
                for (int e = 0; e < examples.GetLength(0) - 1; e++)
                {
                    for (int i = 0; i < 2304; i++)
                    {
                        enters[i] = examples[e, i];
                    }

                    countOuter();
                    if (outer > 0.5) outer = 1; else outer = 0;

                    double error = examples[e, 2304] - outer;

                    gError += Math.Abs(error);
                    for (int i = 0; i < enters.Length; i++)
                    {
                        weights[i] += studCoef * error * enters[i];
                    }
                    num += 1;
                }
            } while (gError != 0);
        }

        public double demonstrate(string path)
        {
            Random rnd = new Random();

            createExamples(examples, 0, 1, path);

            for (int i = 0; i < 2304; i++)
            {
                enters[i] = examples[0, i];
            }
            countOuter();
            return outer;
        }

        private Color[][] GetBitMapColorMatrix(string bitmapFilePath)
        {
            Bitmap b1 = new Bitmap(bitmapFilePath);

            int hight = b1.Height;
            int width = b1.Width;

            Color[][] colorMatrix = new Color[width][];
            for (int i = 0; i < width; i++)
            {
                colorMatrix[i] = new Color[hight];
                for (int j = 0; j < hight; j++)
                {
                    colorMatrix[i][j] = b1.GetPixel(i, j);
                }
            }
            return colorMatrix;
        }

        public void createExamples(double[,] examples, int row, int marker, string filePath)
        {
            Color[][] color;
            int counter;

            color = GetBitMapColorMatrix(filePath);
            counter = 0;
            for (int i = 0; i < 48; i++)
            {
                for (int j = 0; j < 48; j++)
                {
                    if (color[i][j] != Color.FromArgb(255, 0, 0, 0))
                    {
                        examples[row, counter] = 1;
                    }
                    else
                        examples[row, counter] = 0;
                    counter++;
                }
            }
            examples[row, 2304] = marker;
        }
    }
}
