using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication7
{

    class Program
    {
        static int[,] A;

        static void Main(string[] args)
        {
            Reader.CacheAdd("2 4 2 = 4 2 2 = 2 2 -1 = 1 0 1"); //skriptovy priklad
            //Reader.CacheAdd("-5 4 1 / -7 6 1 / 4 -4 -2 / 1 2 1");

            A = new int[3, 3];
            for (int i = 0; i < 9; i++)
                A[i % 3, i / 3] = Reader.ReadInt();

            double[] pom = new double[3];
            for (int i = 0; i < 3; i++)
                pom[i] = Reader.ReadInt();

            Vektor x = new Vektor(pom);
            Vektor xprev = x;
            Vektor y;

            Console.Write("{0} : (", 0); x.Vypis(); Console.Write(") -"); Console.WriteLine();

            for (int i = 0; i < 20; i++)
            {
                y = Pocitej.NasobeniMaticeVektorem(A, x);
                x = y.Clone();
                Pocitej.JednotkovaVelikost(x);

                Console.Write("{0} : (", i + 1);
                x.Vypis();
                Console.Write(") {0}", String.Format("{0:0.###}", Pocitej.SkalarniSoucin(xprev, y)));
                Console.WriteLine();

                xprev = x;
            }

        }
    }

    class Pocitej
    {
        public static double SkalarniSoucin(Vektor x, Vektor y)
        {
            double suma = 0;
            for (int i = 0; i < x.pole.Length; i++)
            {
                suma += x.pole[i] * y.pole[i];
            }
            return suma;
        }

        public static void JednotkovaVelikost(Vektor x)
        {
            double velikost = x.AbsolutValue();
            for (int i = 0; i < x.pole.Length; i++)
            {
                x.pole[i] = (x.pole[i] / velikost);
            }
        }

        public static Vektor NasobeniMaticeVektorem(int[,] A, Vektor x)
        {
            Vektor y = new Vektor(new double[3] { 0, 0, 0 });

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    y.pole[i] += A[j, i] * x.pole[j];
                }
            }

            return y;
        }

        public static Vektor OdectiVektory(Vektor x, Vektor y)
        {
            for (int i = 0; i < x.pole.Length; i++)
                x.pole[i] -= y.pole[i];

            return x;
        }
    }

    /// <summary>
    /// hlavni trida
    /// sklada se z pole, kde jsou ulozeny jednotlive vektorove souradnice
    /// absolutni hodnoty: vypocita absolutni hodnotu daneho vektoru (sebe sama)
    /// dokaze k danemu vektoru pricist vektor, odecist vektor, vynasobit dany vektor skalarem a dany vektor zklonovat
    /// </summary>
    class Vektor
    {
        public double[] pole { get; protected set; }
        public Vektor(double[] pole)
        {
            this.pole = pole;
        }

        public void Vypis()
        {
            for (int i = 0; i < pole.Length; i++)
            {
                Console.Write(String.Format("{0:0.###}", pole[i]));
                Console.Write(" ");
            }
            //Console.WriteLine();
        }

        public double AbsolutValue()
        {
            return (Math.Sqrt(Pocitej.SkalarniSoucin(this, this)));
        }

        public void NasobeniVektoruCislem(double cislo)
        {
            for (int i = 0; i < pole.Length; i++)
            {
                pole[i] = pole[i] * cislo;
            }
        }

        public void Pricti(Vektor x)
        {
            for (int i = 0; i < pole.Length; i++)
            {
                pole[i] += x.pole[i];
            }
        }

        public void Odecti(Vektor x)
        {
            for (int i = 0; i < pole.Length; i++)
            {
                pole[i] -= x.pole[i];
            }
        }

        public Vektor Clone()
        {
            Vektor vratit = new Vektor((double[])this.pole.Clone());
            return vratit;
        }

    }

    class Reader
    {
        protected static List<int> cache = new List<int>();

        public static int Read()
        {
            if (cache.Count() == 0)
                return Console.Read();

            int c = cache.ElementAt(0);
            cache.RemoveAt(0);
            return c;
        }

        public static int ReadInt()
        {
            skipNonInt();
            int x = 0;
            bool isNegative = false;

            int c = Read();
            if (c == '-')
            {
                isNegative = true;
                c = Read();
            }

            while (isDigit(c))
            {
                x = 10 * x + (c - '0');
                c = Read();
            }

            return isNegative ? (-1) * x : x;
        }

        protected static void skipNonInt()
        {
            int c;
            do
            {
                c = Read();
            } while ((c != '-') && !isDigit(c));

            cache.Insert(0, c);
        }

        protected static bool isDigit(int znak)
        {
            return (znak >= '0') && (znak <= '9');
        }

        public static void CacheAdd(string buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                cache.Add(buffer[i]);
            }
            cache.Add(' ');
        }

        public static void CacheClear()
        {
            cache.Clear();
        }

    }


}
