using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Layers2
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

        public MainNeuron(int pixels, int marker, int neurons, List<HiddenNeuron> hiddenNeurons)
        {
            //neurons = 1;
            this.neurons = neurons;
            //enters = new double[10];
            //weights = new double[10];
            enters = new double[neurons];
            weights = new double[neurons];
            stud_coef = 0.01;
            //stud_coef = 0.001;
            size = pixels + 1;
            examples = new double[100, size];
            //hiddenNeurons = new List<HiddenNeuron>();
            this.hiddenNeurons = hiddenNeurons;
            rnd = new Random();
            exes = 100;
            this.pixels = pixels;

            for (int i = 0; i < neurons; i++)
            {
                weights[i] = rnd.NextDouble() * 0.001 + 0.001;
                //hiddenNeurons.Add(new HiddenNeuron(pixels));
            }

            //hiddenNeurons.Add(new HiddenNeuron(pixels));
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    for (int k = 0; k < 10; k++)
                        if (i == marker)
                            createExamples(examples, count, 1, "numbers/" + i + "/" + j + ".bmp");
                        else
                            createExamples(examples, count, 0, "numbers/" + i + "/" + j + ".bmp");
                    count++;
                }
            }

            /*for (int i = 0; i < 100; i++)
            {
                Console.Write("Ряд " + i);
                for (int j = 0; j < size; j++)
                    Console.Write(" " + examples[i, j]);
                Console.WriteLine(" ");
            }*/
            //Console.WriteLine("Press any key");
            //Console.ReadLine();
        }

        private void count()
        {
            summ = 0.0;
            result = 0.0;
            for (int i = 0; i < neurons; i++)
            {
                /*Console.WriteLine("Weights " + weights[i]);
                Console.WriteLine("Enters " + enters[i]);*/
                result += weights[i] * enters[i];
            }
            /*for (int i = 0; i < neurons; i++)
                summ += enters[i];*/
            //for (int i = 0; i < 10; i++)
            //result = weights[0] * enters[0];
            /*Console.WriteLine("Result " + result);
            Console.ReadLine();*/
        }
        public int study()
        {
            int g_error;
            double delta;
            double iter;

            iter = 0.0;
            //do
            {
                g_error = 0;

                for (int i = 0; i < exes; i++)
                {
                    int locres;

                    for (int j = 0; j < neurons; j++)
                    {
                        double[] example;

                        example = new double[pixels];
                        for (int k = 0; k < pixels; k++)
                        {
                            example[k] = examples[i, k];
                            //Console.WriteLine(k + " = " + example[k]);
                        }
                        //Console.ReadLine();
                        enters[j] = hiddenNeurons[j].summ(example);
                    }
                    count();

                    if (result >= 0.5) locres = 1;
                    else locres = 0;
                    //Console.WriteLine(result);
                    //Console.ReadLine();
                    //delta = (result - examples[i, pixels]) * (result * (1 - result));
                    delta = (result - examples[i, pixels]) * (result * (1 - result));

                    //Console.WriteLine("Вес до изменения = " + weights[0]);
                    //Console.WriteLine("=============");
                    for (int j = 0; j < enters.Length; j++)
                    {
                        //for (int k = 0; k < neurons; k++)
                        //hiddenNeurons[k].study(delta, weights[j]);
                        //weights[j] = weights[j] - stud_coef * summ * delta;
                        hiddenNeurons[j].study(delta, weights[j]);
                        weights[j] = weights[j] - stud_coef * enters[j] * delta;
                        //weights[j] = weights[j] - stud_coef * result * delta;
                        //Console.WriteLine("Вес нейрона " + j + " после изменения = " + weights[j]);
                    }
                    //Console.WriteLine("=============");
                    //Console.ReadLine();
                    /*for (int j = 0; j < neurons; j++)
                        Console.WriteLine("Вес входа " + j + " = " + weights[j]);
                    Console.WriteLine("Конец итерации " + iter);*/
                    //iter++;
                    //if (result != 1) result = 0;
                    /*Console.WriteLine("Результат = " + locres);
                    Console.WriteLine("Искомое = " + examples[i, pixels]);*/
                    if (locres != examples[i, pixels]) g_error++;
                    iter += result;
                }
                //Console.ReadLine();
                //Console.WriteLine("Gerror = " + g_error);
                /*Console.WriteLine("W = " + weights[0]);
                Console.WriteLine("E = " + enters[0]);
                Console.WriteLine("Press any key");*/
                /*if (iter / 20000 == 0)
                    Console.ReadLine();*/
                //Console.WriteLine(iter);
                iter = 0.0;
                //Console.ReadLine();
            } //while (g_error > 3);
              //Console.WriteLine("Press any key");
            //Console.ReadLine();

            return g_error;
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
                //Console.WriteLine("meh = " + enters[i]);
            }
            count();
            //Console.WriteLine("MEH" + result);
            return result;
        }
    }
}
