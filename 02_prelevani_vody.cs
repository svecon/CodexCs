using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
Přelévání vody
Úložka na prohledávání stavového prostoru.

Máme tři nádoby o celočíselných objemech a,b,c (a,b,c nejsou větší než 10) ve kterých je na začátku objem x,y,z vody, v tomto pořadí.

Vodu můžeme přelévat z nádoby do nádoby, a to vždy tak, že nádobu kam lijeme, zcela zaplníme nebo tak, že nádobu odkud lijeme, zcela vyprázdníme. Objem přelité vody je určen tím, která z těchto variant nastane dříve.

Vodu nesmíme vylévat nikam jinam ani doplňovat z nějakého jiného zdroje.

Vstupem programu jsou po řadě čísla a,b,c a x,y,z udávající objemy a počáteční obsahy nádob.

Program vytiskne seznam všech objemů (včetně nuly, lze-li), kterých lze přeléváním dosáhnout (celý objem vody v kterékoliv z nádob) a u každého z nich uvede za dvojtečkou minimální počet potřebných přelití. Objemy v tomto seznamu budou vytištěny v rostoucím pořadí.

Příklad:
Vstup:
  4 1 1  1 1 1
Odpovídající výstup:
  0:1 1:0 2:1 3:2

 * */


namespace ConsoleApplication5
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

        public static void CacheDisable()
        {
            cache.Clear();
        }

    }


    class Lahev
    {
        public int objem { get; protected set; }
        public int stav { get; protected set; }

        public Lahev(int objem)
        {
            this.objem = objem;
        }

        public int KolikJeVolno()
        {
            return objem - stav;
        }
        public void Prilij(int kolik)
        {
            if (KolikJeVolno() < kolik)
                throw new ArgumentException("Prelevas moc!");

            stav += kolik;
        }
        public void Odlij(int kolik)
        {
            if (stav < kolik)
                throw new ArgumentException("Prelevas moc!");

            stav -= kolik;
        }
    }

    class Stav
    {

        public Lahev[] lahve { get; protected set; }
        public int pocetKroku { get; protected set; }

        public Stav(Lahev[] lahve, int pocetKroku)
        {
            this.lahve = new Lahev[lahve.Count()];
            for (int i = 0; i < lahve.Count(); i++)
            {
                this.lahve[i] = new Lahev(lahve[i].objem);
                this.lahve[i].Prilij(lahve[i].stav);
            }
            this.pocetKroku = pocetKroku;
        }

        public string GetHash()
        {
            string[] stavy = new string[lahve.Count()];
            for (int i = 0; i < lahve.Count(); i++)
            {
                stavy[i] = lahve[i].stav.ToString();
            }

            return String.Join("-", stavy); ;
        }

        public void ProzkoumejSe()
        {

            ProzkoumaneStavy.Add(this);
            for (int i = 0; i < lahve.Count(); i++)
            {
                NalezeneObjemy.Add(lahve[i].stav, pocetKroku);
            }

            for (int i = 0; i < lahve.Count(); i++)
            {
                if (lahve[i].stav == 0)
                    continue;

                for (int j = 0; j < lahve.Count(); j++)
                {
                    if (i == j)
                        continue;

                    Stav novy = new Stav(lahve, pocetKroku + 1);
                    bool prelilJsemNeco = novy.prelij(novy.lahve[i], novy.lahve[j]);

                    if (!prelilJsemNeco)
                        continue;

                    if (ProzkoumaneStavy.Contains(novy))
                        continue;

                    Program.fronta.Add(novy);
                }
            }

        }

        protected bool prelij(Lahev od, Lahev kam)
        {
            int kolikPreliju = (kam.KolikJeVolno() < od.stav) ? kam.KolikJeVolno() : od.stav;
            if (kolikPreliju == 0) return false;

            od.Odlij(kolikPreliju);
            kam.Prilij(kolikPreliju);

            return true;
        }
    }

    class NalezeneObjemy
    {

        protected static List<int> objemy = new List<int>();
        protected static List<int> poctyKroku = new List<int>();

        public static void Add(int objem, int pocetKroku)
        {
            if (objemy.Contains(objem))
            {
                if (poctyKroku.ElementAt(objemy.IndexOf(objem)) <= pocetKroku)
                    return;
            }

            objemy.Add(objem);
            poctyKroku.Add(pocetKroku);
        }

        public static void Vytiskni()
        {
            seradit();
            for (int i = 0; i < objemy.Count(); i++)
            {
                Console.Write("{0}:{1} ", objemy[i], poctyKroku[i]);
            }
        }

        protected static void seradit()
        {
            for (int i = 0; i < objemy.Count() - 1; i++)
            {
                for (int j = 0; j < objemy.Count() - 1; j++)
                {
                    if (objemy[j] > objemy[j + 1])
                    {
                        int x = objemy[j];
                        objemy[j] = objemy[j + 1];
                        objemy[j + 1] = x;

                        x = poctyKroku[j];
                        poctyKroku[j] = poctyKroku[j + 1];
                        poctyKroku[j + 1] = x;
                    }
                }
            }
        }
    }

    class ProzkoumaneStavy
    {

        protected static List<string> prozkoumano = new List<string>();

        public static void Add(Stav s)
        {
            prozkoumano.Add(s.GetHash());
        }

        public static bool Contains(Stav s)
        {
            return prozkoumano.Contains(s.GetHash());
        }

    }

    class Program
    {

        public static List<Stav> fronta = new List<Stav>();

        static void Main(string[] args)
        {
            Reader.CacheAdd("1 3 5 1 0 5");
            Reader.CacheDisable();

            Lahev[] lahve = new Lahev[3];

            for (int i = 0; i < lahve.Count(); i++)
            {
                lahve[i] = new Lahev(Reader.ReadInt());
            }

            for (int i = 0; i < lahve.Count(); i++)
            {
                lahve[i].Prilij(Reader.ReadInt());
            }

            fronta.Add(new Stav(lahve, 0));

            while (fronta.Count() > 0)
            {
                Stav prvniStav = fronta.First();
                fronta.Remove(prvniStav);
                prvniStav.ProzkoumejSe();
            }
            NalezeneObjemy.Vytiskni();

        }
    }
}
