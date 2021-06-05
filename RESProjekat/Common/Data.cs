using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Common
{
    public class Data : IData
    {
        public int ID { get; set; }
        public double Potrosnja { get; set; }
        public DateTime VremeProracuna { get; set; }
        public DateTime? PoslednjeVremeMerenja { get; set; }
        public string Grad { get; set; }
        public string Funkcija { get; set; }

        public Data()
        {
            ID = 0;
            Potrosnja = 0;
            VremeProracuna = new DateTime(2020, 10, 10, 10, 10, 10, 10);
            PoslednjeVremeMerenja = null;
            Grad = null;
            Funkcija = null;
        }

        public Data(int id, double potrosnja, DateTime vremeProracuna, DateTime? poslednjeVremeMerenja, string grad, string funkcija)
        {
            if (id < 1)
            {
                throw new ArgumentException("ID mora biti pozitivan ceo broj.");
            }
            else if (potrosnja < 0)
            {
                throw new ArgumentException("Potrosnja ne moze biti negativna.");
            }
            else if (!CheckCity(grad) && grad != "")
            {
                throw new ArgumentException("Grad moze da sadrzi samo slova i razmake.");
            }
            else if (funkcija != "CF1" && funkcija != "CF2" && funkcija != "CF3" && funkcija != null)
            {
                throw new ArgumentException("Funkcija moze imati vrednosti: CF1, CF2, CF3 ili null.");
            }
            else if (!(poslednjeVremeMerenja != null && grad == "" && funkcija != null)
                && !(poslednjeVremeMerenja == null && grad != "" && funkcija == null))
            {
                throw new ArgumentException("Poslednje vreme merenja i funkcija postoje samo za izvrsavanje funkcija, " +
                    "dok grad postoji samo za klijenta.");
            }
            else 
            {
                ID = id;
                Potrosnja = potrosnja;
                VremeProracuna = vremeProracuna;
                PoslednjeVremeMerenja = poslednjeVremeMerenja;
                Grad = grad;
                Funkcija = funkcija;
            }
        }

        public bool CheckCity(string grad)
        {
            Regex reg = new Regex("^[A-Za-z ]+$");
            if (reg.IsMatch(grad))
                return true;

            return false;
        }
    }
}
