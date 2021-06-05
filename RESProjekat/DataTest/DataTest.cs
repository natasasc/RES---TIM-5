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
    public class DataTest
    {
        [Test]
        public void DataPrazanKonstruktor()
        {
            DateTime datum = new DateTime(2020, 10, 10, 10, 10, 10, 10);

            Common.IData data = new Common.Data();
            Assert.AreEqual(data.ID, 0);
            Assert.AreEqual(data.Potrosnja, 0);
            Assert.AreEqual(data.VremeProracuna, datum);
            Assert.AreEqual(data.PoslednjeVremeMerenja, null);
            Assert.AreEqual(data.Grad, null);
            Assert.AreEqual(data.Funkcija, null);
        }

        [Test]
        [TestCase(10, 100, "2021/6/2/17/38/10", null, "Novi Sad", null)]
        [TestCase(11, 100, "2021/6/3/11/15/25", "2021/6/2/17/38/10", "", "CF1")]
        [TestCase(12, 100, "2021/6/3/11/15/25", "2021/6/2/17/38/10", "", "CF2")]
        [TestCase(13, 100, "2021/6/3/11/15/25", "2021/6/2/17/38/10", "", "CF3")]
        [TestCase(14, 200, "2021/6/3/17/53/50", null, "Beograd", null)]
        public void DataKonstruktorDobriParametri(int id, double potrosnja, string vremeProracuna, 
            string poslednjeVremeMerenja, string grad, string funkcija)
        {
            Common.Data data;

            DateTime date = GetDate(vremeProracuna);
            DateTime? lastDate = GetLastDate(poslednjeVremeMerenja);

            data = new Common.Data(id, potrosnja, date, lastDate, grad, funkcija);
            Assert.AreEqual(data.ID, id);
            Assert.AreEqual(data.Potrosnja, potrosnja);
            Assert.AreEqual(data.VremeProracuna, date);
            Assert.AreEqual(data.PoslednjeVremeMerenja, lastDate);
            Assert.AreEqual(data.Grad, grad);
            Assert.AreEqual(data.Funkcija, funkcija);
        }

        [Test]
        [TestCase(0, 100, "2021/6/2/17/38/10", null, "Novi Sad", null)]                     // id
        [TestCase(10, -1, "2021/6/2/17/38/10", null, "Novi Sad", null)]                      // potrosnja
        [TestCase(11, 100, "2021/6/3/11/15/25", "2021/6/2/17/38/10", "", "Function1")]      // funkcija 
        [TestCase(12, 200, "2021/6/3/17/53/50", null, "NS 123", null)]                      // grad
        //[ExpectedException(typeof(ArgumentException))]
        public void DataKonstruktorLosiParametri(int id, double potrosnja, string vremeProracuna,
            string poslednjeVremeMerenja, string grad, string funkcija)
        {
            DateTime date = GetDate(vremeProracuna);
            DateTime? lastDate = GetLastDate(poslednjeVremeMerenja);

            Assert.Throws<ArgumentException>(
                () =>
                {
                    Common.Data data = new Common.Data(id, potrosnja, date, lastDate, grad, funkcija);
                });
        }

        [Test]
        [TestCase(10, 100, "2021/6/2/17/38/10", "2021/6/2/17/38/10", "Novi Sad", "CF1")]
        [TestCase(11, 100, "2021/6/3/11/15/25", "2021/6/2/17/38/10", "Beograd", null)]
        [TestCase(12, 100, "2021/6/3/11/15/25", null, "Novi Sad", "CF2")]
        [TestCase(13, 100, "2021/6/3/11/15/25", "2021/6/2/17/38/10", "", null)]
        [TestCase(14, 200, "2021/6/3/17/53/50", null, "", "CF3")]
        [TestCase(15, 200, "2021/6/3/17/53/50", null, "", null)]
        //[ExpectedException(typeof(ArgumentException))]
        public void DataKonstruktorLosaKombinacijaParametara(int id, double potrosnja, string vremeProracuna,
            string poslednjeVremeMerenja, string grad, string funkcija)
        {
            DateTime date = GetDate(vremeProracuna);
            DateTime? lastDate = GetLastDate(poslednjeVremeMerenja);

            Assert.Throws<ArgumentException>(
                () =>
                {
                    Common.Data data = new Common.Data(id, potrosnja, date, lastDate, grad, funkcija);
                });
        }

        [Ignore("Nije test.")]
        public DateTime GetDate(string date)
        {
            string[] niz = date.Split('/');
            DateTime ret = new DateTime(Int32.Parse(niz[0]), Int32.Parse(niz[1]), Int32.Parse(niz[2]),
                Int32.Parse(niz[3]), Int32.Parse(niz[4]), Int32.Parse(niz[5]));

            return ret;
        }

        [Ignore("Nije test.")]
        public DateTime? GetLastDate(string date)
        {
            if (date == null)
                return null;
            else
            {
                string[] niz = date.Split('/');
                DateTime ret = new DateTime(Int32.Parse(niz[0]), Int32.Parse(niz[1]), Int32.Parse(niz[2]),
                    Int32.Parse(niz[3]), Int32.Parse(niz[4]), Int32.Parse(niz[5]));

                return ret;
            }
        }

    }
}
