using System.Collections.Generic;
using System.Linq;
using EFCore.Data;
using EFCore.Models;
using Microsoft.EntityFrameworkCore;

namespace EFCore.Repositories
{
    public class CategoryRepository
    {
        private readonly StoreDataContext _context;

        public CategoryRepository(StoreDataContext context)
        {
            _context = context;
        }

        public IEnumerable<Category> Get()
        {
            return _context
                    .Categories
                    .AsNoTracking()
                    .ToList();
        }

        public Category Get(int id)
        {
            return _context
                    .Categories
                    .AsNoTracking()
                    .FirstOrDefault(
                        c => c.Id.Equals(id)
                    );
        }

        public void Save(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
        }

        public void Update(Category category)
        {
            _context.Entry(category).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Category category)
        {
            _context.Categories.Remove(category);
            _context.SaveChanges();
        }

        public IEnumerable<Product> GetProductsFromCategory(int id)
        {
            return _context
                    .Products
                    .AsNoTracking()
                    .Where(x => x.CategoryId == id)
                    .ToList();
        }
        
    }
}