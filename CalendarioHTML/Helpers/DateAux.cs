using System.Globalization;

namespace CalendarioHTML.Helpers
{
    public static class DateAux
    {
        public static string WeekDayName(this DateTime fecha)
        {
            return CultureInfo.CurrentCulture
                              .DateTimeFormat
                              .GetDayName(fecha.DayOfWeek);
        }

        public static string MonthName(this DateTime fecha)
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(CultureInfo.CurrentCulture
                              .DateTimeFormat
                              .GetMonthName(fecha.Month));
        }

        public static bool IsWeekend(this DateTime fecha)
        {
            return fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday;
        }

        // Festivos Nacionales
        public static HashSet<DateTime> GetHolidays(int year)
        {
            return new HashSet<DateTime>()
            {
                new DateTime(year, 1, 1),
                new DateTime(year, 1, 6),
                GetEasterSunday(year).AddDays(-3), // Jueves Santo
                GetEasterSunday(year).AddDays(-2),
                new DateTime(year, 5, 1),
                new DateTime(year, 5, 2), // C. Madrid
                new DateTime(year, 8, 15),
                new DateTime(year, 10, 12),
                new DateTime(year, 11, 1),
                new DateTime(year, 12, 6),
                new DateTime(year, 12, 8),
                new DateTime(year, 12,25)
            };
        }

       /* public static bool IsHoliday(this DateTime fecha)
        {
            try
            {
                List<DateTime> listaFestivos = new List<DateTime>()

                {
                    new DateTime(fecha.Year, 1, 1),
                    new DateTime(fecha.Year, 1, 6),
                    GetEasterSunday(fecha.Year).AddDays(-3), // Jueves Santo
                    GetEasterSunday(fecha.Year).AddDays(-2),
                    new DateTime(fecha.Year, 5, 1),
                    new DateTime(fecha.Year, 5, 2), // C. Madrid
                    new DateTime(fecha.Year, 8, 15),
                    new DateTime(fecha.Year, 10, 12),
                    new DateTime(fecha.Year, 11, 1),
                    new DateTime(fecha.Year, 12, 6),
                    new DateTime(fecha.Year, 12, 8),
                    new DateTime(fecha.Year, 12,25)

                };

                if (listaFestivos.Any(x => x.Date.Equals(fecha.Date)))
                    return true;
                else
                    return false;
            }
            catch
            {
                return false;
            }

        }*/

        // Obtener Domingo de Pascua
        private static DateTime GetEasterSunday(int year)
        {
            int day = 0;
            int month = 0;

            int g = year % 19;
            int c = year / 100;
            int h = (c - (int)(c / 4) - (int)((8 * c + 13) / 25) + 19 * g + 15) % 30;
            int i = h - (int)(h / 28) * (1 - (int)(h / 28) * (int)(29 / (h + 1)) * (int)((21 - g) / 11));

            day = i - ((year + (int)(year / 4) + i + 2 - c + (int)(c / 4)) % 7) + 28;
            month = 3;

            if (day > 31)
            {
                month++;
                day -= 31;
            }

            return new DateTime(year, month, day);
        }

    }
}
