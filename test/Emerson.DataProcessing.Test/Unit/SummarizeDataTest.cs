using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Application.Models;
using Emerson.DataProcessing.Application.Services;
using Emerson.DataProcessing.Application.Settings;
using Emerson.DataProcessing.Domain.Models;
using FakeItEasy;
using Microsoft.Extensions.Options;
using Shouldly;

namespace Emerson.DataProcessing.Test.Unit
{
    public class SummarizeDataTest
    {
        private readonly IFooDevice _fooDevice;

        private readonly ISummarizeData _summarizeData;

        private readonly IOptions<CompanyOptions> _companyOptions; 
        public SummarizeDataTest()
        {
            _fooDevice = A.Fake<IFooDevice>();

            _companyOptions = A.Fake<IOptions<CompanyOptions>>();

            _summarizeData = new SummarizeData(_fooDevice, _companyOptions);
        }

        [Fact]
        public async Task Validate_Summarize_Data()
        {
            //Arrange
            Foo1 foo1 = GetFakeFoo1Data();
            Foo2 foo2 = GetFakeFoo2Data();

            A.CallTo(() => this._fooDevice.Get<Foo1>(A<string>._))
               .Returns(foo1);

            A.CallTo(() => this._fooDevice.Get<Foo2>(A<string>._))
               .Returns(foo2);

            IEnumerable<SummarizeDevice> summarizeDevices = await _summarizeData.Get();

            summarizeDevices.ShouldNotBe(null);
            summarizeDevices.Count().ShouldBe(2);
            
            var device1 = summarizeDevices.Where(p => p.DeviceName == "Foo1").FirstOrDefault();
            device1.ShouldNotBe(null);
            device1?.FirstReadingDtm.ShouldBe(new DateTime(2023,03,10,10,35,0));
            device1?.LastReadingDtm.ShouldBe(new DateTime(2023,03,22,10,35,0));
            device1?.TemperatureCount.ShouldBe(3);
            device1?.AverageTemperature.ShouldBe(13);
            device1?.HumidityCount.ShouldBe(3);
            device1?.AverageHumdity.ShouldBe(20);

            var device2 = summarizeDevices.Where(p => p.DeviceName == "Foo2").FirstOrDefault();
            device2.ShouldNotBe(null);
            device2?.FirstReadingDtm.ShouldBe(new DateTime(2023,03,15,10,35,0));
            device2?.LastReadingDtm.ShouldBe(new DateTime(2023,03,18,10,35,0));
            device2?.TemperatureCount.ShouldBe(2);
            device2?.AverageTemperature.ShouldBe(15.5);
            device2?.HumidityCount.ShouldBe(2);
            device2?.AverageHumdity.ShouldBe(18);
        }

        [Fact]
        public async Task When_Device_Throw_Exception_Should_Return_null()
        {
            A.CallTo(() => this._fooDevice.Get<Foo1>(A<string>._))
               .Throws(new InvalidDataException("file 1 is not in correct format"));

            var exception = await Should.ThrowAsync<InvalidDataException>(() => _summarizeData.Get());

            exception.ShouldNotBe(null);
            exception.Message.ShouldBe("file 1 is not in correct format");
        }

        private Foo2 GetFakeFoo2Data()
        {
            return new Foo2()
            {
                CompanyId = 2423,
                CompanyName = "Test2",
                Devices = new List<Device>(){
                    new Device()
                    {
                        DeviceId = 111,
                        DeviceName = "Foo2",
                        SensorData = new List<SensorDatum>()
                        {
                            new SensorDatum()
                            {
                                DateTime = "03-15-2023 10:35:00",
                                Value = 15.0,
                                SensorType = "TEMP",
                            },
                            new SensorDatum()
                            {
                                DateTime = "03-16-2023 10:35:00",
                                Value = 16.0,
                                SensorType = "TEMP",
                            },
                            new SensorDatum()
                            {
                                DateTime = "03-17-2023 10:35:00",
                                Value = 16.0,
                                SensorType = "HUM",
                            },
                            new SensorDatum()
                            {
                                DateTime = "03-18-2023 10:35:00",
                                Value = 20.0,
                                SensorType = "HUM",
                            }
                        },
                    },
                }
            };
        }
        private Foo1 GetFakeFoo1Data()
        {
            return new Foo1()
            {
                CompanyId = 1234,
                CompanyName = "Test",
                Trackers = new List<Tracker>(){
                    new Tracker()
                    {
                        DeviceId = 111,
                        DeviceName = "Foo1",
                        Sensors = new List<Sensor>()
                        {
                            new Sensor()
                            {
                                Id = 9876,
                                Name = "Temperature",
                                Crumbs = new List<Crumb>()
                                {
                                    new Crumb()
                                    {
                                        CreatedDtm = "03-21-2023 10:35:00",
                                        Value = 12.5,
                                    },
                                    new Crumb()
                                    {
                                        CreatedDtm = "03-22-2023 10:35:00",
                                        Value = 13,
                                    },
                                    new Crumb()
                                    {
                                        CreatedDtm = "03-20-2023 10:35:00",
                                        Value = 13.5,
                                    }
                                }
                            },
                              new Sensor()
                            {
                                Id = 9876,
                                Name = "Humidty",
                                Crumbs = new List<Crumb>()
                                {
                                    new Crumb()
                                    {
                                        CreatedDtm = "03-10-2023 10:35:00",
                                        Value = 10,
                                    },
                                    new Crumb()
                                    {
                                        CreatedDtm = "03-11-2023 10:35:00",
                                        Value = 20,
                                    },
                                    new Crumb()
                                    {
                                        CreatedDtm = "03-12-2023 10:35:00",
                                        Value = 30,
                                    }
                                }
                            }
                        },
                    },
                },
            };

        }
    }
}