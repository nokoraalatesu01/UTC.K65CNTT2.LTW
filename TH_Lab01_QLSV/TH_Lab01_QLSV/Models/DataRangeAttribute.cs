using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace TH_Lab01_QLSV.Models
{
    public class DateRangeAttribute : ValidationAttribute
    {
        private readonly DateTime _min;
        private readonly DateTime _max;

        // min, max theo định dạng yyyy-MM-dd
        public DateRangeAttribute(string min, string max)
        {
            _min = DateTime.Parse(min, CultureInfo.InvariantCulture);
            _max = DateTime.Parse(max, CultureInfo.InvariantCulture);
        }

        public override bool IsValid(object? value)
        {
            if (value is DateTime d)
                return d.Date >= _min.Date && d.Date <= _max.Date;
            return false;
        }
    }
}