using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductStoreWebAPI.Models;  

namespace ProductStoreWebAPI.Controllers
{
    public class ProductsController : ApiController
    {
        static readonly IProductRepository productrepos = new ProductRepository();

        public dynamic GetProducts(string sidx, string sord, int page, int rows)
        {
            //action def - public IEnumerable<Product> GetAllProducts()
            //return productrepos.GetAll();

            var products = productrepos.GetAll() as IEnumerable<Product>;
            var pageIndex = Convert.ToInt32(page) - 1;
            var pageSize = rows;
            var totalRecords = products.Count();
            var totalPages = (int)Math.Ceiling((float)totalRecords / (float)pageSize);
            products = products.Skip(pageIndex * pageSize).Take(pageSize);
            return new
            {
                total = totalPages,
                page = page,
                records = totalRecords,
                rows = (
                    from product in products
                    select new
                    {
                        i = product.Id.ToString(),
                        cell = new string[] {
                         product.Id.ToString(),
                         product.Name,
                         product.Category,
                         product.Price.ToString()
                        }
                    }).ToArray()
            };
        }

        public Product GetProduct(int id)
        {
            Product item = productrepos.Get(id);
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return item;
        }

        public IEnumerable<Product> GetProductsByCategory(string category)
        {
            return productrepos.GetAll().Where(
                p => string.Equals(p.Category, category, StringComparison.OrdinalIgnoreCase));
        }

        //public Product PostProduct(Product item)
        //{
        //    item = repository.Add(item);
        //    return item;
        //}

        public HttpResponseMessage PostProduct(Product item)
        {
            item = productrepos.Add(item);
            var response = Request.CreateResponse<Product>(HttpStatusCode.Created, item);

            string uri = Url.Link("DefaultApi", new { id = item.Id });
            response.Headers.Location = new Uri(uri);
            return response;
        }

        public HttpResponseMessage PutProduct(int id, Product product)
        {
            product.Id = id;
            if (!productrepos.Update(product))
            {
                //throw new HttpResponseException(HttpStatusCode.NotFound);
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee name not found.");
            }
            else
            {
                var response = Request.CreateResponse<Product>(HttpStatusCode.OK, product);
                string uri = Url.Link("DefaultApi", new { id = product.Id });
                response.Headers.Location = new Uri(uri);
                return response;
            }
            
        }


        public HttpResponseMessage DeleteProduct(int id)
        {
            //productrepos.Remove(id);
            //return new HttpResponseMessage(HttpStatusCode.NoContent);
            var prod = productrepos.Get(id);
            if (prod == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                           "Product with Id = " + id.ToString() + " not found to delete.");

            }
            else
            {
                productrepos.Remove(id);
                return Request.CreateResponse(HttpStatusCode.OK, "Product with Id = " + id.ToString() + " deleted.");
            }
            

            

            
        }


    }
}
