using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace AILab4
{
    class Neuron
    {
        Random rnd;
        int pixels;
        double[,] examples;
        int size;
        int marker;
        double[] enters;
        double[] weights;
        double[,] genofond;
        int hromosoms;
        double stop;
        int exes;
        double mutation;
        double cross_chanse;
        public Neuron(int pixels, int marker, int exes)
        {
            rnd = new Random();
            this.pixels = pixels;
            this.marker = marker;
            this.exes = exes;
            size = pixels + 1;
            examples = new double[100, size];
            enters = new double[pixels];
            weights = new double[pixels];
            hromosoms = 1000;
            mutation = 0.15;
            stop = 0; //допустимый процент ошибок

            genofond = new double[hromosoms, pixels + 3]; //хромосома состоит из весов входов (пикселей) + 1 для кол-ва ошибок + 1 для шанса скрещивания
            for (int i = 0; i < hromosoms; i++)
                for (int j = 0; j < pixels; j++)
                    genofond[i, j] = rnd.Next(-100, 101);
                    //genofond[i, j] = rnd.Next(0, 1);

                    //genofond[i, j] = rnd.NextDouble() * 0.001;
            /*for (int i = 0; i < hromosoms; i++)
            {
                for (int j = 0; j < pixels + 2; j++)
                    Console.Write(genofond[i, j]);
                Console.WriteLine(" ");
            }
            Console.ReadLine();*/


            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (i == marker)
                        createExamples(examples, count, 1, "numbers/" + i + "/" + j + ".bmp");
                    else
                        createExamples(examples, count, 0, "numbers/" + i + "/" + j + ".bmp");
                    count++;
                }
            }
        }

        private double count()
        {
            double result;

            result = 0.0;
            for (int i = 0; i < pixels; i++)
                result += enters[i] * weights[i];
            /*if (result >= 0.5)
                return 1;
            else
                return 0;*/
            return result;
        }

        public void study()
        {
            double g_error;

            g_error = 0;
            do
            {
                count_gen();
                sort();

                g_error++;
                if (g_error != 4)
                    evolute();
                /*g_error = genofond[0, pixels];
                if(g_error != stop)
                    evolute();*/
                //Console.WriteLine(g_error);
            //} while (g_error != stop);
            } while (g_error < 4);

            for (int i = 0; i < pixels; i++)//сохранение итоговых весов
                weights[i] = genofond[0, i];

            Console.WriteLine("Press any key");
            //Console.ReadLine();
        }

        private void count_gen()
        {
            double tmp;

            for(int i = 0; i < hromosoms; i++)
            {
                for (int j = 0; j < pixels; j++)
                    weights[j] = genofond[i, j];
                for (int j = 0; j < exes; j++)
                {
                    for (int k = 0; k < pixels; k++)
                        enters[k] = examples[j, k];
                    tmp = count();
                    if (tmp >= 0.5)//8 правильных
                    //if (tmp > 0)
                        tmp = 1;
                    else
                        tmp = 0;
                    if (tmp != examples[j, pixels])
                        genofond[i, pixels]++;
                    /*if (count() != examples[j, pixels])
                        genofond[i, pixels]++;*/
                    //genofond[i, pixels] += examples[j, pixels] - count();
                }
                Console.WriteLine(genofond[i, pixels]);
            }
        }
        private void sort()
        {
            double[] tmp;

            cross_chanse = 0;
            show_gen("Перед сортировкой");
            tmp = new double[pixels + 2];
            for(int i = 0; i < hromosoms; i++)//сортировка пузырьком
                for(int j = 0; j < hromosoms - 1; j++)
                    if (genofond[j, pixels] > genofond[j + 1, pixels])
                    {
                        for(int k = 0; k < pixels + 1; k++)
                            tmp[k] = genofond[j, k];
                        for (int k = 0; k < pixels + 1; k++)
                            genofond[j, k] = genofond[j + 1, k];
                        for (int k = 0; k < pixels + 1; k++)
                            genofond[j + 1, k] = tmp[k];
                    }
            show_gen("После сортировки");
            for (int i = 0; i < hromosoms; i++)//расстановка шансов скрещивания
            {
                genofond[i, pixels + 1] = hromosoms - i;
                cross_chanse += genofond[i, pixels + 1];
            }
            show_gen("После выставления процентов");
        }
        private void evolute()
        {
            double[,] local_genofond;
            int precent;
            double loc_precent;
            int count;
            double loc_mut;
            //int max;

            local_genofond = new double[hromosoms, pixels + 2];
            //max = hromosoms * 2;
            for(int i = 0; i < hromosoms; i++)
            {
                for(int j = 0; j < pixels; j++)
                {
                    loc_mut = rnd.NextDouble();
                    if (loc_mut > mutation)
                    {
                        //precent = rnd.Next(1, max);
                        //precent = rnd.Next(1, Convert.ToInt32(cross_chanse * 0.1));
                        precent = rnd.Next(1, Convert.ToInt32(cross_chanse));
                        loc_precent = 0;
                        count = 0;
                        do
                        {
                            loc_precent += genofond[count, pixels + 1];
                            count++;
                        }
                        while (precent > loc_precent && count < hromosoms);
                        count--;
                        local_genofond[i, j] = genofond[count, j];
                    }
                    else
                        local_genofond[i, j] = rnd.Next(-100, 100);
                }
            }
            genofond = local_genofond;
        }
        public double demonstrate(string input)
        {
            double result;

            createExamples(examples, 0, 0, input);
            for (int i = 0; i < pixels; i++)
                enters[i] = examples[0, i];
            result = count();
            return result;
        }

        private void show_gen(string text)
        {
            /*Console.WriteLine(text);
            for(int i = 0; i < hromosoms; i++)
            {
                Console.Write("Ряд " + i + ":");
                for (int j = 0; j < pixels + 2; j++)
                    Console.Write(" | " + genofond[i, j]);
                Console.Write(" |\n");
            }*/
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
    }
}
