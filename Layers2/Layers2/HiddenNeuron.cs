using System;
using System.Collections.Generic;
using System.Text;

namespace Layers2
{
    class HiddenNeuron
    {
        double[] enters;
        double[] weights;
        double stud_coef;
        double result;
        Random rnd;
        int pixels;

        public HiddenNeuron(int pixels)
        {
            enters = new double[pixels];
            weights = new double[pixels];
            stud_coef = 0.01;
            //stud_coef = 0.04;
            rnd = new Random();
            this.pixels = pixels;

            for (int i = 0; i < pixels; i++)
                weights[i] = rnd.NextDouble() * 0.001 + 0.001;
            //weights[i] = rnd.NextDouble() * 0.00001 + 0.00001;
        }
        private void count()
        {
            int tmp;

            tmp = 0;
            result = 0.0;
            for (int i = 0; i < pixels; i++)
            {
                /*if (enters[i] == 1)
                {
                    Console.WriteLine("Вес " + weights[i]);
                    Console.WriteLine("Вход " + enters[i]);
                    tmp++;
                }*/
                result += weights[i] * enters[i];
            }
            //Console.WriteLine(result);
            //Console.WriteLine("tmps " + tmp);
            //Console.ReadLine();
        }
        public double summ(double[] enters)
        {
            this.enters = enters;
            count();
            //Console.WriteLine("Result" + result);
            return result;
        }
        public void study(double main_delta, double main_weight)
        {
            double delta;

            delta = (result * (1 - result)) * main_delta * main_weight;
            /*Console.WriteLine("Delta_main = " + main_delta);
            Console.WriteLine("Weight_main = " + main_weight);
            Console.WriteLine("Delta = " + delta);
            Console.ReadLine();*/
            for (int i = 0; i < weights.Length; i++)
            {
                //Console.WriteLine("Вес до изменения = " + weights[0]);
                weights[i] = weights[i] - stud_coef * (enters[i] * delta);
                //Console.WriteLine("Вес после изменения = " + weights[0]);
            }
            //Console.ReadLine();
        }
    }
}
