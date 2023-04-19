using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PudelkoLibrary;

namespace UnitTests
{
    [TestClass]
    public class UnitTestPoleObjetosc
    {
        // Zaokrąglenie do 4 ponieważ kalkulator z którego brałem wyniki zaokraglał do 4 miejsc po przecinku
        private const int PRECISION = 4;

        [TestMethod]
        [DataRow (2, 3, 4, UnitOfMeasure.meter, 24)]
        [DataRow (5, 6, 7, UnitOfMeasure.meter, 210.0)]
        [DataRow (221.3, 432.4, 154.1, UnitOfMeasure.centimeter, 14.7458)]
        [DataRow (456.8, 612.2, 221.4, UnitOfMeasure.centimeter, 61.9152)]
        [DataRow (3000, 2123, 4567, UnitOfMeasure.milimeter, 29.0872)]
        [DataRow (1200, 5423, 8711, UnitOfMeasure.milimeter, 56.6877)]
        public void ObjetoscReturnCorrectValue(double a, double b, double c, UnitOfMeasure unit, double expected)
        {
            Pudelko p = new(a, b, c, unit);
            double result = p.Objetosc;
            Assert.AreEqual(expected, result, PRECISION);
        }

        [TestMethod]
        [DataRow(2, 3, 4, UnitOfMeasure.meter, 52)]
        [DataRow(5, 6, 7, UnitOfMeasure.meter, 214)]
        [DataRow(221.3, 432.4, 154.1, UnitOfMeasure.centimeter, 39.2851)]
        [DataRow(456.8, 612.2, 221.4, UnitOfMeasure.centimeter, 103.2659)]
        [DataRow(3000, 2123, 4567, UnitOfMeasure.milimeter, 59.5315)]
        [DataRow(1200, 5423, 8711, UnitOfMeasure.milimeter, 128.4011)]
        public void PoleReturnCorrectValue(double a, double b, double c, UnitOfMeasure unit, double expected)
        {
            Pudelko p = new(a, b, c, unit);
            double result = p.Pole;
            Assert.AreEqual(expected, result, PRECISION);
        }

        [TestMethod]
        public void OperatorPlusReturnCorrectValue()
        {
            Pudelko p1 = new(4, 5, 3);
            Pudelko p2 = new(5, 8, 2);

            Pudelko result = p1 + p2;

            Assert.AreEqual(9, result.A);
            Assert.AreEqual(8, result.B);
            Assert.AreEqual(3, result.C);

            Pudelko p3 = new(400.0, 500.0, 300.0, UnitOfMeasure.centimeter);

            result = p2 + p3;

            Assert.AreEqual(9, result.A);
            Assert.AreEqual(8, result.B);
            Assert.AreEqual(3, result.C);

            Pudelko p4 = new(1200, 2000, 3211, UnitOfMeasure.milimeter);
            Pudelko p5 = new(120.0, 200.0, 321.1, UnitOfMeasure.centimeter);

            result = p4 + p5;

            Assert.AreEqual(2.4, result.A);
            Assert.AreEqual(2, result.B);
            Assert.AreEqual(3.211, result.C);
        }

        [TestMethod]
        public void OperatorEqualsReturnCorrectValueForSamePudelka()
        {
            Pudelko p1 = new(21.2, 13.3, 98.1, UnitOfMeasure.centimeter);
            Pudelko p2 = new(0.212, 0.133, 0.981);

            bool result = p1 == p2;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OperatorEqualsReturnFalseForDiffrentPudelka()
        {
            Pudelko p1 = new(21.2, 13.3, 98.1, UnitOfMeasure.centimeter);
            Pudelko p2 = new(2000, 133, 981, UnitOfMeasure.milimeter);

            bool result = p1 == p2;

            Assert.IsFalse(result);
        }

        [TestMethod]
        public void OperatorNotEqualsReturnTrueForDiffrentPudelka()
        {
            Pudelko p1 = new(222.1, 122.4, 589.6, UnitOfMeasure.centimeter);
            Pudelko p2 = new(2021, 1124, 4896, UnitOfMeasure.milimeter);

            bool result = p1 != p2;

            Assert.IsTrue(result);
        }

        [TestMethod]
        public void OperatorNotEqualsReturnFalseForSamePudelka()
        {
            Pudelko p1 = new(2.133, 5.4, 3.121);
            Pudelko p2 = new(213.3, 540.0, 312.1, UnitOfMeasure.centimeter);

            bool result = p1 != p2;

            Assert.IsFalse(result);
        }
    }
}
