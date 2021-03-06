﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;


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

/*

83644
SIPVMNAzQ.mdinLBEfeacovGDds lmNKObtuahqXTriesuVYwjppCgFZURyJWHkx

82466
AFfgCPhzQicnerjxRdt uo.kwNasliTZSbneelpYEptmaevXDVmIMLqKUOyJHWBG

81958
DQFCMfvzLdonoijxScm uehkIbieraNZRgtsupUYwbnalnPXTV.qmrAKEHyOJWBG

81536
UhfMpwTzEqunseDxPouelmCkMgt ibvZInrcodNYjAisaeQXOt.LSV.KFyJRHWBG

81484
LDSUMIAzjoraecfxVCumlhNkJte nodZQbsiumvYF.uPiaEXTqetpgdKwyRHOWBG

80387
fEjbNMIzQiVCgeSxwPln ahkyA tro.ZDleiuedYOLt sncXTvqpmtnKFUJRHWBG

79222
wFfAPbLzQavortDxMsecluEkjun iaCZyhtstdSYU. m eqXOVpnNsgKTJIRHWBG

78496
ThwACNEzPit.s MxF pdieVkjenr cgZDquaaoLYUilt mSXQbsevIfKORJyHWBG
 * 
 * 74562
 * qsugpEUDiortav TdlaenolLcum suMwpv.tic QSC dbh.eAIpVPrjRyOfNFJHW
  
 * 
 * 74558
 * WReQwLTDHj. Ml UJrhcuovEFPbisnapNVdt etgfp .maruOICvulosyASpcdiq
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
                    throw new InvalidProgramException("Does not contain Key: " + s[i]);
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

    struct RandValue
    {
        public int c;
        public int val;

        public RandValue(int c, int val)
        {
            this.c = c;
            this.val = val;
        }
    }

    class Density
    {

        Dictionary<int, int> chars;
        List<RandValue> randomizer;
        int randomTo;
        Random rnd;
        int basicSum = 0;
        List<RandValue> basicList;

        public Density(string dictionary)
        {
            chars = new Dictionary<int, int>();
            basicList = new List<RandValue>();
            rnd = new Random();

            foreach (char c in dictionary)
                //if (!chars.ContainsKey(c))
                chars.Add(c, 0);
        }

        int getNextRandomChar()
        {
            int sum = 0;
            int r = rnd.Next(randomTo);

            sum = 0;
            for (int i = 0; i < randomizer.Count; i++)
            {
                sum += randomizer[i].val;

                if (sum >= r)
                {
                    randomTo -= randomizer[i].val;
                    int x = randomizer[i].c;

                    randomizer.RemoveAt(i);

                    return x;
                }

            }

            throw new InvalidProgramException();
        }

        public string FillSpiral(char[,] M)
        {
            generateList();

            int[,] v = new int[,] { { 1, 0 }, { 0, 1 }, { -1, 0 }, { 0, -1 } };

            int x = (M.GetLength(0) / 2) - 1;
            int y = x;
            int dir = 0;
            int ndir = (dir + 1) % 4;
            for (int i = 1; i <= M.GetLength(0) * M.GetLength(1); i++)
            {
                M[x, y] = (char)getNextRandomChar();
                x += v[dir, 0];
                y += v[dir, 1];
                if ((x + v[ndir, 0] >= 0) && (x + v[ndir, 0] < M.GetLength(0)) && (y + v[ndir, 1] >= 0) && (y + v[ndir, 1] < M.GetLength(0)))
                    if (M[x + v[ndir, 0], y + v[ndir, 1]] == 0)
                    {
                        dir = ndir;
                        ndir = (dir + 1) % 4;
                    }
            }

            string s = "";
            for (int i = 0; i < M.GetLength(0); i++)
            {
                for (int j = 0; j < M.GetLength(1); j++)
                {
                    s += (char)M[i, j];
                }
            }
            return s;

        }

        public void CalculatePercentages(string lorem)
        {

            foreach (char c in lorem)
                chars[c]++;

            basicSum = 0; // lorem.Length;
            foreach (KeyValuePair<int, int> pair in chars.OrderByDescending(pair => pair.Value))
            {
                basicSum += pair.Value;
                basicList.Add(new RandValue(pair.Key, pair.Value));
                //Console.WriteLine("{0}: {1}", (char)pair.Key, pair.Value);
            }

            //foreach (KeyValuePair<int, int> pair in chars.OrderByDescending(pair => pair.Value))
            //    Console.WriteLine("{0}: {1} = {2}%", (char)pair.Key, pair.Value, (double)pair.Value / lorem.Length * 100);

        }

        public Vektor CalculatePageRank(double[,] A)
        {
            double[] pom = new double[A.GetLength(0)];
            for (int i = 0; i < pom.Length; i++)
                pom[i] = 1;

            Vektor x = new Vektor(pom);
            Vektor xprev = x;
            Vektor y;

            ////Console.WriteLine(x.pole.Length);
            //Console.WriteLine(A.GetLength(0));
            //Console.Write("{0} : (", 0); x.Vypis(); Console.Write(") -"); Console.WriteLine();

            for (int i = 0; i < 20; i++)
            {
                y = Pocitej.NasobeniMaticeVektorem(A, x);
                x = y.Clone();
                Pocitej.JednotkovaVelikost(x);

                //Console.Write("{0} : (", i + 1);
                //x.Vypis();
                //Console.Write(") {0}", String.Format("{0:0.###}", Pocitej.SkalarniSoucin(xprev, y)));
                //Console.WriteLine();

                xprev = x;
            }
            return x;
        }

        public void CalculatePercentagesRanked(string lorem)
        {
            double[,] matrix = new double[chars.Count, chars.Count];
            List<int> pos = new List<int>();

            foreach (KeyValuePair<int, int> pair in chars)
            {
                pos.Add(pair.Key);
            }

            for (int i = 1; i < lorem.Length; i++)
            {
                matrix[pos.IndexOf(lorem[i]), pos.IndexOf(lorem[i - 1])]++;
            }


            for (int x = 0; x < matrix.GetLength(1); x++)
            {
                double sum = 0;

                for (int y = 0; y < matrix.GetLength(0); y++)
                {
                    sum += matrix[x, y];
                }

                if (sum != 0)
                    for (int y = 0; y < matrix.GetLength(0); y++)
                    {
                        matrix[x, y] /= sum;
                    }
            }

            Vektor ranked = CalculatePageRank(matrix);

            for (int i = 0; i < ranked.pole.Length; i++)
            {
                chars[pos[i]] = (int)(ranked.pole[i] * 1000);
            }

            basicSum = 0; // lorem.Length;
            foreach (KeyValuePair<int, int> pair in chars.OrderByDescending(pair => pair.Value))
            {
                basicSum += pair.Value;
                basicList.Add(new RandValue(pair.Key, pair.Value));
                //Console.WriteLine("{0}: {1}", (char)pair.Key, pair.Value);
            }

            //foreach (KeyValuePair<int, int> pair in chars.OrderByDescending(pair => pair.Value))
            //    Console.WriteLine("{0}: {1} = {2}%", (char)pair.Key, pair.Value, (double)pair.Value / lorem.Length * 100);

        }

        void generateList()
        {
            randomizer = new List<RandValue>(basicList);
            randomTo = basicSum;

            while (randomizer.Count < 64)
            {

                int r = rnd.Next(basicSum);

                int sum = 0;
                foreach (KeyValuePair<int, int> pair in chars.OrderByDescending(pair => pair.Value))
                {
                    sum += pair.Value * 3 / 2;

                    if (sum >= r)
                    {
                        randomTo += pair.Value;
                        randomizer.Add(new RandValue(pair.Key, pair.Value));
                        break;
                    }

                }

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
            double velikost = x.pole.Max();//.AbsolutValue();
            if (velikost == 0)
                return;
            for (int i = 0; i < x.pole.Length; i++)
            {
                x.pole[i] = (x.pole[i] / velikost);
            }
        }

        public static Vektor NasobeniMaticeVektorem(double[,] A, Vektor x)
        {
            Vektor y = new Vektor(new double[x.pole.Length]);
            for (int i = 0; i < x.pole.Length; i++)
            {
                for (int j = 0; j < x.pole.Length; j++)
                {
                    y.pole[i] += (A[j, i] * x.pole[j]);
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


    class Program
    {

        static string lorem = "Lorem ipsum dolor sit amet consectetuer nunc augue at turpis cursus. Vestibulum eleifend dui mauris tincidunt ligula volutpat purus felis adipiscing lacinia. Sagittis orci Lorem lobortis hac tincidunt dis Pellentesque vel et Proin. Magnis tellus sit malesuada id elit hendrerit nec malesuada cursus Nunc. Ipsum justo arcu tincidunt nunc Mauris scelerisque elit Aliquam dictum Vivamus. Semper dui magnis at cursus tellus Vivamus at ligula sem malesuada. Ut interdum nibh Vivamus faucibus elit vitae lorem libero commodo augue. Curabitur mauris eget et orci nec auctor convallis ipsum fringilla condimentum. Vestibulum tellus non lacus et Curabitur aliquet ante convallis facilisi metus. Consectetuer sodales magna ut mollis condimentum sit nunc sed sed laoreet. Commodo malesuada libero Integer laoreet pretium Integer elit sociis Sed felis. Velit ac et leo ac nunc pretium pede pellentesque Vestibulum purus. Vestibulum cursus leo mi Nunc iaculis id Pellentesque Curabitur enim semper. Risus tempor pulvinar laoreet nunc justo tellus Mauris tincidunt pretium magna. Et at ac Phasellus purus urna faucibus metus turpis pede vel. Laoreet faucibus wisi elit Vestibulum justo nibh montes sapien tempus dictum. Sagittis et interdum consectetuer ut ac ipsum libero nulla diam dis. Sed Proin leo tempor consectetuer Nulla Nam justo fames natoque lorem. Vel vestibulum lacinia a et pellentesque Vivamus laoreet et ut vitae. Scelerisque dolor Curabitur id et consequat Cras convallis laoreet Phasellus fames. Vestibulum a scelerisque vel ultrices Nam massa ac Curabitur mauris tempus. Pellentesque id In et tempor Nullam justo Duis et sagittis commodo. Et faucibus a a Lorem amet sem in ac Ut Curabitur. Montes consectetuer accumsan vel justo venenatis nibh dui arcu semper convallis. Dui non tellus ut ligula Vestibulum pede at Nunc urna tellus. Nulla id tincidunt sem eu Nullam nisl Mauris et Vestibulum risus. Cursus ligula consequat enim nec pulvinar a Vestibulum pretium rhoncus nascetur. Arcu sem consequat ut ac dui rutrum Nunc metus lacinia nunc. Quisque condimentum sed est quis nunc Maecenas In nibh metus faucibus. Cursus velit Nam Cum felis at Nam suscipit aliquet wisi non. Et enim pretium tristique vitae ac pellentesque penatibus wisi orci elit. Tincidunt neque parturient felis dolor metus orci sem adipiscing adipiscing semper. Nisl et a turpis non Curabitur hendrerit molestie Vivamus interdum Nulla. Felis dolor adipiscing ut vel feugiat Pellentesque Quisque magnis quam mattis. Orci Nulla risus convallis et platea In enim convallis tortor lobortis. Quisque leo Curabitur faucibus sagittis sem enim semper tempor quis dis. Nam elit eros Sed pede malesuada Nulla sociis congue Mauris vitae. Sem rhoncus justo Cras nulla Aenean ligula augue eros tempus commodo. Pellentesque urna dui elit nec consectetuer ipsum Aliquam quis ut ac. Justo Phasellus sit tellus sociis Nam Maecenas feugiat magna pretium accumsan. Rhoncus vel urna gravida ipsum turpis tristique vel eget semper Morbi. Aenean ac lacinia pretium Integer velit risus non Donec Vivamus interdum. Vitae id Morbi risus pellentesque auctor et Curabitur orci Integer Curabitur. Ornare enim tortor velit odio sagittis pede Curabitur sapien consectetuer vitae. In in egestas Cum iaculis magnis In felis risus neque magna. Nulla nulla Fusce pellentesque nulla ut et Vestibulum nibh turpis magna. Morbi Nulla Vestibulum justo lacus tempus Curabitur malesuada velit condimentum tristique. Commodo pulvinar justo mauris eget et est sapien nec ut condimentum. Et turpis neque pede eget dolor molestie dis Nam ut faucibus. Interdum nibh gravida Duis Vivamus lacus dis Phasellus sit Donec eget. Metus et lacus tellus Curabitur elit leo pharetra nunc nascetur velit. Enim porttitor condimentum enim interdum Curabitur Curabitur et id nunc pede. Interdum fermentum Curabitur parturient congue risus urna ullamcorper quis ipsum ac. Neque congue elit Nullam dui semper rutrum quis Donec malesuada gravida. A lacus accumsan sapien Aliquam eros nibh dui quis sociis laoreet. Tellus Nam leo metus Lorem ipsum semper consequat Vestibulum pretium ante. Nisl Nam eu ipsum natoque orci fames id Vestibulum justo congue. Aliquam sagittis semper quis cursus nibh nibh justo eu diam sem. Aliquam pretium non sit Curabitur lorem Donec montes ut nisl tortor. Dis id consequat nibh nibh Curabitur quis Nulla semper ac vitae. Cursus vel pretium gravida dignissim orci ut tempus dolor ac dolor. Lacinia adipiscing urna tempus consectetuer adipiscing Vivamus adipiscing Aenean ligula Nam. Urna dui Vestibulum at orci Phasellus enim nec sodales ut congue. Ligula adipiscing facilisis Morbi dolor eros ligula sed ac egestas ut. Nisl Duis aliquet sem convallis malesuada dictumst nibh lacus diam nibh. Non est suscipit commodo Phasellus velit Nam a tempus interdum metus. Justo Aenean vel et wisi Proin leo urna nibh euismod Quisque. Tellus wisi auctor augue quam ut sagittis id feugiat convallis Phasellus. Nulla Nam et Morbi aliquam id pede id quis tincidunt Duis. Netus nulla turpis pellentesque mauris Phasellus Nunc Nulla libero molestie felis. Dolor nunc dictum sem consequat rutrum feugiat felis wisi quis sagittis. A dictum Sed volutpat id Curabitur dolor Phasellus tempor orci dictum. Consequat Praesent enim laoreet Mauris id Pellentesque nec magnis urna ridiculus. Amet hendrerit orci Sed ipsum ante vitae cursus eros pretium habitant. Mauris id congue Aenean urna et purus eu gravida elit scelerisque. Et accumsan Pellentesque sem gravida congue adipiscing augue pulvinar Quisque nulla. Sem consequat leo id leo vel augue at semper auctor natoque. Curabitur eu risus Aenean vitae In elit egestas tristique Integer lacus. Nisl lorem ac Aenean Lorem Nulla lorem magna a dui et. Vitae vel lacinia dui Sed elit nibh eu wisi orci euismod. Pellentesque rutrum convallis eros libero Vivamus sed malesuada natoque dolor dapibus. Arcu consequat fermentum quam vel convallis laoreet consectetuer nonummy vestibulum Aliquam. Enim mollis pretium id porta orci fringilla ligula eros ipsum congue. Nibh augue tincidunt est ante Vestibulum elit Nam urna enim vel. Vestibulum metus sagittis urna mauris condimentum consequat id pellentesque quam Sed. Et tortor enim tincidunt lacus at nibh lacus sapien sapien hendrerit. Tristique et semper enim elit ligula In Curabitur ac justo parturient. Vitae ut at parturient Phasellus velit Sed pellentesque nibh convallis enim. Cursus Curabitur ac sit quis vel tincidunt libero In ligula gravida. Ac Nam ipsum iaculis consequat sit egestas In eu auctor tempor. Vestibulum nibh at condimentum quis risus est Quisque facilisis ut Suspendisse. Auctor enim orci Phasellus suscipit interdum Aenean at tempus id at. A Sed penatibus risus habitant urna purus Vivamus Quisque mauris dictumst. Ut cursus hac Maecenas molestie auctor nunc semper vitae dictum mauris. Dolor magna ut risus nibh non Sed eget elit pellentesque ut. Ligula vitae odio id semper lacus convallis Nullam pede ut non. Tempor Nam Praesent est enim auctor Vestibulum Integer mauris consequat elit. Pretium neque volutpat In lacus et velit congue eros Nunc dis. Id a interdum Curabitur mollis pellentesque nibh risus ac quis tincidunt. Non tincidunt facilisi mi tellus condimentum lacinia ut tellus pretium habitant. Ac ut purus consequat urna pretium lorem Vestibulum Vestibulum urna ante. Vel tincidunt Curabitur vel et non faucibus vitae tristique Vestibulum congue. Tempus convallis ipsum In Nulla nulla Morbi nunc adipiscing justo condimentum. Sed ac wisi Nunc Vivamus ac Vestibulum Curabitur id Vestibulum Nulla. Donec Nam et hendrerit Aliquam pretium eget quis laoreet dui consequat. Commodo Nam hac tellus adipiscing Duis sagittis adipiscing sit at venenatis. Ridiculus enim consectetuer Vestibulum non ipsum gravida consectetuer orci Nulla velit. Malesuada nunc gravida mauris quis tincidunt sapien id wisi pellentesque lacus. Condimentum habitant Nam et adipiscing pellentesque faucibus congue vel Vestibulum In. Lorem mauris accumsan pede ullamcorper tellus laoreet montes orci ac sapien. Tempor aliquet eros malesuada hac sed lorem Cum ornare elit at. Non consequat nibh malesuada facilisi mi convallis ipsum Aenean pretium enim. Nullam Aenean enim hendrerit congue nunc molestie Curabitur morbi natoque volutpat. Accumsan Aliquam a Sed et commodo Integer purus augue et nec. Interdum et netus laoreet cursus nulla vel dignissim dolor rutrum et. Auctor dolor dui quis neque Nunc congue tincidunt parturient est laoreet. Ligula Curabitur sem dui condimentum sagittis tincidunt non risus Ut et. Vivamus ridiculus nibh Pellentesque libero consectetuer Lorem id aliquet at dui. Tincidunt vitae dolor in pede lorem vitae interdum elit In nibh. Mauris consectetuer velit tortor vitae quis nisl a et auctor ut. A ipsum nibh sapien sagittis Curabitur tincidunt Quisque eget egestas nunc. Commodo nec netus et dui dolor suscipit tempus lacus libero morbi. Ligula morbi tristique tristique consequat wisi faucibus ut pretium velit at. Sed orci consequat nec Sed Vestibulum non adipiscing nec rhoncus wisi. Volutpat magna tortor sodales est interdum Phasellus metus et tempor elit. Justo porttitor dictumst vel nibh ac Curabitur massa vitae adipiscing Sed. At sagittis pretium sed elit tempor semper neque Integer sociis accumsan. Justo accumsan penatibus aliquet et dui malesuada Curabitur Phasellus ridiculus nec. Odio urna mauris Suspendisse Curabitur cursus scelerisque hendrerit lacus et a. Vel Sed consequat venenatis Nulla at ipsum et Integer justo leo. Vestibulum dapibus urna interdum nunc Vivamus adipiscing felis at Sed senectus. Vivamus urna euismod pretium et velit pellentesque Vivamus quis arcu volutpat. Laoreet In Duis In dui fringilla dui hendrerit urna accumsan nulla. Magnis habitant Sed est nibh Phasellus massa eros tellus justo amet. Elit dictum consequat risus et lacus ut nunc Phasellus ante Morbi. Et Morbi semper et tortor vitae et Sed In fermentum risus. Congue porta condimentum pretium sollicitudin vitae ut Quisque Curabitur nibh sit. Euismod at congue nisl cursus Curabitur nec justo nunc nibh semper. Sit Praesent purus iaculis quis tellus odio velit Pellentesque Sed quis. Augue Aenean Integer sit sed pede tempus risus Curabitur eget egestas. Aenean convallis magna quis hac tellus vitae fringilla Nunc ac scelerisque. Vivamus consequat diam Pellentesque tristique laoreet Pellentesque Aenean mattis nisl auctor. Convallis Phasellus tincidunt consequat enim vitae tellus auctor Suspendisse Vivamus rutrum. Condimentum diam orci interdum egestas Quisque eget wisi tempor condimentum tempus. Ipsum eget amet sociis parturient velit parturient Phasellus Suspendisse quis non. Condimentum ut adipiscing sed Nunc mauris Quisque ante Praesent Praesent justo. Sagittis Vestibulum sagittis Curabitur purus elit dolor quis nibh Curabitur pede. Id velit congue commodo a Donec vitae sodales et metus Vivamus. Magna tortor pellentesque id leo est elit Quisque Nulla Nam ullamcorper. Egestas at Nulla nulla id justo libero laoreet penatibus hac urna. Curabitur wisi Curabitur Duis massa Vestibulum platea sed wisi sed sed. Vel neque interdum laoreet id adipiscing euismod urna faucibus pellentesque Aenean. Tellus dictum quis ut Fusce sit vitae a justo eros Aliquam. Eget condimentum sapien vel vel ut sed dis justo id wisi. Libero nonummy et platea leo sem non Phasellus mattis odio tellus. Feugiat tempus Nullam Aenean pretium vel mattis id ullamcorper volutpat at. Maecenas auctor justo vitae Sed suscipit euismod sodales fames ridiculus Vestibulum. Mollis lobortis congue est pretium mattis id id nulla molestie Nam. Fringilla vitae non elit sit convallis In Nulla montes consequat semper. Pede pellentesque Curabitur pede ut non mauris Integer tellus natoque amet. Egestas et Donec mattis hendrerit eu mi porta ac lacus mauris. Justo mattis ipsum eros libero Nam ac eget nunc dolor tincidunt. Cras felis eget risus consectetuer vitae Nulla sem dignissim nonummy Proin. Convallis laoreet urna turpis elit condimentum dignissim et vel cursus wisi. Feugiat sit in tempus felis mauris sociis gravida eget pharetra tellus. Wisi sem Maecenas egestas platea Maecenas nibh sodales felis augue parturient. Sed tempus magnis ac nec Morbi ipsum facilisis Aenean eu ac. Enim sem congue libero ac Donec elit justo ante Phasellus Sed. Amet pede malesuada Sed laoreet malesuada Phasellus id vitae id in. Turpis consectetuer Nullam mattis id mi Nam et et consequat pellentesque. Aliquam Vestibulum neque elit sit enim tincidunt platea magna quis augue. Nam tortor est dis sit vel fermentum Lorem leo Curabitur mollis. Turpis sed Vestibulum pellentesque consectetuer Sed elit convallis sapien tortor ac. Nisl Curabitur tempor convallis orci Phasellus gravida consectetuer vitae pretium vitae. Amet ipsum eu est ut vitae leo leo Vestibulum neque diam. Quis faucibus nunc tellus eleifend cursus vitae Sed eget Nulla laoreet. Vitae nonummy Curabitur enim ligula Nam massa semper magna orci Lorem. Tempor montes magna Curabitur Curabitur convallis cursus amet auctor cursus quis. Et odio id molestie tincidunt semper ut cursus fermentum accumsan In. Phasellus pede Integer et metus sit id ipsum elit nec tempus. Ac nibh netus dapibus Sed habitant Sed pellentesque dignissim mus eu. Odio sem nunc Maecenas elit dolor quis Praesent Nam fringilla mauris. Malesuada fringilla ante adipiscing ipsum enim semper convallis velit Quisque amet. Convallis felis in pretium pellentesque nibh et velit vel vitae cursus. Tellus habitant sodales nibh metus libero et tincidunt non fringilla wisi. Quisque gravida elit nunc lacus augue Aenean montes risus ut est. Scelerisque consequat urna sodales a auctor amet tellus hendrerit orci netus. Augue et nibh nibh justo pellentesque mauris ut Nulla sagittis dictum. Nec orci congue molestie turpis metus eget lorem ut Phasellus sapien. Amet dui turpis dictumst adipiscing Sed semper cursus ut laoreet mauris. Curabitur Pellentesque eu eget semper convallis vitae dui porttitor congue elit. Vestibulum eget rutrum nonummy leo sem semper convallis id Curabitur porttitor. Sagittis Vestibulum auctor nunc auctor arcu volutpat risus magna mollis tristique. Urna lacus Aliquam hendrerit leo ridiculus quis est augue nibh Nam. Quisque interdum ipsum Maecenas cursus leo congue Pellentesque tempus lacus iaculis. Hendrerit Quisque libero felis aliquet at mauris ac Sed Pellentesque aliquam. Nulla et pharetra sed ipsum orci metus nunc dictumst elit quam. Laoreet leo Curabitur nunc tincidunt Maecenas lacus lobortis et et ut. Malesuada eu feugiat quis Curabitur facilisis sapien congue risus et id. Orci nibh nunc ut ipsum In congue dignissim feugiat enim In. Id condimentum consequat sem enim lacus in laoreet nunc nibh at. Curabitur dolor quis nibh elit Duis nibh vitae metus vitae ornare. Penatibus quis tempor Cum Morbi scelerisque dui pellentesque vitae amet velit. Nulla scelerisque pretium nulla dapibus elit lorem eget nibh massa gravida. Sed id justo ac at Ut ac ante vel feugiat consequat. Phasellus pede ultrices condimentum augue enim turpis Morbi id et ligula. Auctor elit convallis est a Phasellus hendrerit augue sapien laoreet condimentum. Cursus elit vel et sem nibh pretium tincidunt nisl lacinia et. Nullam ac natoque vitae congue Phasellus porttitor faucibus dui faucibus cursus. Tortor mauris pretium mauris quis ipsum quis eget In ac ac. Fermentum facilisi id eu est Praesent ipsum elit dignissim Curabitur amet. Nam tincidunt nascetur lacus vitae felis vitae nibh orci velit rutrum. Vivamus turpis Lorem metus eget consectetuer ipsum ipsum urna sit tincidunt. Nec et libero ullamcorper consequat a Nullam tincidunt Nulla et Quisque. Nulla convallis Nunc ante parturient sagittis ultrices Nulla Vivamus et Nam. Semper dui elit id sagittis ante eget nonummy ac molestie vel. Nulla odio gravida adipiscing sem Integer Ut vel a fermentum Ut. Semper urna sit tristique quis Mauris ac neque sit cursus ut. Quisque wisi Aenean vel ac tortor vitae mollis Donec malesuada nec. Libero Nam tellus nibh urna dui magnis netus pulvinar ut pretium. Cursus ut id orci sapien condimentum Donec ac mus interdum sit. Pellentesque aliquet est Phasellus id ipsum porttitor fames laoreet Lorem consectetuer. Donec risus et elit convallis Vestibulum nunc Praesent elit Curabitur nulla. Pretium quam lacinia auctor in sollicitudin et ac tristique rutrum Curabitur. Nibh Vestibulum justo tortor interdum Morbi justo quis quis malesuada Nunc. Et vel Vestibulum dolor nisl accumsan dui Vestibulum ut nibh vitae. Aliquam nibh rutrum ridiculus libero consequat risus justo Curabitur pretium nibh. Non lorem tincidunt Cras at Donec ipsum ac condimentum magna nec. Tellus Pellentesque est pellentesque orci ultrices enim a faucibus elit Maecenas. Sem ut Quisque Pellentesque tempus pharetra ut Cras pretium lorem nibh. Fusce Sed Vivamus eget tincidunt id suscipit et augue Aenean fames. Tortor ac ornare wisi Nam Curabitur egestas gravida consequat Pellentesque auctor. Cursus laoreet eget id Nam Nulla urna dui elit augue massa. Convallis eu vel interdum Quisque ligula Maecenas risus Nam eget ut. Mollis sit tellus aliquet pretium Quisque quis vel congue Maecenas auctor. Tristique sociis Vestibulum vel libero faucibus nec ipsum Vestibulum aliquam sem. Urna lacinia odio Nam ante natoque id ipsum consequat lorem Pellentesque. Nulla condimentum justo velit tempus Maecenas In consectetuer ipsum ligula pulvinar. Enim faucibus Vestibulum Cras pellentesque consequat ut elit lacinia dapibus et. Cursus massa magna pretium sollicitudin dui odio dictumst penatibus congue Sed. Adipiscing dui mi Proin ultrices In sed semper laoreet laoreet quis. Iaculis morbi nec a est eu Praesent nulla Vestibulum sociis et. Sed velit dui nunc Sed pretium vitae Ut tincidunt feugiat sapien. Elit semper vel vitae mauris magna tincidunt at auctor massa iaculis. Nam urna quam lacinia egestas nibh justo Donec wisi et vitae. Est adipiscing nunc ipsum porttitor wisi nec aliquam Nulla sed elit. Ullamcorper consequat vel enim In quis laoreet sit semper porttitor augue. Elit elit augue Curabitur vitae ac Nam Nullam fringilla risus Pellentesque. Tempus morbi aliquam nibh urna ut justo Nam quis et Donec. Velit at lacinia purus Ut mattis orci cursus at Vestibulum urna. Interdum elit pede cursus at elit amet enim nec sollicitudin quis. Lorem felis lobortis orci dictumst nunc sit nibh platea Nunc sapien. Orci tempus ut ornare Proin tincidunt arcu nunc neque Proin ridiculus. Sed nunc vitae malesuada Phasellus interdum urna platea vel turpis mi. Est at Nam id urna Integer sapien at at Curabitur cursus. Sed urna ac nunc sed quis sed urna id id est. Dignissim eget eget sapien dictum ac adipiscing suscipit Morbi augue Sed. Elit consequat ut venenatis condimentum mauris mi pharetra facilisi pede convallis. Penatibus consectetuer Sed leo lacinia Integer morbi molestie semper magnis Nam. Ornare nibh id et at tellus consectetuer vitae nibh cursus at. Laoreet nibh Phasellus Sed enim condimentum dictumst et iaculis eros Maecenas. Quis Quisque pretium neque fringilla tincidunt sem turpis vitae lobortis pharetra. In Cum Cum tristique dolor tellus risus nibh ipsum lorem Nulla. Velit elit metus quis dui vitae accumsan leo lobortis tincidunt ac. Risus id ut pellentesque nascetur Nam lacus volutpat feugiat iaculis lorem. Dictumst semper a ante leo Curabitur eget tellus gravida pellentesque Suspendisse. In mus porttitor consectetuer tortor eleifend velit et enim in Proin. Et felis sit quis id tempor ipsum non id penatibus nibh. Consequat Ut interdum et a tristique amet Curabitur Phasellus ut wisi. Condimentum egestas convallis Ut Nam Duis a id laoreet pellentesque ut. Ligula eu pede wisi et Fusce neque massa Nam tempus Nam. Laoreet ante nibh egestas dapibus nisl odio et venenatis augue non. Tincidunt a nunc pellentesque leo facilisis scelerisque pretium cursus Praesent Maecenas. Eget Phasellus interdum et sit orci pulvinar cursus condimentum dui sociis. Natoque Sed porttitor mi consectetuer id nibh ipsum Aenean ac Vestibulum. Semper adipiscing penatibus pellentesque nibh adipiscing consequat Curabitur eget quam Integer. Tempus In sagittis eu Quisque Nam lorem adipiscing Lorem nunc dictum. Malesuada pretium gravida netus Nulla ipsum Vestibulum lacinia nonummy turpis sagittis. Metus Praesent urna augue consectetuer vel et Pellentesque Nulla morbi pretium. Consequat velit elit justo sagittis lorem orci feugiat quis velit morbi. Dolor porta ligula non lobortis Quisque interdum odio Vivamus nulla pellentesque. Semper Nam augue arcu neque venenatis condimentum velit et Vestibulum porta. Ac felis massa feugiat Vestibulum nibh adipiscing Ut convallis interdum lacinia. Eros wisi Lorem libero Ut Aenean odio platea adipiscing urna accumsan. Faucibus feugiat nibh tellus elit Vestibulum laoreet et In consequat id. Dolor magna consequat pretium felis Cum ultrices metus a nunc ante. Interdum ac Quisque tellus convallis non pretium Vestibulum aliquam et vitae. Et ut Aenean augue molestie at elit diam odio sed Nam. Fringilla odio tincidunt odio non id Quisque Cum et justo Nulla. Mi Duis gravida Nam elit In quis Integer ut elit tellus. Aenean nunc urna Quisque vitae tincidunt nec nibh Integer eget sem. Ligula orci Morbi pellentesque Nulla purus consequat Vestibulum consectetuer pede quis. Nullam fermentum commodo sapien volutpat Sed tempus id platea Lorem semper. Sociis et semper leo tortor pede Aenean convallis vitae metus sapien. Lobortis Curabitur mauris Nullam elit felis euismod Donec In Phasellus habitant. Ornare mollis Phasellus tincidunt tortor Pellentesque In Morbi habitant ut molestie. In a nulla dolor euismod id condimentum interdum dapibus egestas quis. Consequat dui pretium nec id vitae ullamcorper dignissim Nunc nibh turpis. Senectus a Curabitur amet ipsum tempus Curabitur vel velit Nam tincidunt. Ut tellus dui accumsan tellus vitae volutpat In dolor pretium leo. Urna ut Phasellus orci Aenean lacus orci lobortis enim adipiscing lobortis. Magna amet volutpat ipsum vitae orci eget vel porttitor eros laoreet. Ut fringilla id tristique tincidunt libero Sed risus porttitor mi Nam. Pretium consequat et neque senectus ac Curabitur eget Mauris ante Quisque. Cras semper tristique et tincidunt eros nibh elit Nam In justo. Enim dis habitant nunc quis Sed ac Fusce Lorem Aenean ipsum. Turpis Vestibulum Curabitur tincidunt dignissim tincidunt et pulvinar pretium et id. Sapien sagittis Sed semper Vestibulum tellus enim lacinia pulvinar eu vitae. Congue auctor accumsan vitae fringilla sem malesuada a amet Curabitur at. Tortor nunc velit dapibus velit Curabitur nunc lorem urna dui tempus. Mus congue auctor est dui tortor neque Maecenas eget montes urna. Sed pellentesque dui nec Nam eget auctor Sed id laoreet quis. Adipiscing dui tincidunt Phasellus condimentum auctor et pretium enim est Curabitur. Ligula sem tincidunt adipiscing consectetuer ac pede Aenean vitae vel semper. Fringilla eu non tortor nunc Nam augue ipsum mattis tortor parturient. Justo sed Quisque Aenean libero Sed sed vel condimentum porttitor pede. Risus malesuada Donec metus Pellentesque eget Fusce malesuada lacinia turpis amet. Libero Aenean Curabitur ut Phasellus montes suscipit malesuada dictumst condimentum arcu. Nulla pretium lorem ante ut pellentesque Sed facilisi Phasellus id malesuada. Nulla id elit Nam Aliquam sapien Vivamus magnis nec condimentum laoreet. Felis Pellentesque Maecenas justo convallis Suspendisse mauris tellus orci Nam Ut. Fames sem.";
        //static string dictionary =

        /*
            "ABCDEFGH" +
            "IJKLMNOP" +
            "QRSTUVWX" +
            "YZ.     " +
            "abcdefgh" +
            "ijklmnop" +
            "qrstuvwx" +
            "yz      ";
        /**/

        static void Main(string[] args)
        {
            Density den = new Density("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz .");
            //Density den = new Density("ACDEFHIJLMNOPQRSTUVWabcdefghijlmnopqrstuvwy .");

            den.CalculatePercentages(lorem);
            Map mapy = new Map(8, 8, "WReQwLTDHj. Ml UJrhcuovEFPbisnapNVdt etgfp .maruOICvulosyASpcdiq");



            Console.WriteLine(mapy.Solve(lorem));

            //loop();
        }

        static void loop()
        {
            string dictionary;
            string bestS = "";
            int best = int.MaxValue;

            Density den = new Density("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz .");
            //Density den = new Density("ACDEFHIJLMNOPQRSTUVWabcdefghijlmnopqrstuvwy .");

            den.CalculatePercentages(lorem);
            //den.CalculatePercentagesRanked(lorem);

            for (int x = 0; x < 500; x++)
            {
                for (int i = 0; i < 500; i++)
                {
                    char[,] M = new char[8, 8];

                    dictionary = den.FillSpiral(M);

                    Map map = new Map(8, 8, dictionary);

                    int temp = map.Solve(lorem);

                    if (temp < best)
                    {
                        best = temp;
                        bestS = dictionary;
                    }

                    if (temp < 80000)
                    {
                        Console.WriteLine(temp);
                        Console.WriteLine(dictionary);
                    }
                }

                Console.WriteLine(best);
                Console.WriteLine(bestS);
            }
        }

    }
}
