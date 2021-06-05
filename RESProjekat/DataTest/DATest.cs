using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using Database;
using Moq;

namespace DataTest
{
    [TestFixture]
    public class DATest
    {
        IData _data;
        IData _data2;
        IData _dataResult1;
        IData _dataResult2;

        DateTime datum = new DateTime(2021, 6, 2, 17, 38, 10);
        DateTime? datum1 = null;
        string func = null;
        DateTime datum2 = new DateTime(2021, 6, 3, 11, 15, 25);

        [SetUp]
        public void SetUp()
        {
            // koristimo mock kako bismo izbegli kreiranje objekta
            var moq = new Mock<IData>();
            moq.Setup(o => o.ID).Returns(10);
            moq.Setup(o => o.Potrosnja).Returns(100);
            moq.Setup(o => o.VremeProracuna).Returns(datum);
            moq.Setup(o => o.PoslednjeVremeMerenja).Returns(datum1);
            moq.Setup(o => o.Grad).Returns("Novi Sad");
            moq.Setup(o => o.Funkcija).Returns(func);
            _data = moq.Object;
            var moq2 = new Mock<IData>();
            moq2.Setup(o => o.ID).Returns(11);
            moq2.Setup(o => o.Potrosnja).Returns(100);
            moq2.Setup(o => o.VremeProracuna).Returns(datum2);
            moq2.Setup(o => o.PoslednjeVremeMerenja).Returns(datum);
            moq2.Setup(o => o.Grad).Returns("");
            moq2.Setup(o => o.Funkcija).Returns("CF1");
            _data2 = moq2.Object;
            // mock koristimo kod baza podataka i kod rada s fajlovima u nasem projektu

            var moq3 = new Mock<IData>();
            _dataResult1 = moq3.Object;
            var moq4 = new Mock<IData>();
            _dataResult2 = moq4.Object;
        }

        [Test]
        public void ConvertToTabelaCheck()
        {
            Tabela result1 = DataAccess.DataProcessing.ConvertToTabela(_data);
            Assert.AreEqual(10, result1.ID);
            Assert.AreEqual(100, result1.Potrosnja);
            Assert.AreEqual(datum, result1.VremeProracuna);
            Assert.AreEqual(null, result1.PoslednjeVremeMerenja);
            Assert.AreEqual("Novi Sad", result1.Grad);
            Assert.AreEqual(null, result1.Funkcija);

            Tabela result2 = DataAccess.DataProcessing.ConvertToTabela(_data2);
            Assert.AreEqual(11, result2.ID);
            Assert.AreEqual(100, result2.Potrosnja);
            Assert.AreEqual(datum2, result2.VremeProracuna);
            Assert.AreEqual(datum, result2.PoslednjeVremeMerenja);
            Assert.AreEqual("", result2.Grad);
            Assert.AreEqual("CF1", result2.Funkcija);
        }

        [Test]
        public void ConvertToDataCheck()
        {
            DateTime datum = new DateTime(2021, 6, 2, 17, 38, 10);
            DateTime datum2 = new DateTime(2021, 6, 3, 11, 15, 25);

            Tabela tabela = new Tabela()
            {
                ID = 10,
                Potrosnja = 100,
                VremeProracuna = datum,
                PoslednjeVremeMerenja = null,
                Grad = "Novi Sad",
                Funkcija = null
            };

            Tabela tabela2 = new Tabela()
            {
                ID = 11,
                Potrosnja = 100,
                VremeProracuna = datum2,
                PoslednjeVremeMerenja = datum,
                Grad = "",
                Funkcija = "CF1"
            };


            _dataResult1 = DataAccess.DataProcessing.ConvertToData(tabela);
            Assert.AreEqual(10, _dataResult1.ID);
            Assert.AreEqual(100, _dataResult1.Potrosnja);
            Assert.AreEqual(datum, _dataResult1.VremeProracuna);
            Assert.AreEqual(null, _dataResult1.PoslednjeVremeMerenja);
            Assert.AreEqual("Novi Sad", _dataResult1.Grad);
            Assert.AreEqual(null, _dataResult1.Funkcija);

            _dataResult2 = DataAccess.DataProcessing.ConvertToData(tabela2);
            Assert.AreEqual(11, _dataResult2.ID);
            Assert.AreEqual(100, _dataResult2.Potrosnja);
            Assert.AreEqual(datum2, _dataResult2.VremeProracuna);
            Assert.AreEqual(datum, _dataResult2.PoslednjeVremeMerenja);
            Assert.AreEqual("", _dataResult2.Grad);
            Assert.AreEqual("CF1", _dataResult2.Funkcija);
        }
    }
}
