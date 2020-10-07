using System;
using System.Collections.Generic;

namespace AILab3
{
    class Program
    {
        static void Main(string[] args)
        {
            List<MainNeuron> mainNeurons;
            double outer;

            mainNeurons = new List<MainNeuron>();
            Console.WriteLine("Введите 1 для обучения, 2 для проверки");
            if(Console.ReadLine() == "1")
            {
                for (int i = 0; i < 10; i++)
                    mainNeurons.Add(new MainNeuron(784, i));
                for (int i = 0; i < 10; i++)
                    mainNeurons[i].study();
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
