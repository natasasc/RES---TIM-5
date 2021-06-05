using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface ICommunication
    {
        void FromClient(double usage, DateTime time, string city);
        List<IData> ToClient();
    }
}
