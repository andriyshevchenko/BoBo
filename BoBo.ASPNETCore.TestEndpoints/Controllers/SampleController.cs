using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace BoBo.ASPNETCore.TestEndpoints.Controllers;

[ApiController]
[Route("[controller]")]
public class SampleController : ControllerBase
{
    /// <summary>
    /// Endpoint used exclusively for testing the middleware.
    /// It always throws an exception.
    /// </summary>
    /// <returns>This method never returns; it always throws an exception.</returns>
    [HttpGet]
    public IEnumerable<object> Get()
    {
        throw new InvalidTimeZoneException("wow", new ArgumentOutOfRangeException("such fun"));
    }
}
