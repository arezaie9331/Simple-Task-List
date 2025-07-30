using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Tasklist.Data;

public interface ITaskRepository
{
    Task<List<TaskItem>> GetAll();

    Task<TaskItem> Get(int id);

    Task<bool> Add(TaskItem item);

    Task<bool> Update(TaskItem item);

    Task<bool> Delete(int id);

    Task<bool> DoneItem(int id);

    Task<bool> NotDoneItem(int id);

    bool IsTodayTask(DateTime a, DateTime b,bool isDone);
}