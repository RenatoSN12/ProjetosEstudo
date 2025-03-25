using System.Linq.Expressions;
using StockApp.Domain.Abstractions;
using StockApp.Domain.Entities;

namespace StockApp.Domain.Specification.Items;

public class GetProductByIdSpecification(long id) : Specification<Item>
{
    public override Expression<Func<Item, bool>> ToExpression()
        => item => item.Id == id;    
}