using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;

namespace BookLibrary
{
    public class BookController
    {
        private readonly IJsonRepository _jsonRepository;

        public BookController(IJsonRepository jsonRepository)
        {
            _jsonRepository = jsonRepository;
        }

        public void ShowAllBooks()
        {
            var books = _jsonRepository.GetAllBooks();

            ShowBooks(books);
        }

        public void ShowBooks(List<Book> books)
        {
            if (books != null && books.Count != 0)
            {
                var dashLine = new string('-', 177);

                Console.WriteLine(dashLine);
                Console.WriteLine(string.Format("| {0,-50} | {1,-20} | {2,-20} | {3,-10} | {4,-25} | {5,-20} | {6,-10} |", "Name", "Author", "Category", "Language", "Publication Date", "ISBN", "Is Taken?"));
                Console.WriteLine(dashLine);

                foreach (var book in books)
                {
                    Console.WriteLine(string.Format("| {0,-50} | {1,-20} | {2,-20} | {3,-10} | {4,-25} | {5,-20} | {6,-10} |", book.Name, book.Author, book.Category, book.Language, book.Publication_Date, book.Isbn, book.IsTaken));
                    Console.WriteLine(dashLine);
                }
            }

            else
                Console.WriteLine("No books were found . . .");
        }

        public void AddInitialBooks()
        {
            var books = _jsonRepository.GetInitialBooks();

            var json = JsonConvert.SerializeObject(books, Formatting.Indented);

            File.WriteAllText(@"books.json", json.ToString());
        }

        public void AddBook(Book newBook)
        {
            var books = _jsonRepository.GetAllBooks();

            books.Add(newBook);

            var json = JsonConvert.SerializeObject(books, Formatting.Indented).ToString();

            File.WriteAllText(@"books.json", json);
        }

        public void FilterByAuthor(string author)
        {
            var books = _jsonRepository.GetBooksByAuthor(author);
            ShowBooks(books);
        }

        public void FilterByCategory(string category)
        {
            ShowBooks(_jsonRepository.GetBooksByCategory(category));
        }

        public void FilterByLanguage(string language)
        {
            ShowBooks(_jsonRepository.GetBooksByLanguage(language));
        }

        public void FilterByISBN(string isbn)
        {
            ShowBooks(_jsonRepository.GetBooksByISBN(isbn));
        }

        public void FilterByName(string name)
        {
            ShowBooks(_jsonRepository.GetBooksByName(name));
        }

        public void FilterByAvailability(bool isTaken)
        {
            ShowBooks(_jsonRepository.GetBooksByAvailability(isTaken));
        }

        public void TakeBook()
        {
            Console.WriteLine("Enter your name: \n");
            var name = Console.ReadLine();

            ShowAllBooks();

            Console.WriteLine("\nEnter ISBN of the book you want to rent: \n");
            var wantedISBN = Console.ReadLine();

            var books = _jsonRepository.GetBooksByISBN(wantedISBN);
            ShowBooks(books);

            if (books.Count == 0)
                Console.WriteLine("We do not have such book available . . .");
            else if (books[0].IsTaken)
                Console.WriteLine("This book is already taken");

            else
            {
                Console.WriteLine("Enter how many days you will be renting the book for: ");
                var days = short.Parse(Console.ReadLine());

                while (true)
                {
                    if (days <= 61)
                        break;

                    Console.WriteLine("Can't borrow for more than 2 months, please enter less amount of days: ");
                    days = short.Parse(Console.ReadLine());
                }

                var rentersCointainer = _jsonRepository.GetAllRenters();

                if (rentersCointainer.Find(r => r.Name == name) == null)
                {
                    var allBooks = _jsonRepository.GetAllBooks();

                    books[0].IsTaken = true;
                    books[0].Date_Taken = DateTime.Today.Date;
                    books[0].Date_Returned = books[0].Date_Taken.Value.AddDays(days);

                    allBooks.Find(b => b.Isbn == books[0].Isbn).IsTaken = true;
                    allBooks.Find(b => b.Isbn == books[0].Isbn).Date_Taken = DateTime.Today.Date;
                    allBooks.Find(b => b.Isbn == books[0].Isbn).Date_Returned = DateTime.Today.Date.AddDays(days);

                    var json = JsonConvert.SerializeObject(allBooks, Formatting.Indented).ToString();

                    File.WriteAllText(@"books.json", json);

                    var rentedBooks = new List<Book>
                    {
                        books[0]
                    };

                    var newRenter = new Renter(name, rentedBooks);

                    rentersCointainer.Add(new Renter(name, rentedBooks));

                    var allRenters = _jsonRepository.GetAllRenters();

                    allRenters.Add(newRenter);

                    var json2 = JsonConvert.SerializeObject(allRenters, Formatting.Indented).ToString();

                    File.WriteAllText(@"renters.json", json2);
                }
                else
                {
                    var oldRenter = rentersCointainer.Find(r => r.Name == name);

                    if (oldRenter.RentedBooks.Count == 3)
                    {
                        Console.WriteLine("Maximum limit of rented books reached, please return atleast one book first . . .");
                    }

                    else
                    {
                        var allBooks = _jsonRepository.GetAllBooks();

                        books[0].IsTaken = true;
                        books[0].Date_Taken = DateTime.Today.Date;

                        allBooks.Find(b => b.Isbn == books[0].Isbn).IsTaken = true;
                        allBooks.Find(b => b.Isbn == books[0].Isbn).Date_Taken = DateTime.Today.Date;
                        allBooks.Find(b => b.Isbn == books[0].Isbn).Date_Taken = DateTime.Today.Date.AddDays(days);

                        var booksJson = JsonConvert.SerializeObject(allBooks, Formatting.Indented).ToString();

                        File.WriteAllText(@"books.json", booksJson);

                        oldRenter.RentedBooks.Add(books[0]);

                        var rentersJson = JsonConvert.SerializeObject(rentersCointainer, Formatting.Indented).ToString();

                        File.WriteAllText(@"renters.json", rentersJson);
                    }
                }
            }
        }

        public void ReturnBook()
        {
            Console.WriteLine("Enter your name:\n");

            var renterName = Console.ReadLine();

            var allRenters = _jsonRepository.GetAllRenters();

            var thisRenter = allRenters.Find(r => r.Name == renterName);

            if (thisRenter == null)
                Console.WriteLine("You don't have any rented books . . .");

            else
            {
                ShowBooks(thisRenter.RentedBooks);

                Console.WriteLine("Enter ISBN of book that you are returning:\n");
                var isbn = Console.ReadLine();

                var returnedBook = thisRenter.RentedBooks.Find(b => b.Isbn == isbn);

                if (returnedBook == null)
                    Console.WriteLine("You did not rent such book . . .");
                else
                {
                    thisRenter.RentedBooks = allRenters.Find(r => r.Name == renterName).RentedBooks;
                    thisRenter.RentedBooks.Remove(returnedBook);

                    if (thisRenter.RentedBooks.Count == 0)
                        allRenters.Remove(thisRenter);

                    var rentersJson = JsonConvert.SerializeObject(allRenters, Formatting.Indented).ToString();
                    File.WriteAllText(@"renters.json", rentersJson);

                    var allBooks = _jsonRepository.GetAllBooks();

                    if (allBooks.Find(b => b.Isbn == isbn).Date_Returned < DateTime.Now)
                        Console.WriteLine("Late to return, your house will burn :o");
                    else
                        Console.WriteLine("Successfully returned . . .");

                    allBooks.Find(b => b.Isbn == isbn).IsTaken = false;
                    allBooks.Find(b => b.Isbn == isbn).Date_Taken = null;
                    allBooks.Find(b => b.Isbn == isbn).Date_Returned = null;

                    var booksJson = JsonConvert.SerializeObject(allBooks, Formatting.Indented).ToString();
                    File.WriteAllText(@"books.json", booksJson);
                }
            }
        }

        public void DeleteBook(string wantedISBN)
        {
            var all = _jsonRepository.GetAllBooks();
            var books = _jsonRepository.GetBooksByISBN(wantedISBN);

            if (books.Count != 0 && books[0].IsTaken == true)
                Console.WriteLine("Can't delete this book now because it is taken, wait until it is returned . . .");
            else
            {
                if (books.Count == 0)
                    Console.WriteLine("Such books does not exist in our library . . .");
                else
                {
                    var allBooks = _jsonRepository.GetAllBooks();

                    var deleteIndex = allBooks.FindIndex(b => b.Isbn == wantedISBN);
                    allBooks.RemoveAt(deleteIndex);

                    var json = JsonConvert.SerializeObject(allBooks, Formatting.Indented).ToString();

                    File.WriteAllText(@"books.json", json);

                    Console.WriteLine("Successfully deleted");
                }
            }
        }

        public bool ValidateNewBookISBN(string newIsbn)
        {
            var books = _jsonRepository.GetAllBooks().FindAll(b => b.Isbn == newIsbn);

            if (books.Count == 0)
                return true;

            return false;
        }

        public void CleanRentersList()
        {
            var allRenters = _jsonRepository.GetAllRenters();

            allRenters.Clear();

            var json = JsonConvert.SerializeObject(allRenters, Formatting.Indented).ToString();

            File.WriteAllText(@"renters.json", json);
        }
    }
}
