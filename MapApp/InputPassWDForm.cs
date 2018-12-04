using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MapApp
{
    public partial class InputPassWDForm : Form
    {
        public String _PassWord = String.Empty;
        public String PassWord
        {
            get
            {
                return _PassWord;
            }

        }
        public InputPassWDForm()
        {
            InitializeComponent();
        }

        private void btOk_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbPassWord.Text))
            {
                _PassWord = tbPassWord.Text;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBox.Show("«Î ‰»Î√‹¬Î£°");
            }
        }

        private void InputPassWDForm_Load(object sender, EventArgs e)
        {
            tbPassWord.Focus();
        }
    }
}