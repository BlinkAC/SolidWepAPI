using ParkingSpot.Infrastructure.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.DAL
{
    internal sealed class PostgresUnitOfWork : IUnitOfWork
    {
        private readonly MySpotDbContext _dbContext;

        public PostgresUnitOfWork(MySpotDbContext dbContext)
            => _dbContext = dbContext;

        public async Task ExecuteAsync(Func<Task> action)
        {
            await using var transaction = await _dbContext.Database.BeginTransactionAsync();

            try
            {
                await action();
                await _dbContext.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch (Exception)
            {
                //Si en una de las operaciones falla, toda la transaccion se cancela
                await transaction.RollbackAsync();
                throw;
            }
        }
    }
}
