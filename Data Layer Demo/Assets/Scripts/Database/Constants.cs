﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Database
{
    public static class Constants
    {
        public enum MAZE_OBJECT
        {
            OpenArea = 0, Wall = 1, Starting = 2, Ending = 3, Path = 4, Robot = 5
        }

        public static readonly int RESPONSE_CODE_SUCCESS = 1;
        public static readonly int RESPONSE_CODE_FAILURE = 0;

        public static readonly string DATABASE_NAME = "Rover.db";

        public static readonly string TABLE_MAZE = "Maze";
        public static readonly string TABLE_COMMANDS = "Commands";
        public static readonly string TABLE_PATH = "Path";
        public static readonly string TABLE_SIMULATIONTYPE = "SimulationType";
        public static readonly string TABLE_SENSOR = "Sensor";
        public static readonly string TABLE_ALGORITHMTYPE = "AlgorithmType";
    }
}
