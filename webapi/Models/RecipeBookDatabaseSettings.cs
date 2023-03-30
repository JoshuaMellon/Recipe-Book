namespace webapi.Models
{
    public class RecipeBookDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;

        public string DatabaseName { get; set; } = null!;

        public string RecipesCollectionName { get; set; } = null!;
    }
}
