using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloVilag
{
    class Program
    {
        public int Négyzet(int a)
        {
            return a * a;
        }

        public int Fibonacci(int n)
        {
            if (n == 0 || n == 1)
            {
                return 1;
            }
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }

        // Elvileg bárhol lehet ékezet/unicode karakter...
        public void Függvény()
        {
            // Függvények hívása:
            Console.WriteLine(Négyzet(5));
            Console.WriteLine(Fibonacci(3));

            // Vezérlési parancsok:
            // break: belső ciklus vége utánra ugrik
            // continue: belső ciklusmag vége elé ugrik
            // return: függvény végét jelenti, (és visszatér, ha van visszatérési érték)
            for (int i = 0; true; i++)
            {
                if (i % 2 == 0 && i % 5 == 0)
                {
                    Console.WriteLine("BUMM");
                    break;
                }

                if (i % 2 == 0)
                {
                    Console.WriteLine("BIMM");
                    continue;
                }

                if (i % 5 == 0)
                {
                    Console.WriteLine("BAMM");
                    continue;
                }

                Console.WriteLine(i);
                // continue "címkéje"
            }
            // break "címkéje"

            // break; -> compilation error.

            Console.WriteLine("");

            // return "címkéje"
        }

        // nincs void -> varázslat
        public Program()
        {
            Függvény();

            Console.WriteLine("Írd be a neved!");

            string nev = Console.ReadLine();

            Console.WriteLine("Hello " + nev + "!");

            Console.WriteLine("Hány kedvenc színed van?");

            // "int32" -> 32 bites számmá (int) konvertálás.
            int dbSzin = Convert.ToInt32(Console.ReadLine());

            if (dbSzin == 0)
            {
                Console.WriteLine("Ne hülyéskedj már!");
                // Vezérlési parancs: continue, break, return
                return;
            }

            // Üres dinamikus tömb készítése:
            List<string> szinek = new List<string>();
            // Lista/tömb hossza: szinek.Count (NEM Capacity!!).

            // cw<tab><tab>
            Console.WriteLine("Írd ki a kedvenc színeid, minden sorba egyet!");

            // Szám szövegbe illesztése:
            Console.WriteLine("123 szövegbe illesztése: " + 123 + ".");

            // for<tab><tab>
            for (int i = 0; i < dbSzin; i++)
            {
                string szin = Console.ReadLine();
                if (szin == "")
                {
                    Console.WriteLine("Ne szórakozz!");
                    // Nem intuitív itt a működése:
                    // ciklusvégi művelet elé teszi a vezérlést:
                    continue;
                }
                // Szöveg hossza: szin.Length.
                szinek.Add(szin);
                //break;
            }
            // A break parancs "ideviszi" a vezérlést.

            // Foreach: "A szinek minden elemére végezd el (sorrendben)".
            // foreach<tab><tab>string<tab>szin<tab>szinek<tab>
            foreach (string szin in szinek)
            {
                Console.WriteLine(szin);
            }

            // Végén billentyű leütésre várunk:
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            new Program();

            // for működése:
            // for (kezdő; feltétel; ciklusvégi)
            // {
            // }

            // kezdő;
            // while (feltétel) {
            //   // ...
            //   ciklusvégi;
            // }
        }
    }
}
