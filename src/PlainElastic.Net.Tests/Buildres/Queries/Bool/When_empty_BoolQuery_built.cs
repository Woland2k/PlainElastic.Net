﻿using Machine.Specifications;
using PlainElastic.Net.Queries;


namespace PlainElastic.Net.Tests.Buildres.Queries
{
    [Subject(typeof(BoolQuery<>))]
    class When_empty_BoolQuery_built
    {
        Because of = () => result = new BoolQuery<FieldsTestClass>()
                                                .Must(m => m
                                                )
                                                .MustNot(mn => mn                                                    
                                                )
                                                .Should(s => s                                                    
                                                )
                                                .MinimumNumberShouldMatch(2)
                                                .Boost(10)
                                                .DisableCoord(true)
                                                .ToString();

        It should_return_correct_result = () => result.ShouldBeEmpty();

        private static string result;
    }
}
