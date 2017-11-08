
using System;

namespace TechniquesAndPatternsExamples.SystemTimeExamples
{
    public static class SystemTime
    {
        private static DateTime date;

        public static void Set(DateTime custom)
        {
            date = custom;
        }

        public static void Reset()
        {
            date = DateTime.MinValue;
        }

        public static DateTime Now
        {
            get
            {
                if (date != DateTime.MinValue)
                {
                    return date;
                }
                return DateTime.Now;
            }
        }
    }
}
