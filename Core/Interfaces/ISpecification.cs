
using System.Linq.Expressions;

namespace Core.Interfaces;

public interface ISpecification<T>
{
  Expression<Func<T, bool>>? Criteria { get; }
  Expression<Func<T, object>>? OrderBy { get; }
  Expression<Func<T, object>>? OrderByDescending { get; }
  bool IsDistinct { get; }

}

/*
eks. takes product and returns a type other than product, like brands, types .... this is projection
<T, TResult> takes T and returns TResult
*/
public interface ISpecification<T, TResult> : ISpecification<T>
{
  Expression<Func<T, TResult>>? Select { get; }
}
