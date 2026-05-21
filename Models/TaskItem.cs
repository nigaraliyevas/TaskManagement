using TaskManagement.Enums;

namespace TaskManagement.Models;

public class TaskItem
{
    public int      Id          { get; set; }
    public string   Title       { get; set; } = string.Empty;
    public string   Description { get; set; } = string.Empty;
    public Priority Priority    { get; set; }
    public DateTime? Deadline   { get; set; }  
    public bool     IsCompleted { get; set; }
    public DateTime CreatedAt   { get; set; } = DateTime.UtcNow;

    public WorkStatus Status
    {
        get
        {
            var now = DateTime.UtcNow;

            if (IsCompleted)
                return WorkStatus.Done;

            if (Deadline.HasValue)
            {
            Console.WriteLine($"deadline{Deadline}");
            Console.WriteLine($"now {now}");

                if (Deadline.Value < now)
                    return WorkStatus.Overdue;

                if (Deadline.Value <= now.AddHours(24))
                    return WorkStatus.Urgent;
            }

            return WorkStatus.Active;
        }
    }
}
