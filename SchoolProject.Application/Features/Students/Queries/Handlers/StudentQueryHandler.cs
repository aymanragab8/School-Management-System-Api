using AutoMapper;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Students.Queries.Models;
using SchoolProject.Application.Features.Students.Queries.Response;
using SchoolProject.Application.Helpers;
using SchoolProject.Domain.Interfaces;


namespace SchoolProject.Application.Features.Students.Queries.Handlers
{
    public class StudentQueryHandler : ResponseHandler,
    IRequestHandler<GetStudentListQuery, Response<PaginatedResponse<GetStudentListResponse>>>
    , IRequestHandler<GetStudentByIdQuery, Response<GetStudentByIdResponse>>
    , IRequestHandler<GetStudentLitByGradeQuery, Response<PaginatedResponse<GetStudentListResponse>>>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public StudentQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedResponse<GetStudentListResponse>>> Handle(
            GetStudentListQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.GetAllStudentsAsync();
            var mappedStudents = students.Select(s => _mapper.Map<GetStudentListResponse>(s));
            var paginatedList = await mappedStudents.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return GetData(paginatedList);
        }

        public async Task<Response<GetStudentByIdResponse>> Handle(
            GetStudentByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                return BadRequest<GetStudentByIdResponse>("Enter Valid Id");
            var student = await _studentRepository.GetStudentByIdAsync(request.Id);
            if (student == null)
                return NotFound<GetStudentByIdResponse>($"Student with id :{request.Id} not found");

            bool isAdmin = request.role == "Admin";
            bool isOwner = student.ApplicationUserId == request.currentUserId;

            if (!isAdmin && !isOwner)
                return Unauthorized<GetStudentByIdResponse>();
            var result = _mapper.Map<GetStudentByIdResponse>(student);
            return GetData(result);
        }

        public async Task<Response<PaginatedResponse<GetStudentListResponse>>> Handle(
            GetStudentLitByGradeQuery request, CancellationToken cancellationToken)
        {
            if (request.gradeId <= 0)
                return BadRequest<PaginatedResponse<GetStudentListResponse>>("Enter valid id");
            //var gradeExists = await _gradeService.GetGradeByIdAsync(request.gradeId);
            var students = await _studentRepository.GetStudentsByGradeAsync(request.gradeId);
            if (students == null || !students.Any())
                return NotFound<PaginatedResponse<GetStudentListResponse>>("There are no students in this grade.");
            var mappedStudents = students.Select(s => _mapper.Map<GetStudentListResponse>(s));
            var paginatedList = await mappedStudents.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return GetData(paginatedList);
        }
    }
}
