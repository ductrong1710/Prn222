using System.Collections.Generic;
using BusinessObjects;
using DataAccessObjects;
using Repositories.Interface;

namespace Repositories
{
    public class ProductRepository : IProductRepository
    {
        public void SaveProduct(Product p) => ProductDAO.SaveProduct(p);
        public void DeleteProduct(Product p) => ProductDAO.DeleteProduct(p);
        public void UpdateProduct(Product p) => ProductDAO.UpdateProduct(p);
        public List<Product> GetProducts() => ProductDAO.GetProducts();
        public Product GetProductById(int id) => ProductDAO.GetProductById(id);
    }
}