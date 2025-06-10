namespace Ordering.Orders.DTOs;
public record PaymentDTO(string CardName, string CardNumber, string Expiration, string Cvv, int PaymentMethod);