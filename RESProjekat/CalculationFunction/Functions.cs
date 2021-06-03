using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Database;

namespace CalculationFunction
{
    public class Functions
    {
        public static double IdAcceptance(int id)
        {
            
            List<Tabela> data = DataProcessing.ToCalculationFunction();

            List<Tabela> clientData = new List<Tabela>();
            List<Tabela> functionsData = new List<Tabela>();

            foreach (var item in data)
            {
                if (item.Funkcija == null)
                    clientData.Add(item);
            }

            DateTime poslednjiUpisKlijenta = clientData[clientData.Count - 1].VremeProracuna;

            string funcID = null;
            switch (id)
            {
                case 1: 
                    funcID = "CF1";
                    break;
                case 2:
                    funcID = "CF2";
                    break;
                case 3:
                    funcID = "CF3";
                    break;
            }

            foreach (var item in data)
            {
                if (item.Funkcija != null && item.Funkcija == funcID)
                    functionsData.Add(item);
            }

            functionsData = functionsData.OrderBy(o => o.PoslednjeVremeMerenja).ToList();

            DateTime? poslednjeMerenje = null;
            if (functionsData.Count > 0)
                poslednjeMerenje = functionsData[functionsData.Count - 1].PoslednjeVremeMerenja;

            if (poslednjiUpisKlijenta == poslednjeMerenje)
                return -1;


            List<double> usages = new List<double>();

            foreach (Tabela i in clientData)
            {
                usages.Add(i.Potrosnja);
            }

            double ret = -1;

            switch (id)
            {
                case 1:
                    ret = CalculationFunction1(usages, poslednjiUpisKlijenta);
                    break;
                case 2:
                    ret = CalculationFunction2(usages, poslednjiUpisKlijenta);
                    break;
                case 3:
                    ret = CalculationFunction3(usages, poslednjiUpisKlijenta);
                    break;
            }

            return ret;
        }

        private static double CalculationFunction1(List<double> usages, DateTime poslednjeVrijemeMjerenja)
        {
            double average = 0;
            average = usages.Average();

            //funkcija koja ce upisati podatke u bazu podataka
            DataProcessing.FromCalculationFunction(average, DateTime.Now, poslednjeVrijemeMjerenja, "CF1");

            return average;
        }

        private static double CalculationFunction2(List<double> usages, DateTime poslednjeVrijemeMjerenja)
        {
            double min = 0;
            min = usages.Min();

            //funkcija koja ce upisati podatke u bazu podataka
            DataProcessing.FromCalculationFunction(min, DateTime.Now, poslednjeVrijemeMjerenja, "CF2");

            return min;
        }

        private static double CalculationFunction3(List<double> usages, DateTime poslednjeVrijemeMjerenja)
        {
            double max = 0;
            max = usages.Max();

            //funkcija koja ce upisati podatke u bazu podataka
            DataProcessing.FromCalculationFunction(max, DateTime.Now, poslednjeVrijemeMjerenja, "CF3");

            return max;
        }
    }
}
