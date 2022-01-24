using System;

namespace CSharpTest
{
    public class WeekEnd
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public WeekEnd(DateTime startDate, DateTime endDate)
        {
            // Check range of dates and throw exception
            if (startDate.CompareTo(endDate) > 0)
            {
                throw new ArgumentException("Start date should be earlier than end date");
            }
            StartDate = startDate;
            EndDate = endDate;
        }
    }
}
