using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql
{
    public class SqlEncap
    {
        /// <summary>
        /// Return sql string for select value from one table
        /// </summary>
        /// <param name="selectValue">The value you want select</param>
        /// <param name="tableNme">The table where you select from</param>
        /// <param name="condition">Additional selection criteria</param>
        /// <returns></returns>
        public string Select(List<string>selectValue, string tableName, Dictionary<string,string>condition)
        {
            string result = "";

            result = "SELECT ";

            for(int i = 0; i < selectValue.Count(); i++)
            {
                if(i != 0)
                {
                    result += ",";
                }

                result += selectValue[i];
            }

            result += " FROM " + tableName;

            for(int i = 0; i < condition.Count(); i++)
            {
                if( i == 0)
                {
                    result += " WHERE ";
                }
                else
                {
                    result += " And ";
                }

                result += condition.ElementAt(i).Key + " = " + condition.ElementAt(i).Value;
            }

            result += ";";

            return result;
        }

        /// <summary>
        /// Return sql string for delete value from one table
        /// </summary>
        /// <param name="tableName">The table you delete value from</param>
        /// <param name="condition">Additional selection criteria</param>
        /// <returns></returns>
        public string Delete(string tableName, Dictionary<string, string> condition)
        {
            string result = "";

            result = "DELETE FROM ";
            result += tableName;

            for (int i = 0; i < condition.Count(); i++)
            {
                if (i == 0)
                {
                    result += " WHERE ";
                }
                else
                {
                    result += " And ";
                }

                result += condition.ElementAt(i).Key + " = " + condition.ElementAt(i).Value;
            }

            result += ";";

            return result;
        }

        /// <summary>
        /// Return sql string for insert a new row in table
        /// </summary>
        /// <param name="tableName">The table you want to insert</param>
        /// <param name="columName">The column name in the table</param>
        /// <param name="value">The value of each column</param>
        /// <returns></returns>
        public string Insert(string tableName, List<string> columName, List<string> value)
        {
            string result = "";

            result = "INSERT INTO ";
            result += tableName;

            if(columName.Count() != 0)
            {
                result += " (";
                {
                    for(int i = 0; i < columName.Count(); i++)
                    {
                        if(i == 0)
                        {
                            result += columName[i];
                        }
                        else
                        {
                            result += "," + columName[i];
                        }
                    }
                }
                result += ")";
            }

            result += " VALUES (";

            for(int i = 0; i < value.Count(); i++)
            {
                if (i == 0)
                {
                    result += value[i];
                }
                else
                {
                    result += "," + value[i];
                }
            }

            result += ");";

            return result;
        }

        /// <summary>
        /// Return sql string to update some value in a table
        /// </summary>
        /// <param name="tableName">The table you want update</param>
        /// <param name="setValue">The column name and value which you want update</param>
        /// <param name="condition">Additional selection criteria</param>
        /// <returns></returns>
        public string Update(string tableName, Dictionary<string,string> setValue, Dictionary<string,string> condition)
        {
            string result = "";

            result = "UPDATE ";
            result += tableName;

            for (int i = 0; i < setValue.Count(); i++)
            {
                if (i == 0)
                {
                    result += " SET ";
                }
                else
                {
                    result += ", ";
                }

                result += setValue.ElementAt(i).Key + " = " + setValue.ElementAt(i).Value;
            }

            for (int i = 0; i < condition.Count(); i++)
            {
                if (i == 0)
                {
                    result += " WHERE ";
                }
                else
                {
                    result += " And ";
                }

                result += condition.ElementAt(i).Key + " = " + condition.ElementAt(i).Value;
            }

            result += ";";

            return result;
        }

    }
}
