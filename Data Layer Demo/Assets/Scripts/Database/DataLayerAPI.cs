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

    /* First API: Set Map Structure.
     * Parameters: int id, int[][] maze
     * Return Type: void
     * Team may use: Algorithm
     * Definition: Pass an unique id and the maze to store.
     */

    /* Second API: Get Map Structure.
     * Parameters: int id, 
     * Return Type: Dictionary<String, object>
     * Team may use: Algorithm, UI, Experimental Design
     * Definition: Retrieve the map information with id.
     */

    /* Third API: Set Path Record.
     * Parameters: int id, int mazeId, int[][] path, int[] velocity
     * Return Type: void
     * Team may use: Algorithm
     * Definition: Record the path for mazes.
     */

    /* Fourth API: Get Path Record.
     * Parameters: int id
     * Return Type: Dictionary<String, object>
     * Team may use: UI
     * Definition: Retrieve stored path record.
     */

    /* Fifth API: Set Commands List.
     * Parameters: int id, int mazeId, string[] commands, bool isRobot
     * Return Type: Dictionary<String, object>
     * Team may use: Experimental Design
     * Definition: Record the commands either from AI or the user.
     */

    /* Sixth API: Get Commands List.
     * Parameters: int id
     * Return Type: Dictionary<String, object>
     * Team may use: UI
     * Definition: Retrieve the commands for analysis.
     */

    /* Seventh API: Set Sensor Configuration.
     * Parameters: <UNKNOWN>
     * Return Type: <UNKNOWN>
     * Team may use: Sensors
     * Definition: 
     */

    /* Eighth API: Get Sensor Configuration.
     * Parameters: <UNKNOWN> 
     * Return Type: <UNKNOWN>
     * Team may use: Sensors
     * Definition:
     */
}
