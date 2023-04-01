using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using ThirdParty.Json.LitJson;
using System.Text.Json.Serialization;

namespace Recipe_Book.Models
{
    public class Recipe
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? ID { get; set; }

        // Recipe key points

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public string RecipeName { get; set; } = null!;

        public decimal PriceOfRecipe { get; set; }

        public decimal PricePerPortion { get; set; }

        // Recipe values

        public decimal Calories { get; set; }

        public decimal Fat { get; set; }

        public decimal Sugars { get; set; }

        public decimal Fibre { get; set; }

        public decimal Protein { get; set; }

        public decimal Salt { get; set; }

        // Recipe description

        public string CookingInstructions { get; set; } = null!;

        public string SkillLevel { get; set; } = null!;

        public decimal TimeToCook { get; set; }

        public decimal TimeToPrep { get; set; }

    }
}
