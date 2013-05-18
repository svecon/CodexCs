using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


/*

V některých elektronických zařízeních se texty zadávají pomocí tabulky písmen.
V tabulce se pohybuje kurzor (výchozí poloha v levém horním roku, pohyb tlačítky šipek nahoru, dolů, doleva a doprava) a klávesou Enter volíme zapsání znaku.


Pokud například tabulka vypadá takto:

      ABCDEFGH
      IJKLMNOP
      QRSTUVWX
      YZ

, text "AHOJ" napíšeme stisky tlačítek (jedno z možných řešení)

      Enter
      doprava
      doprava
      doprava
      doprava
      doprava
      doprava
      doprava
      Enter
      doleva
      dolů
      Enter
      doleva
      doleva
      doleva
      doleva
      doleva
      Enter

Napište program, který pro danou tabulku a text určí a vytiskne minimální počet stisků tlačítek potřebných pro napsání textu.

Pozor: Písmena v tabulce se mohou opakovat!

Vstup obsahuje čísla udávající šířku a výšku tabulky (každé na samostatném řádku), dále na jednom řádku obsah tabulky (čtené po řádkách) a nakonec text, který má být napsán. Znaky textu, které nejsou v tabulce, budou ignorovány.

Příklad:
Vstup:
   3
   3
   ABCBFECDF
   ABCDEFA
Výstup:
   15
Komentář:
Tabulka v tomto příkladu má podobu

   ABC
   BFE
   CDF

, text ABCDEFA lze napsat více způsoby, 15 stisků je délka nejkratšího z nich. 
  
 */

namespace Abeceda
{

    struct Coord
    {

        public int x;
        public int y;

        public Coord(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

    }

    class Map
    {

        public Dictionary<char, List<Coord>> dict = new Dictionary<char, List<Coord>>();

        public Coord size;

        public Map(int x, int y, string chars)
        {
            size = new Coord(x, y);

            for (int i = 0; i < chars.Length; i++)
            {
                addChar((char)chars[i], i % size.x, i / size.x);
            }
        }

        void addChar(char c, int x, int y)
        {
            List<Coord> temp;
            try
            {
                dict.TryGetValue(c, out temp);
                temp.Add(new Coord(x, y));
            }
            catch (NullReferenceException)
            {
                temp = new List<Coord>();
                temp.Add(new Coord(x, y));
                dict.Add(c, temp);
            }
        }

        int calcDistance(Coord m, Coord n)
        {
            return Math.Abs(m.x - n.x) + Math.Abs(m.y - n.y);
        }

        string filterSolveableString(string s)
        {
            string ns = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (!dict.ContainsKey(s[i]))
                    continue;
                ns += s[i];
            }
            return ns;
        }

        public int Solve(string s)
        {
            s = filterSolveableString(s);

            if (s.Length == 0)
                throw new ArgumentException();

            List<Coord> prevChar;
            List<Coord> currChar;
            List<int> prevSolution = new List<int>();
            List<int> currSolution = new List<int>();

            dict.TryGetValue(s[0], out prevChar);
            for (int i = 0; i < prevChar.Count; i++)
                prevSolution.Add(calcDistance(prevChar[i], new Coord(0, 0)) + 1);

            for (int charCount = 1; charCount < s.Length; charCount++)
            {
                dict.TryGetValue(s[charCount], out currChar);

                for (int j = 0; j < currChar.Count; j++)
                {
                    currSolution.Add(int.MaxValue);

                    for (int i = 0; i < prevChar.Count; i++)
                        if (calcDistance(prevChar[i], currChar[j]) + prevSolution[i] < currSolution[j])
                            currSolution[j] = calcDistance(prevChar[i], currChar[j]) + prevSolution[i] + 1;

                }

                prevChar = currChar;
                prevSolution = currSolution;
                currSolution = new List<int>();
            }

            return prevSolution.Min();
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Map map = new Map(
                int.Parse(Console.ReadLine()),
                int.Parse(Console.ReadLine()),
                Console.ReadLine()
            );

            Console.WriteLine(map.Solve(Console.ReadLine()));
        }
    }
}
