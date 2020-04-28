## Drone-Data-Layer
## SER 574: Advanced Software Design

### Team Member
* Bingrui Feng (rheafeng)
* Huijing Liang (MollyLiang)
* Jiayan Wang (jywang0)
* Meng-Ze Chen (meng-ze)
* Xinkai Wang (w546296781)
* Yu-Ting Tsao (vin20777)

### Purpose
**Provide a data layer library for the other team to use.**

### Documentations
Currently, **NINE** APIs are provided for accessing database (mocking data for now).
Please see inside [the documentation](https://docs.google.com/document/d/1NfsV-xR6hWIHFi-EsfvijzKPTEMaCFKo8wXRwlO2guY/edit#) folder for more detail.


Step 1: Create Database Manager Instance.<br>
```C#
private DataBaseManager dbm;
dbm = new DataBaseManager();
dbm.ConnectToDB("Rover.db");
```
Step 2: Use your desired APIs below.
#### Sensor
1. 
```
public int SetSensorMatrixById(int timestamp, int sensorId, int[,] matrix)
```
For example:<br>
```
int[,] matrix = new int[4, 4] { 
{ 1, 1, 1, 1 }, 
{ 1, 0, 0, 1 }, 
{ 1, 0, 0, 1 }, 
{ 1, 1, 1, 1 } 
};
int resultCode = dbm.SetSensorMatrixById(20200420, 2, matrix);
```

2. 
```
public int[,] GetSensorMatrixById(int sensorId, int timestamp)
```
For example:<br>
```
int[,] matrix = dbm.GetSensorMatrixById(2, 20200420);
```

#### Algorithm
3.
```
public int CreateExploredMaze(int mazeId, int[,] exploredMaze)
```
For example:<br>
```
int mazeId = 3;
int[,] exploredMaze = new int[4, 4] { 
{ 1, 1, -1, -1 }, 
{ 1, 0, 0, -1 }, 
{ 1, 0, 0, 1 }, 
{ 1, 1, 1, 1 } 
};
int resultCode = dbm.CreateExploredMaze(mazeId, exploredMaze);
```

4.
```
public string[][] GetMazeById(int mazeId)
```
For example:<br>
```
int mazeId = 3;
int[,] storedMaze = dbm.GetMazeById(mazeId);
```

5.
```
public int UpdateMaze(int[,] updatedMaze)
```
For example:<br>
```
int mazeId = 3;
int[,] updatedMaze = new int[4, 4] { 
{ 1, 1, 1, 1 }, 
{ 1, 0, 0, 1 }, 
{ 1, 0, 0, 1 }, 
{ 1, 1, 1, 1 } 
};
int resultCode = dbm.UpdateMaze(mazeId, updatedMaze);
```

6.
```
public int UpdateCoverage(float mazeCoverage)
```
For example:<br>
```
float mazeCoverage = 0.4F;
int resultCode = dbm.UpdateCoverage(mazeCoverage);
```

7.
```
public int UpdateTimeTaken(int second)
```
For example:<br>
```
int second = 101;
int resultCode = dbm.UpdateTimeTaken(second);
```

8.
```
public int UpdateMoveHistory(String[] path)
```
For example:<br>
```
String[] path = new String[5] { "East", "East", "North", "East", "South" };
int resultCode = dbm.UpdateMoveHistory(path);
```

9.
```
public int UpdatePoints(int points)
```
For example:<br>
```
int points = 999;
int resultCode = dbm.UpdatePoints(points);
```

### Demo
Please download the demo project to see how to call and use our dll.
Also, the dll is generated from **DatabaseProviderClass.cs** file which provides source code.
<img width="978" alt="截圖 2020-02-25 下午9 23 10" src="https://user-images.githubusercontent.com/31400661/75311787-1bab7380-5815-11ea-97a2-30650d218f4d.png">
