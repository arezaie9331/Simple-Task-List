using Microsoft.EntityFrameworkCore;

namespace Tasklist.Data
{
    public class TasklistContext : DbContext
    {
        public TasklistContext(DbContextOptions<TasklistContext> options) : base(options)
        {
            
        }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
