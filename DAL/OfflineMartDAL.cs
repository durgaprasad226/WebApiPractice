using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BigBasketModels;

namespace DAL
{
    public class OfflineMartDAL
    {
        OfflineMartContext obj = null;
        public OfflineMartDAL()
        {
            obj = new OfflineMartContext();
        }
        public List<Product> SelectAllProducts()
        {
            try
            {
                //OfflineMartContext obj = new OfflineMartContext();
                List<Product> productlist = obj.Products.ToList<Product>();
                return productlist;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Product FindProductById(int id)
        {
            try
            {
                //OfflineMartContext obj = new OfflineMartContext();
                Product p = null;
                p = obj.Products.Find(id);
                return p;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Product RemoveProduct(Product p)
        {
            try
            {
                //OfflineMartContext obj = new OfflineMartContext();
                Product d = null;
                d = obj.Products.Find(p.Id);
                obj.Products.Remove(d);
                obj.SaveChanges();
                return d;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void AddProduct(Product p)
        {
            try
            {
                //OfflineMartContext obj = new OfflineMartContext();
                obj.Products.Add(p);
                obj.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Product EditProduct(Product d)
        {
            try
            {
                //OfflineMartContext obj = new OfflineMartContext();
                Product h = null;
                h = obj.Products.Find(d.Id);
                h.Name = d.Name;
                h.Price = d.Price;
                h.Qty = d.Qty;
                obj.SaveChanges();
                return h;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
