﻿using Microsoft.AspNetCore.Mvc;

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
