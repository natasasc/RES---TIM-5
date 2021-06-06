using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using DataAccess;

namespace CalculationFunction
{
    public class Functions : IExecute
    {
        private readonly ICRUD dp = new DataAccess.DataProcessing();
        public double IdAcceptance(int id)
        {
            string funcID = null;
            if (id != 1 && id != 2 && id != 3)
                throw new ArgumentException("Id funkcije moze imati vrednosti 1, 2 ili 3.");

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

            return Load(funcID, id);
        }

        [ExcludeFromCodeCoverage]
        public double Load(string funcID, int id)
        {
            List<IData> data = dp.ToCalculationFunction();

            List<IData> clientData = new List<IData>();
            List<IData> functionsData = new List<IData>();

            foreach (var item in data)
            {
                if (item.Funkcija == null)
                    clientData.Add(item);
            }

            foreach (var item in data)
            {
                if (item.Funkcija != null && item.Funkcija == funcID)
                    functionsData.Add(item);
            }

            return CallCF(functionsData, clientData, id);
        }

        public double CallCF(List<IData> functionsData, List<IData> clientData, int id)
        {
            if (functionsData == null || clientData == null)
                throw new ArgumentNullException("Liste ne mogu biti null.");

            if (clientData.Count == 0)
                throw new ArgumentException("Nema podataka koje je klijent upisao");

            clientData = clientData.OrderBy(o => o.VremeProracuna).ToList();

            DateTime poslednjiUpisKlijenta = clientData[clientData.Count - 1].VremeProracuna;

            functionsData = functionsData.OrderBy(o => o.PoslednjeVremeMerenja).ToList();

            DateTime? poslednjeMerenje = null;
            if (functionsData.Count > 0)
                poslednjeMerenje = functionsData[functionsData.Count - 1].PoslednjeVremeMerenja;

            if (poslednjiUpisKlijenta == poslednjeMerenje)
                return -1;

            List<double> usages = new List<double>();

            foreach (IData i in clientData)
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

        public double CalculationFunction1(List<double> usages, DateTime poslednjeVrijemeMjerenja)
        {
            if (usages == null)
                throw new ArgumentNullException("CF1 ne moze racunati ako nema podataka.");

            double average = 0;
            average = usages.Average();

            //funkcija koja ce upisati podatke u bazu podataka
            dp.FromCalculationFunction(average, DateTime.Now, poslednjeVrijemeMjerenja, "CF1");

            return average;
        }

        public double CalculationFunction2(List<double> usages, DateTime poslednjeVrijemeMjerenja)
        {
            if (usages == null)
                throw new ArgumentNullException("CF2 ne moze racunati ako nema podataka.");

            double min = 0;
            min = usages.Min();

            //funkcija koja ce upisati podatke u bazu podataka
            dp.FromCalculationFunction(min, DateTime.Now, poslednjeVrijemeMjerenja, "CF2");

            return min;
        }

        public double CalculationFunction3(List<double> usages, DateTime poslednjeVrijemeMjerenja)
        {
            if (usages == null)
                throw new ArgumentNullException("CF3 ne moze racunati ako nema podataka.");

            double max = 0;
            max = usages.Max();

            //funkcija koja ce upisati podatke u bazu podataka
            dp.FromCalculationFunction(max, DateTime.Now, poslednjeVrijemeMjerenja, "CF3");

            return max;
        }
    }
}
