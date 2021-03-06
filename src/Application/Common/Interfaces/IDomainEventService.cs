using LowCostFligtsBrowser.Domain.Common;
using System.Threading.Tasks;

namespace LowCostFligtsBrowser.Application.Common.Interfaces
{
    public interface IDomainEventService
    {
        Task Publish(DomainEvent domainEvent);
    }
}
