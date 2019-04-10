using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleApp.Models
{
    public interface IProductsRepository
    {
        Task<IEnumerable<Product>> GetAllAsync();
        Task<Product> GetAsync(int id);
        Task<bool> CreateAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> ExistsAsync(int id);
        Task<bool> DeleteAsync(int id);
    }

    public class ProductsRepository : IProductsRepository
    {
        public ProductsRepository(SampleAppContext context) => this.context = context;

        private readonly SampleAppContext context;

        public async Task<bool> CreateAsync(Product product)
        {
            if (product == null)
                throw new System.ArgumentNullException(nameof(product));
            await context.Product.AddAsync(product);
            await context.SaveChangesAsync();
            return true;
        }

        public async Task<Product> GetAsync(int id) => await context.Product.FindAsync(id);

        public async Task<IEnumerable<Product>> GetAllAsync() => await context.Product.ToListAsync();

        public async Task<bool> UpdateAsync(Product product)
        {
            if (product == null)
                throw new System.ArgumentNullException(nameof(product));
            if (product.Id <= 0)
                return false;
            context.Entry(product).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }
            return true;
        }

        public Task<bool> ExistsAsync(int id) => context.Product.AnyAsync(e => e.Id == id);

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await GetAsync(id);
            if (product == null)
                return false;
            context.Product.Remove(product);
            await context.SaveChangesAsync();
            return true;
        }
    }
}
