using Recipe_Book.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Recipe_Book.Services
{
    public class RecipeService
    {
        private readonly IMongoCollection<Recipe> _recipesCollection;

        public RecipeService (IOptions<RecipeBookDatabaseSettings> recipeBookDatabaseSettings)
        {
            var mongoClient = new MongoClient (recipeBookDatabaseSettings.Value.ConnectionString);

            var mongoDatabase = mongoClient.GetDatabase(recipeBookDatabaseSettings.Value.DatabaseName);

            _recipesCollection = mongoDatabase.GetCollection<Recipe>(recipeBookDatabaseSettings.Value.RecipesCollectionName);
        }

        public async Task<List<Recipe>> GetAsync() => await _recipesCollection.Find(_ => true).ToListAsync();
    }
}
