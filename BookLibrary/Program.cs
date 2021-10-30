using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BookLibrary
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputSelections = new InputSelections();
            var repository = new JsonRepository();
            var bookController = new BookController(repository);

            Console.Write("Select your operation:\n");
            Console.WriteLine(inputSelections.DisplayOperations);

            uint operation = Convert.ToUInt16(Console.ReadLine());

            switch(operation)
            {
                case 1:
                    bookController.ShowAllBooks();
                    Console.WriteLine("\nDo you want to filter the list? Press [y]es, to filter, any other key to exit:");
                    FilteringSelection(inputSelections, bookController);
                    break;
                case 2:
                    var newBook = InputBookInfo(bookController);
                    bookController.AddBook(newBook);
                    break;
                case 3:
                    bookController.TakeBook();
                    break;
                case 4:
                    bookController.ReturnBook();
                    break;
                case 5:
                    bookController.ShowAllBooks();
                    Console.WriteLine("Enter ISBN of book you want to delete:\n");
                    var wantedISBN = Console.ReadLine();
                    bookController.DeleteBook(wantedISBN);
                    break;
                case 6:
                    bookController.AddInitialBooks();
                    bookController.CleanRentersList();
                    break;
                default:
                    Console.WriteLine("Unknown operation. Try again . . .");
                    break;
            }
        }

        private static void FilteringSelection(InputSelections inputSelections, BookController bookController)
        {
            if (Console.ReadLine() == "y")
            {
                Console.WriteLine(inputSelections.DisplayFilters);

                uint filter = Convert.ToUInt16(Console.ReadLine());

                switch (filter)
                {
                    case 1:
                        Console.WriteLine("\nEnter author to filter by:");
                        var author = Console.ReadLine();
                        bookController.FilterByAuthor(author);
                        break;
                    case 2:
                        Console.WriteLine("\nEnter category to filter by:");
                        var category = Console.ReadLine();
                        bookController.FilterByCategory(category);
                        break;
                    case 3:
                        Console.WriteLine("\nEnter language to filter by:");
                        var language = Console.ReadLine();
                        bookController.FilterByLanguage(language);
                        break;
                    case 4:
                        Console.WriteLine("\nEnter ISBN to filter by:");
                        var isbn = Console.ReadLine();
                        bookController.FilterByISBN(isbn);
                        break;
                    case 5:
                        Console.WriteLine("\nEnter name to filter by:");
                        var name = Console.ReadLine();
                        bookController.FilterByName(name);
                        break;
                    case 6:
                        Console.WriteLine("\nEnter [f]alse or [t]rue to filter by availability:");
                        var isTaken = Console.ReadLine();

                        if (isTaken == "f")
                        {
                            bookController.FilterByAvailability(false);
                            break;
                        }
                        else if (isTaken == "t")
                        {
                            bookController.FilterByAvailability(true);
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Wrong input . . .");
                            break;
                        }
                    default:
                        Console.WriteLine("Unknown option . . .");
                        break;
                }
            }
           
        }

        private static Book InputBookInfo(BookController bookController)
        {
            Console.WriteLine("Enter Book Name:");
            var name = Console.ReadLine().ToLower();

            Console.WriteLine("Enter Book Author:");
            var author = Console.ReadLine().ToLower();

            Console.WriteLine("Enter Book Category:");
            var category = Console.ReadLine().ToLower();

            Console.WriteLine("Enter Book Language:");
            var language = Console.ReadLine().ToLower();

            Console.WriteLine("Enter Book Publication Date:");
            var publication_date = Console.ReadLine().ToLower();

            while (true)
            {
                if (DateTime.TryParse(publication_date, out _))
                    break;

                Console.WriteLine("Incorrect date format, try again: ");
                publication_date = Console.ReadLine().ToLower();
            }

            Console.WriteLine("Enter Book ISBN:");
            var isbn = Console.ReadLine().ToLower();

            while (true)
            {
                if (bookController.ValidateNewBookISBN(isbn))
                    break;

                Console.WriteLine("Book with such ISBN already exist, try again:");
                isbn = Console.ReadLine();
            }

            Regex rgx = new(@"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$");

            while (true)
            {
                if (rgx.IsMatch(isbn))
                    break;

                Console.WriteLine("Incorrect ISBN format, try again: ");
                isbn = Console.ReadLine().ToLower();
            }

            var newBook = new Book
            {
                Name = name,
                Author = author,
                Category = category,
                Language = language,
                Publication_Date = DateTime.Parse(publication_date),
                Isbn = isbn
            };

            return newBook;
        }
    }
}
