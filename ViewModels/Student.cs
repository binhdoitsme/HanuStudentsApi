namespace HanuEdmsApi.ViewModels
{
    public class Student
    {
        public int Id { get; set; }
        public string DisplayName { get; set; }
        public string ClassName { get; set; }
        public string Faculty { get; set; }

        public Student(Profile profile)
        {
            Id = profile.Id;
            DisplayName = profile.DisplayName;
            ClassName = profile.ClassName;
            Faculty = profile.Faculty;
        }
    }
}