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
    public partial class Form1 : Form
    {
        int timer = 3000;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("FLASH");
            comboBox1.Items.Add("ATF");
            comboBox1.Items.Add("HTB");
            comboBox1.Items.Add("QCC");
            comboBox1.Items.Add("QCH");

            comboBox2.Items.Add("Line1");
            comboBox2.Items.Add("Line2");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Start();
            File.WriteAllText(@"C:\data\Buffer001.txt", string.Empty);

            /*********************************path******************************************************/
            string path = @"Z:\Continental_work\T76\";           //进入网盘路径      生产路径@"Y:\data\testdata\"   Z:\Continental_work\T76
            string T76FlashPath = path + @"FLASH\";    //TcmData\BDMFLASH\REPORTS\DUTRESULTS\    FLASH
            string T76ATFPath = path + @"ATF\";           //TcmData\LTCC\REPORTS\DUTRESULTS\     LTCC
            string T76HTBPath = path+ @"HTB\";     //TcmData\EOLHOT\REPORTS\DUTRESULTS
            string T76QCCPath = path+ @"QCC\";    //TcmData\QCC\REPORTS\DUTRESULTS
            string T76QCHPath = path+ @"QCH\";      //TcmData\QCH\REPORTS\DUTRESULTS

            string T88FlashPath = path + @"T88TcmData\BDMFLASH\REPORTS\DUTRESULTS\";
            string T88ATFPath = path+ @"T88TcmData\LTCC\REPORTS\DUTRESULTS\";
            string T88HTBPath = path+ @"T88TcmData\EOLHOT\REPORTS\DUTRESULTS\";
            string T88QCCPath = path+ @"T88TcmData\QCC\REPORTS\DUTRESULTS\";
            string T88QCHpath = path+ @"T88TcmData\QCH\REPORTS\DUTRESULTS\";
            /**********************************************************************************************/


            /**********使用DayTimeTransformation类编写的实例，生成“日期+时间的字符串”********************/
            DayTimeTransformation beginDayTime = new DayTimeTransformation();
            DayTimeTransformation endDayTime = new DayTimeTransformation();
            DateTime begin = beginDayTime.GetDayTime(dateTimePicker1.Value,dateTimePicker2.Value);
            DateTime end = endDayTime.GetDayTime(dateTimePicker3.Value,dateTimePicker4.Value);
            /**********************************************************************************************/


            if(begin>end)
            {
                MessageBox.Show("起始时间不能大于结束时间，请重新选择查询时间段！");
                return;
            }
            else
            {
                DirectoryInfo file = new DirectoryInfo(path);
                if(file.Exists)
                {
                    if(comboBox1.Text=="FLASH")
                    {
                        if (comboBox2.Text == "Line1")
                        {
                            if (T76.Checked && T88.Checked)
                            {
                                MessageBox.Show("不能同时选择T88，T76查询项，请选择其中一个！");
                                return;    
                            }
                            else if ((!T76.Checked) && (!T88.Checked))
                            {
                                MessageBox.Show("T88,T76选项不能为空，请选择其中一个！");
                                return;
                            }
                            else if (T76.Checked && (!T88.Checked))
                            {                                
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function flash = new Function();
                                    string flashFolder=flash.FileName(f);
                                    string fullFlashPath = T76FlashPath + flashFolder;

                                    textBox1.Text = flashFolder;

                                    DirectoryInfo dirFlashPath = new DirectoryInfo(fullFlashPath);

                                    if(dirFlashPath.Exists)
                                    {
                                        string[] flashFile = Directory.GetFiles(fullFlashPath,"*F00",SearchOption.AllDirectories);
                                        int fileNumber = flashFile.Length;

                                        for (int i=0;i<fileNumber;i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(flashFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if(dtm>=begin&&dtm<=end)
                                            {
                                                if (fileLine[1].Contains("T76-FLASH-01")==true)
                                                {
                                                    flash.String02(fileLine, 4, "T76-FLASH-01");
                                                }
                                            }
                                            Application.DoEvents();
                                        }
                                    }                                
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                                Application.DoEvents();
                            }
                            else if((!T76.Checked)&&T88.Checked)
                            {
                                MessageBox.Show("line1线不能选择T88查询项，请重新选择产品！");
                                return;
                            }
                        }                      
                        else if(comboBox2.Text=="Line2")
                        {
                            if (T76.Checked && T88.Checked)
                            {
                                MessageBox.Show("不能同时选择T88，T76查询项，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && (!T88.Checked))
                            {
                                MessageBox.Show("T88,T76选项不能为空，请选择其中一个！");
                                return;
                            }
                            else if (T76.Checked && (!T88.Checked))
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function flash = new Function();
                                    string flashFolder = flash.FileName(f);
                                    string fullFlashPath = T76FlashPath + flashFolder;

                                    textBox1.Text = flashFolder;

                                    DirectoryInfo dirFlashPath = new DirectoryInfo(fullFlashPath);

                                    if (dirFlashPath.Exists)
                                    {
                                        string[] flashFile = Directory.GetFiles(fullFlashPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = flashFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(flashFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("T76-CFLASH-01") == true)
                                                {
                                                    flash.String02(fileLine, 4, "T76-CFLASH-01");
                                                }
                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f3 = new Form2();
                                f3.ShowDialog();
                                Application.DoEvents();
                            }
                            else if ((!T76.Checked) && T88.Checked)
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function flash = new Function();
                                    string flashFolder = flash.FileName(f);
                                    string fullFlashPath = T88FlashPath + flashFolder;

                                    textBox1.Text = flashFolder;

                                    DirectoryInfo dirFlashPath = new DirectoryInfo(fullFlashPath);

                                    if (dirFlashPath.Exists)
                                    {
                                        string[] flashFile = Directory.GetFiles(fullFlashPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = flashFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(flashFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("T76-CFLASH-01") == true)
                                                {
                                                    flash.String02(fileLine, 4, "T76-CFLASH-01");
                                                }
                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                            }
                            Form2 f2 = new Form2();
                            f2.ShowDialog();
                            Application.DoEvents();
                        }
                        else
                        {
                            MessageBox.Show("请选择相应线别！");
                            return;
                        }

                    }else if(comboBox1.Text=="ATF")
                    {
                        if(comboBox2.Text=="Line1")
                        {
                            if(T76.Checked && T88.Checked)
                            {
                                MessageBox.Show("不能同时选择T88，T76查询项，请选择其中一个！");
                                return;
                            }else if((!T76.Checked) && (!T88.Checked))
                            {
                                MessageBox.Show("T88,T76选项不能为空，请选择其中一个！");
                                return;
                            }else if((!T76.Checked) && T88.Checked)
                            {
                                MessageBox.Show("line1线不能选择T88查询项，请重新选择产品！");
                                return;
                            }else if(T76.Checked && (!T88.Checked))
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function atf = new Function();
                                    string atfFolder = atf.FileName(f);
                                    string fullAtfPath = T76ATFPath + atfFolder;

                                    textBox1.Text = atfFolder;

                                    DirectoryInfo dirAtfPath = new DirectoryInfo(fullAtfPath);

                                    if (dirAtfPath.Exists)
                                    {
                                        string[] atfFile = Directory.GetFiles(fullAtfPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = atfFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(atfFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("T76FE-ATF-01") == true)
                                                {
                                                    atf.String02(fileLine, 1, "T76FE-ATF-01");
                                                }
                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                                Application.DoEvents();
                            }

                        }
                        else if(comboBox2.Text=="Line2")
                        {
                            if (T76.Checked && T88.Checked)
                            {
                                MessageBox.Show("不能同时选择T88，T76查询项，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && (!T88.Checked))
                            {
                                MessageBox.Show("T88,T76选项不能为空，请选择其中一个！");
                                return;
                            }
                            else if (T76.Checked && (!T88.Checked))
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function atf = new Function();
                                    string atfFolder = atf.FileName(f);
                                    string fullAtfPath = T76ATFPath + atfFolder;

                                    textBox1.Text = atfFolder;

                                    DirectoryInfo dirAtfPath = new DirectoryInfo(fullAtfPath);

                                    if (dirAtfPath.Exists)
                                    {
                                        string[] atfFile = Directory.GetFiles(fullAtfPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = atfFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(atfFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("te0963-0208") == true)
                                                {
                                                    atf.String02(fileLine, 1, "te0963-0208");
                                                }
                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                                Application.DoEvents();
                            }
                            else if((!T76.Checked) && T88.Checked)
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function atf = new Function();
                                    string atfFolder = atf.FileName(f);
                                    string fullAtfPath = T88ATFPath + atfFolder;

                                    textBox1.Text = atfFolder;

                                    DirectoryInfo dirAtfPath = new DirectoryInfo(fullAtfPath);

                                    if (dirAtfPath.Exists)
                                    {
                                        string[] atfFile = Directory.GetFiles(fullAtfPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = atfFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(atfFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("te0963-0208") == true)
                                                {
                                                    atf.String02(fileLine, 1, "te0963-0208");
                                                }
                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                            }

                        }
                        Application.DoEvents();
                    }
                    else if(comboBox1.Text=="HTB")
                    {
                        if (comboBox2.Text == "Line1")
                        {
                            if (T76.Checked && T88.Checked)
                            {
                                MessageBox.Show("不能同时选择T88，T76查询项，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && (!T88.Checked))
                            {
                                MessageBox.Show("T88,T76选项不能为空，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && T88.Checked)
                            {
                                MessageBox.Show("line1线不能选择T88查询项，请重新选择产品！");
                                return;
                            }
                            else if (T76.Checked && (!T88.Checked))
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function htb = new Function();
                                    string htbFolder = htb.FileName(f);
                                    string fullHtbPath = T76HTBPath + htbFolder;

                                    textBox1.Text = htbFolder;

                                    DirectoryInfo dirHtbPath = new DirectoryInfo(fullHtbPath);

                                    if (dirHtbPath.Exists)
                                    {
                                        string[] htbFile = Directory.GetFiles(fullHtbPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = htbFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(htbFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("T76FE-HTB-01") == true)
                                                {
                                                    htb.String02(fileLine, 1, "T76FE-HTB-01");                                                 
                                                }
                                                else if (fileLine[1].Contains("T76FE-HTB-02") == true)
                                                {
                                                    htb.String02(fileLine, 1, "T76FE-HTB-02");
                                                }
                                                else if (fileLine[1].Contains("TF_HTB3A") == true)
                                                {
                                                    htb.String02(fileLine, 1, "TF_HTB3A");
                                                }
                                                else if (fileLine[1].Contains("TF_HTB4A") == true)
                                                {
                                                    htb.String02(fileLine, 1, "TF_HTB4A");
                                                }
                                                else if (fileLine[1].Contains("TF_HTB5A") == true)
                                                {
                                                    htb.String02(fileLine, 1, "TF_HTB5A");
                                                }
                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                            }
                            Application.DoEvents();
                        }
                        else if (comboBox2.Text == "Line2")
                        {
                            if (T76.Checked && T88.Checked)
                            {
                                MessageBox.Show("不能同时选择T88，T76查询项，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && (!T88.Checked))
                            {
                                MessageBox.Show("T88,T76选项不能为空，请选择其中一个！");
                                return;
                            }
                            else if (T76.Checked && (!T88.Checked))
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function htb = new Function();
                                    string htbFolder = htb.FileName(f);
                                    string fullHtbPath = T76HTBPath + htbFolder;

                                    textBox1.Text = htbFolder;

                                    DirectoryInfo dirHtbPath = new DirectoryInfo(fullHtbPath);

                                    if (dirHtbPath.Exists)
                                    {
                                        string[] htbFile = Directory.GetFiles(fullHtbPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = htbFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(htbFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("te0963-0219") == true)
                                                {
                                                    htb.String02(fileLine, 1, "te0963-0219");
                                                }
                                                else if (fileLine[1].Contains("te0963-0299") == true)
                                                {
                                                    htb.String02(fileLine, 1, "te0963-0299");
                                                }
                                                else if (fileLine[1].Contains("TF_HTB3B") == true)
                                                {
                                                    htb.String02(fileLine, 1, "TF_HTB3B");
                                                }
                                                else if (fileLine[1].Contains("TF_HTB4B") == true)
                                                {
                                                    htb.String02(fileLine, 1, "TF_HTB4B");
                                                }
                                                else if (fileLine[1].Contains("TF_HTB5B") == true)
                                                {
                                                    htb.String02(fileLine, 1, "TF_HTB5B");
                                                }
                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                                Application.DoEvents();
                            }
                            else if ((!T76.Checked) && T88.Checked)
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function htb = new Function();
                                    string htbFolder = htb.FileName(f);
                                    string fullHtbPath = T88HTBPath + htbFolder;

                                    textBox1.Text = htbFolder;

                                    DirectoryInfo dirHtbPath = new DirectoryInfo(fullHtbPath);

                                    if (dirHtbPath.Exists)
                                    {
                                        string[] htbFile = Directory.GetFiles(fullHtbPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = htbFile.Length;
                                       
                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(htbFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("te0963-0219") == true)
                                                {
                                                    htb.String02(fileLine, 1, "te0963-0219");
                                                }
                                                else if (fileLine[1].Contains("te0963-0299") == true)
                                                {
                                                    htb.String02(fileLine, 1, "te0963-0299");
                                                }
                                                else if (fileLine[1].Contains("TF_HTB3B") == true)
                                                {
                                                    htb.String02(fileLine, 1, "TF_HTB3B");
                                                }
                                                else if (fileLine[1].Contains("TF_HTB4B") == true)
                                                {
                                                    htb.String02(fileLine, 1, "TF_HTB4B");
                                                }
                                                else if (fileLine[1].Contains("TF_HTB5B") == true)
                                                {
                                                    htb.String02(fileLine, 1, "TF_HTB5B");
                                                }
                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                            }
                        }
                        Application.DoEvents();
                    }
                    else if(comboBox1.Text=="QCC")
                    {
                        if (comboBox2.Text == "Line1")
                        {
                            if (T76.Checked && T88.Checked)
                            {
                                MessageBox.Show("不能同时选择T88，T76查询项，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && (!T88.Checked))
                            {
                                MessageBox.Show("T88,T76选项不能为空，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && T88.Checked)
                            {
                                MessageBox.Show("line1线不能选择T88查询项，请重新选择产品！");
                                return;
                            }
                            else if (T76.Checked && (!T88.Checked))
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function qcc = new Function();
                                    string qccFolder = qcc.FileName(f);
                                    string fullQccPath = T76QCCPath + qccFolder;

                                    textBox1.Text =qccFolder;

                                    DirectoryInfo dirQccPath = new DirectoryInfo(fullQccPath);

                                    if (dirQccPath.Exists)
                                    {
                                        string[] qccFile = Directory.GetFiles(fullQccPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = qccFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(qccFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("T76QCC_L01") == true)
                                                {
                                                    qcc.String02(fileLine, 1, "T76QCC_L01");
                                                }
                                                else if (fileLine[1].Contains("T76QCC_L02") == true)
                                                {
                                                    qcc.String02(fileLine, 1, "T76QCC_L02");
                                                }
                                                else if (fileLine[1].Contains("T76QCC_L03") == true)
                                                {
                                                    qcc.String02(fileLine, 1, "T76QCC_L03");
                                                }
                                               
                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                            }
                            Application.DoEvents();
                        }
                        else if (comboBox2.Text == "Line2")
                        {
                            if (T76.Checked && T88.Checked)
                            {
                                MessageBox.Show("不能同时选择T88，T76查询项，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && (!T88.Checked))
                            {
                                MessageBox.Show("T88,T76选项不能为空，请选择其中一个！");
                                return;
                            }
                            else if (T76.Checked && (!T88.Checked))
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function qcc = new Function();
                                    string qccFolder = qcc.FileName(f);
                                    string fullQccPath = T76QCCPath + qccFolder;

                                    textBox1.Text = qccFolder;

                                    DirectoryInfo dirQccPath = new DirectoryInfo(fullQccPath);

                                    if (dirQccPath.Exists)
                                    {
                                        string[] qccFile = Directory.GetFiles(fullQccPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = qccFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(qccFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("CT76QCC_L01") == true)
                                                {
                                                    qcc.String02(fileLine, 1, "CT76QCC_L01");
                                                }
                                                else if (fileLine[1].Contains("CT76QCC_L02") == true)
                                                {
                                                    qcc.String02(fileLine, 1, "CT76QCC_L02");
                                                }
                                                else if (fileLine[1].Contains("CT76QCC_L03") == true)
                                                {
                                                    qcc.String02(fileLine, 1, "CT76QCC_L03");
                                                }

                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                                Application.DoEvents();
                            }
                            else if ((!T76.Checked) && T88.Checked)
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function qcc = new Function();
                                    string qccFolder = qcc.FileName(f);
                                    string fullQccPath = T88QCCPath + qccFolder;

                                    textBox1.Text = qccFolder;

                                    DirectoryInfo dirQccPath = new DirectoryInfo(fullQccPath);

                                    if (dirQccPath.Exists)
                                    {
                                        string[] qccFile = Directory.GetFiles(fullQccPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = qccFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(qccFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("TF_QCT1B") == true)
                                                {
                                                    qcc.String02(fileLine, 1, "TF_QCT1B");
                                                }
                                                else if (fileLine[1].Contains("TF_QCT3B") == true)
                                                {
                                                    qcc.String02(fileLine, 1, "TF_QCT3B");
                                                }
                                                else if (fileLine[1].Contains("TF_QCT5B") == true)
                                                {
                                                    qcc.String02(fileLine, 1, "TF_QCT5B");
                                                }

                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                            }
                        }
                        Application.DoEvents();
                    }
                    else if(comboBox1.Text=="QCH")
                    {
                        if (comboBox2.Text == "Line1")
                        {
                            if (T76.Checked && T88.Checked)
                            {
                                MessageBox.Show("不能同时选择T88，T76查询项，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && (!T88.Checked))
                            {
                                MessageBox.Show("T88,T76选项不能为空，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && T88.Checked)
                            {
                                MessageBox.Show("line1线不能选择T88查询项，请重新选择产品！");
                                return;
                            }
                            else if (T76.Checked && (!T88.Checked))
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function qch = new Function();
                                    string qchFolder = qch.FileName(f);
                                    string fullQchPath = T76QCHPath + qchFolder;

                                    textBox1.Text = qchFolder;

                                    DirectoryInfo dirQchPath = new DirectoryInfo(fullQchPath);

                                    if (dirQchPath.Exists)
                                    {
                                        string[] qchFile = Directory.GetFiles(fullQchPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = qchFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(qchFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("T76QCH_L01") == true)
                                                {
                                                    qch.String02(fileLine, 1, "T76QCH_L01");
                                                }
                                                else if (fileLine[1].Contains("T76QCH_L02") == true)
                                                {
                                                    qch.String02(fileLine, 1, "T76QCH_L02");
                                                }
                                                else if (fileLine[1].Contains("T76QCH_L03") == true)
                                                {
                                                    qch.String02(fileLine, 1, "T76QCH_L03");
                                                }

                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                            }
                            Application.DoEvents();
                        }
                        else if (comboBox2.Text == "Line2")
                        {
                            if (T76.Checked && T88.Checked)
                            {
                                MessageBox.Show("不能同时选择T88，T76查询项，请选择其中一个！");
                                return;
                            }
                            else if ((!T76.Checked) && (!T88.Checked))
                            {
                                MessageBox.Show("T88,T76选项不能为空，请选择其中一个！");
                                return;
                            }
                            else if (T76.Checked && (!T88.Checked))
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function qch = new Function();
                                    string qchFolder = qch.FileName(f);
                                    string fullQchPath = T76QCHPath + qchFolder;

                                    textBox1.Text = qchFolder;

                                    DirectoryInfo dirQchPath = new DirectoryInfo(fullQchPath);

                                    if (dirQchPath.Exists)
                                    {
                                        string[] qchFile = Directory.GetFiles(fullQchPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = qchFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(qchFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("CT76QCH_L01") == true)
                                                {
                                                    qch.String02(fileLine, 1, "CT76QCH_L01");
                                                }
                                                else if (fileLine[1].Contains("CT76QCH_L02") == true)
                                                {
                                                    qch.String02(fileLine, 1, "CT76QCH_L02");
                                                }
                                                else if (fileLine[1].Contains("CT76QCH_L03") == true)
                                                {
                                                    qch.String02(fileLine, 1, "CT76QCH_L03");
                                                }

                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                                Application.DoEvents();
                            }
                            else if ((!T76.Checked) && T88.Checked)
                            {
                                DateTime dt = new DateTime();
                                DateTime f = new DateTime();
                                DateTime dtm = new DateTime();
                                DayTimeTransformation dttf = new DayTimeTransformation();
                                dt = begin;
                                while (dt >= begin && dt <= end)
                                {
                                    dt = dt.AddDays(1);
                                    f = dt.AddDays(-1);
                                    Function qch = new Function();
                                    string qchFolder = qch.FileName(f);
                                    string fullQchPath = T88QCHpath + qchFolder;

                                    textBox1.Text = qchFolder;

                                    DirectoryInfo dirQchPath = new DirectoryInfo(fullQchPath);

                                    if (dirQchPath.Exists)
                                    {
                                        string[] qchFile = Directory.GetFiles(fullQchPath, "*F00", SearchOption.AllDirectories);
                                        int fileNumber = qchFile.Length;

                                        for (int i = 0; i < fileNumber; i++)
                                        {
                                            progressBar1.Visible = true;
                                            progressBar1.Maximum = fileNumber;
                                            progressBar1.Minimum = 0;
                                            progressBar1.Value = i;

                                            textBox2.Text = Convert.ToString(i);
                                            string[] fileLine = File.ReadAllLines(qchFile[i]);
                                            dtm = dttf.GetValueDayTime(fileLine[0]);
                                            if (dtm >= begin && dtm <= end)
                                            {
                                                if (fileLine[1].Contains("TF_QCT2B") == true)
                                                {
                                                    qch.String02(fileLine, 1, "TF_QCT2B");
                                                }
                                                else if (fileLine[1].Contains("TF_QCT4B") == true)
                                                {
                                                    qch.String02(fileLine, 1, "TF_QCT4B");
                                                }
                                                else if (fileLine[1].Contains("TF_QCT6B") == true)
                                                {
                                                    qch.String02(fileLine, 1, "TF_QCT6B");
                                                }

                                            }
                                            Application.DoEvents();
                                        }
                                    }
                                }
                                Form2 f2 = new Form2();
                                f2.ShowDialog();
                                Application.DoEvents();
                            }
                        }
                        Application.DoEvents();
                    }
                    else
                    {
                        MessageBox.Show("请选择相应的测试站位！");
                        return;
                    }
                    Application.DoEvents();

                }
                else
                {
                    MessageBox.Show("请检查网盘链接路径！");
                    return;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer--;
            textBox3.Text = Convert.ToString(timer);
            if(timer==0)
            {
                timer1.Stop();
                MessageBox.Show("时间到，请重新输入用户名和密码！");
                this.Close();
            }
        }
    }
}
