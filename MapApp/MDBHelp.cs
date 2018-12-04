using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;

namespace MapApp
{
    public class MDBHelp
    {
        private static String GetConString(String MDBFileName)
        {
            return String.Format( "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};",MDBFileName);
        }


        public static bool ExcuteSQL(String MDBFileName,String SQL)
        {
            OleDbConnection con = new OleDbConnection(GetConString(MDBFileName));
            con.Open();
            try
            {
                OleDbCommand cmd = new OleDbCommand(SQL, con);
                if (cmd.ExecuteNonQuery() > 0)
                {
                    con.Close();
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }

        public static DataSet GetDataSet(String MDBFileName, String SQL)
        {
            try
            {
                OleDbConnection con = new OleDbConnection(GetConString(MDBFileName));
                OleDbCommand cmd = new OleDbCommand(SQL, con);
                OleDbDataAdapter odda = new OleDbDataAdapter(cmd);
                DataSet ds = new DataSet();
                odda.Fill(ds);
                return ds;
            }
            catch
            {
                return null;
            }
        }
    }
}
