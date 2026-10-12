

namespace CalendarioHTML.Models
{
    public class MonthModel
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public string? Name { get; set; }
        public List<WeekModel> Weeks { get; set; } = [];
    }
}
