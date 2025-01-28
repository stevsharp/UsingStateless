

namespace OrderStateMachine.Model;

public class Comment
{
    public int Id { get; set; }
    public int StepId { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public Step Step { get; set; } = null!;
}
