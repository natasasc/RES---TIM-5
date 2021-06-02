using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Database;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Parallel.Invoke
            (
                () => AutomaticSend(),
                () => ManualSend()
            );
        }

        public static void AutomaticSend()
        {
            double potrosnja = 50;

            while (true)
            {
                if (potrosnja > 100)
                    potrosnja = 50;

                if (potrosnja % 2 == 1)
                    DataAccess.DataProcessing.FromClient(potrosnja, DateTime.Now, "Novi Sad");
                else
                    DataAccess.DataProcessing.FromClient(potrosnja, DateTime.Now, "Beograd");

                potrosnja++;

                Thread.Sleep(10000);
            }
        }

        public static void ManualSend()
        {
            while (true)
            {
                Console.WriteLine("Unesite potrosnju:");
                double potrosnja = double.Parse(Console.ReadLine());

                Console.WriteLine("Unesite grad:");
                string grad = Console.ReadLine();

                DataAccess.DataProcessing.FromClient(potrosnja, DateTime.Now, grad);
            }
        }

    }
}
