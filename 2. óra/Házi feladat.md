# Házi feladat 3. órára

A feladat lényege az interface használatának megértése és begyakorlása.
Az utóbbi alkalomhoz hasonlóan (Szervezo-nél) a most elkezdett feladatot fogjuk folytatni.

Megint sok házifeladatot adok, amiből nagyon kevés kötelező, de szerintem hasznos minél többet csinálni.

## A feladat

A feladatunk különböző alakzatokról adatok kigyűjtése. Több, egyszerű feladatunk van: az alakzatok különböző paraméterei alapján összegzés, számlálás, stb. programozási tételeket fogjuk használni.

A legfontosabb két alakzat: a tengelyekkel párhuzamos oldalú **téglalap** és a **kör**. Ezen kívül szorgalmi feladat lesz ezen lista bővítése: háromszög, tetszőleges konvex négyszög, tetszőleges (önmagát nem metsző, lyukmentes) sokszög.

A feladatok:

1. A maximális területű alakzatot add meg, a meghatározó paramétereivel (téglalap esetén pl. bal felső és jobb alsó pontok koordinátái)
2. Adjuk meg, hogy hány alakzatra igaz, hogy a **kerület négyzete osztva a területtel <= 20**. Ez egyféle mérőszáma egy alakzat "kerekségének": körnél a legkisebb, szabályos sokszögeknél "viszonylag kicsi".

**Fontos:** Az bemenő adatokat tároljuk el az alakzatoknak, és onnan számoljunk, különben elveszíti a feladat a lényegét! (Ne csak pl. a kerületet és területet tároljuk)

## Kimenet

```
1. feladat: (2.5,1.0) középpontű 3 sugarú kör. a legnagyobb területű alakzat.
2. feladat: 3 alakzat "kerek".
```

## Segítség

Minden számot tizedes ponttal írjunk le, hogyha lebegőpontos számolás része, így az osztás az elvárt módon fog működni. Ez általában nem okozhat hibát, de vegyük elejét. Pl. `3/4*r*r*r*3.14` -> `3.0/4.0*r*r*r*3.14` (vagy `2.356*r*r*r`).

Itt megpróbálok spoiler-eket tenni valami értelmes felirattal, ami alapján érthető, hogy miket csinálunk.

<details> 
  <summary> Útmutató (ne nézd meg, ha nem akadsz el :) ) </summary>
  <p>Definiáljuk a két osztályt: Teglalap és Kor. Ezekhez gyűjtsük össze a megfelelő adatokat, amiket a konstruktoron keresztül kell megadni.</p>
  <p>Adjuk meg a fontos metódusaikat, amiket később fogunk használni: pl. GetKerulet és GetTerulet (Más metódusokkal meg lehet oldani helyesen a feladatot).</p>
  <p>Közös metódus fejlécek legyenek, hogy egy új interfészt tudjunk létrehozni.</p>
  <p>Hozzunk létre egy IAlakzat interfészt, ami a fontos metódus fejléceit tartalmazza. `{}` helyett `;`-t használjunk.</p>
  <p>Csináljunk egy listát List<IAlakzat> típussal.</p>
  <p>Az inicializálásnál használjuk a Kor és a Teglalap típusokat is, hogy lássuk, hogyan működik.</p>
  <p>A két feladatot ennek segítségével meg tudjuk oldani. Használjunk `foreach()`-et.</p>
</details>

<details> 
  <summary> Hogyan írjuk ki a "fontos adatait" az alakzatnak? </summary>
  Készítsünk egy string Stringify() metódust az interfésznek. Ezt kérjük le a következő feladatban.
</details>

<details> 
  <summary> Hogyan olvassunk be lebegőpontos számot? (Ha szeretnénk beolvasni az adatokat) </summary>
  `double ertek = Convert.ToDouble(Console.Readline());`
</details>

## Szorgalmik

- Olvassuk be az adatokat. Lásd lentebb.
- Adjuk meg a(z egyik) legbalrább lévő pontot tartalmazó alakzatot. Ehhez kérjük be a pozícióját az alakzatoknak. (Több megoldás esetén bármelyik megadható.)
- Vegyük észre, hogy két hasonló alakzatnak azonos a K^2/T száma. Próbáljuk meg ezt kihasználni kevesebb számolás eléréséhez.
- Támogatott alakzatok bővítése: háromszög, konvex négyszög, vagy akár **tetszőleges** (önmagát nem metsző, lyukmentes) **sokszög**; ez utóbbi algoritmusban érdekes feladat :)

## Beolvasás

Ez sem kötelező, de érdemes megcsinálni.

Én így oldanám meg:

```
Van még alakzat? Igen
Milyen alakzat? Kör
Mi a középpont X koordinátája? 1.5
Mi a középpont Y koordinátája? 2
Mi a kör sugara? 3.1
Van még alakzat? Nem
```
