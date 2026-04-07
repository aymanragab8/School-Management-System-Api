using AutoMapper;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Enrollments.Queries.Models;
using SchoolProject.Application.Features.Enrollments.Queries.Response;
using SchoolProject.Application.Helpers;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Application.Features.Enrollments.Queries.Handlers
{
    public class EnrollmentQueryHandler : ResponseHandler
        , IRequestHandler<GetEnrollmentListQuery, Response<PaginatedResponse<GetEnrollmentListResponse>>>
        , IRequestHandler<GetEnrollmentsByCourseIdQuery, Response<PaginatedResponse<GetEnrollmentsByCourseIdResponse>>>
        , IRequestHandler<GetEnrollmentByIdQuery, Response<GetEnrollmentByIdResponse>>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public EnrollmentQueryHandler(IEnrollmentRepository enrollmentRepository, IStudentRepository studentRepository, ICourseRepository courseRepository, IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }
        public async Task<Response<PaginatedResponse<GetEnrollmentListResponse>>> Handle(
            GetEnrollmentListQuery request, CancellationToken cancellationToken)
        {
            bool isAdmin = request.role == "Admin";

            IQueryable<Enrollment> enrollmentsQuery;

            if (isAdmin)
            {
                enrollmentsQuery = await _enrollmentRepository.GetAllEnrollmentsAsync();
            }
            else
            {
                var student = await _studentRepository.GetStudentByUserIdAsync(request.currentUserId);
                if (student == null)
                    return NotFound<PaginatedResponse<GetEnrollmentListResponse>>("Student not found.");

                enrollmentsQuery = await _enrollmentRepository.GetEnrollmentsByStudentIdAsync(student.Id);
            }

            var mappedQuery = enrollmentsQuery
                .Select(s => _mapper.Map<GetEnrollmentListResponse>(s));

            var paginatedList = await mappedQuery.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return GetData(paginatedList);
        }

        public async Task<Response<GetEnrollmentByIdResponse>> Handle(GetEnrollmentByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.enrollmentId <= 0)
                return BadRequest<GetEnrollmentByIdResponse>("Enter valid id");
            var enrollment = await _enrollmentRepository.GetEnrollmentByIdAsync(request.enrollmentId);
            if (enrollment == null)
                return NotFound<GetEnrollmentByIdResponse>($"Enrollment with id :{request.enrollmentId} not found");
            var result = _mapper.Map<GetEnrollmentByIdResponse>(enrollment);
            return GetData(result);
        }

        public async Task<Response<PaginatedResponse<GetEnrollmentsByCourseIdResponse>>> Handle(
            GetEnrollmentsByCourseIdQuery request, CancellationToken cancellationToken)
        {
            if (request.CourseId <= 0)
                return BadRequest<PaginatedResponse<GetEnrollmentsByCourseIdResponse>>("Enter valid course id");

            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
                return NotFound<PaginatedResponse<GetEnrollmentsByCourseIdResponse>>("Course not found.");

            bool isAdmin = request.role == "Admin";
            bool isTeacherOfCourse = request.role == "Teacher" &&
                course.TeacherId.ToString() == request.currentUserId;

            if (!isAdmin && !isTeacherOfCourse)
                return Unauthorized<PaginatedResponse<GetEnrollmentsByCourseIdResponse>>("You are not allowed to view this course's enrollments.");

            var enrollments = await _enrollmentRepository.GetEnrollmentsByCourseIdAsync(request.CourseId);
            var mappedEnrollments = enrollments
                .Select(e => _mapper.Map<GetEnrollmentsByCourseIdResponse>(e))
                .AsQueryable();
            var paginatedList = await mappedEnrollments.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return GetData(paginatedList);
        }
    }
}
