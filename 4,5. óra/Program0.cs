using System;

// Ebben a kódban sok hiba van... Nem is megyek végig rajtuk.
// Sok a kódismétlés, elsősorban ezt akarjuk elkerülni.

// Legszembetűnőbb (és legkönnyebben kivédhető) probléma:
// Console.WriteLine("Gép vagy ember legyen a kezdő játékos? (G vagy E)");
// bool elsoGep = false;
// if (Console.ReadLine().ToLower() == "g") {
//     elsoGep = true;
// }

namespace code0
{
    class Program
    {
        public Program() {
            Console.WriteLine("NIM");
            Console.WriteLine("Ez egy kis közismert játék...");
            Console.WriteLine("Van N darab gyufánk. Két játékos felváltva lép, és elvehet 1, 2 vagy 3 gyufát a lépésében.");
            Console.WriteLine("Aki elveszi az utolsó gyufát, nyert. (Hiszen a másik játékos nem fog tudni lépni.)");
            Console.WriteLine();

            Console.WriteLine("Hány gyufa legyen a játékban?");
            int gyufa = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Gép vagy ember legyen a kezdő játékos? (G vagy E)");
            bool elsoGep = false;
            if (Console.ReadLine().ToLower() == "g") {
                elsoGep = true;
            }
            Console.WriteLine("Gép vagy ember legyen a második játékos? (G vagy E)");
            bool masodikGep = false;
            if (Console.ReadLine().ToLower() == "g") {
                masodikGep = true;
            }

            bool elsoJon = true;
            while (gyufa > 0) {
                Console.WriteLine($"Jelenleg {gyufa} darab gyufa van az asztalon.");
                if (elsoJon) {
                    if (elsoGep) {
                        gyufa -= 1; // van jobb stratégia :)
                        Console.WriteLine("Az első játékos 1 gyufát vett el.");
                    } else {
                        Console.WriteLine("Első játékos jön.");
                        int i = 0;
                        do {
                            Console.WriteLine("Hány gyufát szeretnél elvenni?");
                            i = Convert.ToInt32(Console.ReadLine());
                        } while (i < 1 || i > 3 || i > gyufa);
                        gyufa -= i;
                        Console.WriteLine($"Az első játékos {i} gyufát vett el.");
                    }
                } else {
                    if (masodikGep) {
                        gyufa -= 1; // van jobb stratégia :)
                        Console.WriteLine($"A második játékos 1 gyufát vett el.");
                    } else {
                        Console.WriteLine("Második játékos jön.");
                        int i = 0;
                        do {
                            Console.WriteLine("Hány gyufát szeretnél elvenni?");
                            i = Convert.ToInt32(Console.ReadLine());
                        } while (i < 1 || i > 3 || i > gyufa);
                        gyufa -= i;
                        Console.WriteLine($"Az második játékos {i} gyufát vett el.");
                    }
                }
                elsoJon = !elsoJon;
            }
            // itt gyufa == 0, véget ért a játék
            if (elsoJon) {
                Console.WriteLine("Az első játékos nem tud lépni, tehát a második játékos nyert.");
            } else {
                Console.WriteLine("A második játékos nem tud lépni, tehát az első játékos nyert.");
            }

        }
        static void Main(string[] args)
        {
            new Program();
        }
    }
}
