using System;
using System.IO;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace DateTimeFormatTests.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp app;
        Platform platform;

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        // TimePicker Partitions (take cartesian product):
        //     Hour Format String: empty, H, HH, h, hh
        //     Minute Format String: empty, m, mm
        //     Second Format String: empty, s, ss
        //     AM/PM Format String: empty, t, tt
        //     Hour: empty, 12 AM, 12 PM, 1-11 AM, 1-11 PM
        //     Minute: empty, 0, 59, 1-58
        //     Separator: none, ".", "/", ":", ",", "-", " "
        //     Other letters: abceijklnopqruvwxABCDEGIJLNOPQRSTUVWXYZ
        // DatePicker Partitions (take cartesian product):
        //     Day Format String: empty, d, dd, ddd, dddd
        //     Month Format String: empty, M, MM, MMM, MMMM
        //     Year Format String: empty, y, yy, yyy, yyyy
        //     Separator: none, ".", "/", ":", ",", "-", " "
        //     Other letters: abceijklnopqruvwxABCDEGIJLNOPQRSTUVWXYZ

        //[Test]
        //public void AppLaunches()
        //{
        //    app.Screenshot("First screen.");
        //}

        // TimePicker Format String: H, m, s, t
        // TimePicker Time: 12 AM, 0 minute
        // Separator: ".", " "
        [Test]
        public void TimePicker24HMidnight()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "H.m.s t");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "0, 0");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("0.0.0 A", text);
        }

        // TimePicker Format String: hh, m, tt
        // TimePicker Time: 12 AM, 0 minute
        // Separator: "/", " "
        [Test]
        public void TimePicker12HMidnight()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "hh m/tt");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "0, 0");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("12 0/AM", text);
        }

        // TimePicker Format String: HH, mm, tt
        // TimePicker Time: 1-11 PM, 1-58 minute (1)
        // Separator: ":", " "
        [Test]
        public void TimePicker24HAfternoon()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "HH:mm tt");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "13, 5");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("13:05 PM", text);
        }

        // TimePicker Format String: h, ss
        // TimePicker Time: 1-11 PM, 59 minute (11)
        // Separator: "-"
        [Test]
        public void TimePicker12HAfternoon()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "h-ss");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "23, 59");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("11-00", text);
        }

        // TimePicker Format String: hh
        // TimePicker Time: 12 PM, 1-58 minute
        // Separator: none
        [Test]
        public void TimePicker12HNoon()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "hh");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "12, 37");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("12", text);
        }

        // TimePicker Format String: HH, tt
        // TimePicker Time: 12 PM, 0 minute
        // Separator: " "
        [Test]
        public void TimePicker24HNoon()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "HH tt");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "12, 0");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("12 PM", text);
        }

        // TimePicker Format String: hh, mm
        // TimePicker Time: 1-11 AM, 59 minute
        // Separator: ":"
        [Test]
        public void TimePicker12HMorning()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "hh:mm");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "7, 59");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("07:59", text);
        }

        // TimePicker Format String: H
        // TimePicker Time: 1-11 AM, 1-58 minute
        // Separator: "."
        [Test]
        public void TimePicker24HMorning()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "H.");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "5, 1");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("5.", text);
        }

        // TimePicker Format String: mm t
        // TimePicker Time: 1 PM, 59 minute
        // Separator: ","
        [Test]
        public void TimePickerNoHour()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "mm, t");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "13, 59");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("59, P", text);
        }

        // TimePicker Format String: first half of uppercase letters
        // TimePicker Time: 11 PM, 59 minute
        [Test]
        public void TimePickerRandomUppercaseBegin()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "ABCDEGIJLNOP");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "23, 59");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("ABCDEGIJLNOP", text);
        }

        // TimePicker Format String: latter half of uppercase letters
        // TimePicker Time: 11 PM, 59 minute
        [Test]
        public void TimePickerRandomUppercaseEnd()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "QRSTUVWXYZ");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "23, 59");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("QRSTUVWXYZ", text);
        }

        // TimePicker Format String: other lowercase letters
        // TimePicker Time: 11 PM, 59 minute
        [Test]
        public void TimePickerRandomLowercaseLetters()
        {
            app.Tap(x => x.Marked("TimePicker"));
            app.ClearText("timeFormatString");
            app.EnterText("timeFormatString", "abceijklnopqruvwx");
            app.PressEnter();
            app.ClearText("settingTime");
            app.EnterText("settingTime", "23, 59");
            app.PressEnter();
            var text = app.WaitForElement("timeClockOptions")[0].Text;
            Assert.AreEqual("abceijklnopqruvwx", text);
        }

        // DatePicker Format String: d, M, y
        // Separator: "/"
        [Test]
        public void DatePickerOneDigit()
        {
            app.ClearText("dateFormatString");
            app.EnterText("dateFormatString", "d/M/y");
            app.PressEnter();
            app.ClearText("settingDate");
            app.EnterText("settingDate", "1999, 1, 31");
            app.PressEnter();
            var text = app.WaitForElement("dateCalendarOptions")[0].Text;
            Assert.AreEqual("31/1/99", text);
        }

        // DatePicker Format String: dd, MM, yy
        // Separator: "-"
        [Test]
        public void DatePickerTwoDigit()
        {
            app.ClearText("dateFormatString");
            app.EnterText("dateFormatString", "MM-dd-yy");
            app.PressEnter();
            app.ClearText("settingDate");
            app.EnterText("settingDate", "2000, 2, 29");
            app.PressEnter();
            var text = app.WaitForElement("dateCalendarOptions")[0].Text;
            Assert.AreEqual("02-29-00", text);
        }

        // DatePicker Format String: ddd, MMM, yyy
        // Separator: ","
        [Test]
        public void DatePickerThreeDigit()
        {
            app.ClearText("dateFormatString");
            app.EnterText("dateFormatString", "yyy, MMM, ddd");
            app.PressEnter();
            app.ClearText("settingDate");
            app.EnterText("settingDate", "2010, 4, 15");
            app.PressEnter();
            var text = app.WaitForElement("dateCalendarOptions")[0].Text;
            Assert.AreEqual("2010, Apr, Thu", text);
        }

        // DatePicker Format String: dddd, MMMM, yyyy
        // Separator: "."
        [Test]
        public void DatePickerFourDigit()
        {
            app.ClearText("dateFormatString");
            app.EnterText("dateFormatString", "MMMM.dddd.yyyy");
            app.PressEnter();
            app.ClearText("settingDate");
            app.EnterText("settingDate", "2015, 8, 1");
            app.PressEnter();
            var text = app.WaitForElement("dateCalendarOptions")[0].Text;
            Assert.AreEqual("August.Saturday.2015", text);
        }

        // DatePicker Format String: MMMM, yy
        // Separator: " "
        [Test]
        public void DatePickerNoDay()
        {
            app.ClearText("dateFormatString");
            app.EnterText("dateFormatString", "MMMM yy");
            app.PressEnter();
            app.ClearText("settingDate");
            app.EnterText("settingDate", "1997, 10, 30");
            app.PressEnter();
            var text = app.WaitForElement("dateCalendarOptions")[0].Text;
            Assert.AreEqual("October 97", text);
        }

        // DatePicker Format String: dddd
        // Separator: none
        [Test]
        public void DatePickerDay()
        {
            app.ClearText("dateFormatString");
            app.EnterText("dateFormatString", "dddd");
            app.PressEnter();
            app.ClearText("settingDate");
            app.EnterText("settingDate", "2020, 7, 20");
            app.PressEnter();
            var text = app.WaitForElement("dateCalendarOptions")[0].Text;
            Assert.AreEqual("Monday", text);
        }

        // DatePicker Format String: MMM, yyyy
        // Separator: " "
        [Test]
        public void DatePickerColon()
        {
            app.ClearText("dateFormatString");
            app.EnterText("dateFormatString", "yyyy: MMM");
            app.PressEnter();
            app.ClearText("settingDate");
            app.EnterText("settingDate", "2002, 12, 31");
            app.PressEnter();
            var text = app.WaitForElement("dateCalendarOptions")[0].Text;
            Assert.AreEqual("2002: Dec", text);
        }

        // TimePicker Format String: first half of uppercase letters
        // TimePicker Time: 11 PM, 59 minute
        [Test]
        public void DatePickerRandomUppercaseBegin()
        {
            app.ClearText("dateFormatString");
            app.EnterText("dateFormatString", "ABCDEGIJLNOP");
            app.PressEnter();
            app.ClearText("settingDate");
            app.EnterText("settingDate", "2002, 12, 31");
            app.PressEnter();
            var text = app.WaitForElement("dateCalendarOptions")[0].Text;
            Assert.AreEqual("ABCDEGIJLNOP", text);
        }

        // TimePicker Format String: latter half of uppercase letters
        // TimePicker Time: 11 PM, 59 minute
        [Test]
        public void DatePickerRandomUppercaseEnd()
        {
            app.ClearText("dateFormatString");
            app.EnterText("dateFormatString", "QRSTUVWXYZ");
            app.PressEnter();
            app.ClearText("settingDate");
            app.EnterText("settingDate", "2002, 12, 31");
            app.PressEnter();
            var text = app.WaitForElement("dateCalendarOptions")[0].Text;
            Assert.AreEqual("QRSTUVWXYZ", text);
        }

        // TimePicker Format String: other lowercase letters
        // TimePicker Time: 11 PM, 59 minute
        [Test]
        public void DatePickerRandomLowercaseLetters()
        {
            app.ClearText("dateFormatString");
            app.EnterText("dateFormatString", "abceijklnopqruvwx");
            app.PressEnter();
            app.ClearText("settingDate");
            app.EnterText("settingDate", "2002, 12, 31");
            app.PressEnter();
            var text = app.WaitForElement("dateCalendarOptions")[0].Text;
            Assert.AreEqual("abceijklnopqruvwx", text);
        }
    }
}
