﻿using Stock.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Core.Specifications
{
    public class BaseSpecifications<T> : ISpecifications<T> where T : BaseEntity
    {
        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
        public Expression<Func<T, object>> OrderByAsyning { get; set; }
        public Expression<Func<T, object>> OrderByDsyning { get; set; }
      

        public BaseSpecifications()
        {

        }
        public BaseSpecifications(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        public void OrderByAsyn(Expression<Func<T, object>> OrderByAsynExpression)
        {
            OrderByAsyning = OrderByAsynExpression;
        }
        public void OrderByDsyn(Expression<Func<T, object>> OrderByDsynExpression)
        {
            OrderByDsyning = OrderByDsynExpression;
        }

    }
}
