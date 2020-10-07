using System;
using System.Collections.Generic;

namespace Layers2
{
    class Program
    {
        static void Main(string[] args)
        {
            int hiddens;
            double outer;
            int pixels;
            List<MainNeuron> mainNeurons;
            List<HiddenNeuron> hiddenNeurons;

            mainNeurons = new List<MainNeuron>();
            hiddenNeurons = new List<HiddenNeuron>();
            hiddens = 10;
            pixels = 784;
            for (int i = 0; i < hiddens; i++)
            {
                hiddenNeurons.Add(new HiddenNeuron(pixels));
            }
            Console.WriteLine("Введите 1 для обучения, 2 для проверки");
            if (Console.ReadLine() == "1")
            {
                int errors;

                for (int i = 0; i < 10; i++)
                    mainNeurons.Add(new MainNeuron(784, i, hiddens, hiddenNeurons));
                do
                {
                    errors = 0;
                    for (int i = 0; i < 10; i++)
                        errors += mainNeurons[i].study();
                    Console.WriteLine("Errors = " + errors);
                } while (errors > 20);

                Console.WriteLine("Обучение завершено!");
            }
            Console.WriteLine("Введите 1 для обучения, 2 для проверки");
            if (Console.ReadLine() == "2")
            {
                double max;
                int result;
                for (int j = 0; j < 10; j++)
                {
                    max = -100000.0;
                    result = 0;
                    for (int i = 0; i < 10; i++)
                    {
                        outer = mainNeurons[i].demonstrate("numbers/" + j + "/2.bmp");
                        if (outer > max)
                        {
                            max = outer;
                            result = i;
                        }
                    }
                    Console.WriteLine("Ожидаемое значение - " + j);
                    for (int i = 0; i < 10; i++)
                        Console.WriteLine("Коэффициент " + i + " = " + mainNeurons[i].demonstrate("numbers/" + j + "/2.bmp"));
                    Console.WriteLine("Итоговое значение - " + result);
                }
            }
        }
    }
}
