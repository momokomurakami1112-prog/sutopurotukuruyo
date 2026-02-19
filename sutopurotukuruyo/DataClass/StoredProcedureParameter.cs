using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sutopurotukuruyo
{
    public class StoredProcedureParameter
    {
        public string? Name { get; set; }
        public string? SqlType { get; set; }
        public string? LengthText { get; set; }
        public bool IsOutput { get; set; }
        public string? Comment { get; set; }
        public string? PrecisionText { get; set; }
        public string? ScaleText { get; set; }
    }
}
