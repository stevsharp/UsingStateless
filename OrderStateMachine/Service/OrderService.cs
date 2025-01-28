

using Microsoft.EntityFrameworkCore;
using OrderStateMachine.Model;

namespace OrderStateMachine.Service;

public class OrderService(ApplicationDbContext context) : IOrderService
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> CreateOrderAsync(string customerId)
    {
        var order = new Order
        {
            CustomerId = customerId,
            OrderDate = DateTime.UtcNow,
            State = OrderState.PendingDeposit,
            Steps =
            [   
                new Step { Name = "Make Deposit", StepOrder = 1, IsCompleted = false },
                new Step { Name = "Review Documents", StepOrder = 2, IsCompleted = false },
                new Step { Name = "Approve Order", StepOrder = 3, IsCompleted = false }
            ]
        };

        _context.Orders.Add(order);

        await _context.SaveChangesAsync();

        return order.Id;
    }

    public async Task<List<Order>> GetAllOrdersAsync()
    {
        return await _context.Orders
            .Include(o => o.Steps)
            .ToListAsync();
    }

    public async Task<Order?> GetOrderByIdAsync(int orderId)
    {
        return await _context.Orders
            .Include(o => o.Steps)
            .FirstOrDefaultAsync(o => o.Id == orderId);
    }

    public async Task CompleteStepAsync(int stepId)
    {
        var step = await _context.Steps.FindAsync(stepId);

        if (step == null) throw new Exception("Step not found");

        step.IsCompleted = true;
        _context.Steps.Update(step);

        await _context.SaveChangesAsync();
    }
}
