namespace Assignment.Application.Endpoints.Product;

public record ProductViewModel
{
    public int Id { get; init; }
    public string Name { get; init; } = "";
    public string Description { get; init; } = "";
    public int? Price { get; set; } 
    public bool Active { get; init; }
    public int CreatedBy { get; init; }
    public DateTime CreatedOn { get; init; }
    public int? ModifiedBy { get; init; }
    public DateTime? ModifiedOn { get; init; }

}

