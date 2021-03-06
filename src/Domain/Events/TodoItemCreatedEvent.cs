using LowCostFligtsBrowser.Domain.Common;
using LowCostFligtsBrowser.Domain.Entities;

namespace LowCostFligtsBrowser.Domain.Events
{
    public class TodoItemCreatedEvent : DomainEvent
    {
        public TodoItemCreatedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
