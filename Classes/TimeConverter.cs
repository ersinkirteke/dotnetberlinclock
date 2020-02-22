using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace BerlinClock
{
    public class TimeConverter : ITimeConverter
    {
        public string convertTime(string aTime)
        {
            TimeSpan timeSpan = new TimeSpan();
            bool isTimeTwentyFourAndCorrectTime = false;

            try
            {
                timeSpan = TimeSpan.ParseExact(aTime, "hh\\:mm\\:ss",
                                            CultureInfo.InvariantCulture);
            }
            catch (OverflowException ex)
            {
                isTimeTwentyFourAndCorrectTime = aTime == "24:00:00";

                if (!isTimeTwentyFourAndCorrectTime)
                    throw ex;
            }

            StringBuilder sb = new StringBuilder();
            int hours = timeSpan.Hours;
            int minutes = timeSpan.Minutes;
            int seconds = timeSpan.Seconds;
            bool yellowLamp = seconds % 2 == 0;

            if (yellowLamp)
                sb.Append("Y");
            else
                sb.Append("O");

            sb.Append("\r\n");

            int hourBelowRedLampCount = hours % 5;
            int hourTopRedLampCount = hours / 5;

            for (int i = 0; i < 4; i++)
            {
                if (i < hourTopRedLampCount || isTimeTwentyFourAndCorrectTime)
                    sb.Append("R");
                else
                    sb.Append("O");
            }

            sb.Append("\r\n");

            for (int i = 0; i < 4; i++)
            {
                if (i < hourBelowRedLampCount || isTimeTwentyFourAndCorrectTime)
                    sb.Append("R");
                else
                    sb.Append("O");
            }

            sb.Append("\r\n");

            int minutesBelowRedLampCount = minutes % 5;
            int quartersCount = minutes / 15;
            int remainMinutes;

            if (minutes < 15)
                remainMinutes = minutes;
            else
                remainMinutes = minutes - quartersCount * 15;

            int yellowMinutesCount = remainMinutes / 5;
            int zeroCount = 11 - quartersCount * 3 - yellowMinutesCount;

            for (int i = 0; i < quartersCount; i++)
            {
                sb.Append("YYR");
            }

            for (int i = 0; i < yellowMinutesCount; i++)
            {
                sb.Append("Y");
            }

            for (int i = 0; i < zeroCount; i++)
            {
                sb.Append("O");
            }

            sb.Append("\r\n");

            for (int i = 0; i < 4; i++)
            {
                if (i < minutesBelowRedLampCount)
                    sb.Append("Y");
                else
                    sb.Append("O");
            }

            return sb.ToString();
        }
    }
}
