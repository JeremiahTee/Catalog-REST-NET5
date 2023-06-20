using System;
using System.Collections.Generic;
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
        
        void IItemsRepository.CreateItem(Item item)
        {
            _itemsCollection.InsertOne(item);
        }

        void IItemsRepository.DeleteItem(Guid id)
        {
            var filter = _filterBuilder.Eq(Item => Item.Id, id);
            _itemsCollection.DeleteOne(filter);
        }

        Item IItemsRepository.GetItem(Guid id)
        {
            var filter = _filterBuilder.Eq(Item => Item.Id, id);
            return _itemsCollection.Find(filter).SingleOrDefault();
        }

        IEnumerable<Item> IItemsRepository.GetItems()
        {
            return _itemsCollection.Find(new BsonDocument()).ToList();
        }

        void IItemsRepository.UpdateItem(Item item)
        {
            var filter = _filterBuilder.Eq(existsingItem => existsingItem.Id, item.Id);
            _itemsCollection.ReplaceOne(filter, item);
        }
    }
}