using NUnit.Framework;
using System;
using ExelApp;
using System.Collections.Generic;
using NUnit.Framework.Constraints;
using System.Text.RegularExpressions;

namespace TestNUnit
{
    [TestFixture]
    public class GridTests
    {
        private Grid grid;
        private Cell B1;
        private Cell A0;
        private Cell C0;


        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            grid = new Grid();
        }
        [SetUp]
        public void SetUp()
        {
            B1 = new Cell("B1", 1, 1);
            A0 = new Cell("A0", 0, 0);
            C0 = new Cell("C0", 0, 2);
        }

        [Test]
        public void SetGrid_InitializesGridWithCorrectSize()
        {
            // Arrange
            int expectedRowCount = 20;
            int expectedColCount = 20;

            // Act
            grid.SetGrid(expectedRowCount, expectedColCount);

            // Assert
            Assert.AreEqual(expectedRowCount, grid.RowCount);
            Assert.AreEqual(expectedColCount, grid.ColCount);
        }

        [Test]
        public void NewGrid_ShouldHaveCorrectRowCountAndColCount()
        {
            // Assert - перевірка того, що значення RowCount і ColCount дорівнюють 10
            Assert.AreEqual(10, grid.RowCount);
            Assert.AreEqual(10, grid.ColCount);
        }

        [Test]
        [TestCase("5 / 0")]
        [TestCase("25 / 0")]
        [TestCase("525 / 0")]
        public void Calculate_DivideByZeroExceptionThrown_ReturnsErrorMessage(string expr)
        {
            Assert.Throws<DivideByZeroException>(() => grid.Calculate(expr));
        }

        [Test]
        [TestCase("25*3+A0/B3-5", 2)]
        [TestCase("B5*B6-1/C31*AA5", 4)]
        [TestCase("25*3+A0/B3-5A+AboBa", 2)]
        public void TestNumberOfCells(string expression, int expectedNumberOfCells)
        {
            var matches = grid.FindAllCells(expression);
            Assert.That(matches, Has.Count.EqualTo(expectedNumberOfCells));
        }

        [Test]
        [TestCase("25*3+A0/B3-5")]
        [TestCase("B5*B6-1/C31*AA5")]
        [TestCase("25*3+A0/B3-5A+AboBa")]
        public void TestFirstLetterOfCells(string expression)
        {
            MatchCollection matches = grid.FindAllCells(expression);
            foreach (Match match in matches)
            {
                string cell = match.Value;
                Assert.IsTrue(char.IsUpper(cell[0]), $"Cell {cell} does not start with an uppercase letter.");
            }
        }

    }


}