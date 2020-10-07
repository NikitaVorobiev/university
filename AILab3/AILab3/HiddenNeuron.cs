using System;
using System.Collections.Generic;
using System.Text;

namespace AILab3
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
            stud_coef = 0.1;
            rnd = new Random();
            this.pixels = pixels;

            for (int i = 0; i < pixels; i++)
                weights[i] = rnd.NextDouble() * 0.001 + 0.001;
        }
        private void count()
        {
            int tmp;

            tmp = 0;
            result = 0.0;
            for (int i = 0; i < pixels; i++)
            {
                result += weights[i] * enters[i];
            }
        }
        public double summ(double[] enters)
        {
            this.enters = enters;
            count();
            return result;
        }
        public void study(double main_delta, double main_weight)
        {
            result = -1 * result;
            result = 1 / (1 + Math.Pow(Math.E, result));
            double delta;
            delta = (result * (1 - result)) * main_delta * main_weight;
            for (int i = 0; i < weights.Length; i++)
            {
                weights[i] = weights[i] - stud_coef * (enters[i] * delta);
            }
        }
    }
}
