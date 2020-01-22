/*
2. óra:
HF Megbeszélése
break, return működése "élesben"
Egymáshoz tartozó adatok együtt tárolása -> struktúra
struktúra + viselkedések (metódusok) együtt tárolása -> objektum -> Objektumorientált programozás
Másképp: Adat + viselkedés -> egy felelősség -> egy objektum (Single Responsibility Principle)

OOP alapvető elve:
Nem a fordítónak/gépnek írjuk a kódot, a fordító okos (nagyon!).
A kódot az azt olvasó ember számára írjuk (1-szer írnak minden kódot, de 100+ alkalommal olvassák).
Új fejlesztéseket gyorsan lekódolni (pl. startupoknál) -> jól fejleszthető kódot kell írni, alkalmasat változásokra.
Tehát a gyorsaság nem elsődleges szermpont mostmár!

TODO konkrétumok leírása ide :)

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laci2
{
    // Új feladat: Egy szervező érhet rá nem összefüggő intervallumban
    class Szervezo2
    {
        // Adott órában ráér-e
        // 0. elem mindig false, 1.-5. elem "értelmes"
        private List<bool> raeresiMatrix;

        public Szervezo2(List<bool> raer)
        {
            raeresiMatrix = new List<bool>();
            // 0. elemet feltöltjük:
            raeresiMatrix.Add(false);

            foreach (bool b in raer)
            {
                raeresiMatrix.Add(b);
            }
        }

        public Szervezo2(int kezd, int veg)
        {
            raeresiMatrix = new List<bool>();
            for (int ora = 0; ora < 6; ora++)
            {
                raeresiMatrix.Add(false);
            }
            for (int ora = kezd; ora < veg; ora++)
            {
                raeresiMatrix[ora] = true;
            }
        }

        public bool Raer(int ora)
        {
            return raeresiMatrix[ora];
        }
    }
    class Szervezo
    {
        // Adattagok
        private int kezd;
        private int veg;
        // Mivel (mostmár) private, ez:
        // if (szervezok[i].kezd <= ora && ora < szervezok[i].veg)
        // {
        //     ...
        // }
        // nem fog lefordulni a Program osztályban

        // Konstruktor
        public Szervezo(int a, int b)
        {
            kezd = a;
            veg = b;
            // Ugyanaz, mint:
            // this.kezd = a;
            // this.veg = b;
        }

        // kívülről ilyen lenne:
        // public bool Raer(Szervezo self, int ora)
        // {
        //     return self.kezd <= ora && ora < self.veg;
        // }
        public bool Raer(int ora)
        {
            // Ez is lefordul, de a "this" elhagyható:
            //return this.kezd <= ora && ora < this.veg;
            return kezd <= ora && ora < veg;
        }
    }
    class Program
    {
        public bool IsJoOra(List<Szervezo2> szervezok, int ora)
        {
            for (int i = 0; i < szervezok.Count; i++)
            {
                // Raer(szervezok[i], ora); nem fordul le!
                // De így működik gyakorlatilag :)
                if (szervezok[i].Raer(ora))
                {
                    return true;
                }
            }
            // lefutna break esetén...
            return false;
        }
        public Program()
        {
            List<Szervezo2> szervezok = new List<Szervezo2>()
            {
                new Szervezo2(1,2),
                new Szervezo2(1,2),
                new Szervezo2(3,5),
                new Szervezo2(2,3),
                // A szervező2 képes nem összefüggő intervallumot
                // IS tárolni.
                new Szervezo2(
                    new List<bool>() {true, false, true, false, false}
                )
            };

            Console.WriteLine("1. feladat1:");
            /*
            bool mindJoOra = true;
            for (int ora = 1; ora < 6; ora++)
            {
                bool joOra = false;
                for (int i = 0; i < szervezok.Count; i++)
                {
                    // Raer(szervezok[i], ora); nem fordul le!
                    // De így működik gyakorlatilag :)
                    if (szervezok[i].Raer(ora))
                    {
                        joOra = true;
                        break;
                    }
                }
                if (!joOra)
                {
                    mindJoOra = false;
                    break;
                }
            }
            */
            bool mindJoOra = true;
            for (int ora = 1; ora < 6; ora++)
            {
                if (!IsJoOra(szervezok, ora))
                {
                    mindJoOra = false;
                    break;
                }
            }
            if (mindJoOra)
            {
                Console.WriteLine("OK");
            }
            else
            {
                Console.WriteLine("Nem OK");
            }

            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            new Program();
        }
    }
}
