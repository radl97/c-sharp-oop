/*
2. óra:

Menete:

HF Megbeszélése
break, return működése "élesben"

1. lépés:

Egymáshoz tartozó adatok együtt tárolása -> struktúra
struktúra + viselkedések (metódusok) együtt tárolása -> objektum -> Objektumorientált programozás (OOP)
Másképp: Adat + viselkedés -> egy felelősség -> egy objektum (Single Responsibility Principle)

OOP alapvető elve:
Nem a fordítónak/gépnek írjuk a kódot, a fordító okos (nagyon!).
A kódot az azt olvasó ember számára írjuk (1-szer írnak minden kódot, de 100+ alkalommal olvassák).
Új fejlesztéseket gyorsan kell lekódolni (pl. startupoknál) -> jól fejleszthető kódot kell írni, alkalmasat változásokra.
Tehát a futás gyorsasága, memóriaigénye nem elsődleges szermpont mostmár!

Struktúra (van C#-ban talán, de nem igazán használt): egymáshoz tartozó adatok halmaza:
class Szervezo{public int kezd,veg;}

Osztály (class): egymáshoz tartozó adatok *és viselkedés* halmaza:

class Szervezo {
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
    Azonos név, nincs adattípus -> konstruktor.
    Az eredeti esetet és használatot fedi le.
    Minden órát megjelöl [kezd;veg) intervallumban
  - Szervezo2(List<bool> raer)
    konstruktor
    Minden időpontra egy i/h érték, hogy ráér-e
    Gyakorlatilag a tömb másolása kell csak.
  - Raer(int ora)
    Csak egy tömbelérés :)

+1: a Program() függvényből egy részt kitettem külön függvénybe, IsJoOra néven.
  Megjegyzés: Általában egy függvény 1 ciklust tartalmaz maximum (nem olyan fontos ökölszabály)

Apró megjegyzések:
Konvenció=több ekvivalens/hasonló működésű kód között hogyan válasszunk...
Nyelv-, néha cégfüggő
  Változónév kisbetűvel, metódusnév, osztály nagybetűvel kezdődik
  Ezen kívül CamelCase sémának felelnek meg (nem snake_case-nek)
  Komment nagybetűvel kezdődik, ponttal zárul.
  IsSomething, GetSomething -> lekérdezés (előbbi bool változó lekérdezésére)
  SetSomething -> módosítás
    Ne csináljanak váratlan dolgokat (pl. Get ne módosítson)
  Angolt/magyart ne vegyítsünk (itt sántít a kód :/ )
---

Házi:

több házit adok, próbálok a legkönnyebtől fokozatosan nehezedő házikat adni.
Ezekből egyet, vagy kettőt lehet érdemes csinálni.
HF1*: Pár kisebb/nagyobb módosítást/bővítést kíván ezen a kódon, a fejleszthetőséget és az elkülönített felelősséget próbálja bemutatni.

HF1a: befejezni a második feladatot. Az első feladat nagyon sok segítséget ad ebben.

HF1b: SzervezoManager osztály létrehozása, ami kezeli valamilyen formában a szervezőket.
Tanácsok:
  Nem érdemes egyből beletenni az összes szervezőt. (AddSzervezo használatával lehet újat belerakni)
  Csak a számunkrw fontos kérdéseket vezessük ki: (IsAllTimeGood, IsEveryoneNeeded)

HF1c: azokat a szervezőket, akik mindenképp kellenek (Első házi 2. feladat szerint), írjuk ki név szerint.
  --> legyen neve a szervezőnek, ezt is adjuk meg.

HF2: Rekonstruálni a lenti kódot. Egy folyamatként mutattam be az OOP lényegét. Vagy a folyamaton végigmenve, vagy csak a végleges kódot rekonstruálni sok hasznot rejthet.
  Pozitívuma, hogy nem lehet nagyon elakadni, végső soron ctrl+c ctrl+v "megoldása" a házinak.

HF3: (ajánlott) Egy, az ehhez a feladathoz hasonlót szeretnék adni, amit az előző kód alapján meg lehet oldani.
  Egy olyan alkalmazást tervezünk, ami segítségével egy társaság a költekezését tudja követni.
  Az ötlet, hogy mindenki a vonaton beledob egy közös kasszába (ugyanannyi) pénzt, utána mindent ebből költenek.
  Mindig felírják az applikációba, hogy ki mit/mennyit vett, amit az applikáció menedzsel.
  A pénzügyekkel nyaralás végén az applikáció összeállít egy tartozási gráfot (ki kinek mennyivel tartozik), és egy statisztikát.
  (ötlet a Splitwise alkalmazás alapján :) )

  A feladatunk az összeállítás részében pár statisztikai adat készítése.
  Adottak rendelések, amik tartalmazzák a vevő nevét, a vásárolt termék árát és a mennyiségét (egész szám), és a vétel idejét (hanyadik napja a nyaralásnak/óra/perc/másodperc).
  Ezeket az adatokat már vevő szerint szétbontva kapjuk meg.
  A bemenettel nem kell foglalkozni, úgy tehetjük struktúrába, ahogy szeretnénk.
  1. feladat: Adjuk meg, hogy ki az, aki legtöbbször rendelt!
  2. feladat: Adjuk meg, hogy mennyit költött Feri, az egyes sorszámú személy.
  3. feladat: Írjuk ki, hogy ki az, aki biztosan nem fog tartozni senkinek, hiszen ő költött a legtöbbet.
HF4: Algoritmikusan bonyolultabb feladat :)
  HF3 bővítése: Adjunk meg egy egyszerűsített tartozási gráfot.
  Egyszerűsítettnek tekintjük a gráfot, ha kevesebb, mint N él van benne, és nincs tranzitív tartozás (azaz Anna tartozik Bélának és Béla tartozik Cecilnek)
  Belátható, hogy mindig van ilyen gráf.
  Kimenet: Ki kinek mennyivel tartozik.
  CSOKIT ÉRŐ FELADAT
    feltételek:
      hatékonyan van implementálva (memóriaigény bármilyen lehet, futási idő ne nőjjön a felhasználók számával sokat)
      A 3. feladat kész van teljesen

HF5: (előretekintés, utánanézős) interface definiálása, két külön szervező osztály közös használatához.
  Mese: A Szervezo és Szervezo2 ugyanazt a célt szolgálják, mindkettő saját előnyeivel és hátrányaival.
    Mindkettőt ki lehet aknázni: alapvetően használjuk a Szervzo-t, de ha szükség van rá, akkor a Szervezo2-t használjuk.

Amikről várhatóan lesz szó köv. alkalommal:
  interfész/implementáció
  interface (mint kulcsszó)
  x,y-r,phi
  leszármazás
  OOP ökölszabályok
  property, miért használjuk
  referencia- vagy értékszerinti átadás?
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
