using AutoMapper;
using MediatR;
using SchoolProject.Application.Bases;
using SchoolProject.Application.Features.Grades.Queries.Models;
using SchoolProject.Application.Features.Grades.Queries.Response;
using SchoolProject.Application.Helpers;
using SchoolProject.Domain.Interfaces;

namespace SchoolProject.Application.Features.Grades.Queries.Handlers
{
    public class GradeQueryHandler : ResponseHandler
        , IRequestHandler<GetGradeListQuery, Response<PaginatedResponse<GetGradeListResponse>>>
        , IRequestHandler<GetGradeByIdQuery, Response<GetGradeByIdResponse>>
    {
        private readonly IGradeRepository _gradeRepository;
        private readonly IMapper _mapper;

        public GradeQueryHandler(IGradeRepository gradeRepository, IMapper mapper)
        {
            _gradeRepository = gradeRepository;
            _mapper = mapper;
        }

        public async Task<Response<PaginatedResponse<GetGradeListResponse>>> Handle(GetGradeListQuery request, CancellationToken cancellationToken)
        {
            var grades = await _gradeRepository.GetAllGradesAsync();
            var mappedGrades = grades.Select(s => _mapper.Map<GetGradeListResponse>(s));
            var paginatedList = await mappedGrades.ToPaginatedListAsync(request.PageNumber, request.PageSize);
            return GetData(paginatedList);
        }

        public async Task<Response<GetGradeByIdResponse>> Handle(GetGradeByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.GradeId <= 0)
                return BadRequest<GetGradeByIdResponse>("Enter valid id");
            var grade = await _gradeRepository.GetGradeByIdAsync(request.GradeId);
            if (grade == null)
                return NotFound<GetGradeByIdResponse>($"Grade with id :{request.GradeId} not found");
            var result = _mapper.Map<GetGradeByIdResponse>(grade);
            return GetData(result);
        }


    }
}
