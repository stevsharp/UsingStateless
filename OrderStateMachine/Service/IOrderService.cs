using OrderStateMachine.Model;

namespace OrderStateMachine.Service;

public interface IOrderService
{
    Task<int> CreateOrderAsync(string customerId);
    Task<List<Order>> GetAllOrdersAsync();
    Task<Order?> GetOrderByIdAsync(int orderId);
    Task CompleteStepAsync(int stepId);
}
