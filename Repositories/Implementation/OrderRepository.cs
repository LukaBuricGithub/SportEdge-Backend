using Microsoft.EntityFrameworkCore;
using SportEdge.API.Data;
using SportEdge.API.Models.Domain;
using SportEdge.API.Repositories.Interface;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;

namespace SportEdge.API.Repositories.Implementation
{
    /// <summary>
    /// Repository implementation for order-related data operations.
    /// </summary>
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext dbContext;

        public OrderRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        /// <inheritdoc/>
        public async Task<Order> CreateAsync(Order order)
        {
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            return order;
        }

        /// <inheritdoc/>
        public async Task<Order> GetAsync(int id)
        {
            return await dbContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariation)
                        .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariation)
                        .ThenInclude(pv => pv.SizeOption)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        /// <inheritdoc/>
        public async Task<List<Order>> GetAllAsync()
        {
            return await dbContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariation)
                        .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariation)
                        .ThenInclude(pv => pv.SizeOption)
                .ToListAsync();
        }

        /// <inheritdoc/>
        public async Task<List<Order>> GetAllByUserIdAsync(int userId)
        {
            return await dbContext.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariation)
                        .ThenInclude(pv => pv.Product)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.ProductVariation)
                        .ThenInclude(pv => pv.SizeOption) 
                .Where(o => o.UserId == userId)
                .ToListAsync();
        }

    }
}
