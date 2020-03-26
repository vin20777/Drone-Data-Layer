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
    }
}
