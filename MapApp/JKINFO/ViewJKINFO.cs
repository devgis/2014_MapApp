using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapApp.JKINFO
{
    public partial class ViewJKINFO : Form
    {
        public MapApp.Entities.JKINFO OBJJKINFO;
        public ViewJKINFO(MapApp.Entities.JKINFO EditJKINFO)
        {
            InitializeComponent();
            tbADDR.Text = EditJKINFO.ADDR;
            tbBJZ.Text = EditJKINFO.BJZ.ToString();
            tbDDZ.Text = EditJKINFO.DDZ.ToString();
            tbDKH.Text = EditJKINFO.DKH.ToString();
            tbDW.Text = EditJKINFO.DW;
            tbFZH.Text = EditJKINFO.FZH.ToString();
            tbNDH.Text = EditJKINFO.NDH.ToString();
            tbSBLX.Text = EditJKINFO.SBLX.ToString();
            tbSJ.Text = EditJKINFO.SJ;
            tbZT.Text = EditJKINFO.ZT.ToString();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            //OBJJKINFO = new MapApp.Entities.JKINFO();
            //OBJJKINFO.ADDR = tbADDR.Text;
            //OBJJKINFO.DW = tbDW.Text;
            //OBJJKINFO.SJ = tbSJ.Text;
            //try
            //{
            //    OBJJKINFO.BJZ = Convert.ToInt32(tbBJZ.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("����ֵ�趨�����֣�");
            //    tbBJZ.Text = "";
            //    tbBJZ.Focus();
            //    return;
            //}

            //try
            //{
            //    OBJJKINFO.DDZ = Convert.ToInt32(tbDDZ.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("�ϵ�ֵ�趨�����֣�");
            //    tbDDZ.Text = "";
            //    tbDDZ.Focus();
            //    return;
            //}

            //try
            //{
            //    OBJJKINFO.DKH = Convert.ToInt32(tbDKH.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("�˿ں������֣�");
            //    tbDKH.Text = "";
            //    tbDKH.Focus();
            //    return;
            //}

            //try
            //{
            //    OBJJKINFO.FZH = Convert.ToInt32(tbFZH.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("��վ�������֣�");
            //    tbFZH.Text = "";
            //    tbFZH.Focus();
            //    return;
            //}

            //try
            //{
            //    OBJJKINFO.NDH = Convert.ToInt32(tbNDH.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("�ڵ�������֣�");
            //    tbNDH.Text = "";
            //    tbNDH.Focus();
            //    return;
            //}

            //try
            //{
            //    OBJJKINFO.SBLX = Convert.ToInt32(tbSBLX.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("�籸���������֣�");
            //    tbSBLX.Text = "";
            //    tbSBLX.Focus();
            //    return;
            //}

            //try
            //{
            //    OBJJKINFO.ZT = Convert.ToInt32(tbZT.Text);
            //}
            //catch
            //{
            //    MessageBox.Show("״̬�����֣�");
            //    tbZT.Text = "";
            //    tbZT.Focus();
            //    return;
            //}
            this.DialogResult = DialogResult.OK;
        }
    }
}