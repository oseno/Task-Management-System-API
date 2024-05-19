using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace OsenoTaskManagementSystem.Models
{
    public class User
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string Username { get; set; } 
        public string Password { get; set; } 
        public DateTime DateCreated { get; set; }
    }
    public class AddUserModel
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
