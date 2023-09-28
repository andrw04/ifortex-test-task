using System.Text.Json.Serialization;

namespace TestTask.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string ProductName { get; set; }

        public int Price { get; set; }

        public int Quantity { get; set; } 

        public int UserId { get; set; }

        [JsonIgnore]
        public virtual User User { get; set; }
    }
}
