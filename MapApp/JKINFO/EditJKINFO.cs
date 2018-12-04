using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapApp.JKINFO
{
    public partial class EditJKINFO : Form
    {
        public MapApp.Entities.JKINFO OBJJKINFO;
        public EditJKINFO(MapApp.Entities.JKINFO EditJKINFO)
        {
            InitializeComponent();
            cbSBLX.DataSource = SysInfo.TypeDataTable;
            cbSBLX.ValueMember = "JKType";
            cbSBLX.DisplayMember = "JKTypeName";

            OBJJKINFO = new MapApp.Entities.JKINFO();
            OBJJKINFO.MAPINFO_ID = EditJKINFO.MAPINFO_ID;
            //tbNDH.ReadOnly = true;
            tbNDH.Text = EditJKINFO.NDH.ToString();
            cbSBLX.SelectedValue = EditJKINFO.SBLX;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                OBJJKINFO.NDH = Convert.ToInt32(tbNDH.Text);
            }
            catch
            {
                MessageBox.Show("�ڵ�������֣�");
                tbNDH.Text = "";
                tbNDH.Focus();
                return;
            }

            try
            {
                OBJJKINFO.SBLX = Convert.ToInt32(cbSBLX.SelectedValue);
            }
            catch
            {
                MessageBox.Show("��ѡ���籸���������֣�");
                cbSBLX.Focus();
                return;
            }
            this.DialogResult = DialogResult.OK;
        }
    }
}