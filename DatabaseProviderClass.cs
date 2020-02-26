using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer
{
    public class DatabaseProviderClass
    {
        public DatabaseProviderClass(){
            Console.WriteLine("DataLayer connected.");
        }

        public void SetMapStructure(int id, int[][] myMap)
        {
            Console.WriteLine("Store map into the database.");
        }

        public int[][] GetMapStructure(int id)
        {
            Console.WriteLine("Retrieve map from id: " + id + " from database.");
            int[][] foo = new int[][] {
                new int[] {1, 1, 1, 1, 1},
                new int[] {1, 0, 0, 0, 1},
                new int[] {1, 1, 1, 0, 1},
                new int[] {1, 2, 0, 0, 1},
                new int[] {1, 1, 1, 1, 1} };
            return foo;
        }

        public void SetCommands(int id, String[] commands)
        {
            Console.WriteLine("Store commands for future analysis.");
        }

        public String[] GetCommands(int id)
        {
            Console.WriteLine("Retrieve commands from id: " + id + " from database.");
            String[] foo = new String[] { "right", "right", "down", "left", "left" };
            return foo;
        }

        public void SetPathRecords(int id, int[][] paths)
        {
            Console.WriteLine("Store path into the database.");
        }

        public int[][] GetPathRecords(int id)
        {
            Console.WriteLine("Retrieve paths from id: " + id + " from database.");
            int[][] foo = new int[][] {
                 new int[] { 1, 1 },
                 new int[] { 2, 1 },
                 new int[] { 3, 1 },
                 new int[] { 3, 2 },
                 new int[] { 3, 3 },
                 new int[] { 2, 3 },
                 new int[] { 1, 3 }
            };
            return foo;
        }
    }
}

