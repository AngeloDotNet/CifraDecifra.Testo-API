using System;
using API_Cifra_Decifra_Testo.Models.InputModels;
using API_Cifra_Decifra_Testo.Models.Services.Application;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API_Cifra_Decifra_Testo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManipulationController : ControllerBase
    {
        private readonly ILogger<ManipulationController> logger;
        private readonly IManipulationTextService manipulatorService;
        private readonly IWebHostEnvironment env;

        public ManipulationController(ILogger<ManipulationController> logger, IManipulationTextService manipulatorService, IWebHostEnvironment env)
        {
            this.logger = logger;
            this.manipulatorService = manipulatorService;
            this.env = env;
        }

        [HttpGet("Welcome")]
        public IActionResult Welcome()
        {
            return Ok(string.Concat("Ciao sono le ore: ", DateTime.Now.ToLongTimeString()));
        }

        [HttpPost("Testo-Encrypt")]
        public IActionResult TestoEncrypt([FromForm] InputTesto model)
        {
            try
            {
                string testo = manipulatorService.TestoCifratoGeneratorAsync(model, env);
                return Ok(testo);
            }
            catch(Exception exc)
            {
                return StatusCode(500, exc.ToString());
            }
        }

        [HttpPost("Testo-Decrypt")]
        public IActionResult TestoDecrypt([FromForm] InputTesto model)
        {
            try
            {
                string testo = manipulatorService.TestoCifratoRestoreAsync(model, env);
                return Ok(testo);
            }
            catch(Exception exc)
            {
                return StatusCode(500, exc.ToString());
            }
        }
    }
}