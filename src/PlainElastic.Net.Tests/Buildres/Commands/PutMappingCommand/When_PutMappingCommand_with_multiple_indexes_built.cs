﻿using Machine.Specifications;

namespace PlainElastic.Net.Tests.Buildres.Commands
{
    [Subject(typeof(PutMappingCommand))]
    class When_PutMappingCommand_with_multiple_indexes_built
    {

        Because of = () => result = new PutMappingCommand(indexes: new[] { "Index1", "Index2" }, types: new[] { "Type1", "Type2" })
            .BuildCommand();


        It should_starts_with_indexes_and_types = () => result.ShouldStartWith("index1,index2/type1,type2");

        It should_contains_mnapping_verb = () => result.ShouldContain("_mapping");

        It should_return_correct_value = () => result.ShouldEqual(@"index1,index2/type1,type2/_mapping");


        private static string result;
    }
}
