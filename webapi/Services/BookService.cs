﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webapi.Models;

namespace webapi.Services
{
    public class BookService
    {
        public List<Book> RetrieveBooks()
        {
            var books = new List<Book>();
            string dataDirectory = Path.Combine(AppContext.BaseDirectory, "AppData");
            string filePath = Path.Combine(dataDirectory, "books.txt");

            if (File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                books = JsonConvert.DeserializeObject<List<Book>>(fileContent);
            }

            return books;
        }

        public void SaveBooks(List<Book> books)
        {
            string dataDirectory = Path.Combine(AppContext.BaseDirectory, "AppData");
            string filePath = Path.Combine(dataDirectory, "books.txt");
            string content = JsonConvert.SerializeObject(books);

            // Ensure the directory exists before writing the file
            Directory.CreateDirectory(dataDirectory);
            File.WriteAllText(filePath, content);
        }
    }
}
