using HanuEdmsApi.Converter;
using HanuEdmsApi.EF;
using HanuEdmsApi.Exceptions;
using HanuEdmsApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HanuEdmsApi.Controllers
{
    [Route("/grades")]
    [ApiController]
    public class GradesController : ControllerBase
    {
        private readonly HanuEdmsContext Context;
        private readonly OneWayConverter<Models.Grades, ViewModels.Grades> Converter;

        public GradesController(HanuEdmsContext context)
        {
            Context = context;
            Converter = new GradesConverter();
        }

        [HttpGet]
        public IActionResult GetGradesByStudentId([FromQuery] int studentId, [FromQuery] string authToken)
        {
            // authentication
            string username = JwtUtils.ValidateJWT(authToken);
            if (username is null)
            {
                return Unauthorized(new MissingAuthTokenException());
            }

            var gradesList = Context.Student
                                .Include(s => s.Grades).ThenInclude(g => g.Course)
                                .Where(s => s.Id == studentId)
                                .FirstOrDefault()
                                ?.Grades
                                .Select(g => Converter.ForwardConverter(g))
                                .ToList();
            return Ok(gradesList);
        }
    }
}