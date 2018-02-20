using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CarrosUsadosAzureSearch.Api.Controllers.Base
{
    [Produces("application/json")]
    [Route("api/v1/[controller]")]
    public class BaseController : Controller
    {
    }
}
