using AutoMapper;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Teachers.Queries.Models;
using SchoolProject.Application.Features.Teachers.Queries.Response;
using SchoolProject.Application.Helpers;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Application.Features.Teachers.Queries.Handlers
{
    public class TeacherQueryHandler : ResponseHandler,
    IRequestHandler<GetTeacherListQuery, Response<PaginatedResponse<GetTeacherListResponse>>>
    , IRequestHandler<GetTeacherByIdQuery, Response<GetTeacherByIdResponse>>
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IMapper _mapper;

        public TeacherQueryHandler(ITeacherRepository teacherRepository, IMapper mapper)
        {
            _teacherRepository = teacherRepository;
            _mapper = mapper;
        }
        public async Task<Response<PaginatedResponse<GetTeacherListResponse>>> Handle(GetTeacherListQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _teacherRepository.GetAllTeachersAsync();
            var mappedTeachers = teachers.Select(s => _mapper.Map<GetTeacherListResponse>(s));
            var paginatedList = await mappedTeachers.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return GetData(paginatedList);
        }

        public async Task<Response<GetTeacherByIdResponse>> Handle(GetTeacherByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
                return BadRequest<GetTeacherByIdResponse>("Enter valid id");

            var teacher = await _teacherRepository.GetTeacherByIdAsync(request.Id);
            if (teacher == null)
                return NotFound<GetTeacherByIdResponse>($"Teacher with id {request.Id} not found");

            bool isAdmin = request.role == "Admin";
            bool isOwner = teacher.ApplicationUserId == request.currentUserId;

            if (!isAdmin && !isOwner)
                return Unauthorized<GetTeacherByIdResponse>();

            var result = _mapper.Map<GetTeacherByIdResponse>(teacher);
            return GetData(result);
        }
    }
}
