using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ooify
{
    class Program
    {
        public Program()
        {
            // A feladat egy 5 órás verseny felügyelésében segédkezni. 1-től 6-ig tart a verseny.
            // Mindenki megadta, hogy mettől meddig ér rá. (A beolvasással jelenleg nem foglalkozunk)
            // Pl. aki azt írta, hogy 1-3 ér rá, 2 órát ér rá.
            // Arra kell válaszolnunk, hogy:
            //  1) Minden órában van valaki, aki felügyel?
            //  2) Van-e, akire nincs szükség a felügyelésben?
            List<int> kezd = new List<int>() { 3, 2, 1 };
            List<int> veg = new List<int>() { 5, 3, 2 };

            // Értéket adunk neki: mi van, ha nem kerül a for ciklusba? (C# kényszeríti is)
            bool vanFelugyelo = false;
            for (int ora = 1; ora <= 5; ora++)
            {
                vanFelugyelo = false;
                for (int i = 0; i < kezd.Count(); i++)
                {
                    if (kezd[i] <= ora && ora < veg[i])
                    {
                        vanFelugyelo = true;
                        break;
                    }
                }
                if (!vanFelugyelo)
                {
                    break;
                }
            }

            if (vanFelugyelo)
            {
                Console.WriteLine("Van felügyelő minden órára");
            }
            else
            {
                Console.WriteLine("Nincs minden időpontban felügyelő!");
            }

            // ... Ugyanez, de egyel kevesebb elemmel...?
            // Azt, amit használni fogunk, mentsük el függvénybe természetesen :)
        }
        static void Main(string[] args)
        {
        }
    }
}
