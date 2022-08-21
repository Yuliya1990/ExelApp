using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using ExelApp;

namespace TestExcel
{
    //class Cell

    [TestClass]
    public class TestCheckForLoop
    {
        [TestMethod]
        public void A0toA0()
        {
            Cell B1 = new Cell("B1", 0, 0);
            Cell A0 = new Cell("A0", 0, 0);
            bool expected = false;

            A0.New_IDependCells.Add(B1);
            A0.New_IDependCells.Add(A0);
            bool result = A0.CheckForLoop(A0.New_IDependCells);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void A0toB1toA0()
        {
            Cell B1 = new Cell("B1", 0, 0);
            Cell A0 = new Cell("A0", 0, 0);
            bool expected = false;

            A0.New_IDependCells.Add(B1);
            B1.New_IDependCells.Add(A0);
            A0.DependfromMeCells.Add(B1);
            bool result = A0.CheckForLoop(A0.New_IDependCells);
            Assert.AreEqual(expected, result);
        }

        [TestMethod]
        public void B1toA0toC0toB1()
        {
            Cell B1 = new Cell("B1", 0, 0);
            Cell A0 = new Cell("A0", 0, 0);
            Cell C0 = new Cell("C0", 0, 0);
            bool expected = false;

            A0.IDependCells.Add(C0);
            C0.DependfromMeCells.Add(A0);
            A0.DependfromMeCells.Add(B1);
            B1.IDependCells.Add(A0);
            C0.IDependCells.Add(B1);
            B1.DependfromMeCells.Add(C0);
            bool result = A0.CheckForLoop(A0.DependfromMeCells);
            Assert.AreEqual(expected, result);
        }
    }

    //class To26Sys
    [TestClass]
    public class TestSys26
    {
        _26BasedSystem s26 = new _26BasedSystem();
        [TestMethod]
        public void Int15ToP()
        {
            string expected = "P";
            string result = s26.To26Sys(15);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void Int26toAA()
        {
            string expected = "AA";
            string result = s26.To26Sys(26);
            Assert.AreEqual(expected, result);
        }
        [TestMethod]
        public void B5to1and5()
        {
            int[] expected = { 1, 5 };
            int[] result = s26.From26Sys("B5");
            Assert.AreEqual(expected[0], result[0]);
            Assert.AreEqual(expected[1], result[1]);
        }
    }

    //class Grid
    [TestClass]
    public class TestCalculator
    {
        [TestMethod]
        public void TestDivByZero()
        {
            string res = Convert.ToString(Calculator.Evaluate("5 / 0"));
            string expected = "∞";
            Assert.AreEqual(expected, res);
        }
        public void TestModByZero()
        {
            string res = Convert.ToString(Calculator.Evaluate("5 mod 0"));
            string expected = "∞";
            Assert.AreEqual(expected, res);
        }
        public void DivByZero()
        {
            string res = Convert.ToString(Calculator.Evaluate("5 div 0"));
            string expected = "∞";
            Assert.AreEqual(expected, res);
        }

    }
}
