using AutoMapper;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Courses.Commands.Models;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Application.Features.Grades.Commands.Handlers
{
    public class CourseCommandHandler : ResponseHandler
        , IRequestHandler<AddCourseCommand, Response<string>>
        , IRequestHandler<UpdateCourseCommand, Response<string>>
        , IRequestHandler<DeleteCourseCommand, Response<string>>
    {
        private readonly ICourseRepository _courseRepository;
        private readonly ITeacherRepository _teacherRepository;
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;

        public CourseCommandHandler(
            ICourseRepository courseRepository,
            ITeacherRepository teacherRepository,
            IGradeRepository gradeRepository,
            IMapper mapper)
        {
            _courseRepository = courseRepository;
            _teacherRepository = teacherRepository;
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddCourseCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId);
            if (teacher == null)
                throw new ArgumentNullException("There is no teacher with this id.");
            var grade = await _gradeRepository.GetByIdAsync(request.GradeId);
            if (grade == null)
                throw new ArgumentNullException("There is no grade with this id.");
            var course = _mapper.Map<Course>(request);
            await _courseRepository.AddAsync(course);

            return Created("The course was added successfully.");
        }

        public async Task<Response<string>> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                return BadRequest<string>("Enter course ID number correctly.");

            var course = await _courseRepository.GetByIdAsync(request.Id);
            if (course == null)
                return NotFound<string>("The course is not found.");

            bool isAdmin = request.role == "Admin";
            bool isTeacherOfCourse = request.role == "Teacher" &&
                course.TeacherId.ToString() == request.currentUserId;

            if (!isAdmin && !isTeacherOfCourse)
                return Unauthorized<string>("You are not allowed to update this course.");

            if (!string.IsNullOrEmpty(request.Name))
                course.Name = request.Name;
            if (request.Credits.HasValue)
                course.Credits = request.Credits.Value;

            if (isAdmin)
            {
                if (request.TeacherId.HasValue)
                    course.TeacherId = request.TeacherId.Value;
                if (request.GradeId.HasValue)
                    course.GradeId = request.GradeId.Value;
            }

            course.UpdatedAt = DateTime.UtcNow;
            await _courseRepository.UpdateAsync(course);

            return Updated("The course's data has been successfully updated.");
        }

        public async Task<Response<string>> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
        {
            if (request.courseId <= 0)
                throw new ArgumentException("Enter the course ID number correctly.");
            var course = await _courseRepository.GetByIdAsync(request.courseId);
            if (course == null)
                return NotFound<string>($"Course with id : {request.courseId} not found");
            course.IsDeleted = true;
            await _courseRepository.UpdateAsync(course);

            return Deleted("The course was successfully removed.");
        }
    }
}
