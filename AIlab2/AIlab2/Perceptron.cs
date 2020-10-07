using System;
using System.Drawing;

namespace AIlab2
{
    class Perceptron
    {
        Random rnd = new Random();

        double[] enters;
        double outer;
        double[] weights;
        double studCoef = 0.1; //коэффициент обучения

        double[,] examples = new double[24, 10];
        public Perceptron()
        {
            for(int i = 1; i < 11; i++)
            {
                createExamples(examples, i, 1, "imagesTrue/triangle" + i + ".bmp");
            }
            for(int i = 1; i < 14; i++)
            {
                createExamples(examples, i, 0, "imagesFalse/notTriangle" + i + ".bmp");
            }
            enters = new double[9];
            weights = new double[enters.Length];
            for (int i = 0; i < enters.Length; i++)
            {
                weights[i] = rnd.NextDouble() * 0.2 + 0.1;
            }
            
            //System.out.println("Этап пройден 1");
        }

        public void countOuter()
        {
            outer = 0;
            for (int i = 0; i < enters.Length; i++)
            {
                outer += enters[i] * weights[i];
            }
            
            if (outer > 0.8) outer = 1; else outer = 0;

            //System.out.println("Этап пройден 2");
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
                    for(int i = 0; i < 9; i++)
                    {
                        enters[i] = examples[e, i];
                    }

                    countOuter();
                    double error = examples[e,9] - outer;

                    gError += Math.Abs(error);
                    for (int i = 0; i < enters.Length; i++)
                    {
                        weights[i] += studCoef * error * enters[i];
                    }
                    num += 1;
                }
            } while (gError != 0);

            //System.out.println("Этап пройден 3");
        }

        public void demonstrate()
        {
            Random rnd = new Random();

            createExamples(examples, 0, 1, "imagesFalse/notTriangle" + rnd.Next(1, 14) + ".bmp");

            for (int i = 0; i < 9; i++)
            {
                enters[i] = examples[0, i];
            }
            countOuter();

            if (outer == 1)
            {
                Console.WriteLine("Фигура - треугольник");
            }
            else
            {
                Console.WriteLine("Фигура - не треугольник");
            }
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
            for(int i = 0; i < 3; i++)
            {
                for (int j=0;j<3; j++)
                {
                    if (color[i][j] == Color.FromArgb(255, 0, 0, 0))
                    {
                        examples[row, counter] = 1;
                    } else
                        examples[row, counter] = 0;
                    counter++;
                }
            }
            examples[row, 9] = marker;
        }
    }
}
