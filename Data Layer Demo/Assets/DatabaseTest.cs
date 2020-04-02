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
    public InputField text3;
    public InputField outputtext;
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
        outputtext.text = "success to connect db";
    }

    public void ExecuteSql()
    {
        //Debug.Log(dp.captionText.text);
        if (dp.captionText.text.Equals("GetAllMazeRecord"))
        {
            Debug.Log("1");
            string[][] res = db.GetAllMazeRecord();
            string text = "";
            for(int i =0;i<res[0].Length;i++)
            {
                for (int j = 0; j < res[1].Length; j++)
                {
                    text += res[i][j] + ",";
                }

            }
            outputtext.text = text;
        }
        else if (dp.captionText.text.Equals("GetMazeById"))
        {
            string[][] res = db.GetMazeById(int.Parse(text1.text));
            string text = "";
            for (int i = 0; i < res[0].Length; i++)
            {
                for (int j = 0; j < res[1].Length; j++)
                {
                    text += res[i][j] + ",";
                }

            }
            outputtext.text = text;
        }
/*        else if (dp.value.Equals("InsertMazeRecord"))
        {
            string[] temp = text2.text.Split(',');
            int[] temp2 = Array.ConvertAll<string, int>(temp, int.Parse);
            string[] temp3 = text3.text.Split('],[');
            db.InsertMazeRecord(int.Parse(text1.text), temp2, temp3);
        }*/
        else if (dp.captionText.text.Equals("UpdateMazeDirection"))
        {
            string[] temp = text2.text.Split(',');
            int res = db.UpdateMazeDirection(int.Parse(text1.text),temp);
            outputtext.text = res.ToString();
        }
        else if (dp.captionText.text.Equals("DeleteMazeById"))
        {
            int res = db.DeleteMazeById(int.Parse(text1.text));
            outputtext.text = res.ToString();
        }
/*        else if (dp.value.Equals("getMazeByID"))
        {
            db.getMazeByID(int.Parse(text1.text));
        }*/
        else if (dp.captionText.text.Equals("getPathSize"))
        {
            int[,] res = db.getPathSize(int.Parse(text1.text));
            string text = "";
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < res.GetLength(1); j++)
                {
                    text += res[i,j] + ",";
                }

            }
            outputtext.text = text;
        }
        else if (dp.captionText.text.Equals("getPathByID"))
        {
            int[,] res = db.getPathByID(int.Parse(text1.text));
            string text = "";
            for (int i = 0; i < res.GetLength(0); i++)
            {
                for (int j = 0; j < res.GetLength(1); j++)
                {
                    text += res[i, j] + ",";
                }

            }
            outputtext.text = text;
        }
        else if (dp.captionText.text.Equals("getSensorByID"))
        {
            string res = db.getSensorByID(int.Parse(text1.text));
            outputtext.text = res;
        }
        else if (dp.captionText.text.Equals("getCommandsSize"))
        {
            string[] res = db.getCommandsSize(int.Parse(text1.text));
            outputtext.text = res.ToString();
        }
        else if (dp.captionText.text.Equals("getCommandByID"))
        {
            string[] res = db.getCommandByID(int.Parse(text1.text));
            outputtext.text = res.ToString();
        }
    }

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
                dataformat.text = "0 input";
                break;
            case 1:
                dataformat.text = "1 input id";
                break;
/*            case 2:
                dataformat.text = "3 input id, node, edge";
                break;*/
            case 2:
                dataformat.text = "2 input id, edge";
                break;
            case 3:
                dataformat.text = "1 input id";
                break;
            case 4:
                dataformat.text = "1 input id";
                break;
            case 5:
                dataformat.text = "1 input id";
                break;
            case 6:
                dataformat.text = "1 input id";
                break;
            case 7:
                dataformat.text = "1 input id";
                break;
            case 8:
                dataformat.text = "1 input id";
                break;
        }
    }
}
