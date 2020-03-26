using Assets.Scripts.Database;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitTest : MonoBehaviour
{
    // Start is called before the first frame update
    private DataBaseManager dbm;
    void Start()
    {
        dbm = new DataBaseManager();
        dbm.ConnectToDB("Rover.db");
        TestInsertMaze();
        //TestMazeSize();
        //TestMaze();
        //TestPathsize();
        //TestPath();
        //TestSensor();
        //TestCommandSize();
        //TestCommand();
    }
    /// <summary>
    /// This method is used to test insert record into maze table
    /// </summary>
    void TestInsertMaze()
    {
        int[] nodes = new int [4] {1, 2, 3, 4};
        string[,] edges = new string [4, 3]{
         {"1","3", "S"}, {"2", "3", "N"}, {"2", "4", "W"}, {"3", "4", "E"}
        };

        int resultCode = dbm.InsertMazeRecord(1, nodes, edges);
        Debug.Log("The result code is:" + (resultCode == Constants.RESPONSE_CODE_SUCCESS ? "Failure": "Success"));
    }

    void TestMazeSize()
    {
        int[,] unit = dbm.getMazeSize(1);
        Debug.Log("total number of elements in Maze" + unit.Length);
        Debug.Log("row: " + unit.GetLength(0));
        Debug.Log("col: " + unit.GetLength(1));
    }

    void TestMaze()
    {
        int[,] unit = dbm.getMazeByID(1);
        for (int x = 0; x<unit.GetLength(0);x++) 
        {
            for (int y = 0; y < unit.GetLength(1); y++)
            {
                Debug.Log("x: " + x + " y: " + y + " val: " + unit[x, y]);
            }
        }
    }

    void TestPathsize()
    {
        int[,] unit = dbm.getPathSize(1);
        Debug.Log("total steps in Path" + unit.GetLength(0));
    }

    void TestPath()
    {
        Debug.Log("print path");
        int[,] unit = dbm.getPathByID(1);
        for (int x = 0; x < unit.GetLength(0); x++)
        {
            Debug.Log("x: " + unit[x,0] + " y: " + unit[x,1]);
        }
    }

    void TestSensor()
    {
        Debug.Log("sensor: "+ dbm.getSensorByID(1));
    }

    void TestCommandSize()
    {
        Debug.Log("command size: "+ dbm.getCommandsSize(1).GetLength(0));
    }
    void TestCommand()
    {
        string[] unit = dbm.getCommandByID(1);
        for (int i = 0; i < unit.GetLength(0); i++)
        {
            Debug.Log("index: " + i + " command: " + unit[i]);
        }
       
    }
}
