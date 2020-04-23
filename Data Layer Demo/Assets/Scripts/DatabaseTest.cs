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
            int[,] matrix = db.GetMazeById(int.Parse(text1.text));
            outputtext.text = string.Empty;

            Debug.Log("Get Maze Result:");

            string str = "";
            for (int i = 0; i <= matrix.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= matrix.GetUpperBound(1); j++)
                {
                    str += matrix[i, j];
                    if (j != matrix.GetUpperBound(1))
                    {
                        str += ",";
                    }
                }
                str += "\n";
            }
            outputtext.text = str;
        }
        else if (dp.captionText.text.Equals("CreateExploredMaze"))
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

            int resultCode = db.CreateExploredMaze(id, matrix);

            outputtext.text = "Create Explored Maze Result:" +
                  (resultCode == 1 ? "Success"
                   : "Failure");
        }
        else if (dp.captionText.text.Equals("UpdateMaze"))
        {
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

            int resultCode = db.UpdateMaze(matrix);

            outputtext.text = "Update Maze Result:" +
                  (resultCode == 1 ? "Success"
                   : "Failure");
        }
        else if (dp.captionText.text.Equals("UpdateCoverage"))
        {
            int resultCode = db.UpdateCoverage(float.Parse(text2.text));

            outputtext.text = "Update Maze Coverage Result:" +
                  (resultCode == 1 ? "Success"
                   : "Failure");
        }
        else if (dp.captionText.text.Equals("UpdateTimeTaken"))
        {
            int resultCode = db.UpdateTimeTaken(int.Parse(text2.text));

            outputtext.text = "Update Maze Time Taken Result:" +
                  (resultCode == 1 ? "Success"
                   : "Failure");
        }
        else if (dp.captionText.text.Equals("UpdateMoveHistory"))
        {
            string[] strArray = text2.text.Split(',');
            int resultCode = db.UpdateMoveHistory(strArray);

            outputtext.text = "Update Maze Move History Result:" +
                  (resultCode == 1 ? "Success"
                   : "Failure");
        }
        else if (dp.captionText.text.Equals("UpdatePoints"))
        {
            int resultCode = db.UpdatePoints(int.Parse(text2.text));

            outputtext.text = "Update Maze Points Result:" +
                  (resultCode == 1 ? "Success"
                   : "Failure");
        }
        else if (dp.captionText.text.Equals("SetSensorMatrixById"))
        {
            string[] str = text1.text.Split(',');
            int timestamp = int.Parse(str[0]);
            int id = int.Parse(str[1]);

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

            int resultCode = db.SetSensorMatrixById(timestamp, id, matrix);

            outputtext.text = "Insert Sensor Result:" +
                  (resultCode == 1 ? "Success"
                   : "Failure");
        }
        else if (dp.captionText.text.Equals("GetSensorMatrixById"))
        {
            string[] input = text1.text.Split(',');
            int timestamp = int.Parse(input[0]);
            int id = int.Parse(input[1]);
            int[,] matrix = db.GetSensorMatrixById(id, timestamp);
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
        text1.enabled = true;
        text2.enabled = true;
        switch (value)
        {          
            case 0:    //null
                dataformat.text = string.Empty;
                field1.text = "Input parameter 1:";
                field2.text = "Input parameter 2:";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text1.enabled = false;
                text2.enabled = false;
                break;
            case 1:    //get all maze record
                dataformat.text = "This method has 0 input";
                field1.text = "Do Not Input: ";
                field2.text = "Do Not Input: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text1.enabled = false;
                text2.enabled = false;
                break;
            case 2:    //get maze by id
                dataformat.text = "This method has 1 input: id";
                field1.text = "Id:";
                field2.text = "Do Not Input: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text2.enabled = false;
                break;
            case 3:    //create maze
                dataformat.text = "This method has 2 inputs: id, matrix";
                field1.text = "Id:";
                field2.text = "Matrix: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text2.placeholder.GetComponent<Text>().text = "format: -1,1,1;1,2,1;1,1,-1";
                break;
            case 4:     //update maze matrix
                dataformat.text = "This method has 1 inputs: matrix";
                field1.text = "Do Not Input:";
                field2.text = "Matrix: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text2.placeholder.GetComponent<Text>().text = "format: -1,1,1;1,2,1;1,1,-1";
                text1.enabled = false;
                break;
            case 5:    //update maze coverage
                dataformat.text = "This method has 1 input: maze coverage";
                field1.text = "Do Not Input: ";
                field2.text = "Coverage: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text2.placeholder.GetComponent<Text>().text = "example: 0.8";
                text1.enabled = false;
                break;
            case 6:    //update maze time taken
                dataformat.text = "This method has 1 inputs: maze time taken";
                field1.text = "Do Not Input: ";
                field2.text = "Time Taken: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text2.placeholder.GetComponent<Text>().text = "example: 75";
                text1.enabled = false;
                break;
            case 7:   //update maze move history
                dataformat.text = "This method has 1 input: maze move history";
                field1.text = "Do Not Input: ";
                field2.text = "Move History: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text2.placeholder.GetComponent<Text>().text = "format: North,East,West,South";
                text1.enabled = false;
                break;
            case 8:   //update maze points
                dataformat.text = "This method has 1 input: points";
                field1.text = "Do Not Input: ";
                field2.text = "Points: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text2.placeholder.GetComponent<Text>().text = "example: 1000";
                text1.enabled = false;
                break;
            case 9:    //set sensor
                dataformat.text = "This method has 2 inputs: timestamp and id, matrix";
                field1.text = "Id and TimeStamp:";
                field2.text = "Matrix: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text1.placeholder.GetComponent<Text>().text = "format: 1,20200420";
                text2.placeholder.GetComponent<Text>().text = "format: -1,1,1;1,2,1;1,1,-1";
                break;
            case 10:    //get sensor
                dataformat.text = "This method has 1 input: id and timestamp";
                field1.text = "Id and TimeStamp:";
                field2.text = "Do Not Input: ";
                text1.text = string.Empty;
                text2.text = string.Empty;
                text1.placeholder.GetComponent<Text>().text = "format: 1,20200420";
                text2.enabled = false;
                break;
        }
    }
}
