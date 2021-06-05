using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ICRUD
    {
        List<IData> ToCalculationFunction();
        void FromCalculationFunction(double result, DateTime time, DateTime lastTime, string funcID);
    }
}
