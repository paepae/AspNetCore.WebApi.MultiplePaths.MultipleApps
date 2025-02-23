﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace PaePae.Test.AspNetCore.WebApi.MultiplePaths.MultipleApps.V1.Controllers
{
    [ApiController]
    [Route("dummy")]
    public class DummyController : ControllerBase
    {
        private readonly ILogger<DummyController> logger;

        public DummyController(ILogger<DummyController> logger)
        {
            this.logger = logger;
        }

        [HttpGet]
        public string Index()
        {
            return "Hello V1";
        }
    }
}
