using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace DPWDR.Technical.Interview.Data.Entities
{
    public class Product
    {

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        [JsonPropertyName("description")]
        public string? Description { get; set; }


        [JsonPropertyName("image")]
        public string? Image { get; set; }


        public int Stock { get; set; }

        public DateTime Date { get; set; }


    }
}
