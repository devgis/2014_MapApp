using System;
using System.Collections.Generic;
using System.Text;
using MapApp.DAL;
using System.Data;

namespace MapApp.BLL
{
    public class JKINFOBLL
    {
        JKINFODAL dalJKINFO;
        public JKINFOBLL()
        {
            dalJKINFO = new JKINFODAL();
        }

        public bool AddJKINFO(MapApp.Entities.JKINFO OBJJKINFO, String MDBFileName)
        {
            return dalJKINFO.AddJKINFO(OBJJKINFO, MDBFileName);
        }

        public MapApp.Entities.JKINFO GetJKINFO(Int32 NDH, String MDBFileName)
        {
            return dalJKINFO.GetJKINFO(NDH, MDBFileName);
        }

        public bool DeleteJKINFO(Int32 NDH, String MDBFileName)
        {
            return dalJKINFO.DeleteJKINFO(NDH, MDBFileName);
        }

        public bool EditJKINFO(MapApp.Entities.JKINFO OBJJKINFO, String MDBFileName)
        {
            return dalJKINFO.EditJKINFO(OBJJKINFO, MDBFileName);
        }

        public DataSet GetList(String MDBFileName, String Where)
        {
            return dalJKINFO.GetList(MDBFileName, Where);
        }

    }
}
