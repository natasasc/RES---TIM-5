using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace DataTest
{
    [TestFixture]
    public class XmlTest
    {
        [Test]
        [TestCase(null)]
        public void ReadXMLCheck1(string path)
        {
            Assert.Throws<ArgumentNullException>(
                   () =>
                   {
                       ResidentExecutor.Program.ReadXML(path);
                   });
        }

        [Test]
        [TestCase("")]
        [TestCase("          ")]
        public void ReadXMLCheck3(string path)
        {
            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       ResidentExecutor.Program.ReadXML(path);
                   });
        }

        [Test]
        [TestCase("nepostojeci")]
        public void ReadXMLCheck2(string path)
        {
            Assert.Throws<FileNotFoundException>(
                   () =>
                   {
                       ResidentExecutor.Program.ReadXML(path);
                   });
        }

        [Test]
        public void LoadXmlCheck1()
        {
            XmlDocument xmlDoc = new XmlDocument();
            string id = "id";
            string time = "time";

            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       ResidentExecutor.Program.LoadNodes(xmlDoc, id, time);
                   });
        }

        [Test]
        public void LoadXmlCheck2()
        {
            XmlDocument xmlDoc = ResidentExecutor.Program.ReadXML(Directory.GetCurrentDirectory() + @"\data.xml");
            string id = "id";
            string time = "vreme";

            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       ResidentExecutor.Program.LoadNodes(xmlDoc, id, time);
                   });
        }

        [Test]
        public void ValidateNull()
        {
            List<XmlNodeList> lista = null;
            Assert.Throws<ArgumentNullException>(
                   () =>
                   {
                       ResidentExecutor.Program.Validate(lista);
                   });
        }

        [Test]
        public void ValidateBadCount()
        {
            List<XmlNodeList> lista = new List<XmlNodeList>();

            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       ResidentExecutor.Program.Validate(lista);
                   });
        }

        [Test]
        public void WorkNull()
        {
            List<string> lista = new List<string>();
            string podatak = null;
            lista.Add(podatak);

            Assert.Throws<ArgumentNullException>(
                   () =>
                   {
                       ResidentExecutor.Program.Work(lista);
                   });
        }

        [Test]
        public void WorkBadId()
        {
            List<string> lista = new List<string>();
            lista.Add("2");
            lista.Add("0");

            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       ResidentExecutor.Program.Work(lista);
                   });
        }

        [Test]
        public void WorkBadTime()
        {
            List<string> lista = new List<string>();
            lista.Add("dva");

            Assert.Throws<FormatException>(
                   () =>
                   {
                       ResidentExecutor.Program.Work(lista);
                   });
        }

        [Test]
        [TestCase(-1)]
        [TestCase(-10)]
        public void WriteDataCheck(double value)
        {
            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       ResidentExecutor.Program.WriteData(value);
                   });
        }
    }
}
