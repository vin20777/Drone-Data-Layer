using System.Collections.Generic;
using Mono.Data.Sqlite;
using UnityEngine;

/// <summary>
/// This is the main interface class for data layer team.
/// 
/// Author: Jiayan Wang, Xinkai Wang, Yu-Ting Tsao
/// </summary>
public enum MAZE_OBJECT
{
    OpenArea = 0, Wall = 1, Starting = 2, Ending = 3, Path = 4
}

public class DataLayerAPI
{
    private readonly string fileName = "Rover.db";
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader dataReader;

    public DataLayerAPI()
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

    /// <summary>
    /// First API: Set Map Structure.
    /// Parameters: int id, int[][] maze
    /// Return Type: void
    /// Team may use: Algorithm
    /// Definition: Pass an unique id and the maze to store.
    /// </summary>
    /// <param name="id"></param>
    /// <param name="maze"></param>
    public void setMapStructure(int id, int[][] maze)
    {

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
        dataReader = ExecuteQuery("SELECT X,Y,Value FROM Maze WHERE ID = " + id + ";");
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
        result.Add("maze", maze);
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
        dataReader = ExecuteQuery("SELECT max(X),max(Y) FROM Maze WHERE ID = " + id + ";");
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
    public void setPathRecord(int id, int mazeId, int[][] path, int[] velocity)
    {

    }

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
        dataReader = ExecuteQuery("SELECT Step, X , Y FROM Path WHERE ID = " + id + ";");
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
        dataReader = ExecuteQuery("SELECT count(Step) FROM Path WHERE SolutionID = " + id + ";");
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

    public void setCommandsList(int id, int mazeId, string[] commands, bool isRobot)
    {

    }

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
        dataReader = ExecuteQuery("SELECT Step,Command FROM Commands WHERE ID = " + id + ";");
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
