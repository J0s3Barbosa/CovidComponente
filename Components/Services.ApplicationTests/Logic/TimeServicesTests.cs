using Microsoft.VisualStudio.TestTools.UnitTesting;
using Services.Application.Services;
using System;

namespace Services.Application.Logic.Tests
{
    [TestClass()]
    public class TimeServicesTests
    {
        readonly string timeZone = "bras";
        const string dateFormat = "dd/MM/yyyy";
        const string dateTimeFormat = "dd/MM/yyyy HH:mm:ss";
        const string timeFormat = "HH:mm:ss";

        public TimeServicesTests()
        {
        }

        [TestMethod()]
        public void DiffInTimeTest()
        {
            var timeZone = "bras";

            string start = TimeServices.DateTimeFormated(timeZone, DateTime.Now.AddHours(-1)).ToString(dateTimeFormat);
            string finish = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToString(dateTimeFormat);


            var time = TimeServices.GetDiffTime(start, finish);

            Assert.IsNotNull(time);
            Assert.AreEqual("01:00:00", time);

        }
        [TestMethod()]
        public void FixDiffInTimeTest()
        {
            var time = TimeServices.GetDiffTime("10:53 pm", "01:40 am");

            Assert.IsNotNull(time);
            Assert.AreNotEqual("-22:13:00", time);
            Assert.AreEqual("02:13:00", time);

        }

        [TestMethod()]
        public void DiffInTimeFailTest()
        {
            var timeZone = "Lisboa";

            string start = TimeServices.DateTimeFormated(timeZone, DateTime.Now.AddHours(-1)).ToShortTimeString();
            string finish = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToShortTimeString();
            var time = TimeServices.GetDiffTime(start, finish);
            Action ac = () =>
            {
                Assert.AreEqual("02:00:00", time);
              
            };

            Assert.IsNotNull(time);
            Assert.ThrowsException<AssertFailedException>(ac);
        }

        [TestMethod()]
        public void DiffInTimeArgumentExceptionTest()
        {
            var timeZone = "Lisboa";

            string start = TimeServices.DateTimeFormated(timeZone, DateTime.Now.AddHours(-1)).ToShortTimeString();
            string finish = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToShortTimeString();
            var time = TimeServices.GetDiffTime(start, finish);
            Action ac = () =>
            {
                if (time != "02:00:00")
                {
                    throw new ArgumentException("not the same");
                }
            };

            Assert.IsNotNull(time);
            Assert.ThrowsException<ArgumentException>(ac);
        }

        //before 56
        // after refact 60
        [TestMethod()]
        public void Adding_two_spentTimes_Test()
        {

            var timeZone = "Lisboa";

            string start = TimeServices.DateTimeFormated(timeZone, DateTime.Now.AddHours(-1)).ToShortTimeString();
            string finish = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToShortTimeString();
            var firstTimeSpent1 = TimeServices.GetDiffTime(start, finish);

            string starting = TimeServices.DateTimeFormated(timeZone, DateTime.Now.AddHours(-3)).ToShortTimeString();
            string finishinh = TimeServices.DateTimeFormated(timeZone, DateTime.Now).ToShortTimeString();

            var timeSpentTwo = TimeServices.GetDiffTime(starting, finishinh);

            var result = TimeServices.AddingTime(firstTimeSpent1, timeSpentTwo);

            Assert.IsNotNull(result);
            Assert.AreEqual("01:00:00", firstTimeSpent1);
            Assert.AreEqual("03:00:00", timeSpentTwo);
            Assert.AreEqual("04:00:00", result);

        }

      


        [TestMethod()]
        public void ChangingCultureTimeTest()
        {

            var timeZone = "Lisboa";

            var result = TimeServices.DateTimeFormated(timeZone, DateTime.Now);

            Assert.IsNotNull(result);

        }
      

        [TestMethod()]
        public void LocalCultureTimeTest()
        {

            System.Globalization.CultureInfo customCulture = new System.Globalization.CultureInfo("en-US", true);

            customCulture.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd h:mm tt";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
            System.Threading.Thread.CurrentThread.CurrentUICulture = customCulture;

            DateTime newDate = System.Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd h:mm tt"));


            Assert.IsNotNull(newDate);

        }

    }
}