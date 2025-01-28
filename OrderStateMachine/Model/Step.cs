

namespace OrderStateMachine.Model
{
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
}
