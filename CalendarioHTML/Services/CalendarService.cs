using CalendarioHTML.Helpers;
using CalendarioHTML.Models;

namespace CalendarioHTML.Services
{
    public class CalendarService
    {
        private readonly HashSet<DateTime> _holidays;

        public CalendarService(int year)
        {
            _holidays = DateAux.GetHolidays(year);

            // Miniaturas
            _holidays.UnionWith(DateAux.GetHolidays(year - 1));
            _holidays.UnionWith(DateAux.GetHolidays(year + 1));
        }

        public MonthModel GenerateMonth(DateTime monthFirstDate)
        {
            var monthDates = new MonthModel
            {
                Year = monthFirstDate.Year,
                Month = monthFirstDate.Month,
                Name = monthFirstDate.MonthName()
            };

            var daysInMonth = DateTime.DaysInMonth(monthFirstDate.Year, monthFirstDate.Month);

            var week = new WeekModel();

            //Empezar la semana en lunes
            int offset =  ((int)monthFirstDate.DayOfWeek + 6) % 7;

            //Celdas vaciass previas al dia 1
            for (int i = 0; i < offset; i++)
            {
                week.Days.Add(new DayCell());
            }

            //Celdas con dias del mes
            for (int day = 1; day <= daysInMonth; day++)
            {
                DateTime fecha = new DateTime(monthFirstDate.Year, monthFirstDate.Month, day);
                week.Days.Add(
                    new DayCell
                    {
                        Day = day,
                        Holiday = _holidays.Contains(fecha)
                    });

                //Al llegar al septimo dia
                if (week.Days.Count == 7)
                {
                    //Añadimos la semana a la lista y creamos una nueva
                    monthDates.Weeks.Add(week);
                    week = new();
                }
            }

            //La última semana del mes
            if (week.Days.Any())
            {
                //Añadir celdas vacias hasta el domingo
                while (week.Days.Count < 7)
                {
                    week.Days.Add(new DayCell());
                }
                monthDates.Weeks.Add(week);
            }

            //Si hay mas de 5 semanas
            if (monthDates.Weeks.Count() == 6 )
            {
                //La semana con menos dias
                var shortestWeek = monthDates.Weeks
                    .OrderBy(w => w.Days.Count(d => d.Day.HasValue))
                    .First();

                shortestWeek.IsCompact = true;
            }


            return monthDates;
        }
    }
}
