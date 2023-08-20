using Microsoft.AspNetCore.Server.Kestrel.Transport.Quic;
using Newtonsoft.Json;
using webapi.Models;

namespace webapi.Services
{
    public class QuoteService
    {
        public List<Quote> RetrieveQuotes()
        {
            var quotes = new List<Quote>();
            string dataDirectOry = Path.Combine(AppContext.BaseDirectory, "AppData");
            string filePath = Path.Combine(dataDirectOry, "quotes.txt");

            if(File.Exists(filePath))
            {
                string fileContent = File.ReadAllText(filePath);
                quotes = JsonConvert.DeserializeObject<List<Quote>>(fileContent);
            }

            return quotes;
        }

        public void SaveQuotes(List<Quote> quotes)
        {
            string dataDirectory = Path.Combine(AppContext.BaseDirectory, "AppData");
            string filePath = Path.Combine(dataDirectory, "quotes.txt");
            string content = JsonConvert.SerializeObject(quotes);

            Directory.CreateDirectory(dataDirectory);
            File.WriteAllText(filePath, content); 
        }
    }
}

