using System.Text.Json;
using Emerson.DataProcessing.Application.Interfaces;
using Emerson.DataProcessing.Application.Models;
using Emerson.DataProcessing.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Emerson.DataProcessing.Controllers;

[ApiController]
[Route("[controller]")]
[Produces("application/json")]
public class DeviceDataController : ControllerBase
{
    private readonly ILogger<DeviceDataController> _logger;
    private readonly IFoo1Device _foo1Device;
    private readonly IFoo2Device _foo2Device;
    private readonly ISummarizeData _summarizeData;

    public DeviceDataController(ILogger<DeviceDataController> logger,
                                IFoo1Device foo1Device,
                                IFoo2Device foo2Device,
                                ISummarizeData summarizeData)
    {
        _logger = logger;
        _foo1Device = foo1Device;
        _foo2Device = foo2Device;
        _summarizeData = summarizeData;
    }

    [HttpGet("Foo1")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Foo1))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Foo1>> GetFoo1Data()
    {
        var foo1 = await _foo1Device.Get();

        return (foo1 != null) ? Ok(foo1) : NotFound();
    }

    [HttpGet("Foo2")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Foo2))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Foo1>> GetFoo2Data()
    {
        var foo2 = await _foo2Device.Get();

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

        string strFile = Directory.GetCurrentDirectory() +
                                @"/Json/summarize.json";

        if(System.IO.File.Exists(strFile))
        {
            System.IO.File.Delete(strFile);
        }

        System.IO.File.WriteAllText(strFile, jsonString);
    }
}
