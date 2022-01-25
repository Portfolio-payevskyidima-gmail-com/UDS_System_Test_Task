using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace CSharpTest
{
    [TestClass]
    public class WorkDayCalculatorTests
    {

        [TestMethod]
        public void TestNoWeekEnd()
        {
            DateTime startDate = new DateTime(2021, 12, 1);
            int count = 10;

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, null);

            Assert.AreEqual(startDate.AddDays(count-1), result);
        }

        [TestMethod]
        public void TestNormalPath()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25))
            }; 

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendAfterEnd()
        {
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };

            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }

        [TestMethod]
        public void TestWeekendOneDay()
        {
            // Arrange 
            DateTime startDate = new DateTime(2021, 4, 21);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[1]
            {
                new WeekEnd(new DateTime(2021, 4, 23), new DateTime(2021, 4, 23))
            };

            // Act
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            // Asserts
            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 26)));
        }

        [TestMethod]
        public void TestWeekEndConstructor()
        {
            // Arrange 
            ArgumentException exception = null;

            // Act
            try
            {
                WeekEnd weekend = new WeekEnd(new DateTime(2021, 4, 24), new DateTime(2021, 4, 23));
            }
            catch (ArgumentException e)
            {
                exception = e;
            }

            // Assertvs
            Assert.IsNotNull(exception);
        }

        [TestMethod]
        public void TestWeekEndBeforeStartDate()
        {
            // Arrange 
            DateTime startDate = new DateTime(2021, 4, 28);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[3]
            {
                new WeekEnd(new DateTime(2021, 4, 20), new DateTime(2021, 4, 21)),
                new WeekEnd(new DateTime(2021, 4, 28), new DateTime(2021, 4, 28)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };

            // Act
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            // Asserts
            Assert.IsTrue(result.Equals(new DateTime(2021, 5, 4)));
        }

        [TestMethod]
        public void TestWeekEndStartBeforeStartDate()
        {
            // Arrange 
            DateTime startDate = new DateTime(2021, 4, 23);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[3]
            {
                new WeekEnd(new DateTime(2021, 4, 20), new DateTime(2021, 4, 25)),
                new WeekEnd(new DateTime(2021, 4, 28), new DateTime(2021, 4, 28)),
                new WeekEnd(new DateTime(2021, 4, 29), new DateTime(2021, 4, 29))
            };

            // Act
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            // Asserts
            Assert.IsTrue(result.Equals(new DateTime(2021, 5, 2)));
        }

        [TestMethod]
        public void TestWeekEndWeekendChain()
        {
            // Arrange 
            DateTime startDate = new DateTime(2021, 4, 1);
            int count = 10;
            WeekEnd[] weekends = new WeekEnd[3]
            {
                new WeekEnd(new DateTime(2021, 4, 3), new DateTime(2021, 4, 6)),
                new WeekEnd(new DateTime(2021, 4, 7), new DateTime(2021, 4, 7)),
                new WeekEnd(new DateTime(2021, 4, 8), new DateTime(2021, 4, 10))
            };

            // Act
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            // Asserts
            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 18)));
        }

        [TestMethod]
        public void TestAllWeekEndsBeforeStartDate()
        {
            // Arrange 
            DateTime startDate = new DateTime(2021, 4, 20);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 3), new DateTime(2021, 4, 6)),
                new WeekEnd(new DateTime(2021, 4, 7), new DateTime(2021, 4, 7))
            };

            // Act
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            // Asserts
            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 24)));
        }

        [TestMethod]
        public void TestWeekEnd_EndDate_AreEqual_StartDate()
        {
            // Arrange 
            DateTime startDate = new DateTime(2021, 4, 20);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[2]
            {
                new WeekEnd(new DateTime(2021, 4, 3), new DateTime(2021, 4, 6)),
                new WeekEnd(new DateTime(2021, 4, 7), new DateTime(2021, 4, 20))
            };

            // Act
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            // Asserts
            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 25)));
        }

        [TestMethod]
        public void TestWeekEnd_EndDate_AreEqual_StartDate_Than_NormalPath()
        {
            // Arrange 
            DateTime startDate = new DateTime(2021, 4, 20);
            int count = 5;
            WeekEnd[] weekends = new WeekEnd[3]
            {
                new WeekEnd(new DateTime(2021, 4, 3), new DateTime(2021, 4, 6)),
                new WeekEnd(new DateTime(2021, 4, 7), new DateTime(2021, 4, 20)),
                new WeekEnd(new DateTime(2021, 4, 21), new DateTime(2021, 4, 23))
            };

            // Act
            DateTime result = new WorkDayCalculator().Calculate(startDate, count, weekends);

            // Asserts
            Assert.IsTrue(result.Equals(new DateTime(2021, 4, 28)));
        }
    }
}
