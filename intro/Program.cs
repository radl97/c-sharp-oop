 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelloVilag
{
    class Program
    {
        public Program()
        {
            // Kiírás. Támogatja a magyar speciális karaktereket a kód is: őűŐŰ
            Console.WriteLine("Írd be a neved! őűŐŰ");

            // Beolvasás:
            string nev = Console.ReadLine();

            // Kiírás, változó értékének* illesztése szövegbe:
            Console.WriteLine($"Szia {nev}!");

            Console.WriteLine("Hány ismerősöd van?");
            // beolvasás; convert to int32 (sima integer mérete 32 bit)
            int dbIsmeros = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("Kik az ismerőseid?");

            // "tömb" létrehozása (van más, pl. string[])
            List<string> ismerosok = new List<string>();

            // sima ciklus
            for (int i = 0; i < dbIsmeros; i++)
            {
                Console.WriteLine($"Ki a(z) {i}. ismerősöd?");
                string ismeros = Console.ReadLine();
                ismerosok.Add(ismeros);
            }

            Console.WriteLine("Az ismerőseid, sorrendben:");

            // Tömbön (és hasonló dolgokon) iterálás.
            // Ha beszélhetünk valamilyen sorrendről, akkor sorrendben járja be az elemeket.
            // Tömb indexét nem tudjuk ilyenkor (ilyen egyszerűen).
            foreach (string ismerosNeve in ismerosok)
            {
                Console.WriteLine(ismerosNeve);
            }

            // A program leállna -> egy gombnyomást megvár
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            new Program();
        }
    }
}
