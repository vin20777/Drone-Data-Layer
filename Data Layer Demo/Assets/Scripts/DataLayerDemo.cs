using UnityEngine;
using DataLayer;
using System;

public class DataLayerDemo : MonoBehaviour
{
    DatabaseProviderClass dpc;
    void Start()
    {
        Debug.Log("This is a demo of data layer with dll.");
        Debug.Log("1. Initial dpc class from dll.");
        dpc = new DatabaseProviderClass();

        Debug.Log("2. Try store a maze into database.");
        int[][] fooMaze = new int[][] {
                new int[] {1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 1, 1, 0, 1},
                new int[] {1, 2, 0, 0, 1},
                new int[] {1, 1, 1, 1, 1} };
        dpc.SetMapStructure(100, fooMaze);

        Debug.Log("3. Try get the maze from database.");
        int[][] mazeData = dpc.GetMapStructure(100);
        Debug.Log("----You can now use \"mazeData\" now.----");

        Debug.Log("4. Try store a commands list into database.");
        String[] fooCommand = new String[] { "right", "right", "down", "left", "left" };
        dpc.SetCommands(80, fooCommand);

        Debug.Log("5. Try get the command list back from database.");
        String[] commands = dpc.GetCommands(80);
        Debug.Log("----You can now use \"commands\" now.----");

        Debug.Log("6. Try store a path into database.");
        int[][] path = new int[][] {
                 new int[] { 1, 1 },
                 new int[] { 2, 1 },
                 new int[] { 3, 1 },
                 new int[] { 3, 2 },
                 new int[] { 3, 3 },
                 new int[] { 2, 3 },
                 new int[] { 1, 3 }
            };
        dpc.SetPathRecords(70, path);

        Debug.Log("7. Try get the maze from database.");
        int[][] mazePath = dpc.GetPathRecords(70);
        Debug.Log("----You can now use \"mazePath\" now.----");
    }
}
