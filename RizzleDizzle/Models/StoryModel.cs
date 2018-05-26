using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace RizzleDizzle.Models
{
    public class StoryModel
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }
        [BsonElement("name")]
        public string Name { get; set; }
        [BsonElement("messages")]
        public List<string> Messages { get; set; }
    }
}
