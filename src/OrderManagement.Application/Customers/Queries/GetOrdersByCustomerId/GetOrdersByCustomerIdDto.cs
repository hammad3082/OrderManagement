namespace OrderManagement.Application.Customers.Queries.GetOrdersByCustomerId
{
    public record GetOrdersByCustomerIdDto(
        Guid Id,
        Guid CustomerId,
        DateTime OrderDate,
        decimal TotalAmount
    );
}
