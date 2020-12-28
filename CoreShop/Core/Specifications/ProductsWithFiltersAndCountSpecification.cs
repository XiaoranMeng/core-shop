using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersAndCountSpecification : Specification<Product>
    {
        public ProductsWithFiltersAndCountSpecification(ProductSpecificationParameters parameters)
            : base(p =>
                (string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search)) &&
                (parameters.BrandId == 0 || p.ProductBrandId == parameters.BrandId) &&
                (parameters.TypeId == 0 || p.ProductTypeId == parameters.TypeId))
        {

        }
    }
}
