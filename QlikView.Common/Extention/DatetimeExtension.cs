using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QlikView.Common
{
    public static class DatetimeExtension
    {
        public static int DayOfWeek(this DateTime date)
        {
            if (date.DayOfWeek == System.DayOfWeek.Sunday)
                return 7;
            else
                return (int)date.DayOfWeek + 1;
        }

        /// <summary>
        /// Week Of Year For EF
        /// </summary>
        /// <param name="dtime"></param>
        /// <returns></returns>
        public static int WeekOfYear(this DateTime dtime)
        {
            int weeknum = 0;

            DateTime beginDate = new DateTime(dtime.Year, 1, 1);
            DateTime endDate = new DateTime(dtime.Year, 12, 31);

            //weeknum = (dtime.DayOfYear - dtime.DayOfWeek() - (7 - beginDate.DayOfWeek() + 1)) / 7 + 2;

            if (dtime.DayOfYear - beginDate.DayOfYear < 7)
                weeknum = 1;
            else
                weeknum = (dtime.DayOfYear - dtime.DayOfWeek() - (7 - beginDate.DayOfWeek() + 1)) / 7 + 2;

            return weeknum;
        }

        public static string WeekOfYearString(this DateTime dtime)
        {
            DateTime endDate = new DateTime(dtime.Year, 12, 31);

            return string.Format("{0}WK{1}", dtime.Year, dtime.WeekOfYear());

            //if ((endDate.DayOfYear - dtime.DayOfYear) < 7)
            //    return string.Format("{0}WK{1}", dtime.Year + 1, dtime.WeekOfYear());
            //else
            //    return string.Format("{0}WK{1}", dtime.Year, dtime.WeekOfYear()); 
        }

        public static DateTime LastWeekendDate(this DateTime date)
        {
            if (date.DayOfWeek == System.DayOfWeek.Sunday)
                return date.Date.AddDays(-7);
            else
                return date.Date.AddDays(0 - (int)date.DayOfWeek);
        }

        public static DateTime LastTwoWeeksEndDate(this DateTime date)
        {
            if (date.DayOfWeek == System.DayOfWeek.Sunday)
                return date.Date.AddDays(-14);
            else
                return date.Date.AddDays(0 - (int)date.DayOfWeek - 7);
        }
    }
}
