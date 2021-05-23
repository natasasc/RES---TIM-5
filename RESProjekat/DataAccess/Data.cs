using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Data
    {
        public DateTime DateAndTime { get; set; }
        public float Usage { get; set; }
        public DateTime LastDateAndTime { get; set; }

        public Data(float usage, DateTime dateAndTime, DateTime lastDateAndTime)
        {
            Usage = usage;
            DateAndTime = dateAndTime;
            LastDateAndTime = lastDateAndTime;
        }
    }
}
