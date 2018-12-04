using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapApp.JKINFO
{
    public partial class AddJKINFO : Form
    {
        public MapApp.Entities.JKINFO OBJJKINFO; 
        public AddJKINFO()
        {
            InitializeComponent();
            cbSBLX.DataSource = SysInfo.TypeDataTable;
            cbSBLX.ValueMember = "JKType";
            cbSBLX.DisplayMember = "JKTypeName";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            OBJJKINFO = new MapApp.Entities.JKINFO();

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