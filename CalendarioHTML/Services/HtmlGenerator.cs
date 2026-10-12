using CalendarioHTML.Models;
using System.Text;


namespace CalendarioHTML.Services
{
    public class HtmlGenerator
    {
        public StringBuilder Generate(List<CalendarPage> calendarPages, int year)
        {
            try
            {
                StringBuilder htmlCalendar = new StringBuilder();
                htmlCalendar.AppendLine(GenerateHead(year));

                foreach (var page in calendarPages)
                {
                    htmlCalendar.AppendLine(GenerateMonthPage(page));
                }

                htmlCalendar.AppendLine(GenerateEnd());

                return htmlCalendar;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string GenerateHead(int year)
        {
            return $"""
            <!DOCTYPE html>
            <html>
            <head>
                <meta charset="utf-8">
                <link rel="stylesheet" href="calendar.css">
                <meta name="viewport" content="width=device-width, initial-scale=1">
                <link href='https://fonts.googleapis.com/css?family=Montserrat' rel='stylesheet'>
                <link rel="preconnect" href="https://fonts.googleapis.com">
                <link rel="preconnect" href="https://fonts.gstatic.com" crossorigin>
                <link href="https://fonts.googleapis.com/css2?family=Montserrat:wght@300&display=swap" rel="stylesheet">
                <title>Calendario {year}</title>
            </head>

            <body>
            """;
        }

        public string GenerateEnd()
        {
            return """
                </body>
                </html>
                """;
        }

        public string GenerateMonthPage(CalendarPage monthPage)
        {
            StringBuilder htmlMonth = new StringBuilder();
            htmlMonth.AppendLine($"""
                    <section class="panel" id="{monthPage.CurrentMonth.Name.ToLower()}">
                    <div class="top">
                        <div class="title">
                        <span>{monthPage.CurrentMonth.Name} {monthPage.CurrentMonth.Year}</span>
                        </div>
                    
                        <div class="mes siguiente">
                            {GenerateMiniMonth(monthPage.NextMonth, monthPage.CurrentMonth.Year)}
                        </div>
                        <div class="mes anterior">
                            {GenerateMiniMonth(monthPage.PreviousMonth, monthPage.CurrentMonth.Year)}
                        </div>
                    </div>
                    <div class="content">
                    <div class="mes actual">
                    <table>
                    <thead>
                    <tr class="wday">
                        <th>LUNES</th><th>MARTES</th><th>MIERCOLES</th><th>JUEVES</th><th>VIERNES</th><th>SABADO</th><th>DOMINGO</th>
                    </tr>
                    </thead>
                    """);

            /*{(monthPage.CurrentMonth.Weeks.Count() == 4 ? "style=\"margin-bottom: 5em;\"" : string.Empty)}*/

            htmlMonth.AppendLine(GenerateWeeks(monthPage.CurrentMonth));

            htmlMonth.AppendLine("""
                </table>
                </div>
                </div>
                </section>
                """);
                
            return htmlMonth.ToString();
        }

        private string GenerateWeeks(MonthModel monthData)
        {
            StringBuilder htmlWeeks = new StringBuilder();

            htmlWeeks.AppendLine("<tbody>");
            
            foreach (var week in monthData.Weeks)
            {
                htmlWeeks.Append($"<tr{(week.IsCompact ? " class=\"wsmall\"" : string.Empty)}>");
                foreach (var day in week.Days)
                {
                    if(day.Holiday)
                        htmlWeeks.Append($"<td class=\"fest\">{day.Day?.ToString() ?? string.Empty}</td>");
                    else
                    htmlWeeks.Append($"<td>{day.Day?.ToString() ?? string.Empty}</td>");
                }
                htmlWeeks.AppendLine("</tr>");

            }

            htmlWeeks.Append("</tbody>");

            return htmlWeeks.ToString();
        }

        private string GenerateMiniMonth(MonthModel monthData, int currentYear)
        {
            StringBuilder htmlminimonth = new StringBuilder();
            htmlminimonth.Append($"""
                <table>
                <thead><tr><th colspan="7">{monthData.Name}{(monthData.Year != currentYear ? " " + monthData.Year : string.Empty)}</th></tr></thead>
                {GenerateWeeks(monthData)}
                </table>
                """);
            return htmlminimonth.ToString();
        }
    }
}
