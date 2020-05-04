using HanuEdmsApi.Converter;
using HanuEdmsApi.EF;
using HanuEdmsApi.Helpers;
using HanuEdmsApi.Models;
using HanuEdmsApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HanuEdmsApi.Controllers
{
    [Route("/timetable")]
    [ApiController]
    public class TimetableController : ControllerBase
    {
        private readonly HanuEdmsContext Context;
        private readonly OneWayConverter<Timetable, TimetableUnit> Converter;

        public TimetableController(HanuEdmsContext context)
        {
            Context = context;
            Converter = new TimetableUnitConverter();
        }

        [HttpGet]
        public IActionResult GetAllTimetable([FromQuery] int semester, [FromQuery] string authToken)
        {
            // authentication
            string username = JwtUtils.ValidateJWT(authToken);
            if (username is null)
            {
                return Unauthorized();
            }
            int studentId = int.Parse(username);

            ICollection<int> registeredCourseIds = Context.Student
                                                    .Include(s => s.Registration)
                                                    .Where(s => s.Id == studentId)
                                                    .FirstOrDefault()?
                                                    .Registration.Where(r => r.SemesterId == 20192 && r.Status == true)
                                                    .Select(r => r.CourseClassId)
                                                    .ToList();

            return new JsonResult(Context.CourseClass
                    .Include(cc => cc.Timetable)
                    .Include(cc => cc.Course)
                    .Where(cc => registeredCourseIds.Contains(cc.CourseClassId))
                    .AsEnumerable()
                    .Select(cc => cc.Timetable)
                    .SelectMany(t => t)
                    .Select(t => Converter.ForwardConverter(t))
                    .ToList());
        }
    }
}