using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public interface IData
    {
        int ID { get; set; }
        double Potrosnja { get; set; }
        System.DateTime VremeProracuna { get; set; }
        Nullable<System.DateTime> PoslednjeVremeMerenja { get; set; }
        string Grad { get; set; }
        string Funkcija { get; set; }

        bool CheckCity(string grad);
    }
}
