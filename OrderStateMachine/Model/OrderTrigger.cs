

namespace OrderStateMachine.Model
{
    public enum OrderTrigger
    {
        MakeDeposit,         // Trigger when deposit is made
        ReviewDocuments,     // Trigger when documents are reviewed
        ApproveOrder,        // Trigger when the order is approved
        FinishOrder          // Trigger when the process is complete
    }
}