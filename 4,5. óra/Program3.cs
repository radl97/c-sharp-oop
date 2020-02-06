using System;
using System.Collections.Generic;

// A legegyértelműbb jel arra, hogy két adat összetartozik, hogy két listánk van, amiknek
// ugyanakkora a méretük, soha nem használjuk csak az egyik listát a kettő közül, és
// azonos indexeléssel tároljuk őket...
// Létrehoztam egy új osztályt, amit Jatekos-nak neveztem el.
// Nyilvánvalóan tartalmazza a gep és a nev változókat.
// Ezen kívül próbáltam a "rá tartozó viselkedéseket" mellétenni:
// - A konstruktorban meg tudom adni azt, hogy gép legyen vagy nem, és, hogy mi a neve.
// - Az, hogy mit fog lépni, az csak a játékos dolga: GepLep(), EmberLep() és Lep()
//   Az előbbi kettő az kívülről nem hívható meg, viszont jobban látszik a kódban, hogy most mi történik.
// - A nevét is használjuk kívül a játékosnak, ezért ahhoz egy lekérő függvényt készítettem: GetNev()
// Ez a felelősségek szétbontása. Másnak nem kell foglalkoznia a Jatekos gep vagy nev változójával,
// cserébe az szabadon eldöntheti, hogy hogyan reprezentálja.
// A másik pozitívum, hogy egymáshoz tartozó dolgok egy helyen legyenek megtalálhatók:
//   A Jatekos osztály fele az összes játékos által meghozott döntésről, akár gép, akár ember.

// Amit most meg tudnánk csinálni:
// - Még egyszerűbb egy másik gépet csinálni, vagy esetleg egy neten keresztül játszó játékost implementálni:
//   a GepLegyen()-ben adni kell arra lehetőséget, hogy ne gép legyen a játékos,
//   és új "stratégiát" kell kidolgozni a Jatekos osztályban.

// A további problémák a kódban:
// A switch/case-t nem szeretjük. Ez azért van, mert 99%, hogy
//   kiváltható egy jó Objektum-orientált eszközzel.
// Ez a kód az, ami a problémát mutatja nekünk:
// public int Lep(int gyufa) {
//     if (gep) {
//         return GepLep();
//     } else {
//         return EmberLep(gyufa);
//     }   
// }
// Egy osztálynak van olyan mezője, ami több különálló viselkedés közül választ,
// ráadásul a legelején eldől az, hogy melyik irányban fog lefutni a kód.
namespace code3
{
    class Jatekos {
        private bool gep;
        private string nev;
        public Jatekos(bool gep, string nev) {
            this.gep = gep;
            this.nev = nev;
        }
        private int GepLep() {
            return 1;
        }
        private int EmberLep(int gyufa) {
            Console.WriteLine($"{nev} jön.");
            int i = 0;
            do {
                Console.WriteLine("Hány gyufát szeretnél elvenni?");
                i = Convert.ToInt32(Console.ReadLine());
            } while (i < 1 || i > 3 || i > gyufa);
            return i;
        }
        public int Lep(int gyufa) {
            if (gep) {
                return GepLep();
            } else {
                return EmberLep(gyufa);
            }   
        }
        public string GetNev() {
            return nev;
        }
    }
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
        void JatekVege(string nyertesNev) {
            Console.WriteLine($"{nyertesNev} nyert.");
        }
        public Program() {
            Bemutatkozas();

            Console.WriteLine("Hány gyufa legyen a játékban?");
            int gyufa = Convert.ToInt32(Console.ReadLine());

            List<Jatekos> jatekosok = new List<Jatekos>();

            jatekosok.Add(new Jatekos(GepLegyen(), "Az első játékos"));
            jatekosok.Add(new Jatekos(GepLegyen(), "A második játékos"));

            int melyikJon = 0;
            while (gyufa > 0) {
                Console.WriteLine($"Jelenleg {gyufa} darab gyufa van az asztalon.");
                int lepes = jatekosok[melyikJon].Lep(gyufa);
                gyufa -= lepes;
                Console.WriteLine($"{jatekosok[melyikJon].GetNev()} {lepes} gyufát vett el.");
                melyikJon = 1 - melyikJon;
            }
            int nyertes = 1 - melyikJon;
            JatekVege(jatekosok[nyertes].GetNev());
        }
        static void Main(string[] args)
        {
            new Program();
        }
    }
}
