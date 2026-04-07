using AutoMapper;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Grades.Commands.Models;
using SchoolProject.Domain.Entities;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Application.Features.Grades.Commands.Handlers
{
    public class GradeCommandHandler : ResponseHandler
        , IRequestHandler<AddGradeCommand, Response<string>>
        , IRequestHandler<UpdateGradeCommand, Response<string>>
        , IRequestHandler<DeleteGradeCommand, Response<string>>
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;

        public GradeCommandHandler(
            IGradeRepository gradeRepository,
            IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }
        public async Task<Response<string>> Handle(AddGradeCommand request, CancellationToken cancellationToken)
        {
            var isLevelExist = await _gradeRepository.IsLevelExistAsync(request.Level);
            if (isLevelExist)
                return BadRequest<string>("The grade already exists");

            var grade = _mapper.Map<Grade>(request);
            await _gradeRepository.AddAsync(grade);

            return Created("The grade was added successfully.");
        }

        public async Task<Response<string>> Handle(UpdateGradeCommand request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                throw new ArgumentException("Enter grade ID number correctly.");
            var grade = await _gradeRepository.GetByIdAsync(request.Id);
            if (grade == null)
                return NotFound<string>("The grade is not found.");

            if (!string.IsNullOrEmpty(request.Name))
                grade.Name = request.Name;
            if (request.Level.HasValue)
                grade.Level = request.Level.Value;

            grade.UpdatedAt = DateTime.UtcNow;
            await _gradeRepository.UpdateAsync(grade);

            return Updated("The grade's data has been successfully updated.");
        }

        public async Task<Response<string>> Handle(DeleteGradeCommand request, CancellationToken cancellationToken)
        {
            if (request.gradeId <= 0)
                throw new ArgumentException("Enter the grade ID number correctly.");
            var grade = await _gradeRepository.GetByIdAsync(request.gradeId);
            if (grade == null)
                return NotFound<string>($"Grade with id : {request.gradeId} not found");
            grade.IsDeleted = true;
            await _gradeRepository.UpdateAsync(grade);

            return Deleted("The grade was successfully removed.");
        }
    }
}
