

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

public class Step
{
    public int Id { get; set; } 
    public int OrderId { get; set; } 
    public string Name { get; set; } 
    public bool IsCompleted { get; set; } 
    public int StepOrder { get; set; } 

    public Order Order { get; set; } = null!;
    public ICollection<Comment> Comments { get; set; } = [];
}

public class Comment
{
    public int Id { get; set; } 
    public int StepId { get; set; }
    public string Content { get; set; } 
    public DateTime CreatedAt { get; set; } 
    public Step Step { get; set; } = null!;
}

public enum OrderState
{
    PendingDeposit,      // Initial state
    ReviewingDocuments,  // Step after deposit
    ApprovingOrder,      // Final approval step
    Completed            // End state
}

public enum OrderTrigger
{
    MakeDeposit,         // Trigger when deposit is made
    ReviewDocuments,     // Trigger when documents are reviewed
    ApproveOrder,        // Trigger when the order is approved
    FinishOrder          // Trigger when the process is complete
}