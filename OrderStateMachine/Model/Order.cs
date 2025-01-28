

namespace OrderStateMachine.Model;

public class Order
{
    public int Id { get; set; }
    public required string CustomerId { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
    public OrderState State { get; set; } // State Machine Enum

    public ICollection<Step> Steps { get; set; } = [];
}
