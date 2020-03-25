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
    /// <summary>
    /// define the MAZE_OBJECT
    /// </summary>
    public enum MAZE_OBJECT
    {
        Wall = -1, Start = 0, Road = 1
        //OPenArea=0, Wall=1, Start =2, End = 3, Path =4, Robot =5;
    }
    /// <summary>
    /// return the size of the maze according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// <summary>
    /// return the maze according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// <summary>
    /// return the type of the object in maze according to coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="id"></param>
    /// <returns></returns>

    public MAZE_OBJECT getObjectByPosition(int x, int y,int id)
    {

        int[,] res = getMazeSize(id);
        dataReader = ExecuteQuery("SELECT Value FROM Maze WHERE ID = " + id + " And X ="+x+" And Y = "+y+";");
        int val = -2;
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                val = dataReader.GetInt32(2);
            }
        }

        if (val == 1)
            return MAZE_OBJECT.Road;
        else if (val == 0)
            return MAZE_OBJECT.Start;
        else if (val == -1)
            return MAZE_OBJECT.Wall;

        return MAZE_OBJECT.Wall;
    }
    #endregion
    /// <summary>
    ///  return the number of the steps according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// <summary>
    /// return the sepecific path according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// <summary>
    /// return the sensor according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// <summary>
    /// return the size of the commands list
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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
    /// <summary>
    /// return the sepecific Command according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
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

