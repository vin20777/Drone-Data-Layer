using System;
using Assets.Scripts.Database;
using UnityEngine;

/// <summary>
/// This Demo is used to show the functionality of the DataBaseManager,
/// Please run the demo in Unity and see console for details.
/// 
/// Author: Bingrui Feng, Xinkai Wang, Jiayan Wang,
/// Yu-Ting Tsao, Huijing Liang, Meng-Ze Chen
/// </summary>
public class UnitTest : MonoBehaviour
{
    private DataBaseManager dbm;
    private int mazeUid;

    void Start()
    {
        dbm = new DataBaseManager();
        dbm.ConnectToDB("Rover.db");
        TestInsertMaze();
        //TestDeleteMazeById();
    }

    void TestInsertMaze()
    {
        int[] nodes = new int [4] { 1, 2, 3, 4 };
        string[,] edges = new string [4, 3]
        {
            {"1", "3", "S"},
            {"2", "3", "N"},
            {"2", "4", "W"},
            {"3", "4", "E"}
        };
        mazeUid = provideUid();
        int resultCode = dbm.InsertMazeRecord(mazeUid, nodes, edges);
        Debug.Log("Insert Maze Result:" +
            (resultCode == Constants.RESPONSE_CODE_SUCCESS ? "Success" : "Failure"));
    }

    void TestDeleteMazeById()
    {
        int resultCode = dbm.DeleteMazeById(mazeUid);
        Debug.Log("Delete Maze Result:" +
            (resultCode == Constants.RESPONSE_CODE_SUCCESS ? "Success" : "Failure"));
    }

    private int provideUid()
    {
        var now = DateTime.Now;
        var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
        int uniqueId = (int)(zeroDate.Ticks / 10000);
        return uniqueId;
    }
}
