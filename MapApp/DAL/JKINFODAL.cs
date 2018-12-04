using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace MapApp.DAL
{
    public class JKINFODAL
    {
        public bool AddJKINFO(MapApp.Entities.JKINFO OBJJKINFO,String MDBFileName)
        {
            String SQL=String.Format("INSERT INTO JKINFO (NDH,SBLX)values({0},{1})"
                ,OBJJKINFO.NDH,OBJJKINFO.SBLX);
            return MDBHelp.ExcuteSQL(MDBFileName, SQL);
        }


        public bool EditJKINFO(MapApp.Entities.JKINFO OBJJKINFO, String MDBFileName)
        {
            String SQL = String.Format("UPDATE JKINFO  SET NDH={0},SBLX={1} WHERE MAPINFO_ID ={2}"
                , OBJJKINFO.NDH, OBJJKINFO.SBLX, OBJJKINFO.MAPINFO_ID);
            return MDBHelp.ExcuteSQL(MDBFileName, SQL);
        }

        public MapApp.Entities.JKINFO GetJKINFO(Int32 NDH,String MDBFileName)
        {
            MapApp.Entities.JKINFO OBJJKINFO=new MapApp.Entities.JKINFO();
            String SQL = String.Format("SELECT * FROM JKINFO WHERE NDH={0}"
                , NDH);
            DataSet ds = MDBHelp.GetDataSet(MDBFileName, SQL);
            if (ds == null || ds.Tables.Count <= 0 || ds.Tables[0].Rows.Count <= 0)
            {
                return null;
            }
            else
            {
                OBJJKINFO.ADDR = ds.Tables[0].Rows[0]["ADDR"].ToString();
                try
                {
                    OBJJKINFO.BJZ = Convert.ToInt32(ds.Tables[0].Rows[0]["BJZ"].ToString());
                }
                catch
                { }
                try
                {
                    OBJJKINFO.DDZ = Convert.ToInt32(ds.Tables[0].Rows[0]["DDZ"].ToString());
                }
                catch
                { }
                try
                {
                    OBJJKINFO.DKH = Convert.ToInt32(ds.Tables[0].Rows[0]["DKH"].ToString());
                }
                catch
                { }
                OBJJKINFO.DW = ds.Tables[0].Rows[0]["DW"].ToString();
                try
                {
                    OBJJKINFO.FZH = Convert.ToInt32(ds.Tables[0].Rows[0]["FZH"].ToString());
                }
                catch
                { }
                try
                {
                    OBJJKINFO.MAPINFO_ID = Convert.ToInt32(ds.Tables[0].Rows[0]["MAPINFO_ID"].ToString());
                }
                catch
                { }
                try
                {
                    OBJJKINFO.NDH = Convert.ToInt32(ds.Tables[0].Rows[0]["NDH"].ToString());
                }
                catch
                { }
                try
                {
                    OBJJKINFO.SBLX = Convert.ToInt32(ds.Tables[0].Rows[0]["SBLX"].ToString());
                }
                catch
                { }
                OBJJKINFO.SJ = ds.Tables[0].Rows[0]["SJ"].ToString();
                try
                {
                    OBJJKINFO.ZT = Convert.ToInt32(ds.Tables[0].Rows[0]["ZT"].ToString());
                }
                catch
                { }

                return OBJJKINFO;
            }
        }

        public DataSet GetList(String MDBFileName, String Where)
        {
            String SQL = "SELECT * FROM JKINFO ";
            if(!String.IsNullOrEmpty(Where)&&Where!="*")
            {
                SQL+=" WHERE "+Where;
            }
            return MDBHelp.GetDataSet(MDBFileName, SQL);
        }

        public bool DeleteJKINFO(Int32 NDH, String MDBFileName)
        {
            MapApp.Entities.JKINFO OBJJKINFO = new MapApp.Entities.JKINFO();
            String SQL = String.Format("DELETE FROM JKINFO WHERE NDH={0}"
                , NDH);
            return MDBHelp.ExcuteSQL(MDBFileName, SQL);
        }
    }
}
