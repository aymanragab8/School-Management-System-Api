using AutoMapper;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Enrollments.Commands.Models;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Enums;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Application.Features.Enrollments.Commands.Handlers
{
    public class EnrollmentCommandHandler : ResponseHandler
        , IRequestHandler<AddEnrollmentCommand, Response<string>>
        , IRequestHandler<UpdateEnrollmentCommand, Response<string>>
        , IRequestHandler<DeleteEnrollmentCommand, Response<string>>
    {
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IStudentRepository _studentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly IMapper _mapper;

        public EnrollmentCommandHandler(
            IEnrollmentRepository enrollmentRepository,
            IStudentRepository studentRepository,
            ICourseRepository courseRepository,
            IMapper mapper)
        {
            _enrollmentRepository = enrollmentRepository;
            _studentRepository = studentRepository;
            _courseRepository = courseRepository;
            _mapper = mapper;
        }


        public async Task<Response<string>> Handle(AddEnrollmentCommand request, CancellationToken cancellationToken)
        {
            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
                return NotFound<string>($"Student with Id: {request.StudentId} not found");
            var course = await _courseRepository.GetByIdAsync(request.CourseId);
            if (course == null)
                return NotFound<string>($"Student with Id: {request.CourseId} not found");

            if (!course.IsActive)
                return BadRequest<string>("The course is not available for registration.");

            if (student.GradeId != course.GradeId)
                return BadRequest<string>("The student does not belong to the same grade for the course.");

            var isAlreadyEnrolled = await _enrollmentRepository
                .IsEnrolledAsync(request.StudentId, request.CourseId);
            if (isAlreadyEnrolled)
                return BadRequest<string>("The student is already registered for this course.");

            var enrollment = _mapper.Map<Enrollment>(request);
            await _enrollmentRepository.AddAsync(enrollment);

            return Created<string>("The enrollment has been successfully added.");
        }

        public async Task<Response<string>> Handle(UpdateEnrollmentCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new ArgumentException("Enter enrollment ID number correctly.");
            var enrollment = await _enrollmentRepository.GetByIdAsync(request.Id);
            if (enrollment == null)
                return NotFound<string>("The enrollment is not found.");

            if (request.Score.HasValue)
                enrollment.Score = request.Score.Value;

            enrollment.UpdatedAt = DateTime.UtcNow;
            await _enrollmentRepository.UpdateAsync(enrollment);

            return Updated<string>("The enrollment's data has been successfully updated.");
        }

        public async Task<Response<string>> Handle(DeleteEnrollmentCommand request, CancellationToken cancellationToken)
        {
            if (request.enrollmentId <= 0)
                throw new ArgumentException("Enter the enrollment ID number correctly.");
            var enrollment = await _enrollmentRepository.GetByIdAsync(request.enrollmentId);
            if (enrollment == null)
                return NotFound<string>($"Enrollment with id : {request.enrollmentId} not found");
            enrollment.Status = EnrollmentStatus.Dropped;
            await _enrollmentRepository.UpdateAsync(enrollment);

            return Deleted<string>("The enrollment was successfully removed.");
        }
    }
}