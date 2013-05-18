using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*

Ve skladišti o ploše 10x10 kostiček se na políčku [sx,sy] nachází skladník a na políčku [bx,by] bedna, kterou je potřeba dopravit na políčko [cx,cy]. Kolem dokola skladiště jsou zdi, zdi-překážky mohou být i na některých políčkách uvnitř skladiště.

Skladník se pohybuje v každém kroku vždy o jedno políčko doleva, doprava, nahoru nebo dolů, je-li před ním (ve směru pohybu) bedna a za ní volné místo, pak při pohybu bednu odstrčí na volné místo a sám se posune na její pozici, pokud na políčku, kam chce jít, je překážka nebo okraj skladiště, nebo pokud je tam bedna, za kterou je překážka nebo okraj skladiště, pohyb nelze provést.

Napište program, který najde nejmenší počet kroků skladníka potřebný k dopravení bedny na cílové políčko. Pokud bednu na danou cílovou pozici nelze dopravit, vytiskne -1.

Popis vstupu:
Vstup je zadán jako mapa, ve které každý znak určuje obsah políčka:

. (tečka) značí volné políčko
X značí překážku
S značí skladníka
B značí bednu
C značí cílové políčko

Příklad:
Vstup:
  ..........
  ..........
  ..........
  ..........
  ..........
  CSB.......
  ..........
  ..........
  ..........
  ..........
Odpovídající výstup:
  6
 
 */
namespace Sokoban
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

    class MoveableObject
    {

        public int x { get; protected set; }
        public int y { get; protected set; }

        public MoveableObject(int x, int y)
        {
            this.x = x;
            this.y = y; 
        }

        public void Move(int x, int y)
        {
            this.x += x;
            this.y += y;
        }

        public MoveableObject Clone() {
            return new MoveableObject(x, y);
        }
    }

    class Map
    {

        int[,] plan;
        int sizeX;
        int sizeY;
        string allowedChars = ".CSBX";
        MoveableObject sk;
        MoveableObject box;

        int[,] vectors = new int[,] { { 0, 1 }, { 1, 0 }, { 0, -1 }, { -1, 0 } };

        public Map(int sizeX, int sizeY)
        {
            this.sizeX = sizeX;
            this.sizeY = sizeY;
            plan = new int[this.sizeX + 2, this.sizeY + 2];
        }

        public void Load()
        {
            int counter = 0;
            int c;
            setWalls();

            while (counter < sizeX * sizeY)
            {
                c = Reader.Read();
                if (!allowedChars.Contains((char)c))
                    continue;

                switch (c)
                {
                    case 'S': sk = new MoveableObject(counter % sizeX + 1, counter / sizeX + 1);
                        break;
                    case 'B': box = new MoveableObject(counter % sizeX + 1, counter / sizeX + 1);
                        break;
                }

                plan[counter % sizeX + 1, counter / sizeX + 1] = c;
                counter++;
            }
        }

        protected void setWalls()
        {
            for (int i = 0; i <= sizeX + 1; i++)
            {
                plan[i, 0] = 'X';
                plan[i, sizeY + 1] = 'X';
            }
            for (int i = 0; i <= sizeY + 1; i++)
            {
                plan[0, i] = 'X';
                plan[sizeX + 1, i] = 'X';
            }
        }

        public void Print()
        {
            for (int i = 0; i <= sizeY + 1; i++)
            {
                for (int j = 0; j <= sizeX + 1; j++)
                {
                    if (sk.x == j && sk.y == i)
                        Console.Write('S');
                    else if (box.x == j && box.y == i)
                        Console.Write('B');
                    else if ("SB".Contains((char)plan[j, i]))
                        Console.Write('.');
                    else Console.Write((char)plan[j, i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public int Solve()
        {
            int[, , ,] wasIHere = new int[sizeX + 1, sizeY + 1, sizeX + 1, sizeY + 1];
            Queue<MoveableObject> queueSk = new Queue<MoveableObject>();
            Queue<MoveableObject> queueBox = new Queue<MoveableObject>();
            queueSk.Enqueue(sk);
            queueBox.Enqueue(box);

            while (queueSk.Count != 0)
            {
                MoveableObject skQueued = queueSk.Dequeue();
                MoveableObject boxQueued = queueBox.Dequeue();
                int previous = wasIHere[skQueued.x, skQueued.y, boxQueued.x, boxQueued.y];

                for (int i = 0; i < vectors.Length / 2; i++)
                {
                    sk = skQueued.Clone();
                    box = boxQueued.Clone();

                    sk.Move(vectors[i, 0], vectors[i, 1]);
                    if (sk.x == box.x && sk.y == box.y)
                        box.Move(vectors[i, 0], vectors[i, 1]);

                    if (plan[sk.x, sk.y] == 'X' || plan[box.x, box.y] == 'X')
                        continue;

                    if (plan[box.x, box.y] == 'C')
                        return previous + 1;

                    if (wasIHere[sk.x, sk.y, box.x, box.y] != 0)
                        continue;

                    wasIHere[sk.x, sk.y, box.x, box.y] = 1 + previous;
                    queueSk.Enqueue(sk.Clone());
                    queueBox.Enqueue(box.Clone());
                }
            }
            return -1;
        }

    }

    class Program
    {
        static void Main(string[] args)
        {
            Reader.CacheAdd("..................................................CSB...............................................");
            Reader.CacheClear();

            Map map = new Map(10, 10);
            map.Load();
            Console.WriteLine(map.Solve());
        }
    }
}
