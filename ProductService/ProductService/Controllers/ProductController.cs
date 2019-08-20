using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ProductDataLibrary; 

namespace ProductService.Controllers
{
    public class ProductController : ApiController
    {

        [HttpGet]
        public IEnumerable<Product> listProducts()
        {
            using (dbSampleEntities entities = new dbSampleEntities())
            {
                return entities.Products.ToList();
            }
        }

        [HttpGet]
        public Product listProductsByID(int PID)
        {
            using (dbSampleEntities entities = new dbSampleEntities())
            {
                return entities.Products.FirstOrDefault(p => p.ProductID == PID);
            }
        }

        [HttpPost]
        public HttpResponseMessage AddProduct(Product prds)
        {
            try
            {
                using (dbSampleEntities entities = new dbSampleEntities())
                {
                    entities.Products.Add(prds);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, prds);
                    message.Headers.Location = new Uri(Request.RequestUri +
                        prds.ProductID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpPut]
        public HttpResponseMessage UpdateProduct(Product prds)
        {
            try
            {
                using (dbSampleEntities entities = new dbSampleEntities())
                {
                    entities.Products.Add(prds);
                    entities.SaveChanges();

                    var message = Request.CreateResponse(HttpStatusCode.Created, prds);
                    message.Headers.Location = new Uri(Request.RequestUri +
                       prds.ProductID.ToString());

                    return message;
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [HttpDelete]
        public HttpResponseMessage DeleteProduct(int PID)
        {
            try
            {
                using (dbSampleEntities entities = new dbSampleEntities())
                {
                    var entity = entities.Products.FirstOrDefault(e => e.ProductID == PID);
                    if (entity == null)
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.NotFound,
                            "Employee with Id = " + PID.ToString() + " not found to delete");
                    }
                    else
                    {
                        entities.Products.Remove(entity);
                        entities.SaveChanges();
                        return Request.CreateResponse(HttpStatusCode.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }


    }
}
