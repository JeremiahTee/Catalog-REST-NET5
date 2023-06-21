using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Catalog.Models;
using Catalog.Repository;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Catalog.Repositories
{
    public class MongoDbItemsRepository : IItemsRepository
    {
        private const string databaseName = "catalog";

        private const string collectionName = "items";
        private readonly IMongoCollection<Item> _itemsCollection;

        private readonly FilterDefinitionBuilder<Item> _filterBuilder = Builders<Item>.Filter;

        public MongoDbItemsRepository(IMongoClient mongoClient){
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            _itemsCollection = database.GetCollection<Item>(collectionName);
        }
        
        async Task IItemsRepository.CreateItemAsync(Item item)
        {
            await _itemsCollection.InsertOneAsync(item);
        }

        async Task IItemsRepository.DeleteItemAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(Item => Item.Id, id);
            await _itemsCollection.DeleteOneAsync(filter);
        }

        async Task<Item> IItemsRepository.GetItemAsync(Guid id)
        {
            var filter = _filterBuilder.Eq(Item => Item.Id, id);
            return await _itemsCollection.Find(filter).SingleOrDefaultAsync();
        }

        async Task<IEnumerable<Item>> IItemsRepository.GetItemsAsync()
        {
            return await _itemsCollection.Find(new BsonDocument()).ToListAsync();
        }

        async Task IItemsRepository.UpdateItemAsync(Item item)
        {
            var filter = _filterBuilder.Eq(existsingItem => existsingItem.Id, item.Id);
            await _itemsCollection.ReplaceOneAsync(filter, item);
        }
    }
}