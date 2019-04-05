using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace T76AndT88Research
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text;
            string userPassword = textBox2.Text;

            if(userName!="T76ANA")
            {
                MessageBox.Show("请输入正确的用户名！");
                return;
            }else if(userPassword!="ContiANA")
            {
                MessageBox.Show("请输入正确的密码！");
                return;
            }else
            {
                Form1 f1 = new Form1();
                f1.ShowDialog();
                textBox1.Text = "";
                textBox2.Text = "";
            }
        }
    }
}
