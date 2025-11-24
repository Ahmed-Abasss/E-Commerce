using E_Commerce.Domain.Contracts.Specifications;
using E_Commerce.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Services
{
    internal class BaseSpecifications<TEntity, Tkey> : ISpecifications<TEntity, Tkey> where TEntity : BaseEntity<Tkey>
    {

        #region Criteria
        public Expression<Func<TEntity, bool>> criteria { get; }

        public BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            criteria = CriteriaExpression;

        }
        #endregion

        #region Include
        public ICollection<Expression<Func<TEntity, object>>> IncludeExp { get; } = [];
        protected void AddInclude(Expression<Func<TEntity, object>> expression)
        {
            IncludeExp.Add(expression);
        }
        #endregion

        #region OrderByAscending
        public Expression<Func<TEntity, object>> OrderBy {  get; private set; }

        protected void AddOrderBy(Expression<Func<TEntity, object>> expression)
        {
            OrderBy = expression;
        }

        #endregion

        #region OrderByDescinding
        public Expression<Func<TEntity, object>> OrderByDesc {  get; private set; }

    

        protected void AddOrderByDesc(Expression<Func<TEntity, object>> expression)
        {
            OrderByDesc = expression;
        }

        #endregion

        #region Pagination
        public int Skip {  get; private set; }

        public int Take {  get; private set; }

        public bool IsPaginated { get; private set; }

        protected void ApplyPagination (int pageSize , int pageIndex)
        {
            IsPaginated = true;

            Take = pageSize;

            Skip= (pageIndex-1)*pageSize;
        }
        #endregion

    }
}
