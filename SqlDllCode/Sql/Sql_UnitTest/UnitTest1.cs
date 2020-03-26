using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sql;

namespace Sql_UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Test_Select()
        {
            SqlEncap sql = new SqlEncap();

            List<string> selectvalue = new List<string>();
            selectvalue.Add("X");
            selectvalue.Add("Y");
            selectvalue.Add("Value");

            string tableName = "Maze";

            Dictionary<string, string> condition = new Dictionary<string, string>();
            condition.Add("ID", "1");
            condition.Add("Z", "2");

            Assert.AreEqual("SELECT X,Y,Value FROM Maze WHERE ID = 1 And Z = 2;", sql.Select(selectvalue, tableName, condition));
        }

        [TestMethod]
        public void Test_Delete()
        {
            SqlEncap sql = new SqlEncap();

            string tableName = "Maze";

            Dictionary<string, string> condition = new Dictionary<string, string>();
            condition.Add("X", "1");
            condition.Add("Y", "1");
            condition.Add("Z", "2");

            Assert.AreEqual("DELETE FROM Maze WHERE X = 1 And Y = 1 And Z = 2;", sql.Delete(tableName, condition));
        }

        [TestMethod]
        public void Test_Insert()
        {
            SqlEncap sql = new SqlEncap();

            string tableName = "Maze";

            List<string> columnName = new List<string>();
            columnName.Add("X");
            columnName.Add("Y");
            columnName.Add("Z");

            List<string> value = new List<string>();
            value.Add("1");
            value.Add("2");
            value.Add("'maze no.1'");

            Assert.AreEqual("INSERT INTO Maze (X,Y,Z) VALUE (1,2,'maze no.1');", sql.Insert(tableName, columnName, value));
        }

        [TestMethod]
        public void Test_Update()
        {
            SqlEncap sql = new SqlEncap();

            string tableName = "Maze";

            Dictionary<string, string> setValue = new Dictionary<string, string>();
            setValue.Add("X", "1");
            setValue.Add("Y", "1");
            setValue.Add("Z", "2");

            Dictionary<string, string> condition = new Dictionary<string, string>();
            condition.Add("ID", "1");
            condition.Add("MAZE_ID", "9");

            Assert.AreEqual("UPDATE Maze SET X = 1, Y = 1, Z = 2 WHERE ID = 1 And MAZE_ID = 9;", sql.Update(tableName, setValue, condition));
        }
    }
}
