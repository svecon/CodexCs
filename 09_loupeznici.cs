using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;


/*
 
Loupežníci Jednozub a Bambitka řeší vážný problém. Celý život poctivě loupili a hromadili poklady a teď se o ně chtějí spravedlivě podělit, aby mohli na stará kolena odejít do důchodu. Problém je v tom, že naloupené poklady jsou dosti různorodé (mince různých hodnot, šperky, drahé kameny atd.). Nejjednodušší by bylo samozřejmě věci prodat a podělit se o utržené peníze, jenže prodej tak obrovského množství pokladů najednou by je okamžitě prozradil. Podařilo se jim alespoň odhadnout ceny jednotlivých předmětů a po vás by chtěli, abyste je spravedlivě podělili.

Ceny jednotlivých předmětů vám loupežníci připravili do souboru poklad.in. Na prvním řádku se nachází jediné číslo N, které představuje počet předmětů. Na druhém řádku se nachází čísla C1 až CN oddělená mezerami, přičemž číslo Ci představuje cenu i-tého předmětu.

Vašim úkolem je rozdělit poklad na dvě stejně hodnotné části (přičemž žádný předmět nesmíte rozpůlit), případně oznámit loupežníkům, že to není možné. Výsledek uložte do souboru poklad.out, který bude obsahovat pouze jeden řádek čísel (oddělených mezerami), představujících indexy předmětů (tj. čísla od 1 do N), které připadnou Jednozubovi (předměty, které v souboru neuvedete, zbydou na Bambitku). V případě, že poklad nelze spravedlivě rozdělit, uložte do výstupního souboru pouze slovo "no". Pokud existuje správných řešení více, stačí vypsat libovolné z nich.

Počet předmětů N, ani celkový součet cen celého pokladu nepřesáhne hodnotu 2 000 000. Všechny ceny Ci jsou větší, nebo rovny 1 (bezcených tretek se loupežníci dávno zbavili).


Příklad 1:
poklad.in
  5
  5 2 6 4 7
poklad.out
  2 3 4

Příklad 2:
poklad.in
  4
  3 5 3 3
poklad.out
  no
  
 */ 


namespace PartitionProblem
{

    class Solver
    {

        int[] available;
        List<int> treasures;
        int sum;
        int currentMax;

        public Solver(int maxSum)
        {
            available = new int[maxSum];
            treasures = new List<int>();

            // leave max[0] set to 0
            for (int i = 1; i < available.Length; i++)
                available[i] = -1;
        }

        public void AddNewTreasure(int treasure)
        {
            treasures.Add(treasure);
            sum += treasure;

            for (int i = currentMax; i >= 0; i--)
            {
                if ((available[i] != -1) && available[i + treasure] == -1)
                {
                    available[i + treasure] = treasures.Count;

                    if (i + treasure > currentMax)
                        currentMax = i + treasure;
                }
            }
        }

        public void Divide(StreamWriter writer)
        {
            int index = sum / 2;
            if ((sum % 2 == 1) || (available[index] == -1))
                writer.Write("no");
            else
                while (index != 0)
                {
                    writer.Write(available[index]);
                    writer.Write(" ");
                    index -= treasures[available[index] - 1];
                }
            writer.Close();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Reader.ChangeDefaultReader(new StreamReader("poklad.in"));
            Solver s = new Solver(2000000);

            int N = Reader.ReadInt();
            for (int i = 0; i < N; i++)
                s.AddNewTreasure(Reader.ReadInt());

            List<int> resultIndexes = new List<int>();
            s.Divide(new StreamWriter("poklad.out"));
        }
    }

    class Reader
    {
        protected static List<int> cache = new List<int>();

        protected static TextReader readFrom = null;

        public static void ChangeDefaultReader(TextReader newReader)
        {
            readFrom = newReader;
        }

        public static int Read()
        {
            if (cache.Count() == 0)
                return readFrom == null ? Console.Read() : readFrom.Read();

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