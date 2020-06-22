
using System;
using System.Globalization;
using System.Linq;

namespace Services.Application.Services
{
    public static class TimeServices
    {
        const string timeZone = "bras";
        const string dateFormat = "dd/MM/yyyy";
        const string dateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        const string timeFormat = "HH:mm:ss";


        /// <summary>
        /// get the time difference between two datetimes
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public static string GetDiffTime(string start, string finish)
        {
            CultureInfo cultures = new CultureInfo("pt-BR");

            return Convert.ToDateTime(finish, cultures).Subtract(Convert.ToDateTime(start, cultures)).ToString();
        }
        /// <summary>
        /// get the day and time difference between two datetimes
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public static string GetDiffDays(string start, string finish)
        {
            try
            {
                CultureInfo cultures = new CultureInfo("pt-BR");
                var res = string.Empty;
                var dateOne = Convert.ToDateTime(start, cultures);
                var dateTwo = Convert.ToDateTime(finish, cultures);
                var diff = dateTwo.Subtract(dateOne);
                var days = (int)(diff.TotalDays % 365);
                var _days = days < 30 ? days : days % 30;
                var _hour = diff.TotalHours < 24 ? diff.TotalHours : diff.TotalHours % 24;
                if (days != 0)
                {
                    res = string.Format("{0} year(s) {1} month(s) {2} day(s) {3} Hour(s) : {4} Min(s) : {5} Secs", days / 365,
days / 30, _days, _hour, diff.Minutes, diff.Seconds);

                }
                else
                {
                    res = diff.ToString();
                }

                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// get the time diff and sums 
        /// </summary>
        /// <param name="start"></param>
        /// <param name="finish"></param>
        /// <returns></returns>
        public static string AddingTime(string start, string finish)
        {
            DateTime finishing = Convert.ToDateTime(finish);

            System.TimeSpan durationTimeTwo = new System.TimeSpan(finishing.Hour, finishing.Minute, finishing.Second);

            return Convert.ToDateTime(start).Add(durationTimeTwo).ToString(timeFormat);

        }
        /// <summary>
        /// converts DateTime acordangly to timeZoneId
        /// </summary>
        /// <param name="timeZoneId"></param>
        /// <param name="datetime"></param>
        /// <returns></returns>
        public static DateTime DateTimeFormated(string timeZone, DateTime datetime)
        {
            return TimeZoneInfo.ConvertTimeBySystemTimeZoneId(datetime, GetTimeZoneInfo(timeZone).Id);
        }
        /// <summary>
        /// Get Time Zone Info by parts of the timezone name, Ex: Brasi
        /// if null returns local timeZone
        /// </summary>
        /// <param name="timeZone"></param>
        /// <returns></returns>
        public static TimeZoneInfo GetTimeZoneInfo(string timeZone)
        {
            return TimeZoneInfo.GetSystemTimeZones()
                .FirstOrDefault(x => x.DisplayName.Contains(timeZone, StringComparison.OrdinalIgnoreCase))
             ?? TimeZoneInfo.Local;

        }

    }
}
