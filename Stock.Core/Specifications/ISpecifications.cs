﻿using Stock.Core.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {

        public Expression<Func<T, bool>> Criteria { get; set; }
        public List<Expression<Func<T, object>>> Includes { get; set; }

        public Expression<Func<T, object>> OrderByAsyning { get; set; }

        public Expression<Func<T, object>> OrderByDsyning { get; set; }

    }
}
