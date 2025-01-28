
using OrderStateMachine.Model;
using Stateless;

namespace OrderStateMachine.Service
{
    public class App(IOrderService orderService)
    {
        private readonly IOrderService _orderService = orderService;

        public async Task RunAsync()
        {
            Console.WriteLine("=== Happy Scenario: Completing Steps in Order ===");

            // Step 1: Create a new order
            var orderId = await _orderService.CreateOrderAsync("Customer123");
            Console.WriteLine($"Order {orderId} created successfully!");

            // Step 2: Fetch the order
            var order = await _orderService.GetOrderByIdAsync(orderId);

            // Step 3: Initialize the state machine
            var stateMachine = ConfigureStateMachine(order);

            // Step 4: Process steps in order
            foreach (var step in order.Steps.OrderBy(s => s.StepOrder))
            {
                await TryCompleteStepAsync(order, step, stateMachine);
            }

            // Step 5: Final state
            Console.WriteLine($"Order {order.Id} is now in state: {stateMachine.State}");
        }

        public async Task RunBadScenarioAsync()
        {
            Console.WriteLine("=== Bad Scenario: Trying to Complete Steps Out of Order ===");

            // Step 1: Create a new order
            var orderId = await _orderService.CreateOrderAsync("Customer456");
            Console.WriteLine($"Order {orderId} created successfully!");

            // Step 2: Fetch the order
            var order = await _orderService.GetOrderByIdAsync(orderId);

            // Step 3: Initialize the state machine
            var stateMachine = ConfigureStateMachine(order);

            // Step 4: Try to skip "Make Deposit" and go directly to "Review Documents"
            var stepToSkip = order.Steps.FirstOrDefault(s => s.Name == "Review Documents");
            if (stepToSkip != null)
            {
                await TryCompleteStepAsync(order, stepToSkip, stateMachine);
            }

            // Step 5: Complete the first step ("Make Deposit") afterward
            var firstStep = order.Steps.FirstOrDefault(s => s.Name == "Make Deposit");
            if (firstStep != null)
            {
                await TryCompleteStepAsync(order, firstStep, stateMachine);
            }

            // Step 6: Process remaining steps normally
            foreach (var step in order.Steps.OrderBy(s => s.StepOrder))
            {
                if (!step.IsCompleted)
                {
                    await TryCompleteStepAsync(order, step, stateMachine);
                }
            }

            // Final state
            Console.WriteLine($"Order {order.Id} is now in state: {stateMachine.State}");
        }


        private StateMachine<OrderState, OrderTrigger> ConfigureStateMachine(Order order)
        {
            var stateMachine = new StateMachine<OrderState, OrderTrigger>(order.State);

            // Configure state transitions
            stateMachine.Configure(OrderState.PendingDeposit)
                .Permit(OrderTrigger.MakeDeposit, OrderState.ReviewingDocuments);

            stateMachine.Configure(OrderState.ReviewingDocuments)
                .Permit(OrderTrigger.ReviewDocuments, OrderState.ApprovingOrder);

            stateMachine.Configure(OrderState.ApprovingOrder)
                .Permit(OrderTrigger.ApproveOrder, OrderState.Completed);

            stateMachine.Configure(OrderState.Completed)
                .OnEntry(() => Console.WriteLine("Order process completed."));

            return stateMachine;
        }

        private async Task TryCompleteStepAsync(Order order, Step step, StateMachine<OrderState, OrderTrigger> stateMachine)
        {
            // Validate if the step can be completed
            var previousSteps = order.Steps.Where(s => s.StepOrder < step.StepOrder);

            if (previousSteps.Any(s => !s.IsCompleted))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Step '{step.Name}' cannot be completed because previous steps are not completed.");
                Console.ResetColor();
                return;
            }

            // Mark step as completed
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"Completing step: {step.Name}");
            Console.ResetColor();

            await _orderService.CompleteStepAsync(step.Id);

            // Trigger the state machine transition
            switch (step.Name)
            {
                case "Make Deposit":
                    stateMachine.Fire(OrderTrigger.MakeDeposit);
                    break;

                case "Review Documents":
                    stateMachine.Fire(OrderTrigger.ReviewDocuments);
                    break;

                case "Approve Order":
                    stateMachine.Fire(OrderTrigger.ApproveOrder);
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine($"Unknown step: {step.Name}");
                    Console.ResetColor();
                    break;
            }

            // Print the new state
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine($"Order is now in state: {stateMachine.State}");
            Console.ResetColor();
        }
    }
}
