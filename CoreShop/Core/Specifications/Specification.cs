﻿using Core.Intefaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        public Specification()
        {
        }

        public Specification(Expression<Func<T, bool>> predicate)
        {
            Predicate = predicate;
        }

        public Expression<Func<T, bool>> Predicate { get; }

        public List<Expression<Func<T, object>>> Includes { get; } 
            = new List<Expression<Func<T, object>>>();

        public Expression<Func<T, object>> OrderBy { get; private set; }

        public Expression<Func<T, object>> OrderByDescending { get; private set; }

        public int Take { get; private set; }

        public int Skip { get; private set; }

        public bool IsPagingEnabled { get; private set; }

        protected void AddInclude(Expression<Func<T, object>> expression)
        {
            Includes.Add(expression);
        }

        protected void AddOrderBy(Expression<Func<T, object>> expression)
        {
            OrderBy = expression;
        }

        protected void AddOrderByDescending(Expression<Func<T, object>> expression)
        {
            OrderByDescending = expression;
        }

        protected void ApplyPagination(int skip, int take)
        {
            Skip = skip;
            Take = take;
            IsPagingEnabled = true;
        }
    }
}
