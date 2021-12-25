using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MissionariesAndCannibals.Controllers
{
    public class WebApiController : ApiController
    {
        [HttpGet]
        [Route("api/getSolution")]
        public List<State> MNCSolution()
        {
            var env = new Environment();
            var solution = env.BFS();

            var result = env.GetSolution(solution);

            return result;
        }
    }
}
