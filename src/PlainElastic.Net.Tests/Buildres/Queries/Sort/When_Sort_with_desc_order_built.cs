﻿using Machine.Specifications;
using PlainElastic.Net.Queries;
using PlainElastic.Net.Utils;

namespace PlainElastic.Net.Tests.Buildres.Queries
{
    [Subject(typeof(Sort<>))]
    class When_Sort_with_desc_order_built
    {
        Because of = () => result = new Sort<FieldsTestClass>()
                                                .Field("field", order: SortDirection.desc, missing: "_last")
                                                .ToString();

        It should_not_contain_order_part = () => result.ShouldNotContain("order");

        It should_contain_correct_missing_value = () => result.ShouldContain("'missing': '_last'".AltQuote());

        It should_return_correct_value = () => result.ShouldEqual("'sort': [{ 'field': { 'missing': '_last' } }]".AltQuote());

        private static string result;
    }
}
