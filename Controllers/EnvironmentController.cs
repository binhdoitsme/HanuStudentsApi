using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace HanuEdmsApi.Controllers
{
    [Route("/routes")]
    [ApiController]
    public class EnvironmentController : Controller
    {
        private readonly IActionDescriptorCollectionProvider _actionDescriptorCollectionProvider;

        public EnvironmentController(IActionDescriptorCollectionProvider actionDescriptorCollectionProvider)
        {
            _actionDescriptorCollectionProvider = actionDescriptorCollectionProvider;
        }

        [HttpGet]
        public IActionResult GetAllRoutes()
        {
            var routeList = _actionDescriptorCollectionProvider
                                .ActionDescriptors
                                .Items
                                .Select(item => new
                                {
                                    name = item.DisplayName,
                                    routes = item.RouteValues
                                }).ToList();

            return Ok(routeList);
        }
    }
}