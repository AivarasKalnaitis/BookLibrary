using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public class Renter
    {
        public string Name { get; set; }
        public List<Book> RentedBooks { get; set; }

        public Renter(string name, List<Book> rentedBooks)
        {
            Name = name;
            RentedBooks = rentedBooks;
        }
    }
}
