using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPitch4.Models
{
    public class ProductDescription_Base
    {
        public int ProductID { get; set; }
        public string? Name { get; set; }
        public string? ProductModel { get; set; }
        public string? CultureID { get; set; }
        public string? Description { get; set; }
    }

    public class ProductDescription : ProductDescription_Base
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = ObjectId.GenerateNewId().ToString();
    } 
}
