using Emerson.DataProcessing.Application.Helper;
using Emerson.DataProcessing.Domain.Models;
using Shouldly;

namespace Emerson.DataProcessing.Test.Unit
{
    public class JsonParserTest
    {
        [Fact]
        public async Task When_Foo1_Data_Is_In_Correct_Format()
        {
            JsonParser jsonParser = new JsonParser();
            Foo1 foo1 = await jsonParser.ParseJson<Foo1>("DeviceDataFoo1.json");

            foo1.ShouldNotBe(null);
        }

        [Fact]
        public async Task When_Foo2_Data_Is_In_Correct_Format()
        {
            JsonParser jsonParser = new JsonParser();
            Foo2 foo2 = await jsonParser.ParseJson<Foo2>("DeviceDataFoo2.json");

            foo2.ShouldNotBe(null);
        }

        [Fact]
        public async Task When_Invalid_File_Provied_FileNotFoundException_Exception_Is_Thrown()
        {
            JsonParser jsonParser = new JsonParser();
            var exception = await Should.ThrowAsync<FileNotFoundException>(() => jsonParser.ParseJson<Foo2>("DeviceDataFoo3.json"));

            exception.ShouldNotBe(null);
        }
    }
}