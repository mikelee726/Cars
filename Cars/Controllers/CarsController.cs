using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Cars.Controllers
{
    [ApiController]
    [Route("Cars")]
    public class CarsController : ControllerBase
    {
        private readonly string pathToFile;
        private IEnumerable<Auto> autos;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<CarsController> _logger;

        public CarsController(ILogger<CarsController> logger, IWebHostEnvironment env)
        {
            _logger = logger;

            pathToFile = env.ContentRootPath
            + Path.DirectorySeparatorChar.ToString()
            + "Data"
            + Path.DirectorySeparatorChar.ToString()
            + "honda_wmi.json";
        }

        private IEnumerable<Auto> GetDataSet()
        {
            string source = "";

            using (StreamReader SourceReader = System.IO.File.OpenText(pathToFile))
            {
                source = SourceReader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<IEnumerable<Auto>>(source);
        }
        [HttpGet]
        public IEnumerable<Auto> Get([FromQuery] AutoFilter filter)
        {
            autos = GetDataSet();

            if (string.IsNullOrEmpty(filter.SearchText) == false)
            {
                autos = autos.Where(x => x.Name.ToLower().Contains(filter.SearchText.ToLower()));
            }

            if (filter.Country != "View All")
            {
                autos = autos.Where(x => x.Country == filter.Country);
            }
            return autos.ToArray();
        }

        [HttpGet("countries")]
        public IEnumerable<string> GetCountries()

        {
            autos = GetDataSet();
            var countries = autos.Select(x => x.Country).Where(x => string.IsNullOrEmpty(x) == false).Distinct().ToArray();
            return countries;
        }
    }
}
