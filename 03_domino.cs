using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
Zadaná úloha: Domino
Komentář: Jednoduchá úloha na prohledávání.

Program najde nejdelší řadu, jakou lze postavit ze zadaných kostek domina a vytiskne její délku. Kostky lze při připojování otočit.

Kostky domina mají svá políčka ohodnocená hodnotami v rozsahu 1..38, počet kostek je nejvýše 16.

Vstupem programu je počet kostek N (nejvýše 16) a potom N dvojic čísel z rozsahu 1..38 popisující jednotlivé kostičky.

Například pro vstup:

5  1 2  1 2  2 3  2 17  2 17
Bude odpovídající výstup:

5
Tzn. nejdelší řada, kterou ze zadaných kostek můžeme sestavit (například 2-1 1-2 2-17 17-2 2-3), má délku 5.
*/

namespace Domino
{

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

    class Storage
    {

        protected int[,] arr;

        public Storage(int size)
        {
            arr = new int[size + 1, size + 1];
        }

        public void Inc(int x, int y)
        {
            sortTwo(ref x, ref y);
            arr[x, y]++;
        }

        public void Dec(int x, int y)
        {
            sortTwo(ref x, ref y);
            arr[x, y]--;
        }

        public bool Check(int x, int y)
        {
            sortTwo(ref x, ref y);
            return arr[x, y] > 0;
        }

        protected void sortTwo(ref int x, ref int y)
        {
            if (x < y)
                return;

            int c = x;
            x = y;
            y = c;
        }

    }

    class Program
    {
        const int SIZE = 38;

        static int MaxDepth;
        static Storage storage;

        public static void Connect(int last, int depth)
        {
            if (depth > MaxDepth)
                MaxDepth = depth;

            for (int i = 1; i <= SIZE; i++)
            {
                if (!storage.Check(last, i))
                    continue;

                storage.Dec(last, i);
                Connect(i, depth + 1);
                storage.Inc(last, i);
            }
        }

        static void Main(string[] args)
        {
            Reader.CacheAdd("5  1 2  1 2  2 3  2 5  2 5");
            Reader.CacheClear();

            int N = Reader.ReadInt();
            storage = new Storage(SIZE);

            for (int i = 1; i <= N; i++)
            {
                storage.Inc(Reader.ReadInt(), Reader.ReadInt());
            }

            for (int i = 1; i <= SIZE; i++)
            {
                for (int j = 1; j <= SIZE; j++)
                {
                    if (i > j) continue;

                    if (!storage.Check(i, j))
                        continue;

                    storage.Dec(i, j);
                    Connect(i, 1);
                    Connect(j, 1);
                    storage.Inc(i, j);

                }
            }

            Console.WriteLine(MaxDepth);

        }
    }
}
