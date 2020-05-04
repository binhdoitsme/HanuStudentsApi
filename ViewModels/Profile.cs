namespace HanuEdmsApi.ViewModels
{
    public class Profile
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string Dob { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public string Hometown { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string ClassName { get; set; }
        public string Faculty { get; set; }
        public string AcademicYear { get; set; }
        public int PassedCreditCount { get; set; }
        public int RequiredCreditCount { get; set; }
        public double OverallMark { get; set; }
    }
}