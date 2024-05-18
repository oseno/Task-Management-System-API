using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace OsenoTaskManagementSystem.Models
{
    public class Task
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)] 
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }

        //[BsonId]
        //[BsonRepresentation(BsonType.ObjectId)]
        //public string? Id { get; set; }

        //public string username { get; set; } = null!;

        //[BsonElement("items")]
        //[JsonPropertyName("items")]
        //public List<string> movieIds { get; set; } = null!;

    }
}
