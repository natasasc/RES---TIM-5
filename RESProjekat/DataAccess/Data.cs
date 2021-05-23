using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Data
    {
        public DateTime DateAndTime { get; set; }
        public float Usage { get; set; }

        public Data(DateTime dateAndTime, float usage)
        {
            DateAndTime = dateAndTime;
            Usage = usage;
        }
    }
}
