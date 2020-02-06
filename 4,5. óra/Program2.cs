using System;
using System.Collections.Generic;

// Ez a változtatás azért "kellett", mert szimmetrikus az, hogy mit tud az "első" vagy "második" játékos csinálni:
// if (elsoJon) {
//     int lepes = 0;
//     if (elsoGep) {
//         lepes = GepLep();
//     } else {
//         lepes = EmberLep(gyufa);
//     }
//     gyufa -= lepes;
//     Console.WriteLine($"Az első játékos {lepes} gyufát vett el.");
// } else {
//     int lepes = 0;
//     if (masodikGep) {
//         lepes = GepLep();
//     } else {
//         lepes = EmberLep(gyufa);
//     }
//     gyufa -= lepes;
//     Console.WriteLine($"A második játékos {lepes} gyufát vett el.");
// }
// Ezt az elágazást jó lenne egybevonni.
// Először gyűjtsük össze, mik a különbségek, amik megakadályozzák az összevonást:
// - az egyik elsoGep-et használja, másik masodikGep-et
// - Használjuk az "első" és "második" játékos kifejezéseket.
// A változtatás: tároljuk ezeket egy kételemű tömbben!
// Nem hangzik először annyira erősnek, de a kódismétlést így el tudjuk kerülni.
// az "elsoJon" változó helyett a "melyikJon" egy indexként tárolja azt, amit használunk.
// Az nev[melyikJon] segítségével az éppen aktuális elemet használjuk fel.

// Amiket mostmár tudunk változtatni viszonylag gyorsan a kódban:
// - Már nem *annyira* nehézkes egy új gépet bevezetni:
//   . a GepLegyen() függvényt kell változtatni,
//   . és ezt:
//     if (isGep[melyikJon]) {
//         lepes = GepLep();
//     } else {
//         lepes = EmberLep(nev[melyikJon], gyufa);
//     }

// De még mindig van mit fejleszteni a kódban:
// Például van két listánk, és ugyanaz a méretük!
// Ilyet már láttunk...
// List<bool> isGep = new List<bool>();
// List<string> nev = new List<string>();

namespace code2
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
        int EmberLep(string kiJon, int gyufa) {
            Console.WriteLine($"{kiJon} jön.");
            int i = 0;
            do {
                Console.WriteLine("Hány gyufát szeretnél elvenni?");
                i = Convert.ToInt32(Console.ReadLine());
            } while (i < 1 || i > 3 || i > gyufa);
            return i;
        }
        void JatekVege(string nyertesNev) {
            Console.WriteLine($"{nyertesNev} nyert.");
        }
        public Program() {
            Bemutatkozas();

            Console.WriteLine("Hány gyufa legyen a játékban?");
            int gyufa = Convert.ToInt32(Console.ReadLine());

            List<bool> isGep = new List<bool>();
            // Innentől képesek vagyunk kezelni a játékosok neveit rendesen is!
            List<string> nev = new List<string>() {"Az első játékos", "A második játékos"};

            // Ez egyébként picit ronda megoldás, de köztes lépésként nekünk jó lesz...
            isGep.Add(GepLegyen());
            isGep.Add(GepLegyen());

            int melyikJon = 0;
            while (gyufa > 0) {
                Console.WriteLine($"Jelenleg {gyufa} darab gyufa van az asztalon.");
                int lepes = 0;
                if (isGep[melyikJon]) {
                    lepes = GepLep();
                } else {
                    lepes = EmberLep(nev[melyikJon], gyufa);
                }
                gyufa -= lepes;
                Console.WriteLine($"{nev[melyikJon]} {lepes} gyufát vett el.");
                melyikJon = 1 - melyikJon;
            }
            int nyertes = 1 - melyikJon;
            JatekVege(nev[nyertes]);
        }
        static void Main(string[] args)
        {
            new Program();
        }
    }
}
