﻿using Medical_Inventory.Models;

namespace Medical_Inventory.Data.IRepository;

public interface IProductRepository
{
    Task<Product?>? GetFirstOrDefault(long id);
    Task<IEnumerable<Product>?>? GetAll();
    Task Add(Product entity);
    void Update(Product obj);
    void Remove(long id);
    void Save();

    Task<Product?> GetByName(string? name);
    Task<Product?> GetByName(string name, long id);
    void Remove(long productId, long id);
    
}