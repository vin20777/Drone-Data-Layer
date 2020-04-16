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
#### Algorithm
1. Create Explored Maze
```C#
public int CreateExploredMaze(int mazeId, int[,] exploredMaze)
```
2. Get Specific Maze
```C#
public int[,] GetMazeById(int mazeId)
```

3. Update Specific Maze
```C#
public int UpdateMazeById(int mazeId, int[,] updatedMaze)
```

4. Update Maze Coverage
```C#
public int UpdateMazeCoverage(int mazeId, float mazeCoverage)
```

5. Update Time Taken
```C#
public int UpdateTimeTaken(int mazeId, int second)
```

6. Update Move History
```C#
public int UpdateMoveHistory(int mazeId, String[ ] path)
```

7. Update Points
```C#
public void UpdatePoints(int mazeId, int points)
```
#### Sensor
8. Set Sensor Matrix
```C#
public int setSensorMatrixById(int timestamp, int sensorId, int[,] matrix)
```

9. Get Sensor Matrix
```C#
public  int[[,]] getSensorMatrixById(int sensorId)
```


### Demo
Please download the demo project to see how to call and use our dll.
Also, the dll is generated from **DatabaseProviderClass.cs** file which provides source code.
<img width="978" alt="截圖 2020-02-25 下午9 23 10" src="https://user-images.githubusercontent.com/31400661/75311787-1bab7380-5815-11ea-97a2-30650d218f4d.png">
