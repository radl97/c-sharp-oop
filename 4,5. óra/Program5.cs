using System;
using System.Collections.Generic;

// Ez már nem egy nagy probléma.
// A Jatek osztályt hoztam létre, aminek felelőssége a játék vezénylése.
// Abba érdemes belegondolni, hogy gyakorlatilag bármilyen játékot ráhúzhatunk erre a rendszerre:
// public Program() {
//     Bemutatkozas();

//     Console.WriteLine("Hány gyufa legyen a játékban?");
//     Jatek jatek = new Jatek(
//         Convert.ToInt32(Console.ReadLine()) // gyufa
//     );

//     jatek.AddJatekos(CreateJatekos("Az első játékos"));
//     jatek.AddJatekos(CreateJatekos("A második játékos"));

//     while (!jatek.Vege()) {
//         Console.WriteLine($"Jelenleg {jatek.GetGyufa()} darab gyufa van az asztalon.");
//         jatek.Leptet();
//     }
//     JatekVege(jatek.GetNyertesNev());
// }
// Amit változtatni kéne a játék általánosításához:
// - Valószínűleg nem lennének benne gyufák. Más adatot kéne kiírni, ez is könnyen megoldható.
// - Lehet, hogy több játékos lenne. Könnyen megoldható: még egy AddJatekos...

namespace code5
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

    class Jatek {
        private List<IJatekos> jatekosok;
        private int gyufa;
        private int melyikJon;
        public Jatek(int gyufa) {
            jatekosok = new List<IJatekos>();
            this.gyufa = gyufa;
            melyikJon = 0;
        }
        public void AddJatekos(IJatekos jatekos) {
            jatekosok.Add(jatekos);
        }
        public void Leptet() {
            if (Vege()) {
                // ... Hiba lekezelése?
            }
            int lepes = jatekosok[melyikJon].Lep(gyufa);
            gyufa -= lepes;
            Console.WriteLine($"{jatekosok[melyikJon].GetNev()} {lepes} gyufát vett el.");
            melyikJon = 1 - melyikJon;
        }
        public int GetGyufa() {
            return gyufa;
        }
        public bool Vege() {
            return gyufa == 0;
        }
        public string GetNyertesNev() {
            if (!Vege()) {
                // ... Hiba lekezelése?
            }
            int nyertes = 1-melyikJon;
            return jatekosok[nyertes].GetNev();
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
        IJatekos CreateJatekos(string nev) {
            Console.WriteLine("Gép vagy ember legyen a következő játékos? (G vagy E)");
            if (Console.ReadLine().ToLower() == "g") {
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
            Jatek jatek = new Jatek(
                Convert.ToInt32(Console.ReadLine()) // gyufa
            );

            jatek.AddJatekos(CreateJatekos("Az első játékos"));
            jatek.AddJatekos(CreateJatekos("A második játékos"));

            while (!jatek.Vege()) {
                Console.WriteLine($"Jelenleg {jatek.GetGyufa()} darab gyufa van az asztalon.");
                jatek.Leptet();
            }
            JatekVege(jatek.GetNyertesNev());
        }
        static void Main(string[] args)
        {
            new Program();
        }
    }
}
