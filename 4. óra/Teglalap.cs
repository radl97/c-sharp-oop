using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1x
{
    class Teglalap
    {
        private double kerulet, terulet;

        public Teglalap(double magassag, double szelesseg)
        {
            kerulet = 2 * (magassag + szelesseg);
            terulet = magassag * szelesseg;
        }

        public double GetKerulet()
        {
            return kerulet;
        }
        
        public double GetTerulet()
        {
            return terulet;
        }
    }
    class Program
    {
        public Program()
        {
            Console.WriteLine("Szia! Ez a program az alakzatok összkerületét és a legnagyobb alakzat adatait fogja kiírni.");

            List<Teglalap> elemek = new List<Teglalap>();

            while (true)
            {
                Console.WriteLine("Szeretnél még alakzatot beírni?");
                if (Console.ReadLine() == "nem")
                {
                    break;
                }
                // ide jön az alakzat beolvasása:
                Console.WriteLine("Egyelőre csak téglalap támogatott. Írd be a szélességét!");
                double szelesseg = Convert.ToDouble(Console.ReadLine());
                Console.WriteLine("Írd be a magasságát!");
                double magassag = Convert.ToDouble(Console.ReadLine());

                elemek.Add(new Teglalap(magassag, szelesseg));
            }

            double keruletOsszeg = 0;
            double maxTerulet = -1;

            foreach (Teglalap elem in elemek)
            {
                double kerulet = elem.GetKerulet();
                double terulet = elem.GetTerulet();

                if (terulet > maxTerulet)
                {
                    maxTerulet = terulet;
                }
                keruletOsszeg += kerulet;
            }

            Console.WriteLine("Kerületek összege = " + keruletOsszeg);
            Console.WriteLine("Maximális terület = " + maxTerulet); // téglalap adatai???
        }
        public static void Main(string[] args)
        {
            new Program();
        }
    }
}
