using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sql
{
    public class SqlEncap
    {
        public string Select(List<string>selectValue, string tableNme, Dictionary<string,string>condition)
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

            result += " FROM " + tableNme;

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

        public string Delete(string tableNme, Dictionary<string, string> condition)
        {
            string result = "";

            result = "DELETE ";
            result += tableNme;

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

        public string Insert(string tableNme, List<string> columName, List<string> value)
        {
            string result = "";

            result = "INSERT INTO ";
            result += tableNme;

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

            result += " VALUE (";

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

        public string Update(string tableNme, Dictionary<string,string> setValue, Dictionary<string,string> condition)
        {
            string result = "";

            result = "UPDATE ";
            result += tableNme;

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
