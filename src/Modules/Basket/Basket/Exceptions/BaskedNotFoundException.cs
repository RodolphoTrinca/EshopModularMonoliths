using Shared.Exceptions;

namespace Basket.Basket.Exceptions;

public class BaskedNotFoundException(string userName) : NotFoundException("ShoppingCart", userName)
{
}
