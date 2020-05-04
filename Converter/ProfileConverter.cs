namespace HanuEdmsApi.Converter
{
    public class ProfileConverter : OneWayConverter<Models.Student, ViewModels.Profile>
    {
        public ProfileConverter() : base(FromDatabase) { }

        private static ViewModels.Profile FromDatabase(Models.Student student)
        {
            Models.Profile basicProfile = student.BasicProfile;

            return new ViewModels.Profile()
            {
                Id = student.Id,
                AcademicYear = student.AcademicYear.AcademicYearShort,
                ClassName = student.ClassName,
                DisplayName = basicProfile.DisplayName,
                Dob = basicProfile.Dob.ToShortDateString(),
                Email = basicProfile.Email,
                Faculty = student.Faculty.FacultyName,
                Gender = basicProfile.Gender == 0 ? "Male" : "Female",
                Hometown = basicProfile.Hometown,
                Nationality = basicProfile.Nationality,
                OverallMark = student.OverallMark,
                PassedCreditCount = student.PassedCreditCount,
                Phone = basicProfile.Phone,
                RequiredCreditCount = student.Faculty.RequiredCreditCount
            };
        }
    }
}
