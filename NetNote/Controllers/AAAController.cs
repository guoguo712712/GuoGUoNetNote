using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NetNote.Controllers
{
    [Route("api/[controller]")]   
    [ApiController]
    public class AAAController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "AAA";
        }

        [HttpPost]
        public string Post(string name)
        {
            return $"Hello,{name}";
        }
    }
}