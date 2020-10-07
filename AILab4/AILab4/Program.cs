using System;
using System.Collections.Generic;

namespace AILab4
{
    class Program
    {
        static void Main(string[] args)
        {
            int pixels;
            List<Neuron> neurons;

            pixels = 784;
            neurons = new List<Neuron>();
            Console.WriteLine("Press any key to start learning");
            //Console.ReadLine();
            for (int i = 0; i < 10; i++)
                neurons.Add(new Neuron(pixels, i, 100));
            for (int i = 0; i < 10; i++)
                neurons[i].study();
            Console.WriteLine("Learning end well! Press eny key to test");
            //Console.ReadLine();

            double max;
            int result;
            double outer;
            for (int j = 0; j < 10; j++)
            {
                max = -100000.0;
                result = 0;
                for (int i = 0; i < 10; i++)
                {
                    outer = neurons[i].demonstrate("numbers/" + j + "/2.bmp");
                    if (outer > max)
                    {
                        max = outer;
                        result = i;
                    }
                }
                Console.WriteLine("Ожидаемое значение - " + j);
                /*for (int i = 0; i < 10; i++)
                    Console.WriteLine("Коэффициент " + i + " = " + neurons[i].demonstrate("numbers/" + j + "/2.bmp"));*/
                Console.WriteLine("Итоговое значение - " + result);
            }
        }
    }
}
