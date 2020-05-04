using HanuEdmsApi.Converter;
using HanuEdmsApi.EF;
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
        public IEnumerable<TuitionFee> GetTuitionFeesForStudent([FromQuery] int studentId, [FromQuery] string authToken)
        {
            // authenticate

            return Context.Student
                    .Include(s => s.Registration).ThenInclude(r => r.FeeLine).ThenInclude(f => f.Course)
                    .Where(s => s.Id == studentId)
                    .FirstOrDefault()
                    ?.Registration
                    .SelectMany(r => r.FeeLine)
                    .Select(f => Converter.ForwardConverter(f))
                    .ToList();
        }

        [HttpPost]
        public void MockPayTuitionFee([FromQuery] int semester, [FromQuery] int studentId, [FromQuery] string authToken)
        {
            // authenticate

            FeeLine[] tuitionFeeLines = Context.Student
                                            .Include(s => s.Registration)
                                                .ThenInclude(r => r.FeeLine)
                                            .Where(s => s.Id == studentId)
                                            .FirstOrDefault()
                                            ?.Registration
                                            .Where(r => r.SemesterId == semester)
                                            .SelectMany(r => r.FeeLine)
                                            .ToArray();
            
            foreach (var feeLine in tuitionFeeLines)
            {
                feeLine.MarkAsPaid();
            }
            Context.FeeLine.UpdateRange(tuitionFeeLines);
            Context.SaveChanges();
        }
    }
}