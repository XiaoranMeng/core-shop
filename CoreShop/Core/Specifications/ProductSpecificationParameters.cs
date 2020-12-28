﻿namespace Core.Specifications
{
    public class ProductSpecificationParameters
    {
        private const int MAX_PAGE_SIZE = 50;
        private int _pageSize = 6;
        private string _search;

        public string Search 
        {
            get => _search; 
            set => _search = value.ToLower(); 
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value > MAX_PAGE_SIZE
                ? MAX_PAGE_SIZE
                : value;
        }

        public int PageIndex { get; set; } = 1;

        public int? BrandId { get; set; }

        public int? TypeId { get; set; }

        public string OrderBy { get; set; }
    }
}
