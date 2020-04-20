using System;
using System.Collections.Generic;
using Assets.Scripts.Database;
using Mono.Data.Sqlite;
using Sql;
using UnityEngine;

/// <summary>
/// This is the legacy interface class for data layer team.
///
/// Author: Yu-Ting Tsao, Huijing Liang
/// </summary>
public enum MAZE_OBJECT {
    OpenArea = 0,
    Wall = 1,
    Starting = 2,
    Ending = 3,
    Path = 4
}

public class LegacyDataLayerAPI
{
    private readonly string fileName = "Rover.db";
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader dataReader;

    // private Dictionary<int, int[,]> mapDB;

    public LegacyDataLayerAPI()
    {
        string filePath = Application.streamingAssetsPath + "/" + fileName;
        try
        {
            dbConnection = new SqliteConnection("Data Source = " + filePath);
            dbConnection.Open();
            Debug.Log("Success to connect database: " + fileName);
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    internal void setMapStructure(Func<int> provideUid, int[,] sampleMaze)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// First API: Set Map Structure.
    /// Parameters: int id, int[][] maze
    /// Return Type: void
    /// Team may use: Algorithm
    /// Definition: Pass an unique id and the maze to store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="maze"></param>
    public void setMapStructure(int id, int[,] maze)
    {
        // mapDB.Add(id, maze);
    }

    /// <summary>
    /// Second API: Get Map Structure.
    /// Parameters: int id,
    /// Return Type: Dictionary<String, object>
    /// Team may use: Algorithm, UI, Experimental Design
    /// Definition: Retrieve the map information with id.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Dictionary<string, object> getMapStructure(int id)
    {
        int[,] maze = getMazeSize(id);
        dataReader =
            ExecuteQuery("SELECT X,Y,Value FROM Maze WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                int x = dataReader.GetInt32(0);
                int y = dataReader.GetInt32(1);
                int val = dataReader.GetInt32(2);
                maze[x, y] = val;
            }
        }
        Dictionary<string, object> result = new Dictionary<string, object>();
        result.Add("description", generateDescription());
        // result.Add("maze", mapDB[id]);
        return result;
    }

    private Dictionary<int, string> generateDescription()
    {
        Dictionary<int, string> description = new Dictionary<int, string>();
        description.Add((int)MAZE_OBJECT.OpenArea, "OpenArea");
        description.Add((int)MAZE_OBJECT.Wall, "Wall");
        description.Add((int)MAZE_OBJECT.Starting, "Starting");
        description.Add((int)MAZE_OBJECT.Ending, "Ending");
        description.Add((int)MAZE_OBJECT.Path, "Path");

        return description;
    }

    private int[,] getMazeSize(int id)
    {
        dataReader = ExecuteQuery(
            "SELECT max(X),max(Y) FROM Maze WHERE ID = " + id + ";");
        int x = 0;
        int y = 0;
        if (dataReader.Read())
        {
            x = dataReader.GetInt32(0) + 1;
            y = dataReader.GetInt32(1) + 1;
        }
        return new int[x, y];
    }

    /// <summary>
    ///  Third API: Set Path Record.
    ///  Parameters: int id, int mazeId, int[][] path, int[] velocity
    ///  Return Type: void
    ///  Team may use: Algorithm
    ///  Definition: Record the path for mazes.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="mazeId"></param>
    /// <param name="path"></param>
    /// <param name="velocity"></param>
    public void setPathRecord(int id, int mazeId, int[][] path,
                              int[] velocity)
    { }

    /// <summary>
    /// Fourth API: Get Path Record.
    /// Parameters: int id
    /// Return Type: Dictionary<String, object>
    /// Team may use: UI
    /// Definition: Retrieve stored path record.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Dictionary<string, object> getPathRecord(int id)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        int[,] res = getPathSize(id);
        dataReader =
            ExecuteQuery("SELECT Step, X , Y FROM Path WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                int step = dataReader.GetInt32(0) - 1;
                int x = dataReader.GetInt32(1);
                int y = dataReader.GetInt32(2);
                res[step, 0] = x;
                res[step, 1] = y;
            }
        }
        result.Add("path", res);
        result.Add("solutionId", 7);
        return result;
    }

    private int[,] getPathSize(int id)
    {
        dataReader = ExecuteQuery(
            "SELECT count(Step) FROM Path WHERE SolutionID = " + id + ";");
        int step = 0;
        if (dataReader.Read())
        {
            step = dataReader.GetInt32(0);
        }
        return new int[step, 2];
    }
    /// <summary>
    ///  Fifth API: Set Commands List.
    ///  Parameters: int id, int mazeId, string[] commands, bool isRobot
    ///  Return Type: Dictionary<String, object>
    ///  Team may use: Experimental Design
    ///  Definition: Record the commands either from AI or the user.
    /// </summary>

    public void setCommandsList(int id, int mazeId, string[] commands,
                                bool isRobot)
    { }

    /// <summary>
    /// Sixth API: Get Commands List.
    /// Parameters: int id
    /// Return Type: Dictionary<String, object>
    /// Team may use: UI
    /// Definition: Retrieve the commands for analysis.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public Dictionary<string, object> getCommandsList(int id)
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        string[] res = getCommandsSize(id);
        dataReader = ExecuteQuery(
            "SELECT Step,Command FROM Commands WHERE ID = " + id + ";");
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                int index = dataReader.GetInt32(0) - 1;
                res[index] = dataReader.GetString(1);
            }
        }
        result.Add("commands", res);
        return result;
    }

    private string[] getCommandsSize(int id)
    {
        dataReader = ExecuteQuery(
            "SELECT count(Step) FROM Commands WHERE ID = " + id + ";");
        string[] res = new string[0];
        if (dataReader.Read())
        {
            int size = dataReader.GetInt32(0);
            res = new string[size];
        }
        return res;
    }

    /// <summary>
    /// This method is to update existing map direction in maze table.
    /// </summary>
    /// <param name="nodes"></param>
    /// <param name="edges"></param>
    public int UpdateMazeDirection(int id, string[] edges)
    {
        SqlEncap sql = new SqlEncap();
        int result = Constants.RESPONSE_CODE_SUCCESS;

        Dictionary<string, string> setValue = new Dictionary<string, string>();
        setValue.Add(Constants.COLUMN_NODE, edges[0]);
        setValue.Add(Constants.COLUMN_CONNECTTO, edges[1]);
        setValue.Add(Constants.COLUMN_DIRECTION, "'" + edges[2] + "'");

        Dictionary<string, string> condition = new Dictionary<string, string>();
        condition.Add(Constants.COLUMN_ID, id.ToString());
        condition.Add(Constants.COLUMN_NODE, edges[0]);
        condition.Add(Constants.COLUMN_CONNECTTO, edges[1]);

        try
        {
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText =
                sql.Update(Constants.TABLE_MAZE, setValue, condition);
            dbCommand.ExecuteNonQuery();
        }
        catch (SqliteException sqlEx)
        {
            result = Constants.RESPONSE_CODE_FAILURE;
            Debug.LogError(sqlEx);
        }

        return Constants.RESPONSE_CODE_SUCCESS;
    }

    /// <summary>
    /// This method is to delete maze record by ID
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int DeleteMazeById(int id)
    {
        SqlEncap sql = new SqlEncap();
        int result = Constants.RESPONSE_CODE_SUCCESS;

        Dictionary<string, string> condition = new Dictionary<string, string>();
        condition.Add(Constants.COLUMN_ID, id.ToString());

        try
        {
            dbCommand = dbConnection.CreateCommand();
            dbCommand.CommandText = sql.Delete(Constants.TABLE_MAZE, condition);
            dbCommand.ExecuteNonQuery();
        }
        catch (SqliteException sqlEx)
        {
            result = Constants.RESPONSE_CODE_FAILURE;
            Debug.LogError(sqlEx);
        }

        return result;
    }

    /// <summary>
    /// First API: Insert Maze Record.
    /// Parameters: int id, string[,] edges
    /// Return Type: int (Success or Failure)
    /// Team may use: Algorithm
    /// Definition: Pass an unique id and the maze to store.
    /// </summary>
    /// <param name="edges"></param>
    public int InsertMazeRecord(int id, string[,] edges)
    {
        // sample data: 
        // nodes = new int [4] {1, 2, 3, 4};
        // edges = new int [4, 3]{
        // {'1','2','E'}, {'1','4','N'}, {'2','3','W'}, {'3','4','S'}
        //};

        SqlEncap sql = new SqlEncap();
        int result = Constants.RESPONSE_CODE_SUCCESS;
        if (errorCheckMaze(id, edges))
        {
            result = Constants.RESPONSE_CODE_FAILURE;
            return result;
        }

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
                value.Add("'" + edges[i, 2] + "'");
                value.Add("'Description'");

                dbCommand = dbConnection.CreateCommand();
                dbCommand.CommandText =
                    sql.Insert(Constants.TABLE_MAZE, columnName, value);
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

    #region UNDONE Work
    /// <summary>
    /// return the type of the object in maze according to coordinates
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="id"></param>
    /// <returns></returns>

    // public MAZE_OBJECT getObjectByPosition(int x, int y,int id)
    //{
    //    SqlEncap sql = new SqlEncap();
    //    List<string> selectvalue = new List<string>();
    //    selectvalue.Add("Value");
    //    string tableName = "Maze";
    //    Dictionary<string, string> condition = new Dictionary<string,
    //    string>(); condition.Add("ID", id.ToString()); condition.Add("X",
    //    x.ToString()); condition.Add("Y", y.ToString());

    //    int[,] res = getMazeSize(id);
    //    dataReader = ExecuteQuery(sql.Select(selectvalue, tableName,
    //    condition)); int val = -2; while (dataReader.HasRows)
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

    /// <summary>
    ///  return the number of the steps according to id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    #endregion

    /// <summary>
    /// Seventh API: Set Sensor Configuration.
    /// Parameters: <UNKNOWN>
    /// Return Type: <UNKNOWN>
    /// Team may use: Sensors
    /// Definition:
    ///</summary>
    ///  Eighth API: Get Sensor Configuration.
    ///  Parameters: <UNKNOWN>
    ///  Return Type: <UNKNOWN>
    ///  Team may use: Sensors
    ///  Definition:
    ///</summaryc>

    /// </summary>
    /// <param name="queryString"></param>
    /// <returns></returns>

    /* General database functions. */
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