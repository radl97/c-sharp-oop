using System;
using System.Collections.Generic;

// Itt oldjuk meg az alábbival kapcsolatos problémát:
// public int Lep(int gyufa) {
//     if (gep) {
//         return GepLep();
//     } else {
//         return EmberLep(gyufa);
//     }   
// }
// Két működés közül választunk egy olyan változó szerint, amit rögtön tudunk.
// E helyett érdemes két osztályba tenni a viselkedést.
// Mivel kicserélhetőnek kell lenniük egy közegben (Pl. kell tudni szólni, hogy lépjen),
//   ezért egy interfészt határozunk meg, amit kívülről látnak (csomagolópapír).
// Létrehoztam egy IJatekos nevű interfészt. Egyelőre nem teszünk bele semmit.
// Szétbontjuk az eredeti osztályt úgy, hogy az egyik gépként, a másik emberként viselkedik mindig.
// Így nem lesz szükségünk a gep változóra sem. (Hiszen maga a típus határozza meg a viselkedést.)
// Ezután átírjuk ott, ahol használjuk a Jatekosokat IJatekosra.
// Két helyen szembesülünk problémával:
// - Nincs az IJatekosnak int Lep(int) függvénye. Ezt gyorsan megoldjuk, ezt a metódus fejlécet az interfészhez hozzáadjuk.
//   Hasonlóan ezt tesszük a string GetNev() függvénnyel is.
// - Nem tudjuk simán azt mondani, hogy new Jatekos(Gep, ...), vagy hasonlót...
//   Erre egyelőre az a megoldásunk, hogy csinálunk egy függvényt, ami a megfelelő osztályt példányosítja:
//     IJatekos CreateJatekos(string nev) {
//         bool gep = GepLegyen();
//         if (gep) {
//             return new GepJatekos(nev);
//         } else {
//             return new EmberJatekos(nev);
//         }
//     }

namespace code4
{
    interface IJatekos {
        string GetNev();
        int Lep(int gyufa);
    }
    class EmberJatekos : IJatekos {
        private string nev;
        public EmberJatekos(string nev) {
            this.nev = nev;
        }
        public int Lep(int gyufa) {
            // Eredetileg Jatekos.EmberLep()...
            Console.WriteLine($"{nev} jön.");
            int i = 0;
            do {
                Console.WriteLine("Hány gyufát szeretnél elvenni?");
                i = Convert.ToInt32(Console.ReadLine());
            } while (i < 1 || i > 3 || i > gyufa);
            return i;
        }
        public string GetNev() {
            return nev;
        }
    }
    class GepJatekos : IJatekos {
        private string nev;
        public GepJatekos(string nev) {
            this.nev = nev;
        }
        public int Lep(int gyufa) {
            // Eredetileg GepJatekos.Lep()
            // Van viszont egy használatlan paraméter: hány gyufánk van épp.
            // Egyébként ez hasznos egy igazi stratégia kialakításához ;))
            return 1;
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
        IJatekos CreateJatekos(string nev) {
            bool gep = GepLegyen();
            if (gep) {
                return new GepJatekos(nev);
            } else {
                return new EmberJatekos(nev);
            }
        }
        void JatekVege(string nyertesNev) {
            Console.WriteLine($"{nyertesNev} nyert.");
        }
        public Program() {
            Bemutatkozas();

            Console.WriteLine("Hány gyufa legyen a játékban?");
            int gyufa = Convert.ToInt32(Console.ReadLine());

            List<IJatekos> jatekosok = new List<IJatekos>();

            jatekosok.Add(CreateJatekos("Az első játékos"));
            jatekosok.Add(CreateJatekos("A második játékos"));

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
