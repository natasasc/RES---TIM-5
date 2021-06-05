using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IExecute
    {
        double IdAcceptance(int id);
        double CallCF(List<IData> functionsData, List<IData> clientData, int id);
        double CalculationFunction1(List<double> usages, DateTime poslednjeVrijemeMjerenja);
        double CalculationFunction2(List<double> usages, DateTime poslednjeVrijemeMjerenja);
        double CalculationFunction3(List<double> usages, DateTime poslednjeVrijemeMjerenja);
    }
}
