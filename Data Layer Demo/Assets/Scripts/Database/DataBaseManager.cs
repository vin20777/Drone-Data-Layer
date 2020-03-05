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
public class DataBaseManager : MonoBehaviour
{
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader dataReader;
    //Singleton pattern
    private static DataBaseManager instance;
    public static DataBaseManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("DatabaseManager").GetComponent<DataBaseManager>();
            }
            return instance;
        }
    }

    /// <summary>
    /// connect to the database
    /// </summary>
    void Awake()
    {
        string filePath = Application.streamingAssetsPath + "/Rover.db";
        try
        {
            dbConnection = new SqliteConnection("Data Source = " + filePath);
            dbConnection.Open();
            Debug.Log("success to connect");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public enum MAZE_OBJECT
    {
        Wall = -1, Start = 0, Road = 1
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
        dataReader = ExecuteQuery("SELECT Step, X , Y FROM Maze WHERE SolutionID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                int step = dataReader.GetInt32(0);
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
    public List<string> getCommands()
    {
        List<string> commands = new List<string>();
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                string command = dataReader.GetString(0);
                commands.Add(command);
            }
        }

        return commands;
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

