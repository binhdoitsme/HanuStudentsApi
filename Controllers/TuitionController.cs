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
    [Route("/tuition")]
    [ApiController]
    public class TuitionController : ControllerBase
    {
        private readonly HanuEdmsContext Context;
        private OneWayConverter<FeeLine, TuitionFee> Converter;

        public TuitionController(HanuEdmsContext context)
        {
            Context = context;
            Converter = new TuitionFeeConverter();
        }

        [HttpGet]
        public IActionResult GetTuitionFeesForStudent([FromQuery] string authToken)
        {
            // authenticate
            string username = JwtUtils.ValidateJWT(authToken);
            if (username is null)
            {
                return Unauthorized(new MissingAuthTokenException());
            }

            int studentId = int.Parse(username);

            return new JsonResult(Context.Student
                    .Include(s => s.Registration)
                        .ThenInclude(r => r.FeeLine)
                        .ThenInclude(f => f.Course)
                    .Where(s => s.Id == studentId)
                    .FirstOrDefault()
                    ?.Registration
                    .SelectMany(r => r.FeeLine)
                    .Select(f => Converter.ForwardConverter(f))
                    .ToList());
        }

        [HttpPost]
        public IActionResult MockPayTuitionFee([FromQuery] int semester, [FromQuery] string authToken)
        {
            // authenticate
            string username = JwtUtils.ValidateJWT(authToken);
            if (username is null)
            {
                return Unauthorized(new MissingAuthTokenException());
            }

            int studentId = int.Parse(username);

            FeeLine[] tuitionFeeLines = Context.Student
                                            .Include(s => s.Registration)
                                                .ThenInclude(r => r.FeeLine)
                                            .Where(s => s.Id == studentId)
                                            .FirstOrDefault()
                                            ?.Registration
                                            .Where(r => r.SemesterId == semester)
                                            .SelectMany(r => r.FeeLine)
                                            .ToArray();
            System.Console.WriteLine(tuitionFeeLines.Length);
            foreach (var feeLine in tuitionFeeLines)
            {
                feeLine.MarkAsPaid();
            }
            Context.FeeLine.UpdateRange(tuitionFeeLines);
            Context.SaveChanges();
            return Ok();
        }
    }
}