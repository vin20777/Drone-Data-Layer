using System;
using Assets.Scripts.Database;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This user interface is used to show the functionality of the DataBaseManager,
/// Please run the demo in Unity and see the screen for details.
/// 
/// Author: Bingrui Feng, Xinkai Wang, Jiayan Wang,
/// Yu-Ting Tsao, Huijing Liang, Meng-Ze Chen
/// </summary>
public class UserInterface : MonoBehaviour
{
    private DataBaseManager dbm;
    private int mazeUid;

    public Button btnShow;
    public Button btnSelect;
    public Button btnDelete;
    public Button btnInsert;
    public Button btnUpdate;

    public InputField SelectInput;
    public Text Output;

    // Start is called before the first frame update
    void Start()
    {
        dbm = new DataBaseManager();
        dbm.ConnectToDB("Rover.db");
        btnShow.onClick.AddListener(ClickToShow);
        btnSelect.onClick.AddListener(ClickToShowById);
        btnInsert.onClick.AddListener(ClickToInsert);
        btnUpdate.onClick.AddListener(ClickToUpdate);
        btnDelete.onClick.AddListener(ClickToDelete);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickToShow()
    {
        Output.text = String.Empty;
        var arr = dbm.GetAllMazeRecord();

        int rowLength = arr.GetLength(0);
        int colLength = arr[0].Length;

        Debug.Log("Get Maze Result:");

        for (int i = 0; i < rowLength; i++)
        {
            string str = String.Empty;
            for (int j = 0; j < colLength; j++)
            {
                str += arr[i][j] + " ";
            }
            Output.text += str + "\n";
        }
    }

    public void ClickToShowById()
    {
        Output.text = String.Empty;
        var arr = dbm.GetMazeById(System.Convert.ToInt32(SelectInput.text));

        int rowLength = arr.GetLength(0);
        int colLength = arr[0].Length;

        Debug.Log("Get Maze Result:");

        for (int i = 0; i < rowLength; i++)
        {
            string str = String.Empty;
            for (int j = 0; j < colLength; j++)
            {
                str += arr[i][j] + " ";
            }
            Output.text += str + "\n";
        }
    }

    public void ClickToInsert()
    {
        Output.text = String.Empty;
        string[,] edges = new string[4, 3]
        {
            {"1", "3", "S"},
            {"2", "3", "N"},
            {"2", "4", "W"},
            {"3", "4", "E"}
        };
        mazeUid = provideUid();
        int resultCode = dbm.InsertMazeRecord(mazeUid, edges);
        this.ClickToShow();
    }

    public void ClickToUpdate()
    {
        Output.text = String.Empty;
        string[] edges = new string[3] { "1", "3", "E" };
        int resultCode = dbm.UpdateMazeDirection(mazeUid, edges);
        this.ClickToShow();
    }

    public void ClickToDelete()
    {
        Output.text = String.Empty;
        int resultCode = dbm.DeleteMazeById(mazeUid);
        this.ClickToShow();
    }

    private int provideUid()
    {
        var now = DateTime.Now;
        var zeroDate = DateTime.MinValue.AddHours(now.Hour).AddMinutes(now.Minute).AddSeconds(now.Second).AddMilliseconds(now.Millisecond);
        int uniqueId = (int)(zeroDate.Ticks / 10000);
        return uniqueId;
    }
}
