using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;
using CalculationFunction;

namespace ResidentExecutor
{
    class Program
    {
        static void Main(string[] args)
        {
            XmlDocument xmlDoc = new XmlDocument();
            string xmlPath = Directory.GetCurrentDirectory() + @"\data.xml";
            xmlDoc.Load(xmlPath);

            XmlNodeList list = xmlDoc.GetElementsByTagName("id");
            XmlNodeList time = xmlDoc.GetElementsByTagName("time");
            int seconds = int.Parse(time[0].InnerText);

            Console.Write("Broj sekundi izmedju poziva funkcija: " + seconds.ToString());

            double value;
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine("Poziv funkcije " + list[i].InnerText.ToString());
                value = Functions.IdAcceptance(int.Parse(list[i].InnerText));
                if (value != -1)
                {
                    // upis u xml
                }
                System.Threading.Thread.Sleep(seconds * 1000);
            }

            Console.ReadLine();
        }
    }
}
