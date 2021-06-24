using CatalogService.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatalogService.Repository
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly IMongoDatabase _db;

        public CatalogRepository(IMongoDatabase mongoDb)
        {
            _db = mongoDb;
        }

        public IEnumerable<CatalogItem> GetCatalogItems()
        {
            var cols = _db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
            var catalogItems = cols.Find(FilterDefinition<CatalogItem>.Empty).ToEnumerable();
            return catalogItems;
        }

        public CatalogItem GetCatalogItem(Guid catalogItemId)
        {
            var cols = _db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
            var catalogItem = cols.Find(c => c.Id == catalogItemId).FirstOrDefault();
            return catalogItem;
        }

        public void InsertCatalogItem(CatalogItem catalogItem)
        {
            var cols = _db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
            cols.InsertOne(catalogItem);

        }

        public void DeleteCatalogItem(Guid catalogItemId)
        {
            var col = _db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
            col.DeleteOne(c => c.Id == catalogItemId);
        }

        public void UpdateCatalogItem(CatalogItem catalogItem)
        {
            var cols = _db.GetCollection<CatalogItem>(CatalogItem.DocumentName);
            var update = Builders<CatalogItem>.Update
                    .Set(c => c.Name, catalogItem.Name)
                    .Set(c => c.Description, catalogItem.Description)
                    .Set(c => c.Price, catalogItem.Price);

            cols.UpdateOne(c => c.Id == catalogItem.Id, update);
        }
    }
}
