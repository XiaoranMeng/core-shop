using AutoMapper;
using Core.Entities;
using Core.Intefaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Web.DTOs;

namespace Web.Controllers
{
    
    public class ProductsController : BaseController
    {
        private readonly IAsyncRepository<Product> _productRepository;
        private readonly IAsyncRepository<ProductBrand> _brandRepository;
        private readonly IAsyncRepository<ProductType> _typeRepository;
        private readonly IMapper _mapper;

        public ProductsController(
            IAsyncRepository<Product> productRepository,
            IAsyncRepository<ProductBrand> brandRepository,
            IAsyncRepository<ProductType> typeRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var specification = new ProductWithBrandsAndTypesSpecification(id);
            var product = await _productRepository.GetByIdAsync(specification);
            var response = _mapper.Map<Product, ProductDTO>(product);
            return response;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductDTO>>> GetProducts(string orderBy)
        {
            var specification = new ProductsWithBrandsAndTypesSpecification(orderBy);
            var products = await _productRepository.GetListAsync(specification);
            var response = _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductDTO>>(products);
            return Ok(response);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<List<ProductBrand>>> GetProductBrands()
        {
            var brands = await _brandRepository.GetItemsAsync();
            return Ok(brands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<List<ProductType>>> GetProductTypes()
        {
            var types = await _typeRepository.GetItemsAsync();
            return Ok(types);
        }
    }
}