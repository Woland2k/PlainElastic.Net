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

        /// <summary>
        /// Determines whether the filter has all the required information.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if it does; otherwise, <c>false</c>.
        /// </returns>
        protected override bool HasRequiredParts()
        {
            return hasValues;
        }

        /// <summary>
        /// Filters the specified filter.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <returns></returns>
        public FilterFacet<T> Filter(Func<Filter<T>, Filter<T>> filter)
        {
            var result = RegisterJsonPartExpression(filter);
            hasValues = hasValues || !result.GetIsEmpty();
            return this;
        }

        /// <summary>
        /// Applies the json template.
        /// </summary>
        /// <param name="body">The body.</param>
        /// <returns></returns>
        protected override string ApplyJsonTemplate(string body)
        {
            return "{0}: {{ {1} }}".AltQuoteF(facetName, body);
        }

    }
}
