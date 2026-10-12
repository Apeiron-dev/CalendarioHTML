using CalendarioHTML.Models;
using CalendarioHTML.Services;
using System.Globalization;
using System.Text;
using System.Diagnostics;


namespace CalendarioHTML
{
    internal class Program
    {
        static void Main(string[] args)
        {
            try
            {
                
                Console.WriteLine("Calendario Primeros pasos");

                CultureInfo esCulture = new CultureInfo("es-ES");
                CultureInfo.CurrentCulture = esCulture;
                CultureInfo.DefaultThreadCurrentCulture = esCulture;
                CultureInfo.DefaultThreadCurrentUICulture = esCulture;

                // Paramtros entrada
                int year = 0;
                Console.Write("Indicar Año a Generar: ");
                while (!int.TryParse(Console.ReadLine(), out year))
                {
                    Console.WriteLine("Debe introducir un número válido.");
                    Console.Write("Indicar Año a Generar: ");
                }

                int month = 0;
                Console.Write("Indicar Mes inicial (1-12): ");
                while (!int.TryParse(Console.ReadLine(), out month) || month <1 || month > 12)
                {
                    Console.WriteLine("Debe introducir un número comprendido entre 1 y 12.");
                    Console.Write("Indicar Mes inicial (1-12): ");
                }

                HtmlGenerator htmlGen = new();
                CalendarService service = new(year);
                FileService file = new(year);

                DateTime firstDay = new DateTime(year, month, 1);

                List<CalendarPage> calendarPages = [];
                for (int i = firstDay.Month; i <= 12; i++)
                {
                    DateTime loopFirstDay = new DateTime(year, i, 1);

                    CalendarPage monthPage = new()
                    {
                        //Mes anterior
                        PreviousMonth = service.GenerateMonth(loopFirstDay.AddMonths(-1)),
                        //Mes actual
                        CurrentMonth = service.GenerateMonth(loopFirstDay),
                        //Mes proximo
                        NextMonth = service.GenerateMonth(loopFirstDay.AddMonths(1))
                    };

                    calendarPages.Add(monthPage);
                }

                StringBuilder generatedHTML = htmlGen.Generate(calendarPages, year);

                if (file.SaveCalendarFile(generatedHTML))
                    Console.WriteLine("OK Calendario Generado. \nSe recomienta imprimir usando Edge.");
                else
                    Console.WriteLine("ERROR al generar el calendario.");
                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
        }


    }
}
