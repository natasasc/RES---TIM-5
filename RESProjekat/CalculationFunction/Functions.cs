using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculationFunction
{
    public class Functions
    {
        public void IdAcceptance(int id)
        {

            //List<Data> data = funkcija koja ce biti u data access(DateTime.Now)
            DateTime poslednjeVrijemeMjerenja = DateTime.Now; //data[data.length-1]
            List<float> usages = new List<float>();
            //foreach(float in data.usage)
            //{
            //    usages.Add(i);
            //}

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

        private void CalculationFunction1(List<float> usages, DateTime poslednjeVrijemeMjerenja)
        {

            float average = 0;
            average = usages.Average();
            //funkcija koja ce upisivati u bazu podataka (average, DateTime.Now, poslednjeVrijemeMjerenja)
            //funkcija za proslijedjivanje resident executoru
        }

        private void CalculationFunction2(List<float> usages, DateTime poslednjeVrijemeMjerenja)
        {
            float min = 0;
            min = usages.Min();
            //funkcija koja ce upisivati u bazu podataka (min, DateTime.Now, poslednjeVrijemeMjerenja)
            //funkcija za proslijedjivanje resident executoru
        }

        private void CalculationFunction3(List<float> usages, DateTime poslednjeVrijemeMjerenja)
        {
            float max = 0;
            max = usages.Max();
            //funkcija koja ce upisivati u bazu podataka (max, DateTime.Now, poslednjeVrijemeMjerenja)
            //funkcija za proslijedjivanje resident executoru
        }
    }
}
