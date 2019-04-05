using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace T76AndT88Research
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("日期",typeof(string));
            dt.Columns.Add("时间",typeof(string));
            dt.Columns.Add("测试BAY号", typeof(string));
            dt.Columns.Add("串号",typeof(string));
            dt.Columns.Add("测试端口",typeof(string));
            dt.Columns.Add("下线项", typeof(string));
            dt.Columns.Add("最小值", typeof(string));
            dt.Columns.Add("最大值", typeof(string));
            dt.Columns.Add("下线值", typeof(string));
            dt.Columns.Add("单位", typeof(string));

            using (StreamReader read=new StreamReader(@"C:\data\Buffer001.txt",Encoding.Default))
            {
                string line;
                while((line=read.ReadLine())!=null)
                {
                    string[] data = line.Split('*');                  
                    string unit = Convert.ToString(data[9]);
                    string value = Convert.ToString(data[8]);
                    string failName = Convert.ToString(data[5]);
                    string muxSide = Convert.ToString(data[4]);
                    string str = muxSide.Substring(0,1);

                    if(failName=="")
                    {

                    }else if(failName== "CLCHA_Curr4")
                    {

                    }
                    else if(value.Contains("FAILED") == true)
                    {
                        if(unit.Contains("Count")==true)
                        {
                            if(str=="L")
                            {
                                string min = unit.Substring(5, unit.Length - 5);
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = "Left";
                                dr[5] = data[5];
                                dr[6] = min;
                                dr[7] = data[6];
                                dr[8] = data[7];
                                dr[9] = "Count";
                                dt.Rows.Add(dr);
                            }else if(str == "R")
                            {
                                string min = unit.Substring(5, unit.Length - 5);
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = "Right";
                                dr[5] = data[5];
                                dr[6] = min;
                                dr[7] = data[6];
                                dr[8] = data[7];
                                dr[9] = "Count";
                                dt.Rows.Add(dr);
                            }else
                            {
                                string min = unit.Substring(5, unit.Length - 5);
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = data[4];
                                dr[5] = data[5];
                                dr[6] = data[6];
                                dr[7] = data[7];
                                dr[8] = min;
                                dr[9] = "Count";
                                dt.Rows.Add(dr);
                            }
                            
                            
                        }else if(failName == "PN_PRNDLA")
                        {
                            
                            if(str=="L")
                            {
                                string value01 = data[7].Substring(15, data[7].Length - 15);
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = "Left";
                                dr[5] = data[5];
                                dr[6] = data[6];
                                dr[7] = "1000000000.00000";//max
                                dr[8] = value01;//value
                                dr[9] = data[9];//unit
                                dt.Rows.Add(dr);
                            }else if(str=="R")
                            {
                                string value01 = data[7].Substring(15, data[7].Length - 15);
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = "Right";
                                dr[5] = data[5];
                                dr[6] = data[6];
                                dr[7] = "1000000000.00000";//max
                                dr[8] = value01;//value
                                dr[9] = data[9];//unit
                                dt.Rows.Add(dr);
                            }else
                            {
                                string value01 = data[7].Substring(15, data[7].Length - 15);
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = data[4];
                                dr[5] = data[5];
                                dr[6] = data[6];
                                dr[7] = "1000000000.00000";//max
                                dr[8] = value01;//value
                                dr[9] = data[9];//unit
                                dt.Rows.Add(dr);
                            }
                        }else if(failName== "CANH1_CANL1")
                        {
                            if (str == "L")
                            {
                                string value02 = data[7].Substring(15, data[7].Length - 15);
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = "Left";
                                dr[5] = data[5];
                                dr[6] = data[6];
                                dr[7] = "1000000000.00000";//max
                                dr[8] = value02;//value
                                dr[9] = data[9];//unit
                                dt.Rows.Add(dr);
                            }else if(str=="R")
                            {
                                string value02 = data[7].Substring(15, data[7].Length - 15);
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = "Right";
                                dr[5] = data[5];
                                dr[6] = data[6];
                                dr[7] = "1000000000.00000";//max
                                dr[8] = value02;//value
                                dr[9] = data[9];//unit
                                dt.Rows.Add(dr);
                            }else
                            {
                                string value02 = data[7].Substring(15, data[7].Length - 15);
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = data[4];
                                dr[5] = data[5];
                                dr[6] = data[6];
                                dr[7] = "1000000000.00000";//max
                                dr[8] = value02;//value
                                dr[9] = data[9];//unit
                                dt.Rows.Add(dr);
                            }
                        }
                        else
                        {
                            if (str == "L")
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = "Left";
                                dr[5] = data[5];
                                dr[6] = data[6];
                                dr[7] = "N/A";//max
                                dr[8] = data[7];//value
                                dr[9] = data[8];//unit
                                dt.Rows.Add(dr);
                            }
                            else if (str == "R")
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = "Right";
                                dr[5] = data[5];
                                dr[6] = data[6];
                                dr[7] = "N/A";//max
                                dr[8] = data[7];//value
                                dr[9] = data[9];//unit
                                dt.Rows.Add(dr);
                            }
                            else
                            {
                                DataRow dr = dt.NewRow();
                                dr[0] = data[0];
                                dr[1] = data[1];
                                dr[2] = data[2];
                                dr[3] = data[3];
                                dr[4] = data[4];
                                dr[5] = data[5];
                                dr[6] = data[6];
                                dr[7] = "N/A";//max
                                dr[8] = data[7];//value
                                dr[9] = data[9];//unit
                                dt.Rows.Add(dr);
                            }
                        }
                    }
                    else
                    {
                        if(str == "L")
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = data[0];
                            dr[1] = data[1];
                            dr[2] = data[2];
                            dr[3] = data[3];
                            dr[4] = "Left";
                            dr[5] = data[5];
                            dr[6] = data[6];
                            dr[7] = data[7];//max
                            dr[8] = data[8];//value
                            dr[9] = data[9];//unit
                            dt.Rows.Add(dr);
                        }else if(str == "R")
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = data[0];
                            dr[1] = data[1];
                            dr[2] = data[2];
                            dr[3] = data[3];
                            dr[4] = "Right";
                            dr[5] = data[5];
                            dr[6] = data[6];
                            dr[7] = data[7];//max
                            dr[8] = data[8];//value
                            dr[9] = data[9];//unit
                            dt.Rows.Add(dr);
                        }
                        else
                        {
                            DataRow dr = dt.NewRow();
                            dr[0] = data[0];
                            dr[1] = data[1];
                            dr[2] = data[2];
                            dr[3] = data[3];
                            dr[4] = data[4];
                            dr[5] = data[5];
                            dr[6] = data[6];
                            dr[7] = data[7];//max
                            dr[8] = data[8];//value
                            dr[9] = data[9];//unit
                            dt.Rows.Add(dr);
                        }
                            

                    }
                }
            }
            this.dataGridView1.DataSource = dt;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }
}
