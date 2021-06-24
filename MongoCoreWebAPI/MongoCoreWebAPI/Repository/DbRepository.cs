using CoreMongoBookAPI.Middleware;
using CoreMongoBookAPI.Models.Interfaces;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreMongoBookAPI.Repository
{
    public abstract class DbRepository<TEntity> : IRepository<TEntity> where TEntity : class, IId
    {
        private readonly IMongoCollection<TEntity> _entities;
        private IMongoDatabase _db { get; set; }
        private MongoClient _mongoClient { get; set; }
        public IClientSessionHandle Session { get; set; }

        public DbRepository(IMongosettings mongosettings)
        {
            _mongoClient = new MongoClient(mongosettings.Connection);
            _db = _mongoClient.GetDatabase(mongosettings.DatabaseName);
            string collectionName = typeof(TEntity).Name + "s";
            _entities = _db.GetCollection<TEntity>(collectionName);
        }

        public List<TEntity> Get() =>
             _entities.Find(entity => true).ToList();

        public TEntity Get(string id) =>
            _entities.Find<TEntity>(entity => entity.Id == id).FirstOrDefault();

        public TEntity Create(TEntity entity)
        {
            _entities.InsertOne(entity);
            return entity;
        }

        public void Update(string id, TEntity entityIn) =>
            _entities.ReplaceOne(entity => entity.Id == id, entityIn);


        public void Remove(TEntity entityIn) =>
            _entities.DeleteOne(entity => entity.Id == entityIn.Id);

        public void Remove(string id) =>
            _entities.DeleteOne(entity => entity.Id == id);
    }
}
