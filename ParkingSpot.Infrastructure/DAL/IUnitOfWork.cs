using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParkingSpot.Infrastructure.DAL
{
    public interface IUnitOfWork
    {
        Task ExecuteAsync(Func<Task> action);
    }
}
