using Moq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Xunit;

namespace BookLibrary.Tests
{
    public class BookControllerTests
    {      
        [Fact]
        public void ShowAllBooksTest_GetAll_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetAllBooks()).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.ShowAllBooks();

            mock.Verify(x => x.GetAllBooks());
        }

        [Fact]
        public void AddInitialBooksTest_GetInitialBooks_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetInitialBooks()).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.AddInitialBooks();

            mock.Verify(x => x.GetInitialBooks());
        }

        [Fact]
        public void CleanRentersListTest_GetAllRenters_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetAllRenters()).Returns(new List<Renter>());

            var controller = new BookController(mock.Object);
            controller.CleanRentersList();

            mock.Verify(x => x.GetAllRenters());
        }

        [Fact]
        public void AddBookTest_GetAllBooks_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetAllBooks()).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.AddBook(new Book());

            mock.Verify(x => x.GetAllBooks());
        }

        [Fact]
        public void FilterByAuthorTest_GetBooksByAuthor_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetBooksByAuthor("mock author")).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.FilterByAuthor("mock author");

            mock.Verify(x => x.GetBooksByAuthor("mock author"));
        }

        [Fact]
        public void FilterByCategoryTest_GetBooksByCategory_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetBooksByCategory("mock category")).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.FilterByCategory("mock category");

            mock.Verify(x => x.GetBooksByCategory("mock category"));
        }

        [Fact]
        public void FilterByLanguageTest_GetBooksByLanguage_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetBooksByLanguage("mock language")).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.FilterByLanguage("mock language");

            mock.Verify(x => x.GetBooksByLanguage("mock language"));
        }

        [Fact]
        public void FilterByIsbnTest_GetBooksByISBN_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetBooksByISBN("mock isbn")).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.FilterByISBN("mock isbn");

            mock.Verify(x => x.GetBooksByISBN("mock isbn"));
        }

        [Fact]
        public void FilterByNameTest_GetBooksByName_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetBooksByName("mock name")).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.FilterByName("mock name");

            mock.Verify(x => x.GetBooksByName("mock name"));
        }

        [Theory]
        [InlineData(false)]
        [InlineData(true)]
        public void FilterByAvailabilityTest_GetBooksByAvailability_Invoked(bool isTaken)
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetBooksByAvailability(isTaken)).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.FilterByAvailability(isTaken);

            mock.Verify(x => x.GetBooksByAvailability(isTaken));
        }

        [Fact]
        public void DeleteBookTest_GetBooksByIsbnInvoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetBooksByISBN("mock isbn")).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.DeleteBook("mock isbn");

            mock.Verify(x => x.GetBooksByISBN("mock isbn"));
        }

        [Fact]
        public void DeleteBookTest_GetAllBooks_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetBooksByISBN("mock isbn")).Returns(new List<Book>{ new Book()});
            mock.Setup(x => x.GetAllBooks()).Returns(new List<Book> { new Book { Isbn = "mock isbn"} });

            var controller = new BookController(mock.Object);
            controller.DeleteBook("mock isbn");

            mock.Verify(x => x.GetAllBooks());
        }

        [Fact]
        public void ValidateNewBookISBN_GetAllBooks_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetAllBooks()).Returns(new List<Book>());

            var controller = new BookController(mock.Object);
            controller.ValidateNewBookISBN("mock isbn");

            mock.Verify(x => x.GetAllBooks());
        }

        [Fact]
        public void CleanRentersList_GetAllRenters_Invoked()
        {
            var mock = new Mock<IJsonRepository>();
            mock.Setup(x => x.GetAllRenters()).Returns(new List<Renter>());

            var controller = new BookController(mock.Object);
            controller.CleanRentersList();

            mock.Verify(x => x.GetAllRenters());
        }
    }
}