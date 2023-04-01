// import namespaces for the model class, miccro extension, and the mongo driver
using Recipe_Book.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Recipe_Book.Services
{
    public class RecipeService
    {
        // Declare the recipes collection within mongo
        private readonly IMongoCollection<Recipe> _recipesCollection;

        // Declare the recipe service
        public RecipeService (IOptions<RecipeBookDatabaseSettings> recipeBookDatabaseSettings)
        {
            // Connect to the mongo clinent using the declared conenction string within app settings
            var mongoClient = new MongoClient (recipeBookDatabaseSettings.Value.ConnectionString);

            // Get the database from the mongo connection
            var mongoDatabase = mongoClient.GetDatabase(recipeBookDatabaseSettings.Value.DatabaseName);
            
            // Get the collection from the mongo connection
            _recipesCollection = mongoDatabase.GetCollection<Recipe>(recipeBookDatabaseSettings.Value.RecipesCollectionName);
        }
        
        // Declare task for getting the databases within the collection and convert to a list
        public async Task<List<Recipe>> GetAsync() => await _recipesCollection.Find(_ => true).ToListAsync();
        
        // Declare task for getting all the 'Recipes' within the colleciton and convert to a list
        public async Task<Recipe?> GetAsync(string id) => await _recipesCollection.Find(x => x.ID == id).FirstOrDefaultAsync();

        // Declare task for creating a new recipe within the collection
        public async Task CreateAsync(Recipe newRecipe) => await _recipesCollection.InsertOneAsync(newRecipe);

        // Declare task for updating a  recipe within the collectio
        public async Task UpdatedAsync(string id, Recipe updatedRecipe) => await _recipesCollection.ReplaceOneAsync(x => x.ID == id, updatedRecipe);

        // Declare task for removing a recipe within the collectio
        public async Task RemoveAsync(string id) => await _recipesCollection.DeleteOneAsync(x => x.ID == id);
    }
}