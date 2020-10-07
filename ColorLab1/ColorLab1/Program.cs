using System;

namespace ColorLab1
{
    class Program
    {
        static double l;
        static double a;
        static double b;

        static double l0;
        static double a0;
        static double b0;

        static double l1;
        static double c1;
        static double h1;

        static double r2;
        static double g2;
        static double b2;

        static double h3;
        static double s3;
        static double b3;

        static double h4;
        static double s4;
        static double i4;

        static void Main(string[] args)
        {
            Console.WriteLine("Введите канал L");
            l0 = double.Parse(Console.ReadLine());
            l = l0;
            Console.WriteLine("Введите канал A");
            a0 = double.Parse(Console.ReadLine());
            a = a0;
            Console.WriteLine("Введите канал B");
            b0 = double.Parse(Console.ReadLine());
            b = b0;
            show_LAB_channels();

            Console.WriteLine("Конвертация в LCH");
            transform_to_LCH();
            Console.WriteLine("Конвертация в RGB");
            transform_to_RGB();
            Console.WriteLine("Конвертация в HSB");
            transform_to_HSB();
            Console.WriteLine("Конвертация в HSI");
            transform_to_HSI();

            Console.WriteLine("Обратная конвертация из LCH");
            transform_from_LCH();
            Console.WriteLine("Обратная конвертация из RGB");
            transform_from_RGB();
            Console.WriteLine("Обратная конвертация из HSB");
            transform_from_HSB();
            Console.WriteLine("Обратная конвертация из HSI");
            transform_from_HSI();
            double e00 = Calc_Delta_E00();
            Console.WriteLine("Погрешность Е00 = " + e00);
            double e74 = Calc_Delta_E74();
            Console.WriteLine("Погрешность Е74 = " + e74);
            double e94 = Calc_Delta_E94();
            Console.WriteLine("Погрешность Е94 = " + e94);
        }

        private static void show_LAB_channels()
        {
            Console.WriteLine("Значение канала L = " + l0);
            Console.WriteLine("Значение канала A = " + a0);
            Console.WriteLine("Значение канала B = " + b0);
        }
        private static void transform_to_LCH()
        {
            double arct;

            l1 = l0;
            c1 = Math.Sqrt(a0 * a0 + b0 * b0);
            arct = Math.Atan(b0 / a0);
            if (arct >= 0)
                h1 = arct;
            else
                h1 = arct + 360;

            Console.WriteLine("Значение канала L = " + l1);
            Console.WriteLine("Значение канала C = " + c1);
            Console.WriteLine("Значение канала H = " + h1);
        }

        private static void transform_to_RGB()
        {
            double x;
            double y;
            double z;

            double fx;
            double fy;
            double fz;

            double e = 0.008856;
            double k = 903.3;

            fy = (l0 + 16) / 116;
            fz = fy - b0 / 200;
            fx = a0 / 500 + fy;

            if (fx * fx * fx > e)
                x = fx * fx * fx;
            else
                x = (116 * fx - 16) / k;

            if (l0 > k * e)
                y = ((l0 + 16) / 116) * ((l0 + 16) / 116) * ((l0 + 16) / 116);
            else
                y = l0 / k;

            if (fz * fz * fz > e)
                z = fz * fz * fz;
            else
                z = (116 * fz - 16) / k;

            x = 95.799 * x;
            y = 100.00 * y;
            z = 90.926 * z;

            r2 = 3.2404542 * x - 1.5371385 * y - 0.4985314 * z;
            g2 = -0.9692660 * x + 1.8760108 * y + 0.0415560 * z;
            b2 = 0.0556434 * x - 0.2040259 * y + 1.0572252 * z;

            Console.WriteLine("Значение канала R = " + r2);
            Console.WriteLine("Значение канала G = " + g2);
            Console.WriteLine("Значение канала B = " + b2);
        }

        private static void transform_to_HSB()
        {
            double r_;
            double g_;
            double b_;

            double max;
            double min;

            double delta;

            r_ = r2 / 255;
            g_ = g2 / 255;
            b_ = b2 / 255;
            if (r_ > g_ && r_ > b_)
                max = r_;
            else if (g_ > r_ && g_ > b_)
                max = g_;
            else
                max = b_;
            if (r_ < g_ && r_ < b_)
                min = r_;
            else if (g_ < r_ && g_ < b_)
                min = g_;
            else
                min = b_;

            delta = max - min;

            b3 = max;
            if (max == 0)
                s3 = 0;
            else
                s3 = delta / max;

            if (max == r_)
                h3 = 60 * (((g_ - b_) / delta) % 6);
            else if (max == g_)
                h3 = 60 * ((b_ - r_) / delta + 2);
            else
                h3 = 60 * ((r_ - g_) / delta + 4);

            Console.WriteLine("Значение канала H = " + h3);
            Console.WriteLine("Значение канала S = " + s3);
            Console.WriteLine("Значение канала B = " + b3);
        }
        private static void transform_to_HSI()
        {
            double r_;
            double g_;
            double b_;

            double max;
            double min;

            double delta;

            r_ = r2 / 255;
            g_ = g2 / 255;
            b_ = b2 / 255;
            if (r_ > g_ && r_ > b_)
                max = r_;
            else if (g_ > r_ && g_ > b_)
                max = g_;
            else
                max = b_;
            if (r_ < g_ && r_ < b_)
                min = r_;
            else if (g_ < r_ && g_ < b_)
                min = g_;
            else
                min = b_;

            delta = max - min;

            i4 = (max + min) / 2;
            if (delta == 0)
                s4 = 0;
            else
                s4 = delta / (1 - Math.Abs(2 * i4 - 1));
            if (delta == 0)
                h4 = 0;
            else if (max == r_)
                h4 = 60 * (((g_ - b_) / delta) % 6);
            else if (max == g_)
                h4 = 60 * ((b_ - r_) / delta + 2);
            else
                h4 = 60 * ((r_ - g_) / delta + 4);

            Console.WriteLine("Значение канала H = " + h4);
            Console.WriteLine("Значение канала S = " + s4);
            Console.WriteLine("Значение канала I = " + i4);
        }

        private static void transform_from_LCH()
        {
            l0 = l1;
            a0 = c1 * Math.Cos(h1);
            b0 = c1 * Math.Sin(h1);

            show_LAB_channels();
        }
        private static void transform_from_RGB()
        {
            double x;
            double y;
            double z;

            double e;
            double k;

            double fx;
            double fy;
            double fz;

            e = 0.008856;
            k = 903.3;

            x = 0.412453 * r2 + 0.35758 * g2 + 0.180423 * b2;
            y = 0.212671 * r2 + 0.71516 * g2 + 0.072169 * b2;
            z = 0.019334 * r2 + 0.119193 * g2 + 0.950227 * b2;

            x = x / 96.799;
            y = y / 100.00;
            z = z / 90.926;

            if (x > e)
                fx = Math.Pow(x, 3);
            else
                fx = (k * x + 16) / 116;

            if (y > e)
                fy = Math.Pow(y, 3);
            else
                fy = (k * y + 16) / 116;

            if (z > e)
                fz = Math.Pow(z, 3);
            else
                fz = (k * z + 16) / 116;

            l0 = 116 * fy - 16;
            a0 = 500 * (fx - fy);
            b0 = 200 * (fy - fz);

            show_LAB_channels();
        }

        private static void transform_from_HSB()
        {
            double c;
            double x;
            double m;

            c = b3 * s3;
            x = c * (1 - Math.Abs((h3 / 60) % 2 - 1));
            m = b3 - c;

            if (0 <= h3 && h3 < 60)
            {
                r2 = c;
                g2 = x;
                b2 = 0;
            }
            else if (h3 < 120)
            {
                r2 = x;
                g2 = c;
                b2 = 0;
            }
            else if (h3 < 180)
            {
                r2 = 0;
                g2 = c;
                b2 = x;
            }
            else if (h3 < 240)
            {
                r2 = 0;
                g2 = x;
                b2 = c;
            }
            else if (h3 < 300)
            {
                r2 = x;
                g2 = 0;
                b2 = c;
            }
            else
            {
                r2 = c;
                g2 = 0;
                b2 = x;
            }
            r2 = (r2 + m) * 255;
            g2 = (g2 + m) * 255;
            b2 = (b2 + m) * 255;

            transform_from_RGB();
        }

        private static void transform_from_HSI()
        {
            double c;
            double x;
            double m;

            c = (1 - Math.Abs(2 * i4 - 1)) * s4;
            x = c * (1 - Math.Abs((h4 / 60) % 2 - 1));
            m = i4 - c / 2;

            if (0 <= h3 && h3 < 60)
            {
                r2 = c;
                g2 = x;
                b2 = 0;
            }
            else if (h3 < 120)
            {
                r2 = x;
                g2 = c;
                b2 = 0;
            }
            else if (h3 < 180)
            {
                r2 = 0;
                g2 = c;
                b2 = x;
            }
            else if (h3 < 240)
            {
                r2 = 0;
                g2 = x;
                b2 = c;
            }
            else if (h3 < 300)
            {
                r2 = x;
                g2 = 0;
                b2 = c;
            }
            else
            {
                r2 = c;
                g2 = 0;
                b2 = x;
            }
            r2 = (r2 + m) * 255;
            g2 = (g2 + m) * 255;
            b2 = (b2 + m) * 255;

            transform_from_RGB();
        }
        private static double Calc_Delta_E74() //E74
        {
            double result;

            result = Math.Round(Math.Sqrt(Math.Pow(l0 - l, 2) + Math.Pow(a0 - a, 2) + Math.Pow(b0 - b, 2)), 2, MidpointRounding.AwayFromZero);
            return result;
        }
        private static double Calc_Delta_E94() //E94
        {
            double c1;
            double c2;
            double dl;
            double dc;
            double de;
            double dh;
            double sc;
            double sh;
            double result;

            c1 = Math.Sqrt(Math.Pow(a0, 2) + Math.Pow(b0, 2));
            c2 = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            dl = l - l0;
            dc = c2 - c1;
            de = Math.Sqrt(Math.Pow(l0 - l, 2) + Math.Pow(a0 - a, 2) + Math.Pow(b0 - b, 2));
            dh = (de * de) - (dl * dl) - (dc * dc);
            if (dh > 0)
                dh = Math.Sqrt(dh);
            else
                dh = 0;
            sc = 1 + (0.045 * c1);
            sh = 1 + (0.015 * c1);
            dl /= l1;
            dc /= c1 * sc;
            dh /= h1 * sh;
            result = Math.Round(Math.Sqrt(Math.Pow(dl, 2) + Math.Pow(dc, 2) + Math.Pow(dh, 2)), 2, MidpointRounding.AwayFromZero);
            return result;
        }
       private static double Calc_Delta_E00() //E00
        {
            double c1;
            double c2;
            double cx;
            double gx;
            double nn;
            double h1;
            double h2;
            double dl;
            double dc;           
            double dh;
            double lx;
            double cy;
            double hx;
            double tx;
            double ph;
            double rc;
            double sl;
            double sc;
            double sh;
            double rt;
            double result;

            c1 = Math.Sqrt(Math.Pow(a0, 2) + Math.Pow(b0, 2));
            c2 = Math.Sqrt(Math.Pow(a, 2) + Math.Pow(b, 2));
            cx = (c1 + c2) / 2;
            gx = 0.5 * (1 - Math.Sqrt(Math.Pow(cx, 7) / (Math.Pow(cx, 7) + Math.Pow(25, 7))));
            nn = (1 + gx) * l0;
            c1 = Math.Sqrt(nn * nn + Math.Pow(b0, 2));
            h1 = findAngle(nn, b0);
            nn = (1 + gx) * a;
            c2 = Math.Sqrt(nn * nn + Math.Pow(b, 7));
            h2 = findAngle(nn, b);
            dl = l - l0;
            dc = c2 - c1;
            if ((c1 * c2) == 0)
                dh = 0;
            else
            {
                nn = Math.Round(h2 - h1, 12, MidpointRounding.AwayFromZero);
                if (Math.Abs(nn) <= 180)
                    dh = h2 - h1;
                else
                {
                    if (nn > 180)
                        dh = h2 - h1 - 360;
                    else
                        dh = h2 - h1 + 360;
                }
            }
            dh = 2 * Math.Sqrt(c1 * c2) * Math.Sin(dh / 2);
            lx = (l0 + l0) / 2;
            cy = (c1 + c2) / 2;
            if ((c1 * c2) == 0)
                hx = h1 + h2;
            else
            {
                nn = Math.Abs(Math.Round(h1 - h2, 12));
                if (nn > 180)
                {
                    if ((h2 + h1) < 360) hx = h1 + h2 + 360;
                    else hx = h1 + h2 - 360;
                }
                else
                    hx = h1 + h2;
                hx /= 2;
            }
            tx = 1 - 0.17 * Math.Cos(hx - (Math.PI / 6)) + 0.24 * Math.Cos((2 * hx) * Math.PI / 180) + 0.32 * Math.Cos((3 * hx + (Math.PI / 30)) * Math.PI / 180) - 0.20 * Math.Cos(4 * hx - (7 * Math.PI / 20));
            ph = 30 * Math.Exp(-((hx - 275) / 25) * ((hx - 275) / 25));
            rc = 2 * Math.Sqrt(Math.Pow(cy, 7) / (Math.Pow(cy, 7) + Math.Pow(25, 7)));
            sl = 1 + ((0.015 * Math.Pow(lx - 50, 2)) / Math.Sqrt(20 + Math.Pow(lx - 50, 2)));
            sc = 1 + 0.045 * cy;
            sh = 1 + 0.015 * cy * tx;
            rt = -Math.Sin((2 * ph) * Math.PI / 180) * rc;
            dl = dl / (l1 * sl);
            dc = dc / (c1 * sc);
            dh = dh / (h1 * sh);
            result = Math.Round(Math.Sqrt(Math.Pow(dl, 2) + Math.Pow(dc, 2) + Math.Pow(dh, 2) + rt * dc * dh), 2, MidpointRounding.AwayFromZero);
            return result;
        }
        private static double findAngle(double a, double b)
        {
            double local = 0;
            if (a >= 0 && b == 0) return 0;
            if (a < 0 && b == 0) return 180;
            if (a == 0 && b > 0) return 90;
            if (a == 0 && b < 0) return 270;
            if (a > 0 && b > 0) local = 0;
            if (a < 0) local = 180;
            if (a > 0 && b < 0) local = 360;
            return ((Math.Atan(b / a) / Math.PI * 180) + local);
        }
    }
}
