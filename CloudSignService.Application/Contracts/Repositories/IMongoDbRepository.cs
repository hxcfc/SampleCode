using CloudSignService.Domain.Entity;
using CloudSignService.Domain.Entity.MongoDb;
using CloudSignService.Domain.Responses;
using MongoDB.Driver;

namespace CloudSignService.Application.Contracts.Repositories
{
    public interface IMongoDbRepository
    {
        Task<bool?> DeleteAsync<T>(string collectionName, string entityId) where T : class, TMongoDbEntity;

        Task<ResponseWithCountModel<List<T>?>> GetAllAsync<T>(string collectionName,
            PaginationOptions paginationOptions = null) where T : class, TMongoDbEntity;

        Task<ResponseWithCountModel<List<T>?>> GetAllAsync<T>(string collectionName, FilterDefinition<T> filter,
            PaginationOptions paginationOptions = null) where T : class, TMongoDbEntity;

        Task<T?> GetByIdAsync<T>(string collectionName, string entityId) where T : class, TMongoDbEntity;

        Task<T?> GetByWhere<T>(string collectionName, FilterDefinition<T> filter) where T : class, TMongoDbEntity;

        Task<T?> InsertAsync<T>(string collectionName, T entity) where T : class, TMongoDbEntity;

        Task<T?> InsertAsync<T>(string collectionName, T entity, FilterDefinition<T> filter) where T : class, TMongoDbEntity;

        Task<bool?> PartialUpdateAsync<T>(string collectionName, string entityId, T updatedEntity) where T : class, TMongoDbEntity;

        Task<bool?> PartialUpdateAsync<T>(string collectionName, string entityId, T updatedEntity, FilterDefinition<T> filterInput) where T : class, TMongoDbEntity;

        Task<bool?> UpdateAsync<T>(string collectionName, string entityId, T updatedEntity) where T : class, TMongoDbEntity;

        Task<bool?> UpdateAsync<T>(string collectionName, string entityId, T updatedEntity, FilterDefinition<T> filterInput) where T : class, TMongoDbEntity;
    }
}