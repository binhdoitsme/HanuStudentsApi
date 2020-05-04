using HanuEdmsApi.Models;
using HanuEdmsApi.ViewModels;

namespace HanuEdmsApi.Converter
{
    public class TuitionFeeConverter : OneWayConverter<FeeLine, TuitionFee>
    {
        public TuitionFeeConverter() : base(FromDatabase) { }

        private static TuitionFee FromDatabase(FeeLine feeLine)
        {
            var course = feeLine.Course;
            return new TuitionFee()
            {
                CourseName = course.CourseName,
                CreditCount = course.CreditCount,
                IsPaid = feeLine.Status,
                Value = (long)feeLine.LineSum
            };
        }
    }
}
