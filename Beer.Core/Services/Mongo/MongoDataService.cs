using System;
using System.Collections.Generic;
using System.Text;
using Beer.Core.Entities;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Linq;
using System.Threading.Tasks;

namespace Beer.Core.Services.Mongo
{
    public class MongoDataService : IBeerDataService
    {
        private static readonly string MongoConnectionString = "mongodb://localhost:27017"; //todo load from config

        private MongoClient _mongoClient = new MongoClient(MongoConnectionString);

        public MongoDataService()
        {

        }

        private IMongoCollection<YeastCollection> GetYeastCollection(string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var mongoCollection = db.GetCollection<YeastCollection>("Yeast");
            return mongoCollection;
        }

        private async Task<FermentableCollection> GetFermentableCollection(string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var query = Builders<FermentableCollection>.Filter.Eq(f => f.UserEmail, userEmail);
            var mongoCollection = db.GetCollection<FermentableCollection>("Fermentables");
            var fermCollections = await mongoCollection.Find(query).ToListAsync();
            if(fermCollections.Count == 0)
            {
                return null;
            }
            return fermCollections[0];
        }

        private YeastCollection GetYeastCollectionForUser(IMongoCollection<YeastCollection> collection, string email)
        {
            var query = Builders<YeastCollection>.Filter.Eq(f => f.UserEmail, email);
            var usersYeastList = collection.Find(query).Limit(1).SingleOrDefault();
            return usersYeastList;
        }

        private YeastCollection CreateNewYeastCollection(string email, IMongoCollection<YeastCollection> collection)
        {
            var col = new YeastCollection
            {
                UserEmail = email,
                Yeast = DefaultLists.GetDefaultYeast().ToList()
            };
            collection.InsertOne(col);
            return col;
        }

        public IEnumerable<Yeast> GetAllYeastForUser(string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var query = Builders<YeastCollection>.Filter.Eq(f => f.UserEmail, userEmail);
            var mongoCollection = db.GetCollection<YeastCollection>("Yeast");
            var yeastCollections = mongoCollection.Find(query).ToList();
            YeastCollection col = null;
            if(yeastCollections == null || yeastCollections.Count == 0)
            {
                col = CreateNewYeastCollection(userEmail, mongoCollection);
            }
            else
            {
                //should only be one collection per user
                col = yeastCollections[0];
            }
            
            return col.Yeast;
        }

        public Yeast SaveYeast(Yeast yeast, string userEmail)
        {
            var collection = GetYeastCollection(userEmail);
            var usersYeastList = GetYeastCollectionForUser(collection, userEmail);
            if (usersYeastList == null)
            {
                //this should not happen
                CreateNewYeastCollection(userEmail, collection);
                return SaveYeast(yeast, userEmail);
            }
            var yeastToUpdate = usersYeastList.Yeast.SingleOrDefault(y => y.Id == yeast.Id);
            if (yeastToUpdate == null)
            {
                yeast.Id = Guid.NewGuid();                         
            }
            else
            {
                usersYeastList.Yeast.Remove(yeastToUpdate);                        
            }
            usersYeastList.Yeast.Add(yeast);
            var collectionId = usersYeastList.Id;
            var filter = Builders<YeastCollection>.Filter.Eq(f => f.Id, collectionId);
            collection.ReplaceOne(filter, usersYeastList);
            return yeast;
        }


        private FermentableCollection CreateNewFermentableCollection(string email, IMongoCollection<FermentableCollection> collection)
        {
            var col = new FermentableCollection
            {
                UserEmail = email,
                Fermentables = DefaultLists.GetDefaultFermentableCollection().ToList()
            };
            collection.InsertOne(col);
            return col;
        }

        private HopsCollection CreateNewHopsCollection(string email, IMongoCollection<HopsCollection> collection)
        {
            var col = new HopsCollection
            {
                UserEmail = email,
                Hops = DefaultLists.GetDefaultHopsCollection().ToList()
            };
            collection.InsertOne(col);
            return col;
        }

        public async Task<IEnumerable<Fermentable>> GetAllFermentablesForUser(string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var query = Builders<FermentableCollection>.Filter.Eq(f => f.UserEmail, userEmail);
            var mongoCollection = db.GetCollection<FermentableCollection>("Fermentables");
            var fermCollections = await mongoCollection.Find(query).ToListAsync();
            FermentableCollection col = null;
            if (fermCollections == null || fermCollections.Count == 0)
            {
                col = CreateNewFermentableCollection(userEmail, mongoCollection);
            }
            else
            {
                //should only be one collection per user
                col = fermCollections[0];
            }

            return col.Fermentables;
        }

        public async Task<Fermentable> SaveFermentable(Fermentable fermentable, string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var mongoCollection = db.GetCollection<FermentableCollection>("Fermentables");
            var query = Builders<FermentableCollection>.Filter.Eq(f => f.UserEmail, userEmail);
            var fermCollections = await mongoCollection.Find(query).ToListAsync();
            FermentableCollection usersCollection = null;
            if (fermCollections == null || fermCollections.Count == 0)
            {
                usersCollection = CreateNewFermentableCollection(userEmail, mongoCollection);
            }
            else
            {
                //should only be one collection per user
                usersCollection = fermCollections[0];
            }
            var fermToUpdate = usersCollection.Fermentables.SingleOrDefault(f => f.Id == fermentable.Id);
            if(fermToUpdate == null)
            {
                fermentable.Id = Guid.NewGuid();
            }
            else
            {
                usersCollection.Fermentables.Remove(fermToUpdate);
            }
            usersCollection.Fermentables.Add(fermentable);
            var filter = Builders<FermentableCollection>.Filter.Eq(f => f.Id, usersCollection.Id);
            mongoCollection.ReplaceOne(filter, usersCollection);
            return fermentable;

        }

        public async Task<IEnumerable<Hop>> GetAllHopsForUser(string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var query = Builders<HopsCollection>.Filter.Eq(f => f.UserEmail, userEmail);
            var mongoCollection = db.GetCollection<HopsCollection>("Hops");
            var hopsCollections = await mongoCollection.Find(query).ToListAsync();
            HopsCollection col = null;
            if (hopsCollections == null || hopsCollections.Count == 0)
            {
                col = CreateNewHopsCollection(userEmail, mongoCollection);
            }
            else
            {
                //should only be one collection per user
                col = hopsCollections[0];
            }

            return col.Hops;
        }

        public async Task<Hop> SaveHop(Hop hop, string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var mongoCollection = db.GetCollection<HopsCollection>("Hops");
            var query = Builders<HopsCollection>.Filter.Eq(f => f.UserEmail, userEmail);
            var hopsCollections = await mongoCollection.Find(query).ToListAsync();
            HopsCollection usersCollection = null;
            if (hopsCollections == null || hopsCollections.Count == 0)
            {
                usersCollection = CreateNewHopsCollection(userEmail, mongoCollection);
            }
            else
            {
                //should only be one collection per user
                usersCollection = hopsCollections[0];
            }
            var hopToUpdate = usersCollection.Hops.SingleOrDefault(f => f.Id == hop.Id);
            if (hopToUpdate == null)
            {
                hop.Id = Guid.NewGuid();
            }
            else
            {
                usersCollection.Hops.Remove(hopToUpdate);
            }
            usersCollection.Hops.Add(hop);
            var filter = Builders<HopsCollection>.Filter.Eq(f => f.Id, usersCollection.Id);
            mongoCollection.ReplaceOne(filter, usersCollection);
            return hop;
        }


        private RecipesCollection CreateNewRecipeCollection(string email, IMongoCollection<RecipesCollection> collection)
        {
            var col = new RecipesCollection
            {
                UserEmail = email,
                Recipes = new List<Recipe>()
            };
            collection.InsertOne(col);
            return col;
        }

        public async Task<IEnumerable<Recipe>> GetAllRecipesForUser(string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var query = Builders<RecipesCollection>.Filter.Eq(f => f.UserEmail, userEmail);
            var mongoCollection = db.GetCollection<RecipesCollection>("Recipes");
            var recipeCollections = await mongoCollection.Find(query).ToListAsync();
            RecipesCollection col = null;
            if (recipeCollections == null || recipeCollections.Count == 0)
            {
                col = CreateNewRecipeCollection(userEmail, mongoCollection);
            }
            else
            {
                //should only be one collection per user
                col = recipeCollections[0];
            }

            return col.Recipes;
        }

        public async Task<Recipe> SaveRecipe(Recipe recipe, string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var mongoCollection = db.GetCollection<RecipesCollection>("Recipes");
            var query = Builders<RecipesCollection>.Filter.Eq(f => f.UserEmail, userEmail);
            var recipesCollections = await mongoCollection.Find(query).ToListAsync();
            RecipesCollection usersCollection = null;
            if (recipesCollections == null || recipesCollections.Count == 0)
            {
                usersCollection = CreateNewRecipeCollection(userEmail, mongoCollection);
            }
            else
            {
                //should only be one collection per user
                usersCollection = recipesCollections[0];
            }
            var recipeToUpdate = usersCollection.Recipes.SingleOrDefault(f => f.Id == recipe.Id);
            if (recipeToUpdate == null)
            {
                recipe.Id = Guid.NewGuid();
            }
            else
            {
                usersCollection.Recipes.Remove(recipeToUpdate);
            }
            usersCollection.Recipes.Add(recipe);
            var filter = Builders<RecipesCollection>.Filter.Eq(f => f.Id, usersCollection.Id);
            mongoCollection.ReplaceOne(filter, usersCollection);
            return recipe;
        }


        private BatchCollection CreateNewBatchCollection(string email, IMongoCollection<BatchCollection> collection)
        {
            var col = new BatchCollection
            {
                UserEmail = email,
                Batches = new List<Batch>()
            };
            collection.InsertOne(col);
            return col;
        }


        public async Task<IEnumerable<Batch>> GetAllBatchesForUser(string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var query = Builders<BatchCollection>.Filter.Eq(f => f.UserEmail, userEmail);
            var mongoCollection = db.GetCollection<BatchCollection>("Batches");
            var batchCollections = await mongoCollection.Find(query).ToListAsync();
            BatchCollection col = null;
            if (batchCollections == null || batchCollections.Count == 0)
            {
                col = CreateNewBatchCollection(userEmail, mongoCollection);
            }
            else
            {
                //should only be one collection per user
                col = batchCollections[0];
            }

            return col.Batches;
        }

        public async Task<Batch> SaveBatch(Batch batch, string userEmail)
        {
            var db = _mongoClient.GetDatabase("BeerDb");
            var mongoCollection = db.GetCollection<BatchCollection>("Batches");
            var query = Builders<BatchCollection>.Filter.Eq(f => f.UserEmail, userEmail);
            var batchCollections = await mongoCollection.Find(query).ToListAsync();
            BatchCollection usersCollection = null;
            if (batchCollections == null || batchCollections.Count == 0)
            {
                usersCollection = CreateNewBatchCollection(userEmail, mongoCollection);
            }
            else
            {
                //should only be one collection per user
                usersCollection = batchCollections[0];
            }
            var batchToUpdate = usersCollection.Batches.SingleOrDefault(f => f.Id == batch.Id);
            if (batchToUpdate == null)
            {
                batch.Id = Guid.NewGuid();
            }
            else
            {
                usersCollection.Batches.Remove(batchToUpdate);
            }
            usersCollection.Batches.Add(batch);
            var filter = Builders<BatchCollection>.Filter.Eq(f => f.Id, usersCollection.Id);
            mongoCollection.ReplaceOne(filter, usersCollection);
            return batch;
        }

        public async Task<OpResult> DeleteRecipe(Recipe recipe, string userEmail)
        {
            try
            {
                var db = _mongoClient.GetDatabase("BeerDb");
                var mongoCollection = db.GetCollection<RecipesCollection>("Recipes");
                var query = Builders<RecipesCollection>.Filter.Eq(f => f.UserEmail, userEmail);
                var recipesCollections = await mongoCollection.Find(query).ToListAsync();
                RecipesCollection usersCollection = null;
                if (recipesCollections == null || recipesCollections.Count == 0)
                {
                    return new OpResult
                    {
                        Success = false,
                        Message = "Recipe not found"
                    };
                }
                else
                {
                    //should only be one collection per user
                    usersCollection = recipesCollections[0];
                }

                var recipeToUpdate = usersCollection.Recipes.SingleOrDefault(f => f.Id == recipe.Id);
                if (recipeToUpdate == null)
                {
                    return new OpResult
                    {
                        Success = false,
                        Message = "Recipe not found"
                    };
                }
                else
                {
                    usersCollection.Recipes.Remove(recipeToUpdate);
                    var filter = Builders<RecipesCollection>.Filter.Eq(f => f.Id, usersCollection.Id);
                    mongoCollection.ReplaceOne(filter, usersCollection);
                }
                return new OpResult
                {
                    Success = true,
                    Message = "ok"
                };
            }
            catch(Exception ex)
            {
                return new OpResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
            
        }

        public async Task<OpResult> DeleteBatch(Batch batch, string userEmail)
        {
            try
            {
                var db = _mongoClient.GetDatabase("BeerDb");
                var mongoCollection = db.GetCollection<BatchCollection>("Batches");
                var query = Builders<BatchCollection>.Filter.Eq(f => f.UserEmail, userEmail);
                var batchCollections = await mongoCollection.Find(query).ToListAsync();
                BatchCollection usersCollection = null;
                if (batchCollections == null || batchCollections.Count == 0)
                {
                    return new OpResult
                    {
                        Success = false,
                        Message = "Batch not found"
                    };
                }
                else
                {
                    //should only be one collection per user
                    usersCollection = batchCollections[0];
                }

                var batchToUpdate = usersCollection.Batches.SingleOrDefault(f => f.Id == batch.Id);
                if (batchToUpdate == null)
                {
                    return new OpResult
                    {
                        Success = false,
                        Message = "Batch not found"
                    };
                }
                else
                {
                    usersCollection.Batches.Remove(batchToUpdate);
                    var filter = Builders<BatchCollection>.Filter.Eq(f => f.Id, usersCollection.Id);
                    mongoCollection.ReplaceOne(filter, usersCollection);
                }
                return new OpResult
                {
                    Success = true,
                    Message = "ok"
                };
            }
            catch (Exception ex)
            {
                return new OpResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
