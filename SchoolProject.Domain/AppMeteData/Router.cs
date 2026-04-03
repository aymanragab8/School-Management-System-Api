namespace SchoolProject.Domain.AppMeteData
{
    public static class Router
    {
        public const string ConstPart = "/Api/";
        public static class Auth
        {
            public const string Prefix = "Auth/";
            public const string Register = ConstPart + Prefix + "Register";
            public const string Login = ConstPart + Prefix + "Login";
            public const string RefreshToken = ConstPart + Prefix + "RefreshToken";
        }
        public static class Student
        {
            public const string Prefix = "Student/";
            public const string GetAll = ConstPart + Prefix + "List";
            public const string GetByGrade = ConstPart + Prefix + "ByGrade/{gradeId}";
            public const string GetById = ConstPart + Prefix + "{studentId}";
            public const string Create = ConstPart + Prefix + "Create";
            public const string Update = ConstPart + Prefix + "Update/{studentId}";
            public const string Delete = ConstPart + Prefix + "Delete/{studentId}";
        }
        public static class Teacher
        {
            public const string Prefix = "Teacher/";
            public const string GetAll = ConstPart + Prefix + "List";
            public const string GetById = ConstPart + Prefix + "{teacherId}";
            public const string Create = ConstPart + Prefix + "Create";
            public const string Update = ConstPart + Prefix + "Update/{teacherId}";
            public const string Delete = ConstPart + Prefix + "Delete/{teacherId}";
        }
        public static class Grade
        {
            public const string Prefix = "Grade/";
            public const string GetAll = ConstPart + Prefix + "List";
            public const string GetById = ConstPart + Prefix + "{gradeId}";
            public const string Create = ConstPart + Prefix + "Create";
            public const string Update = ConstPart + Prefix + "Update/{gradeId}";
            public const string Delete = ConstPart + Prefix + "Delete/{gradeId}";
        }
        public static class Course
        {
            public const string Prefix = "Course/";
            public const string GetAll = ConstPart + Prefix + "List";
            public const string GetAllByGrade = ConstPart + Prefix + "{gradeId}";
            public const string GetById = ConstPart + Prefix + "{courseId}";
            public const string Create = ConstPart + Prefix + "Create";
            public const string Update = ConstPart + Prefix + "Update/{courseId}";
            public const string Delete = ConstPart + Prefix + "Delete/{courseId}";
        }
        public static class Enrollment
        {
            public const string Prefix = "Enrollment/";
            public const string GetAll = ConstPart + Prefix + "List";
            public const string GetAllByCourse = ConstPart + Prefix + "{courseId}";
            public const string GetById = ConstPart + Prefix + "{enrollmentId}";
            public const string Create = ConstPart + Prefix + "Create";
            public const string Update = ConstPart + Prefix + "UpdateScore/{enrollmentId}";
            public const string Delete = ConstPart + Prefix + "Delete/{enrollmentId}";
        }
    }
}
