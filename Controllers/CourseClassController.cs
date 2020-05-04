using HanuEdmsApi.Converter;
using HanuEdmsApi.EF;
using HanuEdmsApi.Models;
using HanuEdmsApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HanuEdmsApi.Controllers
{
    [Route("/class")]
    [ApiController]
    public class CourseClassController : ControllerBase
    {
        private readonly HanuEdmsContext Context;
        private readonly OneWayConverter<CourseClass, ClassInformation> Converter;

        public CourseClassController(HanuEdmsContext context)
        {
            Context = context;
            Converter = new ClassInformationConverter();
        }

        [HttpGet]
        public IActionResult GetClassInformationForStudent([FromQuery] int semester, 
                                [FromQuery] string classCode, [FromQuery] string authToken)
        {
            // authentication

            CourseClass classFromDb = Context.CourseClass
                                        .Include(cc => cc.Course)
                                            .ThenInclude(c => c.Semester)
                                        .Include(cc => cc.Course)
                                            .ThenInclude(c => c.CourseType)
                                        .Include(cc => cc.Timetable)
                                        .Include(cc => cc.Registration)
                                            .ThenInclude(r => r.Student)
                                            .ThenInclude(s => s.AcademicYear)
                                        .Include(cc => cc.Registration)
                                            .ThenInclude(r => r.Student)
                                            .ThenInclude(s => s.Faculty)
                                        .Include(cc => cc.Registration)
                                            .ThenInclude(r => r.Student)
                                            .ThenInclude(s => s.BasicProfile)
                                        .Where(cc => cc.Course.Semester.Id == semester && cc.CourseClassCode == classCode)
                                        .FirstOrDefault();

            if (classFromDb is null) return NoContent();
            return Ok(Converter.ForwardConverter(classFromDb));
        }
    }
}