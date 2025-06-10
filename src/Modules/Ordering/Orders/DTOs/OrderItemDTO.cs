namespace Ordering.Orders.DTOs;
public record OrderItemDTO(Guid OrderId, Guid ProductId, int Quantity, decimal Price);