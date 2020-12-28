using Core.Entities;

namespace Core.Specifications
{
    public class ProductsWithFiltersAndPaginationSpecification : Specification<Product>
    {
        public ProductsWithFiltersAndPaginationSpecification(ProductSpecificationParameters parameters)
            : base(p =>
                string.IsNullOrEmpty(parameters.Search) || p.Name.ToLower().Contains(parameters.Search) &&
                !parameters.BrandId.HasValue || p.ProductBrandId == parameters.BrandId &&
                !parameters.TypeId.HasValue || p.ProductTypeId == parameters.TypeId)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
            AddOrderBy(p => p.Name);
            ApplyPagination(
                parameters.PageSize * (parameters.PageIndex - 1),
                parameters.PageSize);

            if (!string.IsNullOrEmpty(parameters.OrderBy))
            {
                switch (parameters.OrderBy)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.Price);
                        break;
                    default:
                        AddOrderBy(p => p.Name);
                        break;
                }
            }
        }
    }
}

