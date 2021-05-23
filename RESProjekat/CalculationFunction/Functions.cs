using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;

namespace CalculationFunction
{
    public class Functions
    {
        public static void IdAcceptance(int id)
        {
            
            List<Data> data = DataAccess.DataProcessing.ToCalculationFunction(DateTime.Now);
            DateTime poslednjeVrijemeMjerenja = data[data.Count() - 1].DateAndTime;
            List<float> usages = new List<float>();

            foreach (Data i in data)
            {
                usages.Add(i.Usage);
            }

            switch (id)
            {
                case 1:
                    CalculationFunction1(usages, poslednjeVrijemeMjerenja);
                    break;
                case 2:
                    CalculationFunction2(usages, poslednjeVrijemeMjerenja);
                    break;
                case 3:
                    CalculationFunction3(usages, poslednjeVrijemeMjerenja);
                    break;
            }
        }

        private static void CalculationFunction1(List<float> usages, DateTime poslednjeVrijemeMjerenja)
        {
            float average = 0;
            average = usages.Average();

            //funkcija koja ce upisati podatke u bazu podataka
            DataAccess.DataProcessing.FromCalculationFunction(average, DateTime.Now, poslednjeVrijemeMjerenja);

            //funkcija za proslijedjivanje resident executoru
        }

        private static void CalculationFunction2(List<float> usages, DateTime poslednjeVrijemeMjerenja)
        {
            float min = 0;
            min = usages.Min();

            //funkcija koja ce upisati podatke u bazu podataka
            DataAccess.DataProcessing.FromCalculationFunction(min, DateTime.Now, poslednjeVrijemeMjerenja);


            //funkcija za proslijedjivanje resident executoru
        }

        private static void CalculationFunction3(List<float> usages, DateTime poslednjeVrijemeMjerenja)
        {
            float max = 0;
            max = usages.Max();

            //funkcija koja ce upisati podatke u bazu podataka
            DataAccess.DataProcessing.FromCalculationFunction(max, DateTime.Now, poslednjeVrijemeMjerenja);


            //funkcija za proslijedjivanje resident executoru
        }
    }
}
