using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication8
{
    enum TypUdalosti { prijizdiDoA, nalozeno, prijizdiDoB, vylozeno }
    class Auto
    {
        int dobaNakladani;
        int dobaVykladani;
        int dobaJizdy;
        int nosnost;
        int kolikVeze;
        Model model;

        public void ZpracujUdalost(TypUdalosti co)
        {
            switch (co)
            {
                case TypUdalosti.prijizdiDoA:
                    if (model.Cas >= model.KdyMuzuZacitNakladat)
                        model.KdyMuzuZacitNakladat
                            = model.Cas + dobaNakladani;
                    else
                        model.KdyMuzuZacitNakladat += dobaNakladani;
                    model.Naplanuj(model.KdyMuzuZacitNakladat,
                        this, TypUdalosti.nalozeno);
                    break;
                case TypUdalosti.nalozeno:
                    if (model.pisekVA >= nosnost)
                        kolikVeze = nosnost;
                    else
                        kolikVeze = model.pisekVA;
                    model.pisekVA -= kolikVeze;

                    model.Naplanuj(model.Cas + dobaJizdy, this,
                        TypUdalosti.prijizdiDoB);
                    break;
                case TypUdalosti.prijizdiDoB:
                    model.Naplanuj(model.Cas + dobaVykladani, this,
                        TypUdalosti.vylozeno);
                    break;
                case TypUdalosti.vylozeno:
                    model.pisekVB += kolikVeze;
                    model.Naplanuj(model.Cas + dobaJizdy, this,
                        TypUdalosti.prijizdiDoA);
                    break;
                default:
                    break;
            }
        }
        public Auto(Model model, int dobaNakladani, int dobaVykladani,
            int dobaJizdy, int nosnost)
        {
            this.model = model;
            this.dobaNakladani = dobaNakladani;
            this.dobaVykladani = dobaVykladani;
            this.dobaJizdy = dobaJizdy;
            this.nosnost = nosnost;

            model.Naplanuj(0, this, TypUdalosti.prijizdiDoA);
        }
    }
    class Udalost
    {
        public int kdy;
        Auto kdo;
        TypUdalosti co;
        public Udalost(int kdy, Auto kdo, TypUdalosti co)
        {
            this.kdy = kdy;
            this.kdo = kdo;
            this.co = co;
        }
        public void ZpracujSe()
        {
            kdo.ZpracujUdalost(co);
        }
    }
    class Kalendar
    {
        List<Udalost> seznam;
        public void Pridej(Udalost udalost)
        {
            seznam.Add(udalost);
        }
        public Udalost NajdiAOdstranAVratPrvni()
        {
            Udalost minUdalost = seznam[0];
            foreach (Udalost u in seznam)
            {
                if (u.kdy < minUdalost.kdy)
                    minUdalost = u;
            }
            seznam.Remove(minUdalost);
            return minUdalost;
        }
        public Kalendar()
        {
            seznam = new List<Udalost>();
        }
    }
    class Model
    {
        int pisekCelkem;
        public int pisekVA;
        public int pisekVB;
        public int Cas;
        public int KdyMuzuZacitNakladat;
        Kalendar kalendar;
        public Model(int pisekCelkem)
        {
            this.pisekCelkem = pisekCelkem;
        }
        public int Vypocitej()
        {
            pisekVA = pisekCelkem;
            pisekVB = 0;
            Cas = 0;
            KdyMuzuZacitNakladat = -1;
            kalendar = new Kalendar();

            new Auto(this, 60, 2, 120, 0);
            //new Auto(this, 60, 2, 120, 10);
            //new Auto(this, 60, 2, 120, 10);
            //new Auto(this, 120, 2, 240, 15);

            while (pisekVB < pisekCelkem)
            {
                Udalost udalost = kalendar.NajdiAOdstranAVratPrvni();
                Cas = udalost.kdy;
                udalost.ZpracujSe();
            }
            return Cas;
        }
        public void Naplanuj(int kdy, Auto kdo, TypUdalosti co)
        {
            kalendar.Pridej(new Udalost(kdy, kdo, co));
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            Model model = new Model(1000);
            Console.WriteLine(model.Vypocitej());
        }
    }
}