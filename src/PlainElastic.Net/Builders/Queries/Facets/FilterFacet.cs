using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PlainElastic.Net.Utils;

namespace PlainElastic.Net.Queries
{
    /// <summary>
    /// Allows you to return a count of the hits matching the filter.
    /// see http://www.elasticsearch.org/guide/reference/api/search/facets/filter-facet.html
    /// </summary>
    public class FilterFacet<T> : QueryBase<FilterFacet<T>>
    {
        private string facetName;
        private bool hasValues;

        /// <summary>
        /// The name of the facet used to identify facets results.
        /// </summary>
        public FilterFacet<T> FacetName(string facetName)
        {
            this.facetName = facetName.Quotate();

            return this;
        }

        protected override bool HasRequiredParts()
        {
            return hasValues;
        }

        public FilterFacet<T> Filter(Func<Filter<T>, Filter<T>> filter)
        {
            var result = RegisterJsonPartExpression(filter);
            hasValues = hasValues || !result.GetIsEmpty();
            return this;
        }

        protected override string ApplyJsonTemplate(string body)
        {
            return "{0}: {{ {1} }}".AltQuoteF(facetName, body);
        }

    }
}
