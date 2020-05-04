using HanuEdmsApi.Converter;
using HanuEdmsApi.EF;
using HanuEdmsApi.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace HanuEdmsApi.Controllers
{
    [ApiController]
    [Route("/profile")]
    public class ProfileController : ControllerBase
    {
        private readonly HanuEdmsContext Context;
        private readonly OneWayConverter<Models.Student, ViewModels.Profile> Converter;

        public ProfileController(HanuEdmsContext context)
        {
            Context = context;
            Converter = new ProfileConverter();
        }

        [HttpGet]
        public ViewModels.Profile GetProfileForAuthenticatedUser([FromQuery] string authToken)
        {
            string username = JwtUtils.ValidateJWT(authToken);
            int id = int.Parse(username);
            return GetProfileById(id);
        }

        ViewModels.Profile GetProfileById(int id)
        {
            // authentication => studentId claim

            Models.Student studentProfile = Context.Student.Include(s => s.BasicProfile)
                                            .Include(s => s.Faculty)
                                            .Include(s => s.AcademicYear)
                                            .FirstOrDefault(s => s.Id == id);
            if (studentProfile is null)
            {
                throw new InvalidOperationException();
            }

            ViewModels.Profile profile = Converter.ForwardConverter(studentProfile);
            return profile;
        }
    }
}