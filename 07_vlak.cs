/*

Stavebnice obsahuje ctvercov� kosticky stejn� velikosti s obr�zky kolej�.
Na ka�d� kosticce je vyobrazen jeden �sek spojuj�c� dve strany kosticky.
Kosticky nelze ot�cet, tak�e existuje celkem �est druhu kosticek, podle toho, kter� strany kosticky �sek koleje spojuje:

lev�-prav�
horn�-doln�
lev�-horn�
lev�-doln�
prav�-horn�
prav�-doln�

Napi�te program, kter� pro dan� kosticky zjist� d�lku nejdel�� uzavren� trati, kterou z nich lze sestavit. Trat se nesm� nikde kr�it.

Vstup: �est c�sel ud�vaj�c�ch pocty kosticek typu lev�-prav�, horn�-doln�, lev�-horn�, lev�-doln�, prav�-horn�, prav�-doln�, v tomto porad�.
C�sla jsou v rozsahu 0..100.

V�stup: D�lka nejdel�� uzavren� trati, kterou lze z dan�ch kosticek sestavit.

Napr�klad pro vstup

        3 0 1 2 1 1
bude v�stup 6 odpov�daj�c� trati

        +-   --   -+
        |          |

        |          |
        +-   --   -+
, dve kosticky v tomto pr�pade zustanou nevyu�ity.

*/


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Train
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

    class Program
    {
        static int Min(params int[] vals)
        {
            int min = int.MaxValue;
            for (int i = 0; i < vals.Length; i++)
                if (vals[i] < min)
                    min = vals[i];

            return min;
        }

        static int Even(int val)
        {
            return val & ~1; // & 4094; // (val % 2 == 0) ? val : val - 1;
        }

        static int Solve(int lr, int ud, int lu, int ld, int ru, int rd)
        {
            int common = Min(lu, ld, ru, rd);
            if (common == 0)
                return 0;

            lu -= common; ld -= common; ru -= common; rd -= common;

            int doubles = Min(lr, ud, Math.Max(Min(lu, rd), Min(ru, ld)));

            lr -= doubles; ud -= doubles;

            return 4 * (common + doubles) + Even(lr) + Even(ud);
        }

        static void Main(string[] args)
        {
            //Reader.CacheAdd("3 0 1 2 1 1");
            Console.WriteLine(
                Solve(
                    Reader.ReadInt(),
                    Reader.ReadInt(),
                    Reader.ReadInt(),
                    Reader.ReadInt(),
                    Reader.ReadInt(),
                    Reader.ReadInt()
                )
            );
        }
    }
}