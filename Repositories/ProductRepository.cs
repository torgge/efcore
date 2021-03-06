﻿using System.Collections.Generic;
using System.Linq;
using EFCore.Data;
using EFCore.Models;
using EFCore.ViewModels.ProductViewModels;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repositories
{
    public class ProductRepository
    {
        private readonly StoreDataContext _context;

        public ProductRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<ListProductViewModel> Get()
        {
            return _context.Products
                .Include(x => x.Category)
                .Select(x => new ListProductViewModel
                {
                    Id = x.Id,
                    Title = x.Title,
                    Price = x.Price,
                    Category = x.Category.Title,
                    CategoryId = x.CategoryId
                })
                .AsNoTracking()
                .ToList();
        }

        public Product Get(int id)
        {
            return _context.Products.AsNoTracking().FirstOrDefault(p => p.Id == id);
        }

        public void Save(Product product)
        {
            _context.Add(product);
            _context.SaveChanges();
        }

        public void Update(Product product)
        {
            _context.Entry(product).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}