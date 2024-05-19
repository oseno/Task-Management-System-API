using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace OsenoTaskManagementSystem.Models
{
    public class Task
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public DateTime DateCreated { get; set; }
        public bool IsCompleted { get; set; }
    }
    public class AddTaskModel
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
    }
    public class UpdateTaskModel
    {
        public string? Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = null!;
        public bool IsCompleted { get; set; }
    }
}
