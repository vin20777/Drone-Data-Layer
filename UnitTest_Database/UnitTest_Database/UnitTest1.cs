using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DatabaseManager;

namespace UnitTest_Database
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DBManager DBM = new DBManager();
            Console.WriteLine(DBM.getDataTable("SELECT ID FROM Maze").Rows[0][0]);
            Assert.AreEqual(1, System.Convert.ToInt32(DBM.getDataTable("SELECT ID FROM Maze").Rows[0][0]));
        }
    }
}
