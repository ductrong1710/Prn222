using BusinessObjects;
using Repositories;
using Repositories.Interface;
using System.Collections.Generic;

namespace Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository iProductRepository;

        public ProductService()
        {
            iProductRepository = new ProductRepository();
        }

        public void SaveProduct(Product p) => iProductRepository.SaveProduct(p);
        public void DeleteProduct(Product p) => iProductRepository.DeleteProduct(p);
        public void UpdateProduct(Product p) => iProductRepository.UpdateProduct(p);
        public List<Product> GetProducts() => iProductRepository.GetProducts();
        public Product GetProductById(int id) => iProductRepository.GetProductById(id);
    }
}