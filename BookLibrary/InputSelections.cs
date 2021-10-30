using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    class InputSelections
    {
        public string DisplayOperations
        {
            get { return displayOperations; }
        }

        public string DisplayFilters
        {
            get { return displayFilters; }
        }

        private static string displayOperations = "[1] Show all books\t[2] Add book\t\t[3] Take book\n[4] Return book\t\t[5] Delete book\t\t" +
                                                    "[6-Developer] Add initial books and Clean renters list";

        private static string displayFilters = "[1] Filter by author\t[2] Filter by category \t" +
                                                 "[3] Filter by language\n[4] Filter by ISBN " +
                                                 "\t[5] Filter by name\t[6] Filter by taken or available\n";
    }
}
