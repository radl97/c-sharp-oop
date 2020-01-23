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

Struktúra: egymáshoz tartozó adatok halmaza:
class Szervezo{public int kezd,veg;}

Osztály (class): egymáshoz tartozó adatok *és viselkedés* halmaza:
class Szervezo{
  private int kezd;
  private int veg;
  public Szervezo(int a, int b){
    this.kezd = a;
    this.veg = b;
  }
  public bool Raer(int ora){
    return this.kezd <= ora && ora < veg;
  }
}

Metódusokban, konstruktorokban "this"-szel lehet hivatkozni az adott példányra.
  Pl. this.kezd
Gyakorlatilag 0. paraméter (nyelv konkrétan így működik)
A this kulcsszó elhagyható (ha nincs névütközés): this.kezd -> kezd

public Szervezo(...): konstruktor
  - első dolog, ami meghívódik példány létrehozásakor,
      inicializáció szerepű
  - használat: new Szervezo(...), egy új Szervezo-vel tér vissza
      példányosítás
  - nincs visszatérési típus *jelölve* (public void Szervezo helyett simán public Szervezo)
  - Csak éppen létrejövő példánynál van értelme, this-en keresztül érhető el a referenciája ennek.
Raer: metódus; az objektumhoz tartozó viselkedés
  - Van egy referenciája a példányhoz, csak példányhoz kötve értelmezhető a működése
  - Használat: szervezi.Raer(...)

Egy metódusnévhez (vagy konstruktorhoz) tartozhat több metódus,
HA megkülönböztethető a paraméterlista alapján

Ezek bevezetésével nem használjuk az adattagokat kívülről.
Ezért privátnak jelölhetjük: csak az osztály metódusai érik el az adattagokat.
public int kezd; -> private int kezd;

Általában minden adattag privát lesz, később kap értelmet ez az ökölszabály.

A következő lépés egy új feladathoz kötődött: (változó követelmények)
Van olyan szervező, aki nem folytonos intervallumban ér rá (közepén meg kell tartani egy óráját).
Ebben lesz az igazi ereje a megközelítésnek:
Kizárólag a Szervezo osztályt kell kicserélni (didaktikai célból egy másik, Szervezo2 osztályt hoztam létre)

A megoldásban minden szervező minden órában elmondja, hogy ráér vagy nem.
Tehát minden szervezőre 5 igaz/hamis értéket tárolunk.
(Megjegyzés: a lenti kód egy 0. elemet is tárol, ami mindig hamis, hogy egytől indexeljünk...
Lehetne szebb is ez a rész :) )
Ez az eredeti egy intervallumot is tudja reprezentálni, meg az új eseteket is.

A Szervezo2 metódusai:
  - Szervezo2(int kezd, int veg)
    Azonos név, nincs adattípus -> konstruktor
    Az eredeti esetet és használatot fedi le

TODO folytatás

több házit adok, próbálok a legkönnyebtől fokozatosan nehezedő házikat adni.
Ezekből egyet, vagy kettőt lehet érdemes csinálni.
HF1: befejezni a második feladatot. Az első feladat nagyon sok segítséget ad ebben.

HF2: Rekonstruálni a lenti kódot. Egy folyamatként mutattam be az OOP lényegét. Vagy a folyamaton végigmenve, vagy csak a végleges kódot rekonstruálni sok hasznot rejthet.
  Pozitívuma, hogy nem lehet nagyon elakadni, végső soron ctrl+c ctrl+v "megoldása" a házinak.

HF3: (ajánlott) Egy, az előzőhöz hasonlót szeretnék adni, amit az előző kód alapján meg lehet oldani.
  TODO

HF4: (előretekintés, utánanézős) interface definiálása, két külön szervező osztály közös használatához.
  Mese: A Szervezo és Szervezo2 ugyanazt a célt szolgálják, mindkettő saját előnyeivel és hátrányaival.
    Mindkettőt ki lehet aknázni: alapvetően használjuk a Szervzo-t, de ha szükség van rá, akkor a Szervezo2-t használjuk.


Amikről várhatóan lesz szó köv. alkalommal:
  interfész/implementáció
  leszármazás
  OOP ökölszabályok

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
