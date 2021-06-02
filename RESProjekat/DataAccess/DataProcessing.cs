using Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataProcessing
    {
        private static sqlDBEntities context = new sqlDBEntities();

        //funkcija za proslijedjivanje podataka CalculationfFunction komponenti
        public static List<Tabela> ToCalculationFunction()
        {
            List<Tabela> listaObjekata = context.Tabela.ToList();

            listaObjekata.Sort((x, y) => DateTime.Compare(x.VremeProracuna, y.VremeProracuna));

            return listaObjekata;
        }

        //funkcija za upis podataka u bazu podataka koje je poslao Client
        public static void FromClient(float usage, DateTime time, string city)
        {
            int max = GetMaxID();

            Tabela noviPodatak = new Tabela()
            {
                ID = ++max,
                Potrosnja = usage,
                VremeProracuna = time,
                PoslednjeVremeMerenja = null,
                Grad = city,
                Funkcija = null
            };

            context.Tabela.Add(noviPodatak);
            context.SaveChanges();
        }

        //funkcija za upis podataka u bazu podataka koje je prolijedio CalculationFunction
        public static void FromCalculationFunction(double result, DateTime time, DateTime lastTime, string funcID)
        {
            int max = GetMaxID();

            Tabela noviPodatak = new Tabela()
            {
                ID = ++max,
                Potrosnja = result,
                VremeProracuna = time,
                PoslednjeVremeMerenja = lastTime,
                Grad = null,
                Funkcija = funcID
            };

            context.Tabela.Add(noviPodatak);
            context.SaveChanges();
        }

        //funkcija za proslijedjivanje vrijednosti Client komponenti
        public static List<Tabela> ToClient()
        {
            List<Tabela> listaObjekata = context.Tabela.ToList();
            List<Tabela> novaLista = new List<Tabela>();

            foreach(Tabela d in listaObjekata)
            {
                if(d.Funkcija != null)
                {
                    novaLista.Add(d);
                }
            }

            return novaLista;
        }


        private static int GetMaxID()
        {
            int max = 0;

            List<Tabela> lista = new List<Tabela>();
            lista = context.Tabela.ToList();
            max = lista[0].ID;
            foreach (Tabela t in lista)
            {
                if (t.ID > max)
                    max = t.ID;
            }

            return max;
        }
    }
}
