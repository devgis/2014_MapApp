using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using MapApp.BLL;

namespace MapApp.JKINFO
{
    public partial class JKINFOList : Form
    {
        private String _MDBFileName;
        public JKINFOList(String MDBFileName)
        {
            InitializeComponent();
            _MDBFileName = MDBFileName;
        }

        private void JKINFOList_Load(object sender, EventArgs e)
        {
            JKINFOBLL bllJKINFO = new JKINFOBLL();
            try
            {
                dataGridView1.DataSource = bllJKINFO.GetList(_MDBFileName,"").Tables[0].Copy();
            }
            catch
            {
                MessageBox.Show("加载数据出错！");
            }
        }
    }
}