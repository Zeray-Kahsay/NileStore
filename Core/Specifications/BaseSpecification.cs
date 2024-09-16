using System.Linq.Expressions;
using Core.Interfaces;

namespace Core.Specification;

public class BaseSpecification<T> : ISpecification<T>
{
  public Expression<Func<T, bool>> Criteria => throw new NotImplementedException();
}
