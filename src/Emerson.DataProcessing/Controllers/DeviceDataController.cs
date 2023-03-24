using System.Text.Json;
using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Application.Models;
using Emerson.DataProcessing.Application.Settings;
using Emerson.DataProcessing.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Emerson.DataProcessing.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class DeviceDataController : ControllerBase
{
    private readonly string _jsonPath = "Json";
    private readonly ILogger<DeviceDataController> _logger;
    private readonly IFooDevice _fooDevice;
    private readonly ISummarizeData _summarizeData;

    private readonly CompanyOptions _companyOptions;

    public DeviceDataController(ILogger<DeviceDataController> logger,
                                IFooDevice fooDevice,
                                ISummarizeData summarizeData,
                                IOptions<CompanyOptions> companyOptions)
    {
        _logger = logger;
        _fooDevice = fooDevice;
        _summarizeData = summarizeData;
        _companyOptions = companyOptions.Value;
    }

    [HttpGet("Foo1")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Foo1))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Foo1>> GetFoo1Data()
    {
        var foo1 = await _fooDevice.Get<Foo1>(_companyOptions.Foo1_Json);

        return (foo1 != null) ? Ok(foo1) : NotFound();
    }

    [HttpGet("Foo2")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Foo2))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Foo1>> GetFoo2Data()
    {
        var foo2 = await _fooDevice.Get<Foo2>(_companyOptions.Foo2_Json);

        return (foo2 != null) ? Ok(foo2) : NotFound();
    }

    [HttpGet("Summarize")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SummarizeDevice))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<SummarizeDevice>> GetSummarizeData()
    {
        var result = await _summarizeData.Get();

        SaveSummarizeJson(result);

        return (result != null) ? Ok(result) : NotFound();
    }

    private void SaveSummarizeJson(IEnumerable<SummarizeDevice> result)
    {
        var jsonString = JsonSerializer.Serialize(result);

        string strFile = string.Concat(Directory.GetCurrentDirectory(),
                                "/", _jsonPath, "/", _companyOptions.Summarize_Json);

        if (System.IO.File.Exists(strFile))
        {
            System.IO.File.Delete(strFile);
        }

        System.IO.File.WriteAllText(strFile, jsonString);
    }
}
