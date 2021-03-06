using LowCostFligtsBrowser.Domain.Common;
using LowCostFligtsBrowser.Domain.Entities;

namespace LowCostFligtsBrowser.Domain.Events
{
    public class TodoItemCompletedEvent : DomainEvent
    {
        public TodoItemCompletedEvent(TodoItem item)
        {
            Item = item;
        }

        public TodoItem Item { get; }
    }
}
