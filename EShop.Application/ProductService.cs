using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EShop.Domain.Repositories;
using EShop.Domain;
using EShop.Domain.ProductProvidersExceptions;
namespace EShop.Application;
    public class ProductService : IProductService
    {
        public readonly IRepository _repository;
        public ProductService(IRepository repository) 
        {
            _repository = repository;
        }
        public IEnumerable<Product> ShowAllProducts()
        {
            return _repository.GetAll();
        }
        public Product? GetProductById(int id)
        { 
            return _repository.GetById(id);
        }
        public void AddProduct(Product product) 
        {
        if (GetProductById(product.Id) != null) throw new ProductAllreadyExistsException("Product with this Id allready exists!");
            _repository.Add(product);
        }
        public void UpdateProduct(Product product)
        {
            _repository.Update(product);
        }
        public void DeleteProduct(int id)
        {
        if (GetProductById(id) == null) throw new ProductDoesNotExistException("Product with this Id does not exist!");
            _repository.Delete(id);
        }
}
