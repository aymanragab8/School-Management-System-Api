using AutoMapper;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Courses.Queries.Models;
using SchoolProject.Application.Features.Courses.Queries.Response;
using SchoolProject.Application.Helpers;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Application.Features.Courses.Queries.Handlers
{
    public class CourseQueryHandler : ResponseHandler
        , IRequestHandler<GetCourseListQuery, Response<PaginatedResponse<GetCourseListResponse>>>
        , IRequestHandler<GetCoursesByGradeIdQuery, Response<PaginatedResponse<GetCoursesByGradeIdResponse>>>
        , IRequestHandler<GetCourseByIdQuery, Response<GetCourseByIdResponse>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public CourseQueryHandler(ICourseRepository courseRepository, IStudentRepository studentRepository, IMapper mapper)
        {
            _courseRepository = courseRepository;
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedResponse<GetCourseListResponse>>> Handle(GetCourseListQuery request, CancellationToken cancellationToken)
        {
            var courses = await _courseRepository.GetAllCoursesAsync();
            var mappedCourses = courses.Select(s => _mapper.Map<GetCourseListResponse>(s));
            var paginatedList = await mappedCourses.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return GetData(paginatedList);
        }

        public async Task<Response<GetCourseByIdResponse>> Handle(GetCourseByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.CourseId <= 0)
                return BadRequest<GetCourseByIdResponse>("Enter valid id");

            var course = await _courseRepository.GetCourseByIdAsync(request.CourseId);
            if (course == null)
                return NotFound<GetCourseByIdResponse>($"Course with id {request.CourseId} not found");

            bool isAdmin = request.role == "Admin";
            bool isTeacherOfCourse = request.role == "Teacher" &&
                course.TeacherId.ToString() == request.currentUserId;

            if (!isAdmin && !isTeacherOfCourse)
                return Unauthorized<GetCourseByIdResponse>("You can only view your own courses.");

            var result = _mapper.Map<GetCourseByIdResponse>(course);
            return GetData(result);
        }

        public async Task<Response<PaginatedResponse<GetCoursesByGradeIdResponse>>> Handle(
            GetCoursesByGradeIdQuery request, CancellationToken cancellationToken)
        {
            bool isAdmin = request.role == "Admin";

            if (!isAdmin)
            {
                var student = await _studentRepository.GetStudentByUserIdAsync(request.currentUserId);
                if (student == null)
                    return NotFound<PaginatedResponse<GetCoursesByGradeIdResponse>>("Student not found.");

                if (student.GradeId != request.gradeId)
                    return Unauthorized<PaginatedResponse<GetCoursesByGradeIdResponse>>("You can only view courses of your own grade.");
            }

            var courses = await _courseRepository.GetCoursesByGradeIdAsync(request.gradeId);
            if (!courses.Any())
                return NotFound<PaginatedResponse<GetCoursesByGradeIdResponse>>("No courses found for this grade.");
            var mappedCourses = courses.Select(c => _mapper.Map<GetCoursesByGradeIdResponse>(c));
            var paginatedList = await mappedCourses.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return GetData(paginatedList);
        }
    }
}
