using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BoBo.ASPNETCore.TestEndpoints.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    [HttpGet]
    public IEnumerable<object> Get()
    {
        throw new InvalidTimeZoneException("wow");
    }
}
