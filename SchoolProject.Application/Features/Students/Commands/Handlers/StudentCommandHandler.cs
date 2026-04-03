using AutoMapper;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Students.Commands.Models;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Application.Features.Students.Commands.Handlers
{
    public class StudentCommandHandler : ResponseHandler
        , IRequestHandler<AddStudentCommand, Response<string>>
        , IRequestHandler<UpdateStudentCommand, Response<string>>
        , IRequestHandler<DeleteStudentCommand, Response<string>>

    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentCommandHandler(
            IStudentRepository studentRepository,
            IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<string>> Handle(
            AddStudentCommand request, CancellationToken cancellationToken)
        {
            var isNationalIdExist = await _studentRepository
                .IsNationalIdExistAsync(request.NationalId);

            if (isNationalIdExist)
                return BadRequest<string>("The national ID number already exists");

            var student = _mapper.Map<Student>(request);
            await _studentRepository.AddAsync(student);

            return Created("The student has been successfully added.");
        }

        public async Task<Response<string>> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.StudentId <= 0)
                return BadRequest<string>("Enter student ID number correctly.");

            var student = await _studentRepository.GetByIdAsync(request.StudentId);
            if (student == null)
                return NotFound<string>("The student is not found.");

            bool isAdmin = request.role == "Admin";
            bool isOwner = student.ApplicationUserId == request.currentUserId;

            if (!isAdmin && !isOwner)
                return Unauthorized<string>();

            if (!string.IsNullOrEmpty(request.FullName))
                student.FullName = request.FullName;
            if (!string.IsNullOrEmpty(request.Address))
                student.Address = request.Address;
            if (!string.IsNullOrEmpty(request.PhoneNumber))
                student.PhoneNumber = request.PhoneNumber;

            if (isAdmin && request.GradeId.HasValue)
                student.GradeId = request.GradeId.Value;

            student.UpdatedAt = DateTime.UtcNow;
            await _studentRepository.UpdateAsync(student);

            return Updated("The student's data has been successfully updated.");
        }

        public async Task<Response<string>> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
        {
            if (request.studentId <= 0)
                throw new ArgumentException("Enter the correct id number.");
            var student = await _studentRepository.GetByIdAsync(request.studentId);
            if (student == null)
                return NotFound<string>("The student is not found.");
            student.IsDeleted = true;
            await _studentRepository.UpdateAsync(student);

            return Deleted("The student was successfully removed.");
        }
    }
}