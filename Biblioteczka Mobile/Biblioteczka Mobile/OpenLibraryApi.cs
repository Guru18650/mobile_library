using System;
using System.Net.Http;
using System.Threading.Tasks;
using Biblioteczka_Mobile;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class OpenLibraryAPI
{
    private const string BaseUrl = "http://openlibrary.org/";

    public static async Task<Book> GetBookByISBN(long isbn)
    {
        using (var httpClient = new HttpClient())
        {
            var url = $"{BaseUrl}/isbn/{isbn}.json";
            var response = await httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync(); 
            var json = JObject.Parse(content);

            var book = new Book
            {
                ISBN = isbn,
                Title = json.ContainsKey("title") != false  ? (string)json["title"] : "",
                Author = json.ContainsKey("authors") == true ? await GetAuthorName((string)json["authors"][0]["key"]) : "",
                Publisher = json.ContainsKey("publishers") == true ? (string)json["publishers"][0] : "",
                Language = json.ContainsKey("languages") == true ? (string)json["languages"][0]["key"].ToString().Replace("/languages/", "").Replace("eng", "English").Replace("pl", "Polish") : "",
                Length = json.ContainsKey("number_of_pages") == true ? (int)json["number_of_pages"] : 0,
                PublishDate = json.ContainsKey("publish_date") == true ? (string)json["publish_date"] : "",
                PhysicalFormat = json.ContainsKey("physical_format") == true ? (string)json["physical_format"] : "",
                OlKey = (string)json["key"],
                Cover = json.ContainsKey("covers") == true ? (long)json["covers"][0] : 0
            };

            return book;
        }
    }
    public static async Task<string> GetAuthorName(string AuthorKey)
    {

        string url = $"https://openlibrary.org{AuthorKey}.json";
        using (var client = new HttpClient())
        {
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var json = JObject.Parse(content);

                return json["name"].ToString();
            }
        }

        return null;
    }
}