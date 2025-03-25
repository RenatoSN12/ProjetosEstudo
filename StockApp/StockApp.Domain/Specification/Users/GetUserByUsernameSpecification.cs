using System.Linq.Expressions;
using StockApp.Domain.Entities;
using StockApp.Domain.Abstractions;

namespace StockApp.Domain.Specification.Users;

public class GetUserByUsernameSpecification(string username) : Specification<User>
{
    public override Expression<Func<User, bool>> ToExpression()
        => user => user.Username == username;
}