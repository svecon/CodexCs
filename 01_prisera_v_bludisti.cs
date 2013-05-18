using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*

Vybraná zadaná úloha: Příšera v bludišti
Komentář: V bludišti reprezentovaném maticí políček se nachází příšera. Simulujte její pohyb.

V bludišti reprezentovaném maticí políček se nachází příšera. Příšera je v každém kroku na jednom políčku a je otočená jedním ze čtyř možných směrů - nahoru, doprava, dolů nebo doleva.

V každém kroku výpočtu příšera udělá jeden tah, možné tahy jsou: otočit se doprava, otočit se doleva nebo postoupit kupředu, tj. přejít na sousední políčko ve směru otočení. Na začátku má příšera po pravé ruce zeď a pohybuje se tak, aby pravou rukou stále sledovala tuto zeď (viz příklad).

Vstup programu obsahuje nejdříve šířku a výšku a potom mapu bludiště. Jednotlivé znaky představují jednotlivá políčka mapy: 'X' znamená zeď, '.' znamená volné políčko a znaky '^', '>', 'v' nebo '<' znamenají volné políčko, na kterém právě teď stojí příšera, otočená směrem nahoru, doprava, dolů nebo doleva, v tomto pořadí.

Program načte popis bludiště a potom dvacetkrát pohne příšerou a po každém pohybu vytiskne mapu bludiste ve stejném tvaru, jako ji načetl. Výpis mapy bude vždy ukončen prázdným řádkem.

Pozor: První vypsaná poloha a orientace je až po prvním tahu příšery!


Příklad:
Vstup (začíná prvním znakem na řádku, zde pro přehlednost posunutý doprava):

   10
   6
   XXXXXXXXXX
   X....X...X
   X....X...X
   X.X..X.X.X
   X.X.>..X.X
   XXXXXXXXXX
Výstup (také posunutý doprava):

   XXXXXXXXXX
   X....X...X
   X....X...X
   X.X..X.X.X
   X.X..>.X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X...X
   X.X..X.X.X
   X.X...>X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X...X
   X.X..X.X.X
   X.X...^X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X...X
   X.X..X^X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X^..X
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X>..X
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X.>.X
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X..>X
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X..vX
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X...X
   X.X..X.XvX
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X...X
   X.X..X.X.X
   X.X....XvX
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X...X
   X.X..X.X.X
   X.X....X>X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X...X
   X.X..X.X.X
   X.X....X^X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X...X
   X.X..X.X^X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X...X
   X....X..^X
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X..^X
   X....X...X
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X..<X
   X....X...X
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X.<.X
   X....X...X
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....X<..X
   X....X...X
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

   XXXXXXXXXX
   X....Xv..X
   X....X...X
   X.X..X.X.X
   X.X....X.X
   XXXXXXXXXX

*/

namespace ConsoleApplication4
{

    class InputCacher
    {
        protected static List<int> cache = new List<int>();
        protected static bool enabled = false;

        public static void AddCache(string buffer)
        {
            enabled = true;

            for (int i = 0; i < buffer.Length; i++)
            {
                cache.Add(buffer[i]);
            }
        }

        public static int Read()
        {
            if (!enabled || cache.Count() == 0)
                return Console.Read();

            int c = cache.ElementAt(0);
            cache.RemoveAt(0);
            return c;
        }

        public static void Disable()
        {
            enabled = false;
        }

        public static void Enable()
        {
            enabled = true;
        }
    }

    class CodexReader
    {

        /// <returns>Returns last char read</returns>
        protected static int skipNonIntegers()
        {

            int c = InputCacher.Read();

            while ((c != '-') && !isDigit(c))
            {
                c = InputCacher.Read();
            }
            return c;
        }

        public static int getInt()
        {
            int x = 0;
            int c = skipNonIntegers();
            bool isNegative = false;

            if (c == '-')
            {
                isNegative = true;
                c = InputCacher.Read();
            }

            while (isDigit(c))
            {
                x = 10 * x + (c - '0');
                c = InputCacher.Read();
            }

            return isNegative ? (-1) * x : x;
        }

        public static bool isDigit(int znak)
        {
            return (znak >= '0') && (znak <= '9');
        }

        public static int getChar()
        {
            int c = InputCacher.Read();
            return c;
        }

    }

    class Bludiste
    {

        protected int vyska;
        protected int sirka;
        protected char[,] mapa;
        protected Robot robot;

        public Bludiste(int sirka, int vyska)
        {
            this.vyska = vyska;
            this.sirka = sirka;
        }

        public int VelikostMapy()
        {
            return vyska * sirka;
        }

        public void NactiMapu(int[] buffer)
        {
            mapa = new char[vyska, sirka];
            for (int i = 0; i < VelikostMapy(); i++)
            {
                if ("<>^v".Contains((char)buffer[i]))
                {
                    robot = new Robot(this, new Souradnice(i % sirka, i / sirka), new Orientace((char)buffer[i]));
                    mapa[i / sirka, i % sirka] = '.';
                }
                else
                {
                    mapa[i / sirka, i % sirka] = (char)buffer[i];
                }
            }
        }

        public void VypisMapu()
        {
            mapa[robot.souradnice.y, robot.souradnice.x] = robot.smer.SmerToZnak();

            for (int i = 0; i < vyska; i++)
            {
                for (int j = 0; j < sirka; j++)
                {
                    Console.Write(mapa[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();

            mapa[robot.souradnice.y, robot.souradnice.x] = '.';
        }

        public void PohniObjekty()
        {
            robot.Krok();
        }

        public bool JeNaSouradniciZed(Souradnice s)
        {
            return mapa[s.y, s.x] == 'X';
        }

    }

    public struct Souradnice
    {
        public int x;
        public int y;

        public Souradnice(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }

    public class Orientace
    {
        public enum TOrientace { sever, vychod, jih, zapad };
        protected char[] smerZnak = new char[4] { '^', '>', 'v', '<' };
        protected Souradnice[] posun = new Souradnice[4] { new Souradnice(0, -1), new Souradnice(1, 0), new Souradnice(0, 1), new Souradnice(-1, 0) };

        protected TOrientace smer;

        public Orientace(char znak)
        {
            smer = (TOrientace)Array.IndexOf(smerZnak, znak);
        }

        public Souradnice PosunPriOrientaci()
        {
            return posun[(int)smer];
        }

        public void OtocVpravo()
        {
            smer++;
            if ((int)smer == 4)
                smer = (TOrientace)0;
        }

        public void OtocVlevo()
        {
            smer--;
            if ((int)smer == -1)
                smer = (TOrientace)3;
        }

        public char SmerToZnak()
        {
            return smerZnak[(int)smer];
        }


    }

    class Robot
    {

        public Souradnice souradnice;
        public Orientace smer;
        protected Bludiste bludiste;

        protected bool otocilJsemSe = false;

        public Robot(Bludiste bludiste, Souradnice souradnice, Orientace smer)
        {
            this.souradnice = souradnice;
            this.smer = smer;
            this.bludiste = bludiste;
        }

        public void Krok()
        {
            if (otocilJsemSe)
            {
                pohybKupredu();
                otocilJsemSe = false;
            }
            else if (!jePoPraveRuceZed())
            {
                smer.OtocVpravo();
                otocilJsemSe = true;
            }
            else if (!jePredemnouZed())
            {
                pohybKupredu();
            }
            else
            {
                smer.OtocVlevo();
            }
        }

        protected void pohybKupredu()
        {
            souradnice.x = souradnice.x + smer.PosunPriOrientaci().x;
            souradnice.y += smer.PosunPriOrientaci().y;
        }

        protected bool jePoPraveRuceZed()
        {
            smer.OtocVpravo();
            bool b = bludiste.JeNaSouradniciZed(
                new Souradnice(
                    souradnice.x + smer.PosunPriOrientaci().x,
                    souradnice.y + smer.PosunPriOrientaci().y)
                );
            smer.OtocVlevo();
            return b;
        }
        protected bool jePredemnouZed()
        {
            return bludiste.JeNaSouradniciZed(
                new Souradnice(
                    souradnice.x + smer.PosunPriOrientaci().x,
                    souradnice.y + smer.PosunPriOrientaci().y)
                );
        }


    }

    class Program
    {
        static void Main(string[] args)
        {
            InputCacher.AddCache("10 6 ");
            InputCacher.AddCache("XXXXXXXXXX");
            InputCacher.AddCache("X....X...X");
            InputCacher.AddCache("X....X...X");
            InputCacher.AddCache("X.X..X.X.X");
            InputCacher.AddCache("X.X..>.X.X");
            InputCacher.AddCache("XXXXXXXXXX");
            InputCacher.Disable();

            Bludiste bludiste = new Bludiste(CodexReader.getInt(), CodexReader.getInt());

            List<int> buffer = new List<int>();

            while (buffer.Count() < bludiste.VelikostMapy())
            {
                int c = InputCacher.Read();
                if (".X^>v<".Contains((char)c))
                    buffer.Add(c);
            }
            bludiste.NactiMapu(buffer.ToArray());

            for (int i = 1; i <= 20; i++)
            {
                bludiste.PohniObjekty();
                bludiste.VypisMapu();

            }



        }
    }
}