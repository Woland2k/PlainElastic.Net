using PlainElastic.Net.Utils;
using System;
using System.Linq;

namespace PlainElastic.Net.Queries
{
    /// <summary>
    /// A filter that matches documents matching boolean combinations of other queries. 
    /// Similar in concept to Boolean query, except that the clauses are other filters. 
    /// Can be placed within queries that accept a filter.
    /// see: http://www.elasticsearch.org/guide/reference/query-dsl/bool-filter.html
    /// </summary>
    public class BoolFilter<T> : Filter<T>
    {
        private int shouldPartsCount;
        private bool hasRequiredParts;

        public BoolFilter<T> Must(Func<MustFilter<T>, Filter<T>> mustFilter)
        {
            var result = RegisterJsonPartExpression(mustFilter);
            hasRequiredParts = hasRequiredParts || !result.GetIsEmpty();
            return this;
        }

        public BoolFilter<T> MustNot(Func<MustNotFilter<T>, Filter<T>> mustNotFilter)
        {
            var result = RegisterJsonPartExpression(mustNotFilter);
            hasRequiredParts = hasRequiredParts || !result.GetIsEmpty();
            return this;
        }

        public BoolFilter<T> Should(Func<ShouldFilter<T>, Filter<T>> shouldFilter)
        {
            var shouldResult = RegisterJsonPartExpression(shouldFilter);
            hasRequiredParts = hasRequiredParts || !shouldResult.GetIsEmpty();

            shouldPartsCount = shouldResult.JsonParts.Count();

            return this;
        }

        protected override bool HasRequiredParts()
        {
            return hasRequiredParts;
        }

        protected override string ApplyJsonTemplate(string body)
        {
            return "{{ 'bool': {{ {0} }} }}".AltQuoteF(body);
        }
    }
}