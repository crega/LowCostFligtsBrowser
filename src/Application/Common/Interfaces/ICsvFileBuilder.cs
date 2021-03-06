using LowCostFligtsBrowser.Application.TodoLists.Queries.ExportTodos;
using System.Collections.Generic;

namespace LowCostFligtsBrowser.Application.Common.Interfaces
{
    public interface ICsvFileBuilder
    {
        byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
    }
}
