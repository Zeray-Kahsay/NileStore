using System.Linq.Expressions;
using System.Xml.XPath;
using Core.Interfaces;

namespace Core.Specifications;

public class BaseSpecification<T>(Expression<Func<T, bool>>? criteria) : ISpecification<T>
{
  protected BaseSpecification() : this(null) { }
  public Expression<Func<T, bool>>? Criteria => criteria; // needs to be evaluated by an SpecificationEvaluater class

  public Expression<Func<T, object>>? OrderBy { get; private set; }

  public Expression<Func<T, object>>? OrderByDescending { get; private set; }

  public bool IsDistinct { get; private set; }

  public int Take { get; private set; }

  public int Skip { get; private set; }

  public bool IsPagingEnabled { get; private set; }

  protected void AddOrderBy(Expression<Func<T, object>> OrderByExpression)
  {
    OrderBy = OrderByExpression;
  }
  protected void AddOrderByDescending(Expression<Func<T, object>> OrderByDescendingExpression)
  {
    OrderByDescending = OrderByDescendingExpression;
  }

  protected void ApplyDistinct()
  {
    IsDistinct = true;
  }

  protected void ApplyPaging(int skip, int take)
  {
    Skip = skip;
    Take = take;
    IsPagingEnabled = true;

  }
}

public class BaseSpecification<T, TResult>(Expression<Func<T, bool>> criteria)
      : BaseSpecification<T>(criteria), ISpecification<T, TResult>
{
  protected BaseSpecification() : this(null!) { }

  public Expression<Func<T, TResult>>? Select { get; private set; }

  protected void AddSelect(Expression<Func<T, TResult>> selectExpression)
  {
    Select = selectExpression;
  }
}
