using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapInfo.Mapping;
using MapApp.Common;
using System.IO;
using MapApp.Entities;
using MapInfo.Geometry;
using MapApp.JKINFO;
using MapInfo.Data;
using MapApp.BLL;
using MapInfo.Styles;
using MapInfo.Tools;
using System.Threading;

namespace MapApp
{
    public partial class MainMap : Form
    {
        public string AdminPassword = String.Empty;
        private Feature SelectedFeature = null;
        private Style sDefault = new MapInfo.Styles.CompositeStyle(new MapInfo.Styles.BitmapPointStyle("CAUT1-32.BMP", BitmapStyles.None, Color.Blue, 10));


        public MainMap()
        {
            InitializeComponent();
            mapControl1.Map.ViewChangedEvent += new MapInfo.Mapping.ViewChangedEventHandler(Map_ViewChangedEvent);
            mapControl1.Tools.FeatureSelected += new FeatureSelectedEventHandler(Tools_FeatureSelected);
            mapControl1.Tools.FeatureAdded += new FeatureAddedEventHandler(Tools_FeatureAdded);
            Map_ViewChangedEvent(this, null);
        }

        void Tools_FeatureAdded(object sender, FeatureAddedEventArgs e)
        {
            //MessageBox.Show(e.ClientCoordinate.X.ToString() + "/" + e.ClientCoordinate.Y.ToString());
            //IGeometryEdit iEditG= e.Feature.GetGeometryEditor();
            SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchIntersectsGeometry(e.Feature, IntersectType.Geometry);
            si.QueryDefinition.Columns = new string[] { "*" };// new string[] { "NDH" }; //null
            IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search("JKINFO", si);
            Feature fEdit = ifs[0];
            fEdit.Geometry = new MapInfo.Geometry.Point(mapControl1.Map.GetDisplayCoordSys(), ConvertTOJWDu(e.ClientCoordinate));
            fEdit.Table.UpdateFeature(fEdit);
            //FeatureLayer flayer = (FeatureLayer)mapControl1.Map.Layers["JKINFO"];
            //flayer.Table.Refresh();
            //FeatureLayer lyrTemp = (FeatureLayer)mapControl1.Map.Layers["JKINFO"];
            //if (lyrTemp != null)
            //{
            //    IFeatureEnumerator fen = (lyrTemp.Table as IFeatureCollection).GetFeatureEnumerator();

            //    int n = 0;
            //    while (fen.MoveNext()) n++;
            //}
            //MessageBox.Show(e.Feature.Centroid.x.ToString());
        }

        void Map_ViewChangedEvent(object sender, MapInfo.Mapping.ViewChangedEventArgs e)
        {
            // Display the zoom level
            Double dblZoom = System.Convert.ToDouble(String.Format("{0:E2}", mapControl1.Map.Zoom.Value));
            if (statusStrip1.Items.Count > 0)
            {
                statusStrip1.Items[0].Text = "缩放: " + dblZoom.ToString() + " " + MapInfo.Geometry.CoordSys.DistanceUnitAbbreviation(mapControl1.Map.Zoom.Unit);
            }
        }

        void Tools_FeatureSelected(object sender, FeatureSelectedEventArgs e)
        {
            try
            {
                IResultSetFeatureCollection fSelectCollection = e.Selection[0];
                SelectedFeature = fSelectCollection[0];
            }
            catch
            {
                SelectedFeature = null;
            }
        }

        private void MainMap_Load(object sender, EventArgs e)
        {
            this.Initialize();
        }

        private void MainMap_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (tRewNew.ThreadState == ThreadState.Running)
                {
                    tRewNew.Abort();
                }

            }
            catch
            { }

            try
            {
                this.Dispose();
            }
            catch
            { }
        }
        private System.Drawing.Point pNodePoint;
        private void mapControl1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                pNodePoint = e.Location;
                if (String.IsNullOrEmpty(_TempMWSFilePath))
                {
                    //地图未打开 右键菜单不激活！
                    return;
                }

                if (String.IsNullOrEmpty(AdminPassword))
                {
                    InputPassWDForm frmIPWDF = new InputPassWDForm();
                    if (frmIPWDF.ShowDialog() == DialogResult.OK)
                    {
                        String ConfigPassword = String.Empty;
                        try
                        {
                            ConfigPassword = System.Configuration.ConfigurationManager.AppSettings["AdminPassWord"].ToString();
                        }
                        catch
                        {
                        }
                        if (String.IsNullOrEmpty(ConfigPassword))
                        {
                            MessageBox.Show("密码配置错误！");
                            return;
                        }
                        if (!MD5Help.MD5Encrypt(frmIPWDF.PassWord).Equals(ConfigPassword))
                        {
                            MessageBox.Show("密码错误！");
                            return;
                        }
                        else
                        {
                            AdminPassword = ConfigPassword;
                            foreach (IMapLayer imapLayer in mapControl1.Map.Layers)
                            {
                                if (imapLayer.Alias.Equals("JKINFO"))
                                {
                                    LayerHelper.SetEditable(imapLayer, true);
                                    LayerHelper.SetInsertable(imapLayer, true);
                                    LayerHelper.SetSelectable(imapLayer, true);
                                }
                                else
                                {
                                    LayerHelper.SetEditable(imapLayer, false);
                                    LayerHelper.SetInsertable(imapLayer, false);
                                    LayerHelper.SetSelectable(imapLayer, false);
                                }
                            }
                            MessageBox.Show("登陆成功！");
                        }
                    }
                    else
                    {
                        return;
                    }
                }
                //mapControl1.ContextMenuStrip = EditContextMenu;
                //MessageBox.Show("登陆成功！");
                EditContextMenu.Show(e.Location);

            }
            else
            {
                //SelectedFeature = null;
            }
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        private void Initialize()
        {
            String MapPath=Application.StartupPath+"\\Maps";
            if (Directory.Exists(MapPath))
            {
                String[] Directories = Directory.GetDirectories(MapPath);
                if (Directories.Length <= 0)
                {
                    throw new Exception("Maps不存在地图文件夹！");
                }
                else
                {
                    cbMaps.Items.Clear();
                    cbMaps.Items.Add("");
                    SysInfo.HTDBInfo=new System.Collections.Hashtable();
                    foreach (String sPath in Directories)
                    {
                        DBInfo dbInfo=new DBInfo();

                        String[] sTemp = sPath.Split('\\');
                        dbInfo.Name = sTemp[sTemp.Length - 1];

                        DirectoryInfo dir = new DirectoryInfo(sPath);
                        FileInfo[] mwsFiles = dir.GetFiles("*.mws");
                        if (mwsFiles.Length <= 0)
                        {
                            continue;
                        }
                        else
                        {
                            dbInfo.MWSPath = sPath + "\\" + mwsFiles[mwsFiles.Length - 1];
                        }

                        dbInfo.MDBPath = sPath + "\\JKINFO.mdb";
                        
                        cbMaps.Items.Add(dbInfo.Name);
                        SysInfo.HTDBInfo.Add(dbInfo.Name, dbInfo);
                    }

                }
            }
            else
            {
                throw new Exception("读取地图路径错误！请检查程序目录下是否存在Maps文件夹！");
            }
        }

        private String _TempMWSFilePath=String.Empty;
        System.Windows.Forms.Timer t = new System.Windows.Forms.Timer();
        /// <summary>
        /// 加载地图
        /// </summary>
        /// <param name="MWSFilePath"></param>
        private void LoadMap(String MWSFilePath)
        {
            t.Stop();

            SelectedFeature = null;
            mapControl1.Map.Layers.Clear();

            String MapPath = MWSFilePath;
            MapWorkSpaceLoader mwsLoader = new MapWorkSpaceLoader(MapPath);
            mapControl1.Map.Load(mwsLoader);

            foreach (IMapLayer imapLayer in mapControl1.Map.Layers)
            {
                if (!imapLayer.Alias.Equals("JKINFO"))
                {
                    try
                    {
                        LayerHelper.SetSelectable(imapLayer, false);

                    }
                    catch
                    { }
                }
                else
                {
                    LayerHelper.SetEditable(imapLayer,false);
                }
            }
            _TempMWSFilePath = MWSFilePath;

            //从mdb文件中更新数据到tab表中
            
            t.Interval = 30000;
            t.Tick += new EventHandler(t_Tick);
            //t.Enabled = true;
            t.Start();

            //SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchWhere("NDH IS NOT NULL");
            ////si.QueryDefinition.Columns = new string[] { "NDH" }; //null
            //if (ifs == null)
            //{
            //    ifs = MapInfo.Engine.Session.Current.Catalog.Search(MapInfo.Engine.Session.Current.Catalog.GetTable("JKINFO"), si);
            //}
            //else
            //{
            //    lock (ifs)
            //    {
            //        ifs = MapInfo.Engine.Session.Current.Catalog.Search(MapInfo.Engine.Session.Current.Catalog.GetTable("JKINFO"), si);
            //    }
            //}

            //try
            //{
            //    if (tRewNew.ThreadState != ThreadState.Running)
            //    {
            //        tRewNew.Start();
            //    }
            //}
            //catch
            //{
            //}
        }
        private void ReNewData()
        {
            FeatureLayer lyrTemp = (FeatureLayer)mapControl1.Map.Layers["JKINFO"];
            if (lyrTemp != null)
            {
                IFeatureEnumerator fen = (lyrTemp.Table as IFeatureCollection).GetFeatureEnumerator();
                while (fen.MoveNext())
                {
                    Feature ftemp = fen.Current;
                    try
                    {
                        Int32 iNDH = -1;
                        if (ftemp["NDH"] == null)
                            continue;
                        try
                        {
                            iNDH = Convert.ToInt32(ftemp["NDH"]);
                            foreach (DataRow dr in dtAllRow.Rows)
                            {
                                try
                                {
                                    if (iNDH == Convert.ToInt32(dr["NDH"]))
                                    {
                                        ftemp["SJ"] = dr["SJ"];
                                        ftemp["DW"] = dr["DW"];
                                        ftemp.Table.UpdateFeature(ftemp);
                                        break;
                                    }
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        {
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("更新中发生错误：" + ex.Message);
                    }
                }
            }
        }
        DataTable dtAllRow;
        void t_Tick(object sender, EventArgs e)
        {

            JKINFOBLL bllJKINFO = new JKINFOBLL();
            String MDBFileName = (SysInfo.HTDBInfo[cbMaps.Text] as DBInfo).MDBPath;
            dtAllRow = bllJKINFO.GetList(MDBFileName, "").Tables[0].Copy();

            if (dtAllRow.Rows.Count <= 0)
            {
                return;
            }

            SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchWhere("1=1");
            si.QueryDefinition.Columns = new string[] { "*" };// new string[] { "NDH" }; //null
            IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search("JKINFO", si);
            foreach (MapInfo.Data.Feature f in ifs)
            {
                try
                {
                    Int32 iNDH = -1;
                    if (f["NDH"] != null)
                    {
                        try
                        {
                            iNDH = Convert.ToInt32(f["NDH"]);
                            foreach (DataRow dr in dtAllRow.Rows)
                            {
                                try
                                {
                                    if (iNDH == Convert.ToInt32(dr["NDH"]))
                                    {
                                        f["SJ"] = dr["SJ"];
                                        f["DW"] = dr["DW"];
                                        f.Table.UpdateFeature(f);
                                        break;
                                    }
                                }
                                catch
                                { }
                            }
                        }
                        catch
                        {
                        }
                    }
                }
                catch //(Exception ex)
                {
                    //MessageBox.Show("更新中发生错误：" + ex.Message);
                }
            }

            //SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchWhere("1=1");
            //si.QueryDefinition.Columns = new string[] { "*" } ;// new string[] { "NDH" }; //null
            //IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search("JKINFO", si);
            //if (ifs.Count <= 0)
            //{
            //    return;
            //}
            //else
            //{
            //    //FeatureLayer lyrTemp = (FeatureLayer)mapControl1.Map.Layers["JKINFO"];
            //    //if (lyrTemp != null)
            //    //{
            //    //    IFeatureEnumerator fen = (lyrTemp.Table as IFeatureCollection).GetFeatureEnumerator();
            //    //    while (fen.MoveNext())
            //    //    {
            //    //        Feature ftemp = fen.Current;
            //    //        try
            //    //        {
            //    //            Int32 iNDH = -1;
            //    //            if (ftemp["NDH"] == null)
            //    //                continue;
            //    //            try
            //    //            {
            //    //                iNDH = Convert.ToInt32(ftemp["NDH"]);
            //    //                foreach (DataRow dr in dtAllRow.Rows)
            //    //                {
            //    //                    try
            //    //                    {
            //    //                        if (iNDH == Convert.ToInt32(dr["NDH"]))
            //    //                        {
            //    //                            ftemp["SJ"] = dr["SJ"];
            //    //                            ftemp["DW"] = dr["DW"];
            //    //                            ftemp.Table.UpdateFeature(ftemp);
            //    //                            break;
            //    //                        }
            //    //                    }
            //    //                    catch
            //    //                    { }
            //    //                }
            //    //            }
            //    //            catch
            //    //            {
            //    //            }
            //    //        }
            //    //        catch (Exception ex)
            //    //        {
            //    //            MessageBox.Show("更新中发生错误：" + ex.Message);
            //    //        }
            //    //    }
            //    //}

            //    for (int i = 0; i < ifs.Count; i++)
            //    {
            //        try
            //        {
            //            Int32 iNDH = -1;
            //            if (ifs[i]["NDH"] != null)
            //            {
            //                try
            //                {
            //                    iNDH = Convert.ToInt32(ifs[i]["NDH"]);
            //                    foreach (DataRow dr in dtAllRow.Rows)
            //                    {
            //                        try
            //                        {
            //                            if (iNDH == Convert.ToInt32(dr["NDH"]))
            //                            {
            //                                ifs[i]["SJ"] = dr["SJ"];
            //                                ifs[i]["DW"] = dr["DW"];
            //                                ifs[i].Table.UpdateFeature(ifs[i]);
            //                                break;
            //                            }
            //                        }
            //                        catch
            //                        { }
            //                    }
            //                }
            //                catch
            //                {
            //                }
            //            }
            //        }
            //        catch //(Exception ex)
            //        {
            //            //MessageBox.Show("更新中发生错误：" + ex.Message);
            //        }

            //    }
            //}

        }

        private void cbMaps_SelectedIndexChanged(object sender, EventArgs e)
        {
            sMapName = cbMaps.Text;
            if (!String.IsNullOrEmpty(cbMaps.Text))
            {
                String MapPath = String.Empty;
                try
                {
                    MapPath = (SysInfo.HTDBInfo[cbMaps.Text] as DBInfo).MWSPath;
                }
                catch
                {
                    MessageBox.Show("加载地图失败？");
                }

                this.LoadMap(MapPath);
            }
        }

        private void mapToolBar1_ButtonClick(object sender, ToolBarButtonClickEventArgs e)
        {
            
            if (e.Button.Name == "tbarButtonRefresh")
            {
                if (!String.IsNullOrEmpty(_TempMWSFilePath))
                {
                    LoadMap(_TempMWSFilePath);
                }
            }
            else if (e.Button.Name == "tbarButtonList")
            {
                if (!String.IsNullOrEmpty(_TempMWSFilePath))
                {
                    String MDBFileName = (SysInfo.HTDBInfo[cbMaps.Text] as DBInfo).MDBPath;
                    JKINFOList frmJKINFOList = new JKINFOList(MDBFileName);
                    frmJKINFOList.ShowDialog();
                }
            }
            else if (e.Button.Name == "mapToolBarAddPoint")
            {
                //mapControl1.Tools.LeftButtonTool = "AddPoint";
            }

        }

        private void mapControl1_MouseMove(object sender, MouseEventArgs e)
        {
            DPoint p31 = ConvertTOJWDu(e.Location);
            this.toolStripStatusLabel2.Text = " X：" + p31.x.ToString() + " " + "Y：" + p31.y.ToString(); 
            //this.toolStripStatusLabel2.Text = " 经度：" + p31.x.ToString() + " " + "纬度：" + p31.y.ToString(); 
        }

        private DPoint ConvertTOJWDu(PointF P)
        {
            DPoint p31 = new DPoint();
            MapInfo.Geometry.DisplayTransform converter = mapControl1.Map.DisplayTransform;
            converter.FromDisplay(P, out p31);
            return p31;
        }

        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            AddJKINFO frmAddJKINFO = new AddJKINFO();
            if (frmAddJKINFO.ShowDialog() == DialogResult.OK)
            {
                Table tEditTable = (mapControl1.Map.Layers["JKINFO"] as FeatureLayer).Table;
                Feature f = new Feature(tEditTable.TableInfo.Columns);
                DPoint dp = ConvertTOJWDu(pNodePoint);
                f.Geometry = new MapInfo.Geometry.Point(mapControl1.Map.GetDisplayCoordSys(), dp);
                //f.GeometryIndex = 4;
                f.Style = sDefault;
                foreach (DataRow dr in SysInfo.TypeDataTable.Rows)
                {
                    int iType = Convert.ToInt32(dr["JKType"]);
                    if (frmAddJKINFO.OBJJKINFO.SBLX == iType)
                    {
                        f.Style = new MapInfo.Styles.CompositeStyle(new MapInfo.Styles.BitmapPointStyle(dr["BMPFileName"].ToString(), BitmapStyles.None, Color.Blue, 10));
                    }
                }
                f["NDH"] = frmAddJKINFO.OBJJKINFO.NDH;
                f["SJ"] = frmAddJKINFO.OBJJKINFO.SJ;
                f["DW"] = frmAddJKINFO.OBJJKINFO.DW;
                MapInfo.Data.Key ftrkey=tEditTable.InsertFeature(f);
                //MessageBox.Show(f.Key.Value.ToString());

                ///查询方法
                //SearchInfo si = MapInfo.Data.SearchInfoFactory.SearchWhere("mi_key='8'");
                //IResultSetFeatureCollection ifs = MapInfo.Engine.Session.Current.Catalog.Search(tEditTable, si); //JKINFO

                //frmAddJKINFO.OBJJKINFO.MAPINFO_ID = Convert.ToInt32(ftrkey.Value);

                JKINFOBLL bllJKINFO= new JKINFOBLL();

                String MDBFileName = (SysInfo.HTDBInfo[cbMaps.Text] as DBInfo).MDBPath;
                if (bllJKINFO.AddJKINFO(frmAddJKINFO.OBJJKINFO, MDBFileName))
                {
                    MessageBox.Show("保存成功！");
                }
                else
                {
                    MessageBox.Show("保存失败！");
                    tEditTable.DeleteFeature(ftrkey);
                }
                
            }
        }

        private void tsmiView_Click(object sender, EventArgs e)
        {
            if (SelectedFeature == null)
            {
                MessageBox.Show("请选择点进行查看！");
            }
            else
            {
                try
                {
                    Int32 iNDH = Convert.ToInt32(SelectedFeature["NDH"]);
                    JKINFOBLL bllJKINFO = new JKINFOBLL();
                    String MDBFileName = (SysInfo.HTDBInfo[cbMaps.Text] as DBInfo).MDBPath;
                    MapApp.Entities.JKINFO oJKINFO = bllJKINFO.GetJKINFO(iNDH, MDBFileName);

                    ViewJKINFO frmViewJKINFO = new ViewJKINFO(oJKINFO);
                    frmViewJKINFO.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("数据异常！");
                }
            }
        }

        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            if (SelectedFeature == null)
            {
                MessageBox.Show("请选择点进行删除！");
            }
            else
            {
                try
                {
                    Int32 iNDH=-1;
                    try 
                    {
                        iNDH = Convert.ToInt32(SelectedFeature["NDH"]);
                    }
                    catch
                    { }
                    Table tEditTable = (mapControl1.Map.Layers["JKINFO"] as FeatureLayer).Table;
                    tEditTable.DeleteFeature(SelectedFeature);
                    SelectedFeature = null;
                    JKINFOBLL bllJKINFO = new JKINFOBLL();
                    String MDBFileName = (SysInfo.HTDBInfo[cbMaps.Text] as DBInfo).MDBPath;
                    bllJKINFO.DeleteJKINFO(iNDH, MDBFileName);
                    MessageBox.Show("删除成功！");
                }
                catch
                {
                    MessageBox.Show("数据异常！");
                }
            }
        }

        private void tsmiEdit_Click(object sender, EventArgs e)
        {
            if (SelectedFeature == null)
            {
                MessageBox.Show("请选择点进行查看！");
            }
            else
            {
                JKINFOBLL bllJKINFO = new JKINFOBLL();
                String MDBFileName = (SysInfo.HTDBInfo[cbMaps.Text] as DBInfo).MDBPath;
                try
                {
                    Int32 iNDH = Convert.ToInt32(SelectedFeature["NDH"]);
                    
                    MapApp.Entities.JKINFO oJKINFO = bllJKINFO.GetJKINFO(iNDH, MDBFileName);

                    EditJKINFO frmEditJKINFO = new EditJKINFO(oJKINFO);
                    if (frmEditJKINFO.ShowDialog() == DialogResult.OK)
                    {
                        if (bllJKINFO.EditJKINFO(frmEditJKINFO.OBJJKINFO, MDBFileName))
                        {
                            SelectedFeature["DW"] = frmEditJKINFO.OBJJKINFO.DW;
                            SelectedFeature["SJ"] = frmEditJKINFO.OBJJKINFO.SJ;

                            SelectedFeature.Style = sDefault;
                            foreach (DataRow dr in SysInfo.TypeDataTable.Rows)
                            {
                                int iType = Convert.ToInt32(dr["JKType"]);
                                if (frmEditJKINFO.OBJJKINFO.SBLX == iType)
                                {
                                    SelectedFeature.Style = new MapInfo.Styles.CompositeStyle(new MapInfo.Styles.BitmapPointStyle(dr["BMPFileName"].ToString(), BitmapStyles.None, Color.Blue, 10));
                                    break;
                                }
                            }

                            SelectedFeature.Table.UpdateFeature(SelectedFeature);
                            MessageBox.Show("保存成功！");
                        }
                        else
                        {
                            MessageBox.Show("保存失败！");
                        }
                    }
                }
                catch
                {
                    AddJKINFO frmAddJKINFO = new AddJKINFO();
                    if (frmAddJKINFO.ShowDialog() == DialogResult.OK)
                    {
                        Table tEditTable = (mapControl1.Map.Layers["JKINFO"] as FeatureLayer).Table;

                        SelectedFeature.Style = sDefault;
                        foreach (DataRow dr in SysInfo.TypeDataTable.Rows)
                        {
                            int iType = Convert.ToInt32(dr["JKType"]);
                            if (frmAddJKINFO.OBJJKINFO.SBLX == iType)
                            {
                                SelectedFeature.Style = new MapInfo.Styles.CompositeStyle(new MapInfo.Styles.BitmapPointStyle(dr["BMPFileName"].ToString(), BitmapStyles.None, Color.Blue, 10));
                            }
                        }
                        SelectedFeature["NDH"] = frmAddJKINFO.OBJJKINFO.NDH;
                        SelectedFeature["SJ"] = frmAddJKINFO.OBJJKINFO.SJ;
                        SelectedFeature["DW"] = frmAddJKINFO.OBJJKINFO.DW;
                        tEditTable.UpdateFeature(SelectedFeature);

                        if (bllJKINFO.AddJKINFO(frmAddJKINFO.OBJJKINFO, MDBFileName))
                        {
                            MessageBox.Show("保存成功！");
                        }
                        else
                        {
                            MessageBox.Show("保存失败！");
                        }

                    }
                }
            }
        }

        Thread tRewNew = new Thread(new ThreadStart(RewNew));
        private static String sMapName = "";
        private static IResultSetFeatureCollection ifs = null;
        private static void RewNew()
        {
            while (true)
            {
                JKINFOBLL bllJKINFO = new JKINFOBLL();
                String MDBFileName = (SysInfo.HTDBInfo[sMapName] as DBInfo).MDBPath;
                DataTable dtAllRow = bllJKINFO.GetList(MDBFileName, "").Tables[0].Copy();

                if (dtAllRow.Rows.Count <= 0)
                {
                    return;
                }

                
                if (ifs.Count <= 0)
                {
                    return;
                }
                else
                {
                    //FeatureLayer lyrTemp = (FeatureLayer)mapControl1.Map.Layers["JKINFO"];
                    //if (lyrTemp != null)
                    //{
                    //    IFeatureEnumerator fen = (lyrTemp.Table as IFeatureCollection).GetFeatureEnumerator();
                    //    while (fen.MoveNext())
                    //    {
                    //        Feature ftemp = fen.Current;
                    //        try
                    //        {
                    //            Int32 iNDH = -1;
                    //            if (ftemp["NDH"] == null)
                    //                continue;
                    //            try
                    //            {
                    //                iNDH = Convert.ToInt32(ftemp["NDH"]);
                    //                foreach (DataRow dr in dtAllRow.Rows)
                    //                {
                    //                    try
                    //                    {
                    //                        if (iNDH == Convert.ToInt32(dr["NDH"]))
                    //                        {
                    //                            ftemp["SJ"] = dr["SJ"];
                    //                            ftemp["DW"] = dr["DW"];
                    //                            ftemp.Table.UpdateFeature(ftemp);
                    //                            break;
                    //                        }
                    //                    }
                    //                    catch
                    //                    { }
                    //                }
                    //            }
                    //            catch
                    //            {
                    //            }
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            MessageBox.Show("更新中发生错误：" + ex.Message);
                    //        }
                    //    }
                    //}




                    for (int i = 0; i < ifs.Count; i++)
                    {
                        Feature ftemp = ifs[i];
                        try
                        {
                            Int32 iNDH = -1;
                            if (ftemp["NDH"] == null)
                                continue;
                            try
                            {
                                iNDH = Convert.ToInt32(ftemp["NDH"]);
                                foreach (DataRow dr in dtAllRow.Rows)
                                {
                                    try
                                    {
                                        if (iNDH == Convert.ToInt32(dr["NDH"]))
                                        {
                                            ftemp["SJ"] = dr["SJ"];
                                            ftemp["DW"] = dr["DW"];
                                            ftemp.Table.UpdateFeature(ftemp);
                                            break;
                                        }
                                    }
                                    catch
                                    { }
                                }
                            }
                            catch
                            {
                            }
                        }
                        catch //(Exception ex)
                        {
                            //MessageBox.Show("更新中发生错误：" + ex.Message);
                        }

                    }
                }
                Thread.Sleep(3000);
            }
        }
    }
}