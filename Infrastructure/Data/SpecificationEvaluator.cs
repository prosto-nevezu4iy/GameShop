﻿using ApplicationCore.Entities;
using ApplicationCore.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class SpecificationEvaluator<T> where T : class, IAggregateRoot
    {
        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecification<T> specification)
        {
            var query = inputQuery;

            // modify the IQueryable using the specification's criteria expression
            if (specification.Criteria != null)
                query = query.Where(specification.Criteria);

            // Includes all expression-based includes
            query = specification.Includes.Aggregate(query,
                                    (current, include) => current.Include(include));

            // Apply ordering if expressions are set
            if (specification.OrderBy != null)
                query = query.OrderBy(specification.OrderBy);
            else if (specification.OrderByDescending != null)
                query = query.OrderByDescending(specification.OrderByDescending);

            // Apply paging if enabled  
            if (specification.isPagingEnabled)
            {
                query = query.Skip(specification.Skip)
                             .Take(specification.Take);
            }

            return query;
        }
    }
}
