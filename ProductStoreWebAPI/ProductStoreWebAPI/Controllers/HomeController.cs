using ProductStoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace ProductStoreWebAPI.Controllers
{
    public class HomeController : ApiController
    {
        // You can take any no of parametrs from URI but only one parameter is allowed from Body

        // api/Home/?id=12&name=abba
        [System.Web.Http.HttpGet]
        public void VoidPost([FromUri] int id, [FromUri] string name)
        {
            int iCal = 50 * 100;

            string message = $" Item Id - {id.ToString()} Item Name - {name}";

        }

        // api/Home/5
        public Item GetProduct(int id)
        {
            var item = new Item();
            item.Id = id;
            item.Name = "Nirma Soap";
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            else if(id == 50)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.NotFound)
                {
                    Content = new StringContent(string.Format("No product with ID = {0}", id)),
                    ReasonPhrase = "Product ID Not Found"
                };
                throw new HttpResponseException(resp);
            }
            return item;
        }

        // api/Home?name=Ankit
        public HttpResponseMessage GetProduct(string name)
        {
            var item = new Item();
            item.Id = 57845;
            item.Name = "Dove Shampoo";
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        // /api/Home
        public HttpResponseMessage Get()
        {
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,"Your Value");
            response.Content = new StringContent("hello", Encoding.Unicode);
            response.Headers.CacheControl = new CacheControlHeaderValue()
            {
                MaxAge = TimeSpan.FromMinutes(20)
            };
            return response;
        }
    }
}