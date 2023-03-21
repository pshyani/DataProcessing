using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Application.Models;
using Emerson.DataProcessing.Domain.Models;

namespace Emerson.DataProcessing.Application.Services
{
    public class SummarizeData : ISummarizeData
    {
        public List<SummarizeDevice> result;
        public async Task<IEnumerable<SummarizeDevice>> Get()
        {
            result = new List<SummarizeDevice>();

            var foo1Data = new Foo1Device().Get();

            var foo2Data = new Foo2Device().Get();

            await Task.WhenAll(foo1Data, foo2Data);

            ProcessData(foo1Data.Result);
            ProcessData(foo2Data.Result);

            return result;
        }

        private void ProcessData(Foo1 foo1)
        {
            Parallel.ForEach(foo1.Trackers, tracker =>
            {
                SummarizeDevice summarizeDevice = new SummarizeDevice();
                summarizeDevice.CompanyId = foo1.CompanyId;
                summarizeDevice.CompanyName = foo1.CompanyName;
                summarizeDevice.DeviceId = tracker.DeviceId;
                summarizeDevice.DeviceName = tracker.DeviceName;
                summarizeDevice.FirstReadingDtm = tracker.Sensors
                                                    .SelectMany(s => s.Crumbs)
                                                    .Min(c => DateTime.Parse(c.CreatedDtm));
                summarizeDevice.LastReadingDtm = tracker.Sensors.SelectMany(s => s.Crumbs).Max(c => DateTime.Parse(c.CreatedDtm));
                summarizeDevice.TemperatureCount = tracker.Sensors
                                                    .Where(p => string.Equals(p.Name, "Temperature", StringComparison.OrdinalIgnoreCase))
                                                    .SelectMany(s => s.Crumbs)
                                                    .Count();
                summarizeDevice.HumidityCount = tracker.Sensors
                                                    .Where(p => string.Equals(p.Name, "Humidty", StringComparison.OrdinalIgnoreCase))
                                                    .SelectMany(s => s.Crumbs)
                                                    .Count();
                summarizeDevice.AverageTemperature = Math.Round(tracker.Sensors
                                                    .Where(p => string.Equals(p.Name, "Temperature", StringComparison.OrdinalIgnoreCase))
                                                    .SelectMany(s => s.Crumbs)
                                                    .Average(c => c.Value), 2);
                summarizeDevice.AverageHumdity = Math.Round(tracker.Sensors
                                                    .Where(p => string.Equals(p.Name, "Humidty", StringComparison.OrdinalIgnoreCase))
                                                    .SelectMany(s => s.Crumbs)
                                                    .Average(c => c.Value), 2);
                result.Add(summarizeDevice);
            });
        }

        private void ProcessData(Foo2 foo2)
        {
            Parallel.ForEach(foo2.Devices, device =>
            {
                SummarizeDevice summarizeDevice = new SummarizeDevice();
                summarizeDevice.CompanyId = foo2.CompanyId;
                summarizeDevice.CompanyName = foo2.CompanyName;
                summarizeDevice.DeviceId = device.DeviceId;
                summarizeDevice.DeviceName = device.DeviceName;
                summarizeDevice.FirstReadingDtm = device.SensorData
                                                    .Min(c => DateTime.Parse(c.DateTime));
                summarizeDevice.LastReadingDtm = device.SensorData.Max(c => DateTime.Parse(c.DateTime));
                summarizeDevice.TemperatureCount = device.SensorData
                                                    .Where(p => string.Equals(p.SensorType, "TEMP", StringComparison.OrdinalIgnoreCase))
                                                    .Count();
                summarizeDevice.HumidityCount = device.SensorData
                                                    .Where(p => string.Equals(p.SensorType, "HUM", StringComparison.OrdinalIgnoreCase))
                                                    .Count();
                summarizeDevice.AverageTemperature = Math.Round(device.SensorData
                                                    .Where(p => string.Equals(p.SensorType, "TEMP", StringComparison.OrdinalIgnoreCase))
                                                    .Average(c => c.Value), 2);
                summarizeDevice.AverageHumdity = Math.Round(device.SensorData
                                                    .Where(p => string.Equals(p.SensorType, "HUM", StringComparison.OrdinalIgnoreCase))
                                                    .Average(c => c.Value), 2);
                result.Add(summarizeDevice);
            });
        }
    }
}