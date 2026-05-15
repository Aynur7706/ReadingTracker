using System.Text.Json;
using ReadingTracker.Models;

namespace ReadingTracker.Services
{
    public class JsonService
    {
        private readonly string usersPath;
        private readonly string booksPath;

        public JsonService()
        {
            usersPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "users.json");
            booksPath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "books.json");
        }

        public List<AppUser> GetUsers()
        {
            if (!File.Exists(usersPath))
                return new List<AppUser>();

            string json = File.ReadAllText(usersPath);

            return JsonSerializer.Deserialize<List<AppUser>>(json) ?? new List<AppUser>();
        }

        public void SaveUsers(List<AppUser> users)
        {
            string json = JsonSerializer.Serialize(users, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(usersPath, json);
        }

        public List<Book> GetBooks()
        {
            if (!File.Exists(booksPath))
                return new List<Book>();

            string json = File.ReadAllText(booksPath);

            return JsonSerializer.Deserialize<List<Book>>(json) ?? new List<Book>();
        }

        public void SaveBooks(List<Book> books)
        {
            string json = JsonSerializer.Serialize(books, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            File.WriteAllText(booksPath, json);
        }
    }
}