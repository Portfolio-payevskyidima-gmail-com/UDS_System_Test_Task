using System;

namespace CSharpTest
{
    public class WorkDayCalculator : IWorkDayCalculator
    {

        /// <summary>
        /// Date Calculator
        /// </summary>
        /// <param name="startDate">Start date</param>
        /// <param name="dayCount">Count of working days</param>
        /// <param name="weekEnds">Array of ranges of weekends</param>
        /// <returns>Date of last working day</returns>
        public DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds)
        {
            // Current date start from startDate
            DateTime currentDate = startDate;

            // Check weekends by null
            if (weekEnds == null || currentDate.CompareTo(weekEnds[weekEnds.Length - 1].EndDate) > 0)
            {
                return currentDate.AddDays(dayCount - 1);
            }

            // Check by if startdate is equal last day of last weekend in array
            if (currentDate.CompareTo(weekEnds[weekEnds.Length - 1].EndDate) == 0)
            {
                return currentDate.AddDays(dayCount);
            }

            // Init Counters
            int dayCounter = 1;
            int weekEndsCounter = 0;

            // Length of weekends array
            int weekEndsArrLength = weekEnds.Length;

            // Find current weekend range
            WeekEnd currentWeekEnds = weekEnds[weekEndsCounter];
            while (currentDate.CompareTo(currentWeekEnds.EndDate) > 0 && weekEndsCounter + 1 < weekEndsArrLength)
            {
                // Change currentWeekEnds by next
                currentWeekEnds = weekEnds[++weekEndsCounter];
            }

            // Check current date by entering in current weekends
            if (currentDate.CompareTo(currentWeekEnds.StartDate) >= 0)
            {
                // Set currentDate as last day of cuurent weekend + 1 day
                currentDate = currentWeekEnds.EndDate.AddDays(1);

                // Check if exist next weekends
                if (weekEndsCounter + 1 < weekEndsArrLength)
                {
                    currentWeekEnds = weekEnds[++weekEndsCounter];
                }
            }

            

            // Main loop
            while (dayCounter < dayCount)
            {
                // Check current date by currentWeekend
                while (currentDate.Equals(currentWeekEnds.StartDate))
                {
                    // Jump to the last date of weekend to skip it
                    currentDate = currentWeekEnds.EndDate.AddDays(1);

                    // Chang currentWeekends to next if it exist
                    if (weekEndsCounter + 1 < weekEndsArrLength)
                    {
                        //currentDate = currentWeekEnds.EndDate;
                        currentWeekEnds = weekEnds[++weekEndsCounter];
                    }
                }

                // Make step to next daty and increment day counter
                currentDate = currentDate.AddDays(1);
                dayCounter++;
            }
            
            // return result
            return currentDate;
        }
    }
}
