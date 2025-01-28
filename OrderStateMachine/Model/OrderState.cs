

namespace OrderStateMachine.Model;

public enum OrderState
{
    PendingDeposit,      // Initial state
    ReviewingDocuments,  // Step after deposit
    ApprovingOrder,      // Final approval step
    Completed            // End state
}
