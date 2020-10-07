using System;

namespace AIlab2
{
    class Program
    {
        static void Main(string[] args)
        {
            //new Perceptron().demonstrate();

            int action = 0;//Выбор действия

            Console.WriteLine("Введите '1' для просмтора нейрона, изучающего геометрическую фигуру, введите '2' для просмотра нейронной сети, изучающей цифры");
            action = int.Parse(Console.ReadLine());
            if (action == 1)
            {
                Perceptron perceptron = new Perceptron();
                action = 0;
                do
                {
                    Console.WriteLine("Введите '1' для обучения, введите '2' для проверки, 3 для завершения");
                    action = int.Parse(Console.ReadLine());
                    if (action == 1)
                    {
                        perceptron.study();

                        Console.WriteLine("Обучение завершено");
                    }
                    else if (action == 2)
                    {
                        perceptron.demonstrate();
                        Console.WriteLine("Демонстрация завершена");
                    }
                    else if (action == 3)
                    {
                        Console.WriteLine("Завершение работы");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка, попробуйте снова");
                    }
                } while (action != 3);
            }
            else
            {
                NetworkLayers NetworkLayers0 = new NetworkLayers(0);
                NetworkLayers NetworkLayers1 = new NetworkLayers(1);
                NetworkLayers NetworkLayers2 = new NetworkLayers(2);
                NetworkLayers NetworkLayers3 = new NetworkLayers(3);
                NetworkLayers NetworkLayers4 = new NetworkLayers(4);
                NetworkLayers NetworkLayers5 = new NetworkLayers(5);
                NetworkLayers NetworkLayers6 = new NetworkLayers(6);
                NetworkLayers NetworkLayers7 = new NetworkLayers(7);
                NetworkLayers NetworkLayers8 = new NetworkLayers(8);
                NetworkLayers NetworkLayers9 = new NetworkLayers(9);

                action = 0;
                do
                {
                    Console.WriteLine("Введите '1' для обучения, введите '2' для проверки, 3 для завершения");
                    action = int.Parse(Console.ReadLine());
                    if (action == 1)
                    {
                        NetworkLayers0.study();
                        NetworkLayers1.study();
                        NetworkLayers2.study();
                        NetworkLayers3.study();
                        NetworkLayers4.study();
                        NetworkLayers5.study();
                        NetworkLayers6.study();
                        NetworkLayers7.study();
                        NetworkLayers8.study();
                        NetworkLayers9.study();

                        Console.WriteLine("Обучение завершено");
                    }
                    else if (action == 2)
                    {
                        double outer0 = NetworkLayers0.demonstrate("numbers/9/0.bmp");
                        double outer1 = NetworkLayers1.demonstrate("numbers/9/0.bmp");
                        double outer2 = NetworkLayers2.demonstrate("numbers/9/0.bmp");
                        double outer3 = NetworkLayers3.demonstrate("numbers/9/0.bmp");
                        double outer4 = NetworkLayers4.demonstrate("numbers/9/0.bmp");
                        double outer5 = NetworkLayers5.demonstrate("numbers/9/0.bmp");
                        double outer6 = NetworkLayers6.demonstrate("numbers/9/0.bmp");
                        double outer7 = NetworkLayers7.demonstrate("numbers/9/0.bmp");
                        double outer8 = NetworkLayers8.demonstrate("numbers/9/0.bmp");
                        double outer9 = NetworkLayers9.demonstrate("numbers/9/0.bmp");

                        double max = -10.0;
                        if (outer0 > max)
                            max = outer0;
                        if (outer1 > max)
                            max = outer1;
                        if (outer2 > max)
                            max = outer2;
                        if (outer3 > max)
                            max = outer3;
                        if (outer4 > max)
                            max = outer4;
                        if (outer5 > max)
                            max = outer5;
                        if (outer6 > max)
                            max = outer6;
                        if (outer7 > max)
                            max = outer7;
                        if (outer8 > max)
                            max = outer8;
                        if (outer9 > max)
                            max = outer9;

                        if (max == outer0)
                            Console.WriteLine("Цифра - \"0\"");
                        else if (max == outer1)
                            Console.WriteLine("Цифра - \"1\"");
                        else if (max == outer2)
                            Console.WriteLine("Цифра - \"2\"");
                        else if (max == outer3)
                            Console.WriteLine("Цифра - \"3\"");
                        else if (max == outer4)
                            Console.WriteLine("Цифра - \"4\"");
                        else if (max == outer5)
                            Console.WriteLine("Цифра - \"5\"");
                        else if (max == outer6)
                            Console.WriteLine("Цифра - \"6\"");
                        else if (max == outer7)
                            Console.WriteLine("Цифра - \"7\"");
                        else if (max == outer8)
                            Console.WriteLine("Цифра - \"8\"");
                        else
                            Console.WriteLine("Цифра - \"9\"");
                        Console.WriteLine("0 = " + outer0 + " 1 = " + outer1 + " 2 = " + outer2 + " 3 = " + outer3 + " 4 = " + outer4 + " 5 = " + outer5 + " 6 = " + outer6 + " 7 = " + outer7 + " 8 = " + outer8 + " 9 = " + outer9);
                    }
                    else if (action == 3)
                    {
                        Console.WriteLine("Завершение работы");
                    }
                    else
                    {
                        Console.WriteLine("Ошибка, попробуйте снова");
                    }
                } while (action != 3);
            }
        }
    }
}
