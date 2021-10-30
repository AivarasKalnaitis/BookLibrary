using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class Book
    {
        public string Name { get; init; }
        public string Author { get; init; }
        public string Category { get; init; }
        public string Language { get; init; }
        public DateTime Publication_Date { get; init; }
        public string Isbn { get; init; }
        public bool IsTaken { get; set; }
        public DateTime? Date_Taken { get; set; }
        public DateTime? Date_Returned { get; set; }
    }
}
