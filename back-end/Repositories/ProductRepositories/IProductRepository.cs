﻿using back_end.Models;

namespace back_end.Repositories.ProductRepositories;

public interface IProductRepository
{
    Task<Product> CreateProductAsync(Product product);
    Task<Product?> UpdateProductAsync(Product product, Guid id);
    Task<Product?> DeleteProductByIdAsync(Guid id);
    Task<List<Product>> GetAllProductsAsync();
    Task<Product?> GetProductByIdAsync(Guid id);
}