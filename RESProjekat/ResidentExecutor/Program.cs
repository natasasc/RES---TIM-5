using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using CalculationFunction;
using Common;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace ResidentExecutor
{
    public class Program : IXml
    {
        private static readonly IExecute f = new CalculationFunction.Functions();
        private static IXml instance = new Program();

        [ExcludeFromCodeCoverage]
        static void Main(string[] args)
        {
            string xmlPath = Directory.GetCurrentDirectory() + @"\data.xml";
            XmlDocument xmlDoc = instance.ReadXML(xmlPath);
            string id = "id";
            string time = "time";
            List<XmlNodeList> lista = instance.LoadNodes(xmlDoc, id, time);

            List<string> podaci = instance.Validate(lista);
            instance.Work(podaci);

            //Console.ReadLine();
        }

        public XmlDocument ReadXML(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException("Naziv fajla ne moze biti null.");
            }
            if (path.Trim() == "")
            {
                throw new ArgumentException("Naziv fajla ne moze biti prazan.");
            }

            List<XmlNodeList> ret = new List<XmlNodeList>();
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(path);

            return xmlDoc;
        }

        public List<XmlNodeList> LoadNodes(XmlDocument xmlDoc, string id, string time)
        {
            List<XmlNodeList> ret = new List<XmlNodeList>();

            XmlNodeList list = xmlDoc.GetElementsByTagName(id);
            if (list.Count == 0)
                throw new ArgumentException("Nije pronadjen element sa unetim nazivom.");

            XmlNodeList list2 = xmlDoc.GetElementsByTagName(time);
            if (list2.Count == 0)
                throw new ArgumentException("Nije pronadjen element sa unetim nazivom.");

            ret.Add(list2);
            ret.Add(list);

            return ret;
        }

        public List<string> Validate(List<XmlNodeList> lista)
        {
            if (lista == null)
                throw new ArgumentNullException("Lista ne moze biti null.");
            if (lista.Count != 2)
                throw new ArgumentException("Lista mora da sadrzi 2 elementa.");

            List<string> podaci = new List<string>();
            podaci.Add(lista[0][0].InnerText.ToString());
            for (int i = 0; i < lista[1].Count; i++)
            {
                podaci.Add(lista[1][i].InnerText.ToString());
            }

            return podaci;
        }

        public void Work(List<string> podaci)
        {
            foreach (var item in podaci)
            {
                if (item == null)
                    throw new ArgumentNullException("Podaci ne smeju biti null.");
            }

            for (int i = 1; i < podaci.Count; i++)
            {
                if (podaci[i] != "1" && podaci[i] != "2" && podaci[i] != "3")
                    throw new ArgumentException("Identifikacije funkcija mogu biti 1, 2 ili 3");
            }

            Regex reg = new Regex("^[0-9]+$");
            if (!reg.IsMatch(podaci[0]))
                throw new FormatException("Vreme nije u dobrom formatu.");

            int seconds = int.Parse(podaci[0]);

            Console.Write("Broj sekundi izmedju poziva funkcija: " + seconds.ToString() + "\n");

            double value;
            while (true)
            {
                for (int i = 1; i < podaci.Count; i++)
                {
                    Console.WriteLine("Poziv funkcije " + podaci[i]);
                    value = f.IdAcceptance(int.Parse(podaci[i]));
                    if (value != -1)
                    {
                        instance.WriteData(value);
                    }
                    System.Threading.Thread.Sleep(seconds * 1000);
                }
            }
        }

        public void WriteData(double value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Vrednost ne sme biti manja od 0.");
            }

            string xmlPath = Directory.GetCurrentDirectory() + @"\funcData.xml";
            XmlDocument xmlDoc = instance.ReadXML(xmlPath);

            XmlNode potrosnja = xmlDoc.CreateElement("usage");
            potrosnja.InnerText = value.ToString();
            xmlDoc.DocumentElement.AppendChild(potrosnja);
            xmlDoc.Save(xmlPath);
        }
    }
}
