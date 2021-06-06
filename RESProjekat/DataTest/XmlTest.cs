using Common;
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
        IXml _instance;

        [SetUp]
        public void SetUp()
        {
            _instance = new ResidentExecutor.Program();
        }

        [Test]
        [TestCase(null)]
        public void ReadXMLCheck1(string path)
        {
            Assert.Throws<ArgumentNullException>(
                   () =>
                   {
                       _instance.ReadXML(path);
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
                       _instance.ReadXML(path);
                   });
        }

        [Test]
        [TestCase("nepostojeci")]
        public void ReadXMLCheck2(string path)
        {
            Assert.Throws<FileNotFoundException>(
                   () =>
                   {
                       _instance.ReadXML(path);
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
                       _instance.LoadNodes(xmlDoc, id, time);
                   });
        }

        [Test]
        public void LoadXmlCheck2()
        {
            XmlDocument xmlDoc = _instance.ReadXML(Directory.GetCurrentDirectory() + @"\data.xml");
            string id = "id";
            string time = "vreme";

            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       _instance.LoadNodes(xmlDoc, id, time);
                   });
        }

        [Test]
        public void ValidateNull()
        {
            List<XmlNodeList> lista = null;
            Assert.Throws<ArgumentNullException>(
                   () =>
                   {
                       _instance.Validate(lista);
                   });
        }

        [Test]
        public void ValidateBadCount()
        {
            List<XmlNodeList> lista = new List<XmlNodeList>();

            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       _instance.Validate(lista);
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
                       _instance.Work(lista);
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
                       _instance.Work(lista);
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
                       _instance.Work(lista);
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
                       _instance.WriteData(value);
                   });
        }
    }
}
