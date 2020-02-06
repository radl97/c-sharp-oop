using System;

// Változás az előző "iterációhoz" képest:
// Pár látszólag hasztalan függvény bevezetése.
// A lényege, hogy a hosszabb, de szorosan egymáshoz tartozó kódok egybe kerüljenek.
// Ezen kívül pár helyen elértük, hogy ne legyen kódismétlés.
// A legszembetűnőbb kódismétlések általában egy-egy jól megtalált függvénnyel megoldhatók.

// Ennek a kis változtatásnak a segítségével képesek vagyunk:
// - viszonylag jól változtatni a gép stratégiáját
// - Jobban átlátni a program logikáját: 5 WriteLine helyett csak annyit látunk, hogy Bemutatkozas()

// De még mindig vannak problémák a kódban:
// if (elsoJon) {
//   elsoGep változó használatával csinálj valamit
//   itt "első"-ként hivatkozunk a játékosra
// } else {
//   masodikGep változó használatával csinálj valamit
//   itt "második"-ként hivatkozunk a játékosra
// }

namespace code1
{
    class Program
    {
        public void Bemutatkozas() {
            Console.WriteLine("NIM");
            Console.WriteLine("Ez egy kis közismert játék...");
            Console.WriteLine("Van N darab gyufánk. Két játékos felváltva lép, és elvehet 1, 2 vagy 3 gyufát a lépésében.");
            Console.WriteLine("Aki elveszi az utolsó gyufát, nyert. (Hiszen a másik játékos nem fog tudni lépni.)");
            Console.WriteLine();
        }
        bool GepLegyen() {
            Console.WriteLine("Gép vagy ember legyen a következő játékos? (G vagy E)");
            bool elsoGep = false;
            if (Console.ReadLine().ToLower() == "g") {
                elsoGep = true;
            }
            return elsoGep;
        }
        int GepLep() {
            return 1;
        }
        int EmberLep(int gyufa) {
            Console.WriteLine("Első játékos jön.");
            int i = 0;
            do {
                Console.WriteLine("Hány gyufát szeretnél elvenni?");
                i = Convert.ToInt32(Console.ReadLine());
            } while (i < 1 || i > 3 || i > gyufa);
            return i;
        }
        void JatekVege(bool elsoJottVolna) {
            if (elsoJottVolna) {
                Console.WriteLine("Az első játékos nem tud lépni, tehát a második játékos nyert.");
            } else {
                Console.WriteLine("A második játékos nem tud lépni, tehát az első játékos nyert.");
            }

        }
        public Program() {
            Bemutatkozas();

            Console.WriteLine("Hány gyufa legyen a játékban?");
            int gyufa = Convert.ToInt32(Console.ReadLine());

            bool elsoGep = GepLegyen();
            bool masodikGep = GepLegyen();

            bool elsoJon = true;
            while (gyufa > 0) {
                Console.WriteLine($"Jelenleg {gyufa} darab gyufa van az asztalon.");
                if (elsoJon) {
                    int lepes = 0;
                    if (elsoGep) {
                        lepes = GepLep();
                    } else {
                        lepes = EmberLep(gyufa);
                    }
                    gyufa -= lepes;
                    Console.WriteLine($"Az első játékos {lepes} gyufát vett el.");
                } else {
                    int lepes = 0;
                    if (masodikGep) {
                        lepes = GepLep();
                    } else {
                        lepes = EmberLep(gyufa);
                    }
                    gyufa -= lepes;
                    Console.WriteLine($"A második játékos {lepes} gyufát vett el.");
                }
                elsoJon = !elsoJon;
            }
            JatekVege(elsoJon);
        }
        static void Main(string[] args)
        {
            new Program();
        }
    }
}
