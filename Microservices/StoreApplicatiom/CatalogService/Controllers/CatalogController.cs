using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CatalogService.Models;
using CatalogService.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace CatalogService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalogRepository _catalogRepository;

        public CatalogController(IMongoDatabase db)
        {
            _catalogRepository = new CatalogRepository(db);
        }

        // GET: api/Catalog
        [HttpGet]
        public IActionResult Get()
        {
            var items = _catalogRepository.GetCatalogItems();
            return new OkObjectResult(items);

        }

        // GET: api/Catalog/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(Guid id)
        {
            var item = _catalogRepository.GetCatalogItem(id);
            return new OkObjectResult(item);
        }

        // POST: api/Catalog
        [HttpPost]
        public IActionResult Post([FromBody] CatalogItem item)
        {
            _catalogRepository.InsertCatalogItem(item);
            return CreatedAtAction(nameof(Get), new { id = item.Id }, item);

        }

        // PUT: api/Catalog/5
        [HttpPut("{id}")] 
        public IActionResult Put(int id, [FromBody] CatalogItem item)
        {
            if (item != null)
            {
                _catalogRepository.UpdateCatalogItem(item);
                return new OkResult();
            }
            return new NoContentResult();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            _catalogRepository.DeleteCatalogItem(id);
            return new OkResult();
        }
    }
}
