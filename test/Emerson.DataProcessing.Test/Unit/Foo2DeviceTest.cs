using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Application.Services;
using Emerson.DataProcessing.Domain.Models;
using FakeItEasy;
using Shouldly;

namespace Emerson.DataProcessing.Test.Unit
{
    public class Foo2DeviceTest
    {
        private readonly IJsonParser _jsonParser;

        private readonly IFoo2Device _foo2Device;
        public Foo2DeviceTest()
        {
            this._jsonParser = A.Fake<IJsonParser>();
            this._foo2Device = new Foo2Device(this._jsonParser);
        }

        [Fact]
        public async Task When_Device_Data_Is_In_Correct_Format()
        {
            //Arrange
            Foo2 foo2 = new Foo2()
            {
                CompanyId = 1234,
                CompanyName = "Test",
                Devices = new List<Device>(){
                    new Device()
                    {
                        DeviceId = 111,
                        DeviceName = "A1234",
                        SensorData = new List<SensorDatum>(),
                    },
                }
            };

            A.CallTo(() => this._jsonParser.ParseJson<Foo2>(A<string>._))
               .Returns(foo2);

            //Act
            var result = await _foo2Device.Get();

            //Assert
            result.ShouldNotBe(null);
            result.Devices.Count.ShouldBeGreaterThan(0);
        }

        [Fact]
        public async Task When_Device_Data_Not_Found_Throw_FileNotFoundException()
        {
            //Arrange
            A.CallTo(() => this._jsonParser.ParseJson<Foo2>(A<string>._))
                .Throws(new FileNotFoundException("file not found"));


            Foo2Device foo2Device = new Foo2Device(this._jsonParser);

            //Act
            var exception = await Should.ThrowAsync<FileNotFoundException>(() => foo2Device.Get());

            //Assert
            exception.ShouldNotBe(null);
            exception.Message.ShouldBe("file not found");
        }

        [Fact]
        public async Task When_Device_Data_Not_In_Correct_Format_Throw_InvalidDataException()
        {
            //Arrange
            A.CallTo(() => this._jsonParser.ParseJson<Foo2>(A<string>._))
                .Throws(new InvalidDataException("file is not in correct format"));


            Foo2Device foo2Device = new Foo2Device(this._jsonParser);

            //Act
            var exception = await Should.ThrowAsync<InvalidDataException>(() => foo2Device.Get());

            //Assert
            exception.ShouldNotBe(null);
            exception.Message.ShouldBe("file is not in correct format");
        }
    }
}