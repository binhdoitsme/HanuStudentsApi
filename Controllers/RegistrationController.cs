using HanuEdmsApi.Converter;
using HanuEdmsApi.EF;
using HanuEdmsApi.Exceptions;
using HanuEdmsApi.Helpers;
using HanuEdmsApi.Models;
using HanuEdmsApi.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace HanuEdmsApi.Controllers
{
    [Route("/registrations")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly HanuEdmsContext Context;
        private readonly OneWayConverter<CourseClass, RegistrationClass> ClassConverter;
        private readonly OneWayConverter<Models.Registration, ViewModels.Registration> RegistrationConverter;

        public RegistrationController(HanuEdmsContext context)
        {
            Context = context;
            ClassConverter = new RegistrationClassConverter();
            RegistrationConverter = new RegistrationConverter();
        }

        [HttpGet("available")]
        public IActionResult GetAvailableRegistrations([FromQuery] int semester,
                                                        [FromQuery] string authToken)
        {
            // authenticate
            string username = JwtUtils.ValidateJWT(authToken);
            if (username is null)
            {
                return Unauthorized(new MissingAuthTokenException());
            }

            int studentId = int.Parse(username);

            // return list of course classes
            int facultyId = (int)Context.Student.Where(s => s.Id == studentId)
                                .FirstOrDefault()?.FacultyId.GetValueOrDefault();
            IEnumerable<RegistrationClass> classes = Context.CourseClass
                                                        .Include(cc => cc.Course)
                                                        .Include(cc => cc.Course)
                                                            .ThenInclude(cc => cc.CourseType)
                                                        .Include(cc => cc.Timetable)
                                                        .Where(cc => cc.Course.SemesterId == semester
                                                                && (cc.Course.FacultyId == facultyId || cc.Course.CourseTypeId == 1))
                                                        .AsEnumerable()
                                                        .Select(cc => ClassConverter.ForwardConverter(cc))
                                                        .ToList();
            return new JsonResult(classes);
        }

        [HttpGet]
        public IActionResult GetRegistrations([FromQuery] int semester,
                                                [FromQuery] string authToken)
        {
            // auth
            string username = JwtUtils.ValidateJWT(authToken);
            if (username is null)
            {
                return Unauthorized(new MissingAuthTokenException());
            }

            int studentId = int.Parse(username);

            // return data
            var registrations = Context.Registration
                                    .Include(r => r.CourseClass)
                                    .ThenInclude(cc => cc.Course)
                                    .Where(r => r.SemesterId == semester && r.StudentId == studentId && r.Status == true)
                                    .AsEnumerable()
                                    .Select(r => RegistrationConverter.ForwardConverter(r))
                                    .ToList();
            return new JsonResult(registrations);
        }

        [HttpPost]
        public IActionResult AddRegistrations([FromBody] IList<ViewModels.Registration> registrations,
                                                [FromQuery] string authToken)
        {
            // authentication check
            string username = JwtUtils.ValidateJWT(authToken);
            if (username is null)
            {
                return Unauthorized(new MissingAuthTokenException());
            }

            // check exists in db
            if (registrations is null || registrations.Count == 0)
            {
                return StatusCode(500, new { message = "Cannot register for 0 courses!" });
            }

            IList<string> classCodes = registrations.Select(r => r.ClassCode).ToList();
            int studentId = registrations.FirstOrDefault().StudentId;
            int semesterId = registrations.FirstOrDefault().Semester;

            var courseClassesToUpdate = Context.CourseClass
                                            .Include(cc => cc.Course)
                                            .Where(cc => cc.Course.SemesterId == semesterId
                                                && classCodes.Contains(cc.CourseClassCode)
                                                && cc.RemainingSlots > 0)
                                            .ToList();
            classCodes = courseClassesToUpdate.Select(cc => cc.CourseClassCode).ToList();
            courseClassesToUpdate.ForEach(cc => cc.RemainingSlots--);

            var existingRegistrations = Context.Registration.Include(r => r.CourseClass)
                                        .Where(r => classCodes.Contains(r.CourseClass.CourseClassCode)
                                                    && r.StudentId == studentId
                                                    && r.SemesterId == semesterId)
                                        .ToList();
            existingRegistrations.ForEach(r => r.Status = true);

            IList<string> classCodesWithExistingRegistration = existingRegistrations
                                                                .Select(r => r.CourseClass.CourseClassCode).ToList();
            var newRegistrationsToCreate = registrations.Where(r =>
                                                            !classCodesWithExistingRegistration.Contains(r.ClassCode))
                                                        .Select(r => CreateRegistration(r))
                                                        .ToList();

            // modify db
            Context.AddRange(newRegistrationsToCreate);
            Context.UpdateRange(existingRegistrations);
            Context.UpdateRange(courseClassesToUpdate);
            Context.SaveChanges();

            return Ok();
        }

        private Models.Registration CreateRegistration(ViewModels.Registration registration)
        {
            string classCode = registration.ClassCode;
            var courseClass = Context.CourseClass
                                .Include(cc => cc.Course)
                                .Where(cc => cc.CourseClassCode == classCode)
                                .FirstOrDefault();
            var convertedRegistration = new Models.Registration()
            {
                Status = true,
                SemesterId = registration.Semester,
                StudentId = registration.StudentId,
                CourseId = courseClass.Course.Id,
                CourseClassId = courseClass.CourseClassId
            };

            return convertedRegistration;
        }

        [HttpDelete]
        public IActionResult RemoveRegistrations([FromBody] IList<ViewModels.Registration> registrations,
                                                [FromQuery] string authToken)
        {
            // authentication
            string username = JwtUtils.ValidateJWT(authToken);
            if (username is null)
            {
                return Unauthorized(new MissingAuthTokenException());
            }

            // change the status only --- do NOT delete!
            IList<string> classCodes = registrations.Select(r => r.ClassCode).ToList();
            int studentId = registrations.FirstOrDefault().StudentId;
            int semesterId = registrations.FirstOrDefault().Semester;

            var removedRegistrations = Context.Registration.Include(r => r.CourseClass)
                                        .Where(r => classCodes.Contains(r.CourseClass.CourseClassCode)
                                                    && r.StudentId == studentId
                                                    && r.SemesterId == semesterId)
                                        .ToArray();

            classCodes = removedRegistrations.Select(r => r.CourseClass.CourseClassCode).ToList();
            var courseClassesToUpdate = removedRegistrations.Select(r => r.CourseClass).ToList();
            courseClassesToUpdate.ForEach(cc => ++cc.RemainingSlots);

            foreach (var rg in removedRegistrations)
            {
                rg.Status = false;
            }

            Context.UpdateRange(removedRegistrations);
            Context.UpdateRange(courseClassesToUpdate);
            Context.SaveChanges();

            return Ok();
        }
    }
}