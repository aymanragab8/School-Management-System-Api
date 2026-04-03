namespace SchoolProject.Application.Features.Enrollments.Queries.Response
{
    public class GetEnrollmentsByCourseIdResponse
    {
        public int EnrollmentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string StudentName { get; set; }
        public string StudentPhone { get; set; }
        public string StudentAddress { get; set; }
    }
}
