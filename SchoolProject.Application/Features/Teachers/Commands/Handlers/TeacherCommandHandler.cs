using AutoMapper;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Teachers.Commands.Models;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;


namespace SchoolProject.Application.Features.Teachers.Commands.Handlers
{
    public class TeacherCommandHandler : ResponseHandler
        , IRequestHandler<AddTeacherCommand, Response<string>>
        , IRequestHandler<UpdateTeacherCommand, Response<string>>
        , IRequestHandler<DeleteTeacherCommand, Response<string>>
    {

        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherCommandHandler(
            ITeacherRepository teacherRepository,
            IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddTeacherCommand request, CancellationToken cancellationToken)
        {
            var isNationalIdExist = await _teacherRepository
                .IsNationalIdExistAsync(request.NationalId);

            if (isNationalIdExist)
                return BadRequest<string>("The national ID number already exists");

            var teacher = _mapper.Map<Teacher>(request);
            await _teacherRepository.AddAsync(teacher);

            return Created("The teacher has been successfully added.");
        }

        public async Task<Response<string>> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            if (request.TeacherId <= 0)
                return BadRequest<string>("Enter teacher ID number correctly.");

            var teacher = await _teacherRepository.GetByIdAsync(request.TeacherId);
            if (teacher == null)
                return NotFound<string>("The teacher is not found.");

            bool isAdmin = request.role == "Admin";
            bool isOwner = teacher.ApplicationUserId == request.currentUserId;

            if (!isAdmin && !isOwner)
                return Unauthorized<string>();

            if (!isAdmin && request.Salary.HasValue)
                return Forbidden<string>("Only admins can update the salary.");

            if (!string.IsNullOrEmpty(request.FullName))
                teacher.FullName = request.FullName;
            if (!string.IsNullOrEmpty(request.Address))
                teacher.Address = request.Address;
            if (!string.IsNullOrEmpty(request.PhoneNumber))
                teacher.PhoneNumber = request.PhoneNumber;

            if (isAdmin && request.Salary.HasValue)
                teacher.Salary = request.Salary.Value;

            teacher.UpdatedAt = DateTime.UtcNow;
            await _teacherRepository.UpdateAsync(teacher);

            return Updated("The teacher's data has been successfully updated.");
        }

        public async Task<Response<string>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            if (request.teacherId <= 0)
                throw new ArgumentException("Enter the teacher ID number correctly.");
            var teacher = await _teacherRepository.GetByIdAsync(request.teacherId);
            if (teacher == null)
                return NotFound<string>($"Teacher with id : {request.teacherId} not found");
            teacher.IsDeleted = true;
            await _teacherRepository.UpdateAsync(teacher);

            return Deleted("The teacher was successfully removed.");
        }
    }
}
