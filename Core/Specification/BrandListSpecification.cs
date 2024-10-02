
using Core.Entities;
using Core.Specifications;

namespace Core.Specification;

public class BrandListSpecification : BaseSpecification<Product, string>
{
  public BrandListSpecification()
  {
    AddSelect(p => p.Brand);
    ApplyDistinct();
  }
}
