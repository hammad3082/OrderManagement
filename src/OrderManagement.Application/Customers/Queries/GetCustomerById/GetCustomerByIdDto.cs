namespace OrderManagement.Application.Customers.Queries.GetCustomerById
{
    public record GetCustomerByIdDto
    (
        Guid Id,
        string Name,
        string Email,
        CustomerProfileDto? Profile,
        List<OrderDto> Orders
    );

    public record CustomerProfileDto(
        string Address,
        string PhoneNumber
    );

    public record OrderDto(
        Guid Id,
        DateTime OrderDate,
        decimal TotalAmount
    );
}
