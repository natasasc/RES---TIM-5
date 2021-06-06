using Common;
using Database;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class DataProcessing : ICRUD, ICommunication
    {
        private static sqlDBEntities context = new sqlDBEntities();

        [ExcludeFromCodeCoverage]
        public List<IData> ToCalculationFunction()
        {
            List<Tabela> listaT = context.Tabela.ToList();
            List<IData> listaD = new List<IData>();

            listaT.OrderBy(o => o.VremeProracuna).ToList();

            foreach (var item in listaT)
            {
                listaD.Add(ConvertToData(item));
            }

            return listaD;
        }

        [ExcludeFromCodeCoverage]
        public void FromCalculationFunction(double result, DateTime time, DateTime lastTime, string funcID)
        {
            int max = GetMaxID();

            IData podatak = new Data(++max, result, time, lastTime, "", funcID);
            Tabela noviPodatak = ConvertToTabela(podatak);

            context.Tabela.Add(noviPodatak);
            context.SaveChanges();
        }

        [ExcludeFromCodeCoverage]
        public void FromClient(double usage, DateTime time, string city)
        {
            int max = GetMaxID();

            IData podatak = new Data(++max, usage, time, null, city, null);
            Tabela noviPodatak = ConvertToTabela(podatak);

            context.Tabela.Add(noviPodatak);
            context.SaveChanges();
        }

        [ExcludeFromCodeCoverage]
        public List<IData> ToClient()
        {
            List<Tabela> listaObjekata = context.Tabela.ToList();
            List<IData> novaLista = new List<IData>();

            foreach (Tabela d in listaObjekata)
            {
                if (d.Funkcija != null)
                {
                    novaLista.Add(ConvertToData(d));
                }
            }

            return novaLista;
        }


        [ExcludeFromCodeCoverage]
        public static int GetMaxID()
        {
            object lockObj = new object();
            int max = 0;

            lock (lockObj)
            {
                List<Tabela> lista = new List<Tabela>();
                lista = context.Tabela.ToList();
                max = lista[0].ID;
                foreach (Tabela t in lista)
                {
                    if (t.ID > max)
                        max = t.ID;
                }
            }

            return max;
        }

        public static Tabela ConvertToTabela(IData obj)
        {
            Tabela ret = new Tabela()
            { 
                ID = obj.ID,
                Potrosnja = obj.Potrosnja,
                VremeProracuna = obj.VremeProracuna,
                PoslednjeVremeMerenja = obj.PoslednjeVremeMerenja,
                Grad = obj.Grad,
                Funkcija = obj.Funkcija
            };

            return ret;
        }

        public static IData ConvertToData(Tabela obj)
        {
            IData ret = new Data(obj.ID, obj.Potrosnja, obj.VremeProracuna, obj.PoslednjeVremeMerenja, obj.Grad, obj.Funkcija);
            
            return ret;
        }
    }
}
