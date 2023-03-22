using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Application.Services;
using Emerson.DataProcessing.Domain.Models;
using FakeItEasy;
using Shouldly;

namespace Emerson.DataProcessing.Test;

public class Foo1DeviceTest
{
    private readonly IJsonParser _jsonParser;

    private readonly IFoo1Device _foo1Device;
    public Foo1DeviceTest()
    {
        _jsonParser = A.Fake<IJsonParser>();
         _foo1Device = new Foo1Device(this._jsonParser);
    }

    [Fact]
    public async Task When_Device_Data_Is_In_Correct_Format()
    {
        //Arrange
        Foo1 foo1 = new Foo1(){
            CompanyId = 1234,
            CompanyName = "Test",
            Trackers = new List<Tracker>(){
                new Tracker()
                {
                    DeviceId = 111,
                    DeviceName = "A1234",
                    Sensors = new List<Sensor>(),
                },
            },
        };

         A.CallTo(() => this._jsonParser.ParseJson<Foo1>(A<string>._))
            .Returns(foo1);

        //Act
        var result = await _foo1Device.Get();

        //Assert
        result.ShouldNotBe(null);
        result.Trackers.Count.ShouldBeGreaterThan(0);
    }

    [Fact]
    public async Task When_Device_Data_Not_Found_Throw_FileNotFoundException()
    {
        //Arrange
        A.CallTo(() => this._jsonParser.ParseJson<Foo1>(A<string>._))
            .Throws(new FileNotFoundException("file not found"));


        Foo1Device foo1Device = new Foo1Device(this._jsonParser);

        //Act
        var exception = await Should.ThrowAsync<FileNotFoundException>(() => foo1Device.Get());

        //Assert
        exception.ShouldNotBe(null);
        exception.Message.ShouldBe("file not found");
    }

    [Fact]
    public async Task When_Device_Data_Not_In_Correct_Format_Throw_InvalidDataException()
    {
        //Arrange
        A.CallTo(() => this._jsonParser.ParseJson<Foo1>(A<string>._))
            .Throws(new InvalidDataException("file is not in correct format"));


        Foo1Device foo1Device = new Foo1Device(this._jsonParser);

        //Act
        var exception = await Should.ThrowAsync<InvalidDataException>(() => foo1Device.Get());

        //Assert
        exception.ShouldNotBe(null);
        exception.Message.ShouldBe("file is not in correct format");
    }
}