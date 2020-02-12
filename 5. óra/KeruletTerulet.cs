using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1x
{
    interface IAlakzat
    {
        double GetKerulet();
        double GetTerulet();
    }
    class Kor : IAlakzat
    {
        private double r;

        public Kor(double r)
        {
            this.r = r;
        }

        public double GetKerulet()
        {
            return 2 * r * Math.PI;
        }

        public double GetTerulet()
        {
            return r * r * Math.PI;
        }
    }
    class Teglalap : IAlakzat
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

            List<IAlakzat> elemek = new List<IAlakzat>(); // heterogén kollekció

            while (true)
            {
                Console.WriteLine("Szeretnél még alakzatot beírni?");
                if (Console.ReadLine() == "nem")
                {
                    break;
                }
                // ide jön az alakzat beolvasása:
                Console.WriteLine("Kör vagy téglalap?");
                if (Console.ReadLine() == "kor")
                {
                    Console.WriteLine("Írd be a kör sugarát!");
                    double sugar = Convert.ToDouble(Console.ReadLine());

                    elemek.Add(new Kor(sugar)); // implicit castolás
                }
                else
                {
                    Console.WriteLine("Írd be a szélességét!");
                    double szelesseg = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Írd be a magasságát!");
                    double magassag = Convert.ToDouble(Console.ReadLine());

                    elemek.Add(new Teglalap(magassag, szelesseg)); // implicit castolás
                }
            }

            double keruletOsszeg = 0;
            double maxTerulet = -1;

            foreach (IAlakzat elem in elemek)
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

            Console.ReadKey();

            // közbevetés:
            // int a = 123;
            // double b = (double)a; // explicit cast
            // double b = a; // implicit cast / rejtett castolás
        }
        public static void Main(string[] args)
        {
            new Program();
        }
    }
}
