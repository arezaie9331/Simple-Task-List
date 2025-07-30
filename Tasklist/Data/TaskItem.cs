using System.ComponentModel.DataAnnotations;

namespace Tasklist.Data;

public class TaskItem
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Subject { get; set; }

    [Required]
    public bool IsDone { get; set; }

    public DateTime AddTime { get; set; }

    public bool IsDeleted { get; set; }
}