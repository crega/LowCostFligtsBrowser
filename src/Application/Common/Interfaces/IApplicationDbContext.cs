using LowCostFligtsBrowser.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<TodoList> TodoLists { get; set; }

        DbSet<TodoItem> TodoItems { get; set; }

        DbSet<Airport> Airports { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
