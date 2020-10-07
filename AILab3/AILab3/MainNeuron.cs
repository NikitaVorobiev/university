using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AILab3
{
    class MainNeuron
    {
        double[] enters;
        double[] weights;
        double stud_coef;
        double[,] examples;
        int size;
        double result;
        List<HiddenNeuron> hiddenNeurons;
        Random rnd;
        int exes;
        int pixels;
        int neurons;
        double summ;

        public MainNeuron(int pixels, int marker)
        {
            neurons = 10;
            enters = new double[neurons];
            weights = new double[neurons];
            stud_coef = 0.01;
            size = pixels + 1;
            examples = new double[100, size];
            hiddenNeurons = new List<HiddenNeuron>();
            rnd = new Random();
            exes = 100;
            this.pixels = pixels;

            for (int i = 0; i < neurons; i++)
            {
                weights[i] = rnd.NextDouble() * 0.01 + 0.01;
                hiddenNeurons.Add(new HiddenNeuron(pixels));
            }
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if(i == marker)
                        createExamples(examples, count, 1, "numbers/" + i + "/" + j + ".bmp");
                    else
                        createExamples(examples, count, 0, "numbers/" + i + "/" + j + ".bmp");
                    count++;
                }
            }
        }

        private void count()
        {
            summ = 0.0;
            result = 0.0;
            for (int i = 0; i < neurons; i++)
            {
                result += weights[i] * enters[i];
            }

            result = -1 * result;
            result = 1 / (1 + Math.Pow(Math.E, result));
        }
        public void study()
        {
            double delta = 10;
            double old_delta = 0;
            double[] example;
            while (Math.Abs(old_delta - delta) > 0.0001)
            {
                for (int i = 0; i < exes; i++)
                {
                    example = new double[pixels];
                    for (int k = 0; k < pixels; k++)
                    {
                        example[k] = examples[i, k];
                    }
                    for (int j = 0; j < neurons; j++)
                    {
                        enters[j] = hiddenNeurons[j].summ(example);
                    }
                    count();
                    old_delta = delta;
                    delta = (result - examples[i, pixels]) * (result * (1 - result));
                    for (int j = 0; j < enters.Length; j++)
                    {
                        hiddenNeurons[j].study(delta, weights[j]);
                        weights[j] = weights[j] - stud_coef * enters[j] * delta;
                    }
                }
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

        private void createExamples(double[,] examples, int row, int marker, string filePath)
        {
            Color[][] color;
            int counter;

            color = GetBitMapColorMatrix(filePath);
            counter = 0;
            for (int i = 0; i < 28; i++)
            {
                for (int j = 0; j < 28; j++)
                {
                    if (color[i][j] != Color.FromArgb(255, 0, 0, 0))
                        examples[row, counter] = 1;
                    else
                        examples[row, counter] = 0;
                    counter++;
                }
            }
            examples[row, pixels] = marker;
        }
        public double demonstrate(string path)
        {
            double[] example;

            example = new double[pixels];
            createExamples(examples, 0, 0, path);
            for (int i = 0; i < pixels; i++)
                example[i] = examples[0, i];
            for (int i = 0; i < enters.Length; i++)
            {
                enters[i] = hiddenNeurons[i].summ(example);
            }
            count();
            return result;
        }
    }
}
