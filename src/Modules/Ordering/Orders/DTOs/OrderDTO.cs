using Ordering.Orders.DTOs;

namespace Ordering.Orders.DTOs;
public record OrderDTO(
    Guid Id,
    Guid CustomerId,
string OrderName,
    AddressDTO ShippingAddress,
    AddressDTO BillingAddress,
    PaymentDTO Payment,
    List<OrderItemDTO> Items
    );