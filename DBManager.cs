using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace DatabaseManager
{
    public class DBManager
    {
        public static SQLiteConnection GetSqlconnection()
        {
            SQLiteConnection conn = new SQLiteConnection();
            try
            {
                conn.ConnectionString = "Data Source =Rover.db";
                conn.Open();
                conn.Close();
            }
            catch
            {

                throw new Exception("connect failed");

            }

            return conn;
        }

        //return the line number
        public static int get(string sql)
        {
            SQLiteConnection conn = GetSqlconnection();
            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand(sql, conn);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

        }

        //return the DataTable type of the result. Usually use this.
        public DataTable getDataTable(string sql)
        {
            SQLiteConnection conn = GetSqlconnection();
            try
            {
                conn.Open();
                SQLiteDataAdapter da = new SQLiteDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

        }

        public SQLiteDataReader GetDataReader(string sql)
        {

            SQLiteConnection conn = GetSqlconnection();
            try
            {
                conn.Open();
                SQLiteCommand cmd = new SQLiteCommand();
                return cmd.ExecuteReader(CommandBehavior.CloseConnection);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

        }
      
    }
}
