using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;

/// <summary>
/// This is database manager class that can access the database.
/// 
/// Author: Jiayan Wang
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

    public enum MAZE_OBJECT
    {
        //Wall = -1, Start = 0, Road = 1
        OpenArea = 0, Wall = 1, Start = 2 , Road = 4, Robot = 5
    }
    #region maze
    public int[,] getMazeSize(int id)
    {
        dataReader = ExecuteQuery("SELECT max(X),max(Y) FROM Maze WHERE ID = " + id + ";");
        int x = 0;
        int y = 0;
        if (dataReader.Read())
        {
            x = dataReader.GetInt32(0)+1;
            y = dataReader.GetInt32(1)+1;
        }
        return new int[x, y];
    }

    public int[,] getMazeByID(int id)
    {
        int[,] res = getMazeSize(id);
        dataReader = ExecuteQuery("SELECT X,Y,Value FROM Maze WHERE ID = " + id + ";");
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
    #endregion

    #region path
    public int[,] getPathSize(int id)
    {
        dataReader = ExecuteQuery("SELECT count(Step) FROM Path WHERE SolutionID = " + id + ";");
        int step = 0;
        if (dataReader.Read())
        {
            step = dataReader.GetInt32(0);
        }
        return new int[step, 2];
    }

    public int[,] getPathByID(int id)
    {
        int[,] res = getPathSize(id);
        dataReader = ExecuteQuery("SELECT Step, X , Y FROM Path WHERE SolutionID = " + id + ";");
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

    #region sensor
    public string getSensorByID(int id)
    {
        dataReader = ExecuteQuery("SELECT Comment FROM Sensor WHERE ID = " + id + ";");
        string res = "";
        if (dataReader.Read())
        {
            res = dataReader.GetString(0);
        }

        return res;
    }
    #endregion

    #region command_list

    public string[] getCommandsSize(int id)
    {
        dataReader = ExecuteQuery("SELECT count(Step) FROM Commands WHERE ID = " + id + ";");
        string[] res = new string[0];
        if (dataReader.Read())
        {
            int size = dataReader.GetInt32(0);
            res = new string[size];
        }

        return res;

    }
    public string[] getCommandByID(int id)
    {
        string[] res = getCommandsSize(id);
        dataReader = ExecuteQuery("SELECT Step,Command FROM Commands WHERE ID = " + id + ";");
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

    public SqliteDataReader ExecuteQuery(string queryString)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        Debug.Log(queryString);
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }

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

