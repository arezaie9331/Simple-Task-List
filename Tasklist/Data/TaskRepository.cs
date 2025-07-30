using Microsoft.EntityFrameworkCore;

namespace Tasklist.Data;

public class TaskRepository : ITaskRepository
{
    private TasklistContext _ctx;

    public TaskRepository(TasklistContext ctx)
    {
        _ctx = ctx;
    }

    public async Task<List<TaskItem>> GetAll()
    {

        var ls = await _ctx.TaskItems.ToListAsync();
        return ls.Where(t => IsTodayTask(t.AddTime, DateTime.Now,t.IsDone)).OrderBy(t=>t.IsDone).ToList();
    }

    public async Task<TaskItem> Get(int id)
    {
        if (_ctx.TaskItems.Any(t => t.Id == id))
        {
            return await _ctx.TaskItems.SingleAsync(t => t.Id == id);
        }
        else
        {
            return new TaskItem();
        }
    }

    public async Task<bool> Add(TaskItem item)
    {
        try
        {
            await _ctx.TaskItems.AddAsync(item);
            await _ctx.SaveChangesAsync();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Update(TaskItem item)
    {
        try
        {
            if (_ctx.TaskItems.Any(t => t.Id == item.Id))
            {
                TaskItem task = await _ctx.TaskItems.SingleAsync(t => t.Id == item.Id);
                task.IsDone = item.IsDone;
                task.Subject = item.Subject;
                _ctx.TaskItems.Update(item);
                await _ctx.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> Delete(int id)
    {
        try
        {
            if (_ctx.TaskItems.Any(t => t.Id == id))
            {
                TaskItem task = await _ctx.TaskItems.SingleAsync(t => t.Id == id);
                task.IsDeleted = true;
                _ctx.TaskItems.Update(task);
                await _ctx.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> DoneItem(int id)
    {
        try
        {
            if (_ctx.TaskItems.Any(t => t.Id == id))
            {
                TaskItem task = await _ctx.TaskItems.SingleAsync(t => t.Id == id);
                task.IsDone = true;
                _ctx.TaskItems.Update(task);
                await _ctx.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception)
        {
            return false;
        }
    }

    public async Task<bool> NotDoneItem(int id)
    {
        try
        {
            if (_ctx.TaskItems.Any(t => t.Id == id))
            {
                TaskItem task = await _ctx.TaskItems.SingleAsync(t => t.Id == id);
                task.IsDone = false;
                _ctx.TaskItems.Update(task);
                await _ctx.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }

        }
        catch (Exception)
        {
            return false;
        }
    }

    public bool IsTodayTask(DateTime a, DateTime b,bool isDone)
    {
        if ((a.Year == b.Year && a.Month == b.Month && a.Day == b.Day) || !isDone)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}