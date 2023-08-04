using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BigBasketModels;
using DAL;

namespace WebApiPractice.Controllers
{
    public class ProductController : ApiController
    {
        OfflineMartDAL OBJ = null;
        public ProductController()
        {
            OBJ = new OfflineMartDAL();
        }
        public HttpResponseMessage GetAllProducts()
        {
            //HttpResponseMessage message = null;
            //OfflineMartDAL OBJ = new OfflineMartDAL();
            try
            {
                List<Product> productlist = OBJ.SelectAllProducts();
                if (productlist.Count()>0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK, productlist);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "No products to Display");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage GetProductById(int id)
        {
            //OfflineMartDAL OBJ = new OfflineMartDAL();
            try
            {
                Product p = null;
                p = OBJ.FindProductById(id);
                if(p!=null)
                {
                    return Request.CreateResponse(HttpStatusCode.Found, p);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "With the given id"+ " "+ id +"  "+ "product is not present");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public HttpResponseMessage DeleteProductById(int id)
        {
            //OfflineMartDAL OBJ = new OfflineMartDAL();
            try
            {
                OfflineMartContext obj = new OfflineMartContext();
                Product p = null;
                p = obj.Products.Find(id);
                if (p != null)
                {
                    OBJ.RemoveProduct(p);
                    return Request.CreateResponse(HttpStatusCode.OK, p);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.NotFound, "With the given id" + " " + id + "  " + "product is not present");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        public HttpResponseMessage SaveProduct(Product p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                   // OfflineMartDAL OBJ = new OfflineMartDAL();
                    OBJ.AddProduct(p);
                    return Request.CreateResponse(HttpStatusCode.Created, p);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please enter a valid input data");
                }
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        [HttpPut]
        public HttpResponseMessage UpdateProduct(int id, Product p)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (id == p.Id)
                    {
                        OfflineMartContext obj = new OfflineMartContext();
                        Product x = null;
                        x = obj.Products.Find(id);
                        if (x != null)
                        {
                            //OfflineMartDAL OBJ = new OfflineMartDAL();
                            OBJ.EditProduct(p);
                            return Request.CreateResponse(HttpStatusCode.OK, x);
                        }
                        else
                        {
                            return Request.CreateErrorResponse(HttpStatusCode.NotFound, "With the given id" + id + "product is not found");
                        }
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please Check Id");
                    }
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Please provide a Valid Data");
                }
            }catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }
        public Product GetProductByName(string name)
        {
            OfflineMartContext obj = new OfflineMartContext();
            var item = (from p in obj.Products
                        where p.Name == name
                        select p).FirstOrDefault();
            return item;
        }
    }
}
