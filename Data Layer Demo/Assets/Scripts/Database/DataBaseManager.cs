using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;
using Sql;
using Assets.Scripts.Database;

/// <summary>
/// This is database manager class that can access the database.
/// 
/// Author: Jiayan Wang, Bingrui Feng, Xinkai Wang
/// </summary>
public class DataBaseManager
{
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader dataReader;

    /// <summary>
    /// connect to the database
    /// </summary>
    public void ConnectToDB(string filename)
    {
        string filePath = Application.streamingAssetsPath + "/" + filename;
        try
        {
            dbConnection = new SqliteConnection("Data Source = " + filePath);
            dbConnection.Open();
            Debug.Log("success to connect " + filename);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// return the size of the maze according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    #region maze
    public int[,] getMazeSize(int id)
    {
        SqlEncap sql = new SqlEncap();
        List<string> selectvalue = new List<string>();
        selectvalue.Add("max(X)");
        selectvalue.Add("max(Y)");
        string tableName = "Maze";
        Dictionary<string, string> condition = new Dictionary<string, string>();
        condition.Add("ID", id.ToString());

        dataReader = ExecuteQuery(sql.Select(selectvalue, tableName, condition));
        int x = 0;
        int y = 0;
        if (dataReader.Read())
        {
            x = dataReader.GetInt32(0)+1;
            y = dataReader.GetInt32(1)+1;
        }
        return new int[x, y];
    }
    /// <summary>
    /// return the maze according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int[,] getMazeByID(int id)
    {
        SqlEncap sql = new SqlEncap();
        List<string> selectvalue = new List<string>();
        selectvalue.Add("X");
        selectvalue.Add("Y");
        selectvalue.Add("Value");
        string tableName = "Maze";
        Dictionary<string, string> condition = new Dictionary<string, string>();
        condition.Add("ID", id.ToString());

        int[,] res = getMazeSize(id);
        dataReader = ExecuteQuery(sql.Select(selectvalue, tableName, condition));
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                int x = dataReader.GetInt32(0);
                int y = dataReader.GetInt32(1);
                int val = dataReader.GetInt32(2);
                res[x, y] = val;
            }
        }

        return res;
    }


    /// <summary>
    /// This method is to insert received map data into maze table
    /// </summary>
    /// <param name="id"></param>
    /// <param name="maze"></param>
    public int InsertMazeRecord(int id, int[] nodes, string[,] edges)
    {
        // sample data: 
        // nodes = new int [4] {1, 2, 3, 4};
        // edges = new int [4, 3]{
        // {'1','2','E'}, {'1','4','N'}, {'2','3','W'}, {'3','4','S'}
        //};

        SqlEncap sql = new SqlEncap();
        int result = Constants.RESPONSE_CODE_SUCCESS;

        List<string> columnName = new List<string>();
        List<string> value = new List<string>();

        try
        {
            columnName.Add(Constants.COLUMN_ID);
            columnName.Add(Constants.COLUMN_NODE);
            columnName.Add(Constants.COLUMN_CONNECTTO);
            columnName.Add(Constants.COLUMN_DIRECTION);
            columnName.Add(Constants.COLUMN_DESCRIPTION);

            for (int i = 0; i < edges.GetLength(0); i++)
            {
                value.Clear();
                value.Add(id.ToString());
                value.Add(edges[i, 0]);
                value.Add(edges[i, 1]);
                value.Add("'"+edges[i, 2]+"'");
                value.Add("'Description'");

                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText = sql.Insert(Constants.TABLE_MAZE, columnName, value);
                dbCommand.ExecuteNonQuery();
            }
        }
        catch (SqliteException sqlEx)
        {
            result = Constants.RESPONSE_CODE_FAILURE;
            Debug.LogError(sqlEx);
        }
  
        return result;
    }


    /// <summary>
    /// return the type of the object in maze according to coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="id"></param>
    /// <returns></returns>

    //public MAZE_OBJECT getObjectByPosition(int x, int y,int id)
    //{
    //    SqlEncap sql = new SqlEncap();
    //    List<string> selectvalue = new List<string>();
    //    selectvalue.Add("Value");
    //    string tableName = "Maze";
    //    Dictionary<string, string> condition = new Dictionary<string, string>();
    //    condition.Add("ID", id.ToString());
    //    condition.Add("X", x.ToString());
    //    condition.Add("Y", y.ToString());

    //    int[,] res = getMazeSize(id);
    //    dataReader = ExecuteQuery(sql.Select(selectvalue, tableName, condition));
    //    int val = -2;
    //    while (dataReader.HasRows)
    //    {
    //        if (dataReader.Read())
    //        {
    //            val = dataReader.GetInt32(2);
    //        }
    //    }

    //    if (val == 1)
    //        return Constants.MAZE_OBJECT.;
    //    else if (val == 0)
    //        return MAZE_OBJECT.Start;
    //    else if (val == -1)
    //        return MAZE_OBJECT.Wall;

    //    return MAZE_OBJECT.Wall;
    //}
    #endregion
    /// <summary>
    ///  return the number of the steps according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    #region path
    public int[,] getPathSize(int id)
    {
        SqlEncap sql = new SqlEncap();
        List<string> selectvalue = new List<string>();
        selectvalue.Add("count(Step)");
        string tableName = "Path";
        Dictionary<string, string> condition = new Dictionary<string, string>();
        condition.Add("SolutionID", id.ToString());

        dataReader = ExecuteQuery(sql.Select(selectvalue, tableName, condition));
        int step = 0;
        if (dataReader.Read())
        {
            step = dataReader.GetInt32(0);
        }
        return new int[step, 2];
    }
    /// <summary>
    /// return the sepecific path according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int[,] getPathByID(int id)
    {
        SqlEncap sql = new SqlEncap();
        List<string> selectvalue = new List<string>();
        selectvalue.Add("Step");
        selectvalue.Add("X");
        selectvalue.Add("Y");
        string tableName = "Path";
        Dictionary<string, string> condition = new Dictionary<string, string>();
        condition.Add("SolutionID", id.ToString());

        int[,] res = getPathSize(id);
        dataReader = ExecuteQuery(sql.Select(selectvalue, tableName, condition));
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                int step = dataReader.GetInt32(0)-1;
                int x = dataReader.GetInt32(1);
                int y = dataReader.GetInt32(2);
                res[step, 0] = x;
                res[step, 1] = y;
            }
        }

        return res;
    }
    #endregion
    /// <summary>
    /// return the sensor according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    #region sensor
    public string getSensorByID(int id)
    {
        SqlEncap sql = new SqlEncap();
        List<string> selectvalue = new List<string>();
        selectvalue.Add("Comment");
        string tableName = "Sensor";
        Dictionary<string, string> condition = new Dictionary<string, string>();
        condition.Add("ID", id.ToString());

        dataReader = ExecuteQuery(sql.Select(selectvalue, tableName, condition));
        string res = "";
        if (dataReader.Read())
        {
            res = dataReader.GetString(0);
        }

        return res;
    }
    #endregion
    /// <summary>
    /// return the size of the commands list
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    #region command_list

    public string[] getCommandsSize(int id)
    {
        SqlEncap sql = new SqlEncap();
        List<string> selectvalue = new List<string>();
        selectvalue.Add("count(Step)");
        string tableName = "Commands";
        Dictionary<string, string> condition = new Dictionary<string, string>();
        condition.Add("ID", id.ToString());

        dataReader = ExecuteQuery(sql.Select(selectvalue, tableName, condition));
        string[] res = new string[0];
        if (dataReader.Read())
        {
            int size = dataReader.GetInt32(0);
            res = new string[size];
        }

        return res;

    }
    /// <summary>
    /// return the sepecific Command according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public string[] getCommandByID(int id)
    {
        SqlEncap sql = new SqlEncap();
        List<string> selectvalue = new List<string>();
        selectvalue.Add("Step");
        selectvalue.Add("Command");
        string tableName = "Commands";
        Dictionary<string, string> condition = new Dictionary<string, string>();
        condition.Add("ID", id.ToString());

        string[] res = getCommandsSize(id);
        dataReader = ExecuteQuery(sql.Select(selectvalue, tableName, condition));
        //Debug.Log(dataReader.Read());
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                int index = dataReader.GetInt32(0) - 1 ;
                res[index] = dataReader.GetString(1);
            }
        }

        return res;
    }
    #endregion
    /// <summary>
    /// return the data from the database
    /// </summary>
    /// <param name="queryString"></param>
    /// <returns></returns>
    private SqliteDataReader ExecuteQuery(string queryString)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        Debug.Log(queryString);
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }
    /// <summary>
    /// close the connection with the database
    /// </summary>
    public void CloseConnection()
    {
        if (dbCommand != null)
        {
            dbCommand.Cancel();
        }
        dbCommand = null;

        if (dataReader != null)
        {
            dataReader.Close();
        }
        dataReader = null;

        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
    }

}

