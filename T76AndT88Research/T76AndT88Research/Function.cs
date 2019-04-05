using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace T76AndT88Research
{
    class Function
    {
        public string FileName(DateTime date)
        {
            int year = date.Year;
            string strYear = Convert.ToString(year).Substring(2,2);

            int month = date.Month;
            string strMonth = Convert.ToString(month);

            int day = date.Day;
            string strDay = Convert.ToString(day);

            /*****************对月份字符串进行替换，以符合后面的绝对路径的要求**********************/
            if (strMonth == "1")                                                                 
            {
                strMonth = "Jan";
            }
            else if (strMonth == "2")
            {
                strMonth = "Feb";
            }
            else if (strMonth == "3")
            {
                strMonth = "Mar";
            }
            else if (strMonth == "4")
            {
                strMonth = "Apr";
            }
            else if (strMonth == "5")
            {
                strMonth = "May";
            }
            else if (strMonth == "6")
            {
                strMonth = "Jun";
            }
            else if (strMonth == "7")
            {
                strMonth = "Jul";
            }
            else if (strMonth == "8")
            {
                strMonth = "Aug";
            }
            else if (strMonth == "9")
            {
                strMonth = "Sep";
            }
            else if (strMonth == "10")
            {
                strMonth = "Oct";
            }
            else if (strMonth == "11")
            {
                strMonth = "Nov";
            }
            else if (strMonth == "12")
            {
                strMonth = "Dec";
            }
            /***********************************************************************/

            /**********************对1---10的字符串进行替换*************************/
            if (strDay == "1")                                   
            {
                strDay = "01";
            }
            else if (strDay == "2")
            {
                strDay = "02";
            }
            else if (strDay == "3")
            {
                strDay = "03";
            }
            else if (strDay == "4")
            {
                strDay = "04";
            }
            else if (strDay == "5")
            {
                strDay = "05";
            }
            else if (strDay == "6")
            {
                strDay = "06";
            }
            else if (strDay == "7")
            {
                strDay = "07";
            }
            else if (strDay == "8")
            {
                strDay = "08";
            }
            else if (strDay == "9")
            {
                strDay = "09";
            }
            /***********************************************************************/

            string fileName= strYear + "-" + strMonth + "-" + strDay;
            return fileName;
          
        }


        public void String02(string[] textFile, int dut, string Station)
        {
           // string comm;
            string strDay = textFile[0].Substring(7, 10);
            string strTime = textFile[0].Substring(26, 8);
            string strMuxSide = textFile[3].Substring(34, dut);
           // string str = strMuxSide.Substring(0,1);
            string strSerialNumber = textFile[10].Substring(34, 9);
            string firstChar = strSerialNumber.Substring(0, 1);
            string strFail = "";
            string strUnit = "";
            string strFailName = "";
            string strValue = "";
            string strlimitLow = "";
            string strlimitHigh = "";

            

            

            if (firstChar != "1")
            {



                if (textFile[25].Substring(34, 1) != "0")
                {
                    for (int m = 27; m < textFile.Length; m++)
                    {
                        if ((textFile[m]).Contains("FAIL") == true)
                        {
                            strFail = textFile[m];                                   //有FAIL单词的那一行

                            string[] word = strFail.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);    //截取空格之间的字符串
                            strFailName = word[0];                                                                      //使用数组进行取样指定字符串
                            strUnit = word[1];
                            strValue = word[4];
                            strlimitLow = word[2];
                            strlimitHigh = word[3];

                            break;

                        }
                    }
                }

                string x = strDay + " *" + strTime + " *" + Station + " *" + strSerialNumber + " *" + strMuxSide+ " *" + strFailName + "* " + strlimitLow + " *" + strlimitHigh + " *" + strValue + " *" + strUnit;

                FileStream aFile = new FileStream(@"C:\data\Buffer001.txt", FileMode.Append);
                StreamWriter sw = new StreamWriter(aFile);
                sw.WriteLine(x);
                sw.Close();
            }
            
        }
    }
}
