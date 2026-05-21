using TaskManagement.Enums;

namespace TaskManagement.ViewModels;

public class TaskViewModel
{
    public int      Id          { get; set; }
    public string   Title       { get; set; } = string.Empty;
    public string   Description { get; set; } = string.Empty;
    public Priority Priority    { get; set; }
    public DateTime? Deadline   { get; set; }
    public bool     IsCompleted { get; set; }

    public Dictionary<string, string> Errors { get; set; } = new();

    public bool IsValid()
    {
        Errors.Clear();

        if (string.IsNullOrWhiteSpace(Title))
            Errors["Title"] = "Title boş ola bilməz.";
        else if (Title.Length > 200)
            Errors["Title"] = "Title 200 simvoldan çox ola bilməz.";

        if (Deadline.HasValue && Deadline.Value.Date < DateTime.Today)
            Errors["Deadline"] = "Deadline keçmiş tarix ola bilməz.";

        return Errors.Count == 0;
    }
}

public class TaskListViewModel
{
    public IEnumerable<TaskManagement.Models.TaskItem> Tasks     { get; set; } = [];
    public string                                      SortBy    { get; set; } = "priority";
    public string                                      SortOrder { get; set; } = "asc";
}
