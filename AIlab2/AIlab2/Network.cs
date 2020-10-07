using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AIlab2
{
    class Network
    {
        Random rnd = new Random();

        double[] enters;
        double outer;
        double[] weights;
        double studCoef = 0.1; //коэффициент обучения

        //double[,] examples = new double[100, 2305];
        double[,] examples = new double[100, 362];

        int number;
        public Network(int number)
        {
            /*Network network0 = new Network(0);
            Network network1 = new Network(1);
            Network network2 = new Network(2);
            Network network3 = new Network(3);
            Network network4 = new Network(4);
            Network network5 = new Network(5);
            Network network6 = new Network(6);
            Network network7 = new Network(7);
            Network network8 = new Network(8);
            Network network9 = new Network(9);

            network0.study();
            network1.study();
            network2.study();
            network3.study();
            network4.study();
            network5.study();
            network6.study();
            network7.study();
            network8.study();
            network9.study();*/

            this.number = number;
            int count = 0;

            enters = new double[361];
            //enters = new double[10];
            weights = new double[enters.Length];
            for (int i = 0; i < enters.Length; i++)
                weights[i] = rnd.NextDouble() * 0.2 + 0.1;

            //System.out.println("Этап пройден 1");
        }

        public double countOuter()
        {
            outer = 0;
            for (int i = 0; i < enters.Length; i++)
            {
                outer += enters[i] * weights[i];
            }

            //if (outer > 0.5) outer = 1; else outer = 0;

            return outer;
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
                    //for (int i = 0; i < 2304; i++)
                    for (int i = 0; i < 361; i++)
                    {
                        enters[i] = examples[e, i];
                    }

                    countOuter();
                    if (outer > 0.5) outer = 1; else outer = 0;

                    //double error = examples[e, 2304] - outer;
                    double error = examples[e, 362] - outer;

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

        public double demonstrate(string path)
        {
            Random rnd = new Random();

            //for (int i = 0; i < 2304; i++)
            for (int i = 0; i < 362; i++)
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

    }
}
