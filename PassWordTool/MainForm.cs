using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MD5PassWord
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbPassWord.Text))
            {
                tbMd5.Text = MD5Help.MD5Encrypt(tbPassWord.Text);
            }
            else
            {
                MessageBox.Show("���������룡");
            }
        }

        private void btCopy_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbMd5.Text))
            {
                tbMd5.Copy();
                MessageBox.Show("���Ƴɹ�������Ctrl+V����ճ����");
            }
            else
            {
                MessageBox.Show("���������룡");
            }
        }
    }
}