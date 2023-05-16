using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;


namespace Biblioteczka_Mobile
{
    public static class ApiHandler
    {
        private static readonly HttpClient client = new HttpClient();
        private static readonly string baseUrl = "http://45.138.26.6:3001/";
        public static MainPage MainPage;
        private static bool isInDB = true;
        public static async Task<List<Book>> GetAllBooks()
        {
            string url = $"{baseUrl}books";
            var response = await client.GetStringAsync(url);
            var books = JsonConvert.DeserializeObject<List<Book>>(response);
            return books;
        }

        public static async Task<Book> GetByISBN(long isbn)
        {
            string url = $"{baseUrl}books/{isbn}";

            var response = await client.GetStringAsync(url);
            if (response.Length > 0)
            {
                var book = JsonConvert.DeserializeObject<Book>(response);
                isInDB = true;
                return book;

            }
            else
            {
                var book = await OpenLibraryAPI.GetBookByISBN(isbn);
                isInDB = false;
                return book;
            }
        }
            

        public static async Task<List<Book>> GetAllBooksFromAuthor(string author)
        {
            string url = $"{baseUrl}books/author/{author}";
            var response = await client.GetStringAsync(url);
            var books = JsonConvert.DeserializeObject<List<Book>>(response);
            return books;
        }

        public static async Task<List<Book>> GetAllBooksFromPublisher(string publisher)
        {
            string url = $"{baseUrl}books/publisher/{publisher}";
            var response = await client.GetStringAsync(url);
            var books = JsonConvert.DeserializeObject<List<Book>>(response);
            return books;
        }

        public static async Task<List<Book>> SearchBooks(string query)
        {
            string url = $"{baseUrl}books/search/{query}";
            var response = await client.GetStringAsync(url);
            var books = JsonConvert.DeserializeObject<List<Book>>(response);
            return books;
        }

        public static async Task ShowDetails(long ISBN, Page pageToClose = null)
        {
            var newBook = await GetByISBN(ISBN);

            if(newBook == null)
            {
                if(await MainPage.DisplayAlert("Error", "Book not found in both databases. Do you wish to add it manually?", "Yes", "No"))
                {
                    await MainPage.Navigation.PushAsync(new BookEditor(new Book() { ISBN = ISBN}));
                };

            } else
            {
                await MainPage.Navigation.PushAsync(new BookDetailsPage(newBook, isInDB));
                if (pageToClose != null)
                    MainPage.Navigation.RemovePage(pageToClose);
            }
            
        }

        public static async Task<bool> AddBook(Book book)
        {
            string url = $"{baseUrl}books/add";
            var json = JsonConvert.SerializeObject(book);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, content);
            return response.IsSuccessStatusCode;
        }

        public static async Task<JObject> SearchBooks()
        {
            string url = $"{baseUrl}stats";
            var response = await client.GetStringAsync(url);
            var stats = JObject.Parse(response);
            return stats;
        }

    }

    public class Book
    {
        public long ISBN { get; set; } = 0;
        public string Title { get; set; } = "";
        public string Author { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Language { get; set; } = string.Empty;
        public int Length { get; set; } = 0;
        public string PublishDate { get; set; } = string.Empty; 
        public string PhysicalFormat { get; set; } = string.Empty;
        public string OlKey { get; set; } = string.Empty; 
        public long Cover { get; set; } = 0;
        public string Owner { get; set; } = string.Empty;
    }
}
