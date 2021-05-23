using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataProcessing
    {
        //funkcija za proslijedjivanje podataka calculationfunction komponenti
        public List<Data> ToCalculationFunction(DateTime date)
        {
            //upit za odgovarajuci datum i da ima oznaku da je od stane klijenta tj da je poslednje vrijeme proracuna null
            List<Data> listaObjekata = new List<Data>(); //ono sto je vratio upit
            listaObjekata.Sort((x, y) => DateTime.Compare(x.DateAndTime, y.DateAndTime));
            return listaObjekata;
        }

        //funkcija za upis od strane klijenta
        public void FromClient(float usage, DateTime time)
        {
            //upis u bazu podataka, poslednja kolona NULL
        }

        //funkcija za upis od stane calculationfunction
        public void FromCalculationFunction(float result, DateTime time, DateTime lastTime)
        {
            //upis u bazu podataka
        }

        //funkcija za proslijedjivanje vrijednosti klijentima
        public List<Data> ToClient()
        {
            //upis da li je poslednja kolona NULL
            List<Data> lista = new List<Data>();
            return lista;
        }
    }
}
