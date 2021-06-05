using Common;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTest
{
    [TestFixture]
    public class CFTest
    {
        IExecute _exe;
        IData _data1;
        IData _data2;
        IData _data3;
        IData _data4;
        IData _data5;

        DateTime? date = null;
        DateTime datum1 = new DateTime(2021, 5, 24, 18, 55, 22, 0);
        DateTime datum2 = new DateTime(2021, 6, 3, 14, 23, 28, 510);
        DateTime datum3 = new DateTime(2021, 6, 3, 14, 23, 31, 3);
        DateTime datum4 = new DateTime(2021, 6, 3, 14, 23, 33, 40);
        DateTime datum5 = new DateTime(2021, 6, 3, 14, 27, 40, 530);
        string func = null;

        [SetUp]
        public void SetUp()
        {
            _exe = new CalculationFunction.Functions();

            var moq1 = new Mock<IData>();
            moq1.Setup(o => o.ID).Returns(1);
            moq1.Setup(o => o.Potrosnja).Returns(100);
            moq1.Setup(o => o.VremeProracuna).Returns(datum1);
            moq1.Setup(o => o.PoslednjeVremeMerenja).Returns(date);
            moq1.Setup(o => o.Grad).Returns("Novi Sad");
            moq1.Setup(o => o.Funkcija).Returns(func);
            _data1 = moq1.Object;

            var moq2 = new Mock<IData>();
            moq2.Setup(o => o.ID).Returns(2);
            moq2.Setup(o => o.Potrosnja).Returns(100);
            moq2.Setup(o => o.VremeProracuna).Returns(datum2);
            moq2.Setup(o => o.PoslednjeVremeMerenja).Returns(datum1);
            moq2.Setup(o => o.Grad).Returns("");
            moq2.Setup(o => o.Funkcija).Returns("CF1");
            _data2 = moq2.Object;

            var moq3 = new Mock<IData>();
            moq3.Setup(o => o.ID).Returns(3);
            moq3.Setup(o => o.Potrosnja).Returns(100);
            moq3.Setup(o => o.VremeProracuna).Returns(datum3);
            moq3.Setup(o => o.PoslednjeVremeMerenja).Returns(datum1);
            moq3.Setup(o => o.Grad).Returns("");
            moq3.Setup(o => o.Funkcija).Returns("CF2");
            _data3 = moq3.Object;

            var moq4 = new Mock<IData>();
            moq4.Setup(o => o.ID).Returns(4);
            moq4.Setup(o => o.Potrosnja).Returns(100);
            moq4.Setup(o => o.VremeProracuna).Returns(datum4);
            moq4.Setup(o => o.PoslednjeVremeMerenja).Returns(datum1);
            moq4.Setup(o => o.Grad).Returns("");
            moq4.Setup(o => o.Funkcija).Returns("CF3");
            _data4 = moq4.Object;

            var moq5 = new Mock<IData>();
            moq5.Setup(o => o.ID).Returns(5);
            moq5.Setup(o => o.Potrosnja).Returns(50);
            moq5.Setup(o => o.VremeProracuna).Returns(datum5);
            moq5.Setup(o => o.PoslednjeVremeMerenja).Returns(date);
            moq5.Setup(o => o.Grad).Returns("Beograd");
            moq5.Setup(o => o.Funkcija).Returns(func);
            _data5 = moq5.Object;
        }

        [Test]
        public void CF1Check()
        {
            List<double> usages = new List<double>();
            usages.Add(25);
            usages.Add(50);
            usages.Add(75);
            DateTime poslednjeVrijemeMjerenja = new DateTime(2021, 5, 24, 18, 55, 22, 0);

            Assert.AreEqual(50, _exe.CalculationFunction1(usages, poslednjeVrijemeMjerenja));
        }

        [Test]
        public void CF2Check()
        {
            List<double> usages = new List<double>();
            usages.Add(25);
            usages.Add(50);
            usages.Add(75);
            DateTime poslednjeVrijemeMjerenja = new DateTime(2021, 5, 24, 18, 55, 22, 0);

            Assert.AreEqual(25, _exe.CalculationFunction2(usages, poslednjeVrijemeMjerenja));
        }

        [Test]
        public void CF3Check()
        {
            List<double> usages = new List<double>();
            usages.Add(25);
            usages.Add(50);
            usages.Add(75);
            DateTime poslednjeVrijemeMjerenja = new DateTime(2021, 5, 24, 18, 55, 22, 0);

            Assert.AreEqual(75, _exe.CalculationFunction3(usages, poslednjeVrijemeMjerenja));
        }

        [Test]
        [TestCase(0)]
        [TestCase(4)]
        public void IdAcceptanceCheck(int id)
        {
            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       _exe.IdAcceptance(id);
                   });
        }

        [Test]
        public void CallCFCheck()
        {
            List<IData> clientData = new List<IData>();
            List<IData> functionsData = new List<IData>();
            
            clientData.Add(_data1);
            functionsData.Add(_data2);
            functionsData.Add(_data3);
            functionsData.Add(_data4);
            clientData.Add(_data5);

            Assert.AreEqual(75, _exe.CallCF(functionsData, clientData, 1));
            Assert.AreEqual(50, _exe.CallCF(functionsData, clientData, 2));
            Assert.AreEqual(100, _exe.CallCF(functionsData, clientData, 3));
        }

        [Test]
        public void CallCFCheckNull()
        {
            List<IData> clientData = null;
            List<IData> functionsData = null;

            Assert.Throws<ArgumentNullException>(
                   () =>
                   {
                       _exe.CallCF(functionsData, clientData, 1);
                   });
        }

        [Test]
        public void CallCFCheckEmpty()
        {
            List<IData> clientData = new List<IData>();
            List<IData> functionsData = new List<IData>();

            Assert.Throws<ArgumentException>(
                   () =>
                   {
                       _exe.CallCF(functionsData, clientData, 1);
                   });
        }
    }
}
