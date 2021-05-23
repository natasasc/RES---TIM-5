using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAO;
using DataAccess.DAO.Impl;

namespace DataAccess
{
    public class DataProcessing
    {
        //*********************************
        private static readonly IDataDAO dataDAO = new DataDAOImpl();


        //funkcija za proslijedjivanje podataka CalculationfFunction komponenti
        public static List<Data> ToCalculationFunction(DateTime date)
        {
            List<Data> listaObjekata = dataDAO.FindAll().ToList();
            List<Data> novaLista = new List<Data>();

            listaObjekata.Sort((x, y) => DateTime.Compare(x.DateAndTime, y.DateAndTime));

            string[] datumIVreme;

            string datum = "1.1.0001 00.00.00";     //defaultni DateTime
            string[] danasnjiDatum;

            foreach(Data d in listaObjekata)
            {
                datumIVreme = d.DateAndTime.ToString().Split(' ');
                danasnjiDatum = DateTime.Now.ToString().Split(' ');

                if(d.LastDateAndTime.ToString() != datum && datumIVreme[0] != danasnjiDatum[0])
                {
                    novaLista.Add(d);
                }
            }
            return novaLista;
        }

        //funkcija za upis podataka u bazu podataka koje je poslao Client
        public static void FromClient(float usage, DateTime time)
        {
            Data noviPodatak = new Data(usage, time, new DateTime());
            dataDAO.Save(noviPodatak);
        }

        //funkcija za upis podataka u bazu podataka koje je prolijedio CalculationFunction
        public static void FromCalculationFunction(float result, DateTime time, DateTime lastTime)
        {
            Data noviPodatak = new Data(result, time, lastTime);
            dataDAO.Save(noviPodatak);
        }

        //funkcija za proslijedjivanje vrijednosti Client komponenti
        public static List<Data> ToClient()
        {
            //upis da li je poslednja kolona NULL
            List<Data> listaObjekata = dataDAO.FindAll().ToList();
            List<Data> novaLista = new List<Data>();

            foreach(Data d in listaObjekata)
            {
                if(d.LastDateAndTime.ToString() != "1.1.0001 00.00.00")
                {
                    novaLista.Add(d);
                }
            }

            //proslijediti klijentu u funkciji
            return listaObjekata;
        }
    }
}
