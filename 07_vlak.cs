/*

Stavebnice obsahuje ctvercové kosticky stejné velikosti s obrázky kolejí.
Na každé kosticce je vyobrazen jeden úsek spojující dve strany kosticky.
Kosticky nelze otácet, takže existuje celkem šest druhu kosticek, podle toho, které strany kosticky úsek koleje spojuje:

levá-pravá
horní-dolní
levá-horní
levá-dolní
pravá-horní
pravá-dolní

Napište program, který pro dané kosticky zjistí délku nejdelší uzavrené trati, kterou z nich lze sestavit. Trat se nesmí nikde krížit.

Vstup: Šest císel udávajících pocty kosticek typu levá-pravá, horní-dolní, levá-horní, levá-dolní, pravá-horní, pravá-dolní, v tomto poradí.
Císla jsou v rozsahu 0..100.

Výstup: Délka nejdelší uzavrené trati, kterou lze z daných kosticek sestavit.

Napríklad pro vstup

        3 0 1 2 1 1
bude výstup 6 odpovídající trati

        +-   --   -+
        |          |

        |          |
        +-   --   -+
, dve kosticky v tomto prípade zustanou nevyužity.

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