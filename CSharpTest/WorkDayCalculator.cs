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
            if (weekEnds == null)
            {
                return currentDate.AddDays(dayCount - 1);
            }
            
            // Init Counters
            int dayCounter = 0;
            int weekEndsCounter = 0;

            // Length of weekends array
            int weekEndsArrLength = weekEnds.Length;

            // Current weekend
            WeekEnd currentWeekEnds = weekEnds[weekEndsCounter];

            // Main loop
            while (dayCounter < dayCount)
            {
                // Check current date by currentWeekend
                if (currentDate.Equals(currentWeekEnds.StartDate))
                {
                    // Jump to the last date of weekend to skip it
                    currentDate = currentWeekEnds.EndDate;

                    // Chang currentWeekends to next if it exist
                    if (weekEndsCounter + 1 < weekEndsArrLength)
                    {
                        weekEndsCounter++;
                        currentDate = currentWeekEnds.EndDate;
                        currentWeekEnds = weekEnds[weekEndsCounter];
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
