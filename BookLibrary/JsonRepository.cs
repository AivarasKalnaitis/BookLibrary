using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLibrary
{
    public interface IJsonRepository
    {
        List<Book> GetAllBooks();
        List<Book> GetInitialBooks();
        List<Renter> GetAllRenters();
        List<Book> GetBooksByAuthor(string author);
        List<Book> GetBooksByCategory(string category);
        List<Book> GetBooksByLanguage(string language);
        List<Book> GetBooksByISBN(string isbn);
        List<Book> GetBooksByName(string name);
        List<Book> GetBooksByAvailability(bool availability);
    }

    public class JsonRepository : IJsonRepository
    {
        public List<Book> GetAllBooks()
        {
            var books = JArray.Parse(File.ReadAllText(@"books.json")).ToObject<List<Book>>();

            return books;
        }

        public List<Book> GetInitialBooks()
        {
            var books = JArray.Parse(File.ReadAllText(@"initialBooks.json")).ToObject<List<Book>>();

            return books;
        }

        public List<Renter> GetAllRenters()
        {
            var renters = JArray.Parse(File.ReadAllText(@"renters.json")).ToObject<List<Renter>>();

            return renters;
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            var books = JArray.Parse(File.ReadAllText(@"books.json")).ToObject<List<Book>>().FindAll(b => b.Author == author);

            return books;
        }

        public List<Book> GetBooksByCategory(string category)
        {
            var books = JArray.Parse(File.ReadAllText(@"books.json")).ToObject<List<Book>>().FindAll(b => b.Category == category);

            return books;
        }

        public List<Book> GetBooksByLanguage(string language)
        {
            var books = JArray.Parse(File.ReadAllText(@"books.json")).ToObject<List<Book>>().FindAll(b => b.Language == language);

            return books;
        }

        public List<Book> GetBooksByISBN(string isbn)
        {
            var books = JArray.Parse(File.ReadAllText(@"books.json")).ToObject<List<Book>>().FindAll(b => b.Isbn == isbn);

            return books;
        }

        public List<Book> GetBooksByName(string name)
        {
            var books = JArray.Parse(File.ReadAllText(@"books.json")).ToObject<List<Book>>().FindAll(b => b.Name.Contains(name));

            return books;
        }

        public List<Book> GetBooksByAvailability(bool availability)
        {
            var books = JArray.Parse(File.ReadAllText(@"books.json")).ToObject<List<Book>>().FindAll(b => b.IsTaken == availability);

            return books;
        }
    }
}
