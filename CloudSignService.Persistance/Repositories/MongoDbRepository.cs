using CloudSignService.Application.Contracts.Repositories;
using CloudSignService.Domain.Entity;
using CloudSignService.Domain.Entity.MongoDb;
using CloudSignService.Domain.Responses;
using Common.Exceptions.Exceptions;
using Common.Options;
using Common.Shared;
using Common.Shared.Exceptions;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CloudSignService.Persistance.Repositories
{
    public class MongoDbRepository : IMongoDbRepository
    {
        private readonly IMongoClient _client;
        private readonly IMongoDatabase _database;
        private readonly MongoDatabaseOptions _mongoDatabaseOptions;

        public MongoDbRepository(IOptions<MongoDatabaseOptions> mongoDatabaseOptions)
        {
            _mongoDatabaseOptions = mongoDatabaseOptions.Value;

            _client = new MongoClient(_mongoDatabaseOptions.ConnectionUri);
            _database = _client.GetDatabase(_mongoDatabaseOptions.DatabaseName);
        }

        public async Task<bool?> DeleteAsync<T>(string collectionName, string entityId) where T : class, TMongoDbEntity
        {
            try
            {
                await CheckIfItemExist<T>(collectionName, entityId);

                var collection = _database.GetCollection<T>(collectionName);

                await collection.DeleteOneAsync(p => p._id == entityId);

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<ResponseWithCountModel<List<T>?>> GetAllAsync<T>(string collectionName, PaginationOptions paginationOptions = null) where T : class, TMongoDbEntity
        {
            try
            {
                List<T> listOfItems;
                var collection = _database.GetCollection<T>(collectionName);

                listOfItems = await collection.Find(new BsonDocument()).ToListAsync();
                var records = listOfItems.Count;

                if (paginationOptions != null)
                {
                    listOfItems = await collection.Find(new BsonDocument()).Skip(paginationOptions.Offset)
                        .Limit(paginationOptions.Limit).ToListAsync();
                }

                return new ResponseWithCountModel<List<T>?>() { Data = listOfItems, Records = records };
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<ResponseWithCountModel<List<T>?>> GetAllAsync<T>(string collectionName, FilterDefinition<T> filter, PaginationOptions paginationOptions = null) where T : class, TMongoDbEntity
        {
            try
            {
                List<T> listOfItems;

                var collection = _database.GetCollection<T>(collectionName);

                listOfItems = await collection.Find(filter).ToListAsync();
                var records = listOfItems.Count;

                if (paginationOptions != null)
                {
                    listOfItems = await collection.Find(filter).Skip(paginationOptions.Offset)
                        .Limit(paginationOptions.Limit).ToListAsync();
                }

                return new ResponseWithCountModel<List<T>?>() { Data = listOfItems, Records = records };
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<T?> GetByIdAsync<T>(string collectionName, string entityId) where T : class, TMongoDbEntity
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);

                var objectId = ObjectId.Parse(entityId);

                var filter = Builders<T>.Filter.Eq("_id", objectId);
                var item = await collection.Find(filter).FirstOrDefaultAsync();

                return item;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<T?> GetByWhere<T>(string collectionName, FilterDefinition<T> filter) where T : class, TMongoDbEntity
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);

                var item = await collection.Find(filter).FirstOrDefaultAsync();

                return item;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<T?> InsertAsync<T>(string collectionName, T entity) where T : class, TMongoDbEntity
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);

                await collection.InsertOneAsync(entity);

                return entity;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<T?> InsertAsync<T>(string collectionName, T entity, FilterDefinition<T> filter) where T : class, TMongoDbEntity
        {
            try
            {
                var collection = _database.GetCollection<T>(collectionName);

                var existingDocument = collection.Find(filter).FirstOrDefault();
                if (existingDocument == null)
                {
                    await collection.InsertOneAsync(entity);
                }
                else
                {
                    var matchedFilters = MatchedFilters(filter, collection, existingDocument);

                    throw new BadRequestException($"Items already exist in other record at database: {string.Join(", ", matchedFilters)}");
                }

                return entity;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool?> PartialUpdateAsync<T>(string collectionName, string entityId, T updatedEntity) where T : class, TMongoDbEntity
        {
            try
            {
                await CheckIfItemExist<T>(collectionName, entityId);

                var collection = _database.GetCollection<T>(collectionName);
                var objectId = new ObjectId(entityId);

                var filter = Builders<T>.Filter.Eq("_id", objectId);

                var updateDefinitionBuilder = new UpdateDefinitionBuilder<T>();
                var updateDefinition = updateDefinitionBuilder.Combine(GenerateUpdateDefinition(updatedEntity));

                await collection.UpdateOneAsync(filter, updateDefinition);

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool?> PartialUpdateAsync<T>(string collectionName, string entityId, T updatedEntity, FilterDefinition<T> filterInput) where T : class, TMongoDbEntity
        {
            await CheckIfItemExist<T>(collectionName, entityId);

            try
            {
                var collection = _database.GetCollection<T>(collectionName);
                var existingDocument = collection.Find(filterInput).FirstOrDefault();
                if (existingDocument == null)
                {
                    var objectId = new ObjectId(entityId);

                    var filter = Builders<T>.Filter.Eq("_id", objectId);

                    var updateDefinitionBuilder = new UpdateDefinitionBuilder<T>();
                    var updateDefinition = updateDefinitionBuilder.Combine(GenerateUpdateDefinition(updatedEntity));

                    await collection.UpdateOneAsync(filter, updateDefinition);
                }
                else
                {
                    var matchedFilters = MatchedFilters(filterInput, collection, existingDocument);

                    throw new BadRequestException($"Items already exist in other record at database: {string.Join(", ", matchedFilters)}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool?> UpdateAsync<T>(string collectionName, string entityId, T updatedEntity) where T : class, TMongoDbEntity
        {
            try
            {
                await CheckIfItemExist<T>(collectionName, entityId);
                var collection = _database.GetCollection<T>(collectionName);

                var objectId = new ObjectId(entityId);

                var filter = Builders<T>.Filter.Eq("_id", objectId);

                await collection.ReplaceOneAsync(filter, updatedEntity);

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        public async Task<bool?> UpdateAsync<T>(string collectionName, string entityId, T updatedEntity, FilterDefinition<T> filterInput) where T : class, TMongoDbEntity
        {
            try
            {
                await CheckIfItemExist<T>(collectionName, entityId);
                var collection = _database.GetCollection<T>(collectionName);

                var existingDocument = collection.Find(filterInput).FirstOrDefault();
                if (existingDocument == null)
                {
                    var objectId = new ObjectId(entityId);

                    var filter = Builders<T>.Filter.Eq("_id", objectId);

                    await collection.ReplaceOneAsync(filter, updatedEntity);
                }
                else
                {
                    var matchedFilters = MatchedFilters(filterInput, collection, existingDocument);

                    throw new BadRequestException($"Items already exist in other record at database: {string.Join(", ", matchedFilters)}");
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new BadRequestException(ex.Message);
            }
        }

        private static List<string> MatchedFilters<T>(FilterDefinition<T> filter, IMongoCollection<T> collection, T existingDocument)
                            where T : class, TMongoDbEntity
        {
            var orDocuments = new List<BsonDocument>();
            var matchedFilters = new List<string>();

            var failedProperties = filter.Render(collection.DocumentSerializer,
                collection.Settings.SerializerRegistry);
            var testOr = failedProperties.Names.FirstOrDefault();
            if (testOr == "$or")
            {
                var orArray = failedProperties["$or"].AsBsonArray;
                orDocuments = orArray.Select(x => x.AsBsonDocument).ToList();
                foreach (var orDocument in orDocuments)
                {
                    foreach (var element in orDocument.Elements)
                    {
                        var fieldName = element.Name.ToLower();
                        var fieldValue = element.Value.ToString();

                        var properties = existingDocument.GetType().GetProperties();

                        foreach (var property in properties)
                        {
                            string propertyName = property.Name.ToLower();
                            if (propertyName == fieldName)
                            {
                                var propertyValue = property.GetValue(existingDocument, null);

                                if (propertyValue != null && propertyValue.ToString() == fieldValue)
                                {
                                    matchedFilters.Add($"{propertyName} = {fieldValue}");
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                foreach (var element in failedProperties)
                {
                    var fieldName = element.Name;
                    var fieldValue = element.Value.AsString;

                    var properties = existingDocument.GetType().GetProperties();

                    foreach (var property in properties)
                    {
                        string propertyName = property.Name.ToLower();
                        if (propertyName == fieldName)
                        {
                            var propertyValue = property.GetValue(existingDocument, null);

                            if (propertyValue != null && propertyValue.ToString() == fieldValue)
                            {
                                matchedFilters.Add(propertyName);
                            }
                        }
                    }
                }
            }
            return matchedFilters;
        }

        private async Task CheckIfItemExist<T>(string collectionName, string entityId) where T : class, TMongoDbEntity
        {
            var exist = await GetByIdAsync<T>(collectionName, entityId);

            if (exist == null)
            {
                throw new NotFoundException($"{typeof(T)} with id {entityId} not found");
            }
        }

        private UpdateDefinition<T> GenerateUpdateDefinition<T>(T entity)
        {
            var updateDefinitionBuilder = new UpdateDefinitionBuilder<T>();
            var updateDefinition = updateDefinitionBuilder.Combine(GenerateUpdateDefinitions(entity));

            return updateDefinition;
        }

        private IEnumerable<UpdateDefinition<T>> GenerateUpdateDefinitions<T>(T entity)
        {
            var properties = typeof(T).GetProperties();

            foreach (var property in properties)

                if (property.Name != "_id")
                {
                    {
                        var updatedValue = property.GetValue(entity);

                        if (updatedValue != null)
                        {
                            yield return Builders<T>.Update.Set(property.Name, updatedValue);
                        }
                    }
                }
                else if (property.Name != "shapassword")
                {
                    var updatedValue = property.GetValue(entity);

                    if (updatedValue != null)
                    {
                        yield return Builders<T>.Update.Set(property.Name, HashGenerator.HashPasword(updatedValue.ToString(), out var salt));
                    }
                }
        }
    }
}