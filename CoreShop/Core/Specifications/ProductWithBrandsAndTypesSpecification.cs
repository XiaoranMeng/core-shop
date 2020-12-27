using Core.Entities;

namespace Core.Specifications
{
    public class ProductWithBrandsAndTypesSpecification : Specification<Product>
    {
        public ProductWithBrandsAndTypesSpecification(int id)
           : base(p => p.Id == id)
        {
            AddInclude(p => p.ProductBrand);
            AddInclude(p => p.ProductType);
        }
    }
}
