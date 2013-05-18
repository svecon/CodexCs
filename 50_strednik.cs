using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*
 
Napište program, který přečte korektní zdrojový kód v jazyku C# a vytiskne počet cyklů, které obsahuje, tj. počet všech příkazů for, while, do-while a foreach.

Pozor na komentáře a textové řetězce!
Pozn.: Po skončení vstupního souboru Console.Read() vrací hodnotu -1.


Příklad:

Vstup:

using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApplication1
{
    class AAA
    {
        static void Main(string[] args)
        {
            List<int> seznam = new List<int>();
            seznam.Add(5);
            seznam.Add(7);
            while (true)
            {
                for (int i = 0; i < 100; i++)
                {
                    do
                    {
                        foreach (int prvek in seznam)
                        {
                            Console.WriteLine(prvek);
                            
                        }
                    } while (true);
                    
                }
            }
        }
    }
}


Výstup:

4

 */

namespace Strednik
{
    class Program
    {
        static void Main(string[] args)
        {
            int result = 0;

            bool isString = false;
            bool isLineComment = false;
            bool isMultiComment = false;
            bool isDisabledString = false;

            int word = 0;
            int c;
            int pc = '%';

            while ((c = Console.Read()) != -1)
            {
                if ((c >= 'a') && (c <= 'z')) //(((c >= 'a') && (c <= 'z')) || ((c >= 'A') && (c <= 'Z')))
                    word += c;
                else
                {
                    /*
                    Console.WriteLine('f' + 'o' + 'r');
                    Console.WriteLine('w' + 'h' + 'i' + 'l' +'e');
                    Console.WriteLine('f' + 'o' + 'r' + 'e' + 'a' + 'c' +'h');
                     * 
                     for = 327
                     while = 537
                     foreach = 728
                     */
                    if (((word == 327) || (word == 537) || (word == 728)) && !(isString || isLineComment || isMultiComment))
                    {
                        result++;
                    }
                    word = 0;
                    //string x = @"asdsad\" while "; 
                }
                if ((c == '"') && !(isLineComment || isMultiComment))
                {
                    if (isString && ((pc != '\\') || (isDisabledString)))
                    {
                        isString = false;
                        isDisabledString = false;
                    }
                    else
                    {
                        isString = true;
                        if (pc == '@')
                            isDisabledString = true;
                    }
                }
                else if (((pc == '/') && (c == '*')) && !(isString || isLineComment))
                    isMultiComment = true;
                else if (((pc == '*') && (c == '/')) && !(isString))
                    isMultiComment = false;
                else if (((pc == '/') && (c == '/')) && !(isMultiComment || isString))
                    isLineComment = true;
                else if ((c == 13) || (c == 10))
                {
                    isLineComment = false;
                    isString = false;
                }

                pc = c;
            }

            Console.WriteLine(result);
        }
    }
}
