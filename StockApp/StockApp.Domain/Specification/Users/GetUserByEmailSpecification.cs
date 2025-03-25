using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Users;

public class GetUserByEmailSpecification(string email) : Specification<User>
{
    public override Expression<Func<User, bool>> ToExpression()
        => user => user.Email == email;
}