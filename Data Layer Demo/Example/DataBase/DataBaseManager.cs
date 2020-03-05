using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using UnityEngine;
using Mono.Data.Sqlite;

/// <summary>
/// This is database manager class that can access the database.
/// 
/// Author: Jiayan Wang
/// </summary>
public class DataBaseManager : MonoBehaviour
{
    private int id;
    private string filePath;
    private SqliteConnection dbConnection;
    private SqliteCommand dbCommand;
    private SqliteDataReader dataReader;
    //Singleton pattern
    private static DataBaseManager instance;
    public static DataBaseManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.Find("DatabaseManager").GetComponent<DataBaseManager>();
            }
            return instance;
        }
    }

    /// <summary>
    /// connect to the database
    /// </summary>
    void Awake()
    {
        filePath = Application.streamingAssetsPath + "/VisualMath.db";
        try
        {
            dbConnection = new SqliteConnection("Data Source = " + filePath);
            dbConnection.Open();
          //  Debug.Log("success to connect");
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    /// <summary>
    /// set the id of the account
    /// </summary>
    void Start()
    {
        id = -1;
        //Debug.Log(getIdentification("Inst", "654321"));
        //Debug.Log(getIdentification("jw123", "123456"));
        // Debug.Log(getIdentification("jw123", "123456"));
        //createAccount("test", Identification.Student);
        //createAccount("Instructor1", "Inst","123456", Identification.Teacher);
        // Debug.Log(Login("jy"));
        //Debug.Log(getIdentification());
        // UpdateGradeLevel("Sixth");

        DontDestroyOnLoad(this);
    }

    /// <summary>
    /// find id with name
    /// </summary>
    /// <param name= student name generally></param>
    /// <returns>student id </returns>
    private int FindID(string name)
    {
        int res = -1;
        dataReader = ExecuteQuery("SELECT ID FROM ACCOUNT WHERE Name = '" + name + "';");
        if (dataReader.Read())
        {
            res = dataReader.GetInt32(0);
        }
        if (res == -1) throw new System.Exception("no such student name");
        return res;
    }

    /// <summary>
    /// login with name
    /// </summary>
    /// <param name="name"></param>
    /// <returns> true for success login, false for fail to login</returns>
    public bool Login(string name)
    {
        dataReader = ExecuteQuery("SELECT ID,Identification FROM ACCOUNT WHERE Name = '" + name + "';");
        string i = "";
        int grade = -1;
        if (dataReader.Read())
        {
            id = dataReader.GetInt32(0);
            i = dataReader.GetString(1);
        }
        Debug.Log(id);
        if (i == "Student")
        {
            dataReader = ExecuteQuery("SELECT Grade FROM STUDENT WHERE AccountID =  " + id + ";");
            if (dataReader.Read())
            {
                grade = dataReader.GetInt32(0);
            }
        }
        Debug.Log(grade);
        return (id == -1 || grade != 1) ? false : true;
    }
    /// <summary>
    /// login with username and password
    /// </summary>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <returns> true for success login, false for fail to login</returns>
    public bool Login(string userName, string password)
    {
        dataReader = ExecuteQuery("SELECT ID FROM ACCOUNT WHERE Account = '" + userName + "' AND Password = '" + password + "' AND Identification = '"+ LoginManager.Instance.Identification.ToString()+"'; ");
        if (dataReader.Read())
        {
            id = dataReader.GetInt32(0);
        }
        return (id == -1) ? false : true;
    }
    /// <summary>
    /// create account with name, username and password, the identification will be the option that user selected
    /// </summary>
    /// <param name="name"></param>
    /// <param name="userName"></param>
    /// <param name="password"></param>
    /// <param name="Identification"></param>
    public void createAccount(string name, string userName, string password, Identification Identification)
    {
        dataReader = ExecuteQuery("INSERT INTO ACCOUNT(Name,Account,Password,Identification) VALUES ('" + name + "','"+userName+ "','"+password+"','" + Identification + "');");
    }

    /// <summary>
    /// this method only works for student login menu. It can create account only by using name.
    /// </summary>
    /// <param name="name"></param>
    /// <param name="Identification"></param>
    public void createAccount(string name, Identification Identification)
    {
        dataReader = ExecuteQuery("INSERT INTO ACCOUNT(Name,Identification) VALUES ('" + name + "','" + Identification + "');");
    }
    /// <summary>
    /// student can select the grade.
    /// </summary>
    /// <param name="grade"></param>
    public void SelectGradeLevel(int grade)
    {
        dataReader = ExecuteQuery("UPDATE STUDENT SET Grade = '" + grade + "' WHERE AccountID = " + id);
    }
    /// <summary>
    /// teacher can assign the grade for student with the specific student name.
    /// </summary>
    /// <param name="grade"></param>
    /// <param name="studentName"></param>
    public void AssignGradeToStudent(int grade,string studentName)
    {
        int studID = FindID(studentName);
        Debug.Log(grade);
        if (studID != -1)
        {
            dataReader = ExecuteQuery("UPDATE STUDENT SET Grade = '" + grade + "' WHERE AccountID = " + studID);
        }
    }
    /// <summary>
    /// teacher can assign the assignment by using assignment id to the specific student using student name.
    /// </summary>
    /// <param name="studentName"></param>
    /// <param name="AssignmentId"></param>
    public void AssignHwToStudent(string studentName,int AssignmentId)
    {
        int id = FindID(studentName);
        if (id != -1)
        {
            dataReader = ExecuteQuery("UPDATE ASSIGNMENT SET StudentID = " + id + " WHERE ID = " + AssignmentId + ";");
        }
    }

    //public string getAssignment(int id)
    //{
    //    dataReader = ExecuteQuery("SELECT Identification FROM ASSIGNMENT WHERE ID = " + id + ";");
    //    string result = "";
    //    if (dataReader.Read())
    //    {
    //        result = dataReader.GetString(0);
    //    }
    //    return result;
    //}
    public List<ResultInfo> searchName(string name)
    {
        dataReader = ExecuteQuery("SELECT ACCOUNT.ID,Name,Grade,AssignmentID FROM ACCOUNT,STUDENT WHERE ACCOUNT.Identification = 'Student' AND STUDENT.AccountID = ACCOUNT.ID AND ACCOUNT.Name LIKE '%" + name + "%';");
        List<ResultInfo> result = new List<ResultInfo>();
        //int id = -1;
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                //Debug.Log(dataReader.GetInt32(0) + dataReader.GetString(1) + dataReader.GetInt32(2) + dataReader.GetInt32(3));
                ResultInfo res = new ResultInfo(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetInt32(2), dataReader.GetInt32(3));
                result.Add(res);
            }
        }
       
        return result;
    }


    public int getLevel()
    {
        dataReader = ExecuteQuery("SELECT Grade FROM STUDENT WHERE AccountID = " + id + ";");
        int result = -1;
        if (dataReader.Read())
        {
            result = dataReader.GetInt32(0);
        }

        return result;
    }

    /// <summary>
    /// it can get the identification of current login account.
    /// </summary>
    /// <returns>Identification</returns>
    public Identification getIdentification()
    {
        dataReader = ExecuteQuery("SELECT Identification FROM ACCOUNT WHERE ID = " + id + ";");
        string result = "";
        if (dataReader.Read())
        {
            result = dataReader.GetString(0);
        }
        Identification identification = Identification.Student;
        if (result.Equals(Identification.Teacher.ToString()))
            identification = Identification.Teacher;
        return identification;
    }

    public void AddAssignment(int level, string question, string answer)
    {
        dataReader = ExecuteQuery("INSERT INTO ASSIGNMENT(Level,Content,Answer) VALUES (" + level + ",'" + question + "','" + answer + "');");
    }

    public List<Assignment> GetAssignment(int level)
    {
        dataReader = ExecuteQuery("SELECT ID,Level,Content,Answer FROM ASSIGNMENT WHERE Level = " + level + ";");
        List<Assignment> result = new List<Assignment>();
        //int id = -1;
        while (dataReader.HasRows)
        {
            if (dataReader.Read())
            {
                //Debug.Log(dataReader.GetInt32(0) + dataReader.GetString(1) + dataReader.GetInt32(2) + dataReader.GetInt32(3));
                Assignment res = new Assignment(dataReader.GetInt32(0), dataReader.GetInt32(1), dataReader.GetString(2), dataReader.GetString(3));
                result.Add(res);
            }
        }
        
        return result;
    }

    /// <summary>
    /// get name of current login account.
    /// </summary>
    /// <returns>name</returns>
    public string getName()
    {
        dataReader = ExecuteQuery("SELECT Name FROM ACCOUNT WHERE ID = "+ id + ";");
        string result = "";
        if (dataReader.Read())
        {
            result = dataReader.GetString(0);
        }
        return result;
    }

    /// <summary>
    /// it can create the sql script and send it to the database to execute.
    /// </summary>
    /// <param name="queryString"></param>
    /// <returns></returns>
    public SqliteDataReader ExecuteQuery(string queryString)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        Debug.Log(queryString);
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }

    public void ResetID()
    {
        id = -1;
    }

    /// <summary>
    /// close the connection of database
    /// </summary>
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

    /// <summary>
    /// when the program is closed, the databse will automatically closed.
    /// </summary>
    private void OnApplicationQuit()
    {
        CloseConnection();
    }
}

/// <summary>
/// identification of user
/// </summary>
public enum Identification
{
    Student, Teacher
}