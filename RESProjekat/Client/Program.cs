using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace Client
{
    class Program
    {
        private static readonly ICommunication dp = new DataAccess.DataProcessing();
        
        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            Parallel.Invoke
            (
                () => AutomaticSend(),
                () => ManualSend()
            );
        }

        [ExcludeFromCodeCoverage]
        public static void AutomaticSend()
        {
            double potrosnja = 50;

            while (true)
            {
                if (potrosnja > 100)
                    potrosnja = 50;

                if (potrosnja % 2 == 1)
                    dp.FromClient(potrosnja, DateTime.Now, "Novi Sad");
                else
                    dp.FromClient(potrosnja, DateTime.Now, "Beograd");

                potrosnja++;

                Thread.Sleep(5000);
            }
        }

        [ExcludeFromCodeCoverage]
        public static void ManualSend()
        {
            while (true)
            {
                Console.WriteLine("**************************************************");
                Console.WriteLine("\nIzaberite opciju 1 ili 2:");
                Console.WriteLine("\t1. Upis nove potrosnje");
                Console.WriteLine("\t2. Citanje rezultata funkcija iz baze");
                string unos = Console.ReadLine();

                if (unos == "1")
                {
                    Console.WriteLine("Unesite potrosnju (u mW/h):");
                    double potrosnja = double.Parse(Console.ReadLine());

                    Console.WriteLine("Unesite grad:");
                    string grad = Console.ReadLine();

                    dp.FromClient(potrosnja, DateTime.Now, grad);
                }
                else if (unos == "2")
                {
                    List<IData> podaci = dp.ToClient();
                    foreach (var item in podaci)
                    {
                        Console.WriteLine("\n\tRezultat funkcije: " + item.Potrosnja);
                        Console.WriteLine("\tVreme proracuna: " + item.VremeProracuna.ToString());
                        Console.WriteLine("\tPoslednje vreme merenja: " + item.PoslednjeVremeMerenja.ToString());
                        Console.WriteLine("\tFunkcija: " + item.Funkcija);
                    }
                }
                else
                {
                    Console.WriteLine("Unos nije adekvatan.");
                }

                
            }
        }

    }
}
