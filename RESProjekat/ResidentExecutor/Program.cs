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

            Console.Write("Broj sekundi izmedju poziva funkcija: " + seconds.ToString() + "\n");

            double value;
            while (true)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    Console.WriteLine("Poziv funkcije " + list[i].InnerText.ToString());
                    value = Functions.IdAcceptance(int.Parse(list[i].InnerText));
                    if (value != -1)
                    {
                        WriteData(value);
                    }
                    System.Threading.Thread.Sleep(seconds * 1000);
                }
            }

            //Console.ReadLine();
        }

        private static void WriteData(double value)
        {
            string xmlPath = Directory.GetCurrentDirectory() + @"\funcData.xml";
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xmlPath);

            XmlNode potrosnja = xmlDoc.CreateElement("usage");
            potrosnja.InnerText = value.ToString();
            xmlDoc.DocumentElement.AppendChild(potrosnja);
            xmlDoc.Save(xmlPath);
        }
    }
}
