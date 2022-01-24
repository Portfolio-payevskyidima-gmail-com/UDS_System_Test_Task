using System;

namespace CSharpTest
{
    /// <summary>
    /// Date Calculator
    /// </summary>
    /// <param name="startDate">Start date</param>
    /// <param name="dayCount">Count of working days</param>
    /// <param name="weekEnds">Array of ranges of weekends</param>
    /// <returns>Date of last working day</returns>
    public interface IWorkDayCalculator
    {
        DateTime Calculate(DateTime startDate, int dayCount, WeekEnd[] weekEnds);
    }
}
