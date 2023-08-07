using Assignment.Domain.Common;

namespace Assignment.Domain.Entities;

public class Product : AuditableEntity
{
    public int Id { get; set; }
    public string? Name { get; set; } = "";
    public string? Description { get; set; } = "";
    public int? Price { get; set; } 
    public bool Active { get; set; }
    public bool IsRecordDelted { get; set; }
}
