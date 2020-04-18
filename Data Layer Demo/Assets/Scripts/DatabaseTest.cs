using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DatabaseTest : MonoBehaviour
{

    public Text dataformat;
    public Dropdown dp;
    public InputField dbname;
    public InputField text1;
    public InputField text2;
    public Text field1;
    public Text field2;

    public Text outputtext;
    private DataBaseManager db;
    // Start is called before the first frame update
    void Start()
    {
        db = new DataBaseManager();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectDB()
    {
        db.ConnectToDB(dbname.text);
        outputtext.text = "Success to connect to: " + dbname.text;
    }

    private void AlwaysDisplayMazeRecord()
    {
        string[][] arr = db.GetAllMazeRecord();
        outputtext.text = string.Empty;

        int rowLength = arr.GetLength(0);
        int colLength = arr[0].Length;

        //Debug.Log("Get All Maze Result:");

        for (int i = 0; i < rowLength; i++)
        {
            string str = String.Empty;
            for (int j = 0; j < colLength; j++)
            {
                str += arr[i][j] + " ";
            }
            outputtext.text += str + "\n";
        }
    }

    public void ExecuteSql()
    {
        if (dp.captionText.text.Equals("GetAllMazeRecord"))
        {
            this.AlwaysDisplayMazeRecord();
        }
        else if (dp.captionText.text.Equals("GetMazeById"))
        {
            string[][] arr = db.GetMazeById(int.Parse(text1.text));
            outputtext.text = string.Empty;

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
                outputtext.text += str + "\n";
            }
        }
        else if (dp.captionText.text.Equals("InsertMazeRecord"))
        {

            // format: 1,4,S;2,3,N;3,4,W
            string[] temp = text2.text.Split(';');
            string[,] edges = new string[temp.Length, 3];
            
            for(int i = 0; i < temp.Length; i++)
            {
                string[] values = temp[i].Split(',');
                edges[i, 0] = values[0].ToString();
                edges[i, 1] = values[1].ToString();
                edges[i, 2] = values[2].ToString();
            }
            db.InsertMazeRecord(int.Parse(text1.text), edges);
            text2.placeholder.GetComponent<Text>().text = string.Empty;
            this.AlwaysDisplayMazeRecord();
        }
        else if (dp.captionText.text.Equals("UpdateMazeDirection"))
        {
            string[] temp = text2.text.Split(',');
            int res = db.UpdateMazeDirection(int.Parse(text1.text), temp);
            text2.placeholder.GetComponent<Text>().text = string.Empty;
            this.AlwaysDisplayMazeRecord();
        }
        else if (dp.captionText.text.Equals("DeleteMazeById"))
        {
            int res = db.DeleteMazeById(int.Parse(text1.text));
            this.AlwaysDisplayMazeRecord();
        }
        else if (dp.captionText.text.Equals("SetSensorMatrixById"))
        {
            int id = int.Parse(text1.text);

            string[] split1 = text2.text.Split(';');
            int num1 = split1.Length;
            int num2 = split1[0].Split(',').Length;
            int[,] matrix = new int[num1, num2];
            for (int i = 0; i < split1.Length; i++)
            {
                string[] split2 = split1[i].Split(',');
                for (int j = 0; j < split2.Length; j++)
                {
                    matrix[i, j] = Convert.ToInt32(split2[j]);
                }

            }

            int resultCode = db.SetSensorMatrixById(id, matrix);

            outputtext.text = "Insert Sensor Result:" +
                  (resultCode == 1 ? "Success"
                   : "Failure");
        }
        else if (dp.captionText.text.Equals("GetSensorMatrixById"))
        {
            int[,] matrix = db.GetSensorMatrixById(int.Parse(text1.text));
            string str = "";
            for (int i = 0; i <= matrix.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= matrix.GetUpperBound(1); j++)
                {
                    str += matrix[i, j];
                    if(j != matrix.GetUpperBound(1))
                    {
                        str += ",";
                    }
                }
                str += "\n";
            }
            outputtext.text = str;
        }
    }

    /// <summary>
    /// NOT USED SO FAR
    /// </summary>
    /// <param name="original"></param>
    /// <returns></returns>
    public static string[,] TranStrToTwoArray(string original)
    {
        if (original.Length == 0)
        {
            throw new IndexOutOfRangeException("original's length can not be zero");
        }
        string[] originalRow = original.Split('#');
        string[] originalCol = originalRow[0].Split(',');
        int x = originalRow.Length;
        int y = originalCol.Length;
        string[,] twoArray = new string[x, y];
        for (int i = 0; i < x; i++)
        {
            originalCol = originalRow[i].Split(',');
            for (int j = 0; j < originalCol.Length; j++)
            {
                twoArray[i, j] = originalCol[j];
            }
        }
        return twoArray;
    }

    public void OnValueChange(int value)
    {
        switch (value)
        {          
            case 0:
                dataformat.text = string.Empty;
                field1.text = "Input parameter 1:";
                field2.text = "Input parameter 2:";
                text1.text = string.Empty;
                text2.text = string.Empty;
                break;
            case 1:
                dataformat.text = "This method has 0 input";
                field1.text = "Do Not Input: ";
                field2.text = "Do Not Input: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                break;
            case 2:
                dataformat.text = "This method has 1 input: id";
                field1.text = "Id:";
                field2.text = "Do Not Input: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                break;
            case 3:
                dataformat.text = "This method has 2 inputs: id, edges";
                field1.text = "Id:";
                field2.text = "Edges: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text2.placeholder.GetComponent<Text>().text = "format: 1,4,S;2,3,N;3,4,W";
                break;
            case 4:
                dataformat.text = "This method has 2 inputs: id, edge";
                field1.text = "Id:";
                field2.text = "Edge: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text2.placeholder.GetComponent<Text>().text = "format: 1,3,W";
                break;
            case 5:
                dataformat.text = "This method has 1 input: id";
                field1.text = "Id:";
                field2.text = "Do Not Input: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                break;
            case 6:
                dataformat.text = "This method has 2 inputs: id, matrix";
                field1.text = "Id:";
                field2.text = "Matrix: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text2.placeholder.GetComponent<Text>().text = "format: 1,2,3;2,3,4;3,4,5";
                break;
            case 7:
                dataformat.text = "This method has 1 input: id";
                field1.text = "Id:";
                field2.text = "Do Not Input: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                break;
        }
    }
}
