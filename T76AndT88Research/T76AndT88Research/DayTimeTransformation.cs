using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T76AndT88Research
{
    class DayTimeTransformation
    {            
        public DateTime GetDayTime(DateTime Day,DateTime Time)
        {
            DateTime date = new DateTime();

            string year = Convert.ToString(Day.Year);
            string month = Convert.ToString(Day.Month);
            string day = Convert.ToString(Day.Day);

            string hour = Convert.ToString(Time.Hour);
            string minute = Convert.ToString(Time.Minute);
            string second = Convert.ToString(Time.Second);

            string strDayAndTime = year + "/" + month + "/" + day + " " + hour + ":" + minute + ":" + second;
            date = DateTime.Parse(strDayAndTime);
            return date;
        }  
        
        public DateTime GetValueDayTime(string fileDate)
        {
            DateTime date = new DateTime();
            string strDay = fileDate.Substring(7,10);
            string strTime = fileDate.Substring(26,8);
            string strDayTime = strDay + " " + strTime;
            date = DateTime.Parse(strDayTime);
            return date;
        }

    }
}
