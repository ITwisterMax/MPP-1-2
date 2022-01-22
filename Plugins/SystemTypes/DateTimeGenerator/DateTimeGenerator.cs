using System;
using Generators.Model;

namespace Generators.Plugins.SystemTypes
{
    /// <summary>
    ///     DateTime type generator
    /// </summary>
    public class DateTimeGenerator : AbstractGenerator
    {
        /// <summary>
        ///     Public constructor for DateTimeGenerator
        /// </summary>
        public DateTimeGenerator()
        {
            ItemType = typeof(DateTime);
        }

        /// <summary>
        ///     Override abstract method Generate
        /// </summary>
        /// 
        /// <returns>DateTime</returns>
        public override object Generate()
        {
            // Get random year and month
            int year = Random.Next(DateTime.MinValue.Year, DateTime.MaxValue.Year + 1);
            int month = Random.Next(1, 13);

            // Generate DateTime instance
            return new DateTime(
                year,
                month,
                Random.Next(1, DateTime.DaysInMonth(year, month) + 1),
                Random.Next(0, 24),
                Random.Next(0, 60),
                Random.Next(0, 60),
                Random.Next(0, 1000)
            );
        }
    }
}
