using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ProjectPitch4.Models
{

    public class Region_Base
    {
        public int StateProvinceID { get; set; }
        public string? StateProvinceCode { get; set; }
        public int IsOnlyStateProvinceFlag { get; set; }
        public string? StateProvinceName { get; set; }
        public int TerritoryID { get; set; }
        public string? CountryRegionCode { get; set; }
        public string? CountryRegionName { get; set; }
    }

    public class Region : Region_Base
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string _id { get; set; } = ObjectId.GenerateNewId().ToString();
    }


}
