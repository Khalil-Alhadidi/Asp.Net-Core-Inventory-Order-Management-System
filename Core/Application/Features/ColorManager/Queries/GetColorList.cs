using Application.Common.CQS.Queries;
using Application.Common.Extensions;
using Application.Features.TodoItemManager.Queries;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.ColorManager.Queries
{
    public record GetColorListDto
    {
        public string? Id { get; init; }
        public string? Name { get; init; }
        public bool? isFav { get; init; }
        public DateTime? CreatedAtUtc { get; init; }
    }

    public class GetColorProfile : Profile
    {
        public GetColorProfile()
        {
            CreateMap<Color, GetColorListDto>()
                .ForMember(
                    dest => dest.Name,
                    opt => opt.MapFrom(src => src.Name != null ? src.Name : string.Empty)
                );

        }
    }
    public class GetColorListResult
    {
        public List<GetColorListDto>? Data { get; init; }
    }

    public class GetColorListRequest : IRequest<GetColorListResult>
    {
        public bool IsDeleted { get; init; } = false;
    }

    public class GetColorListHandler : IRequestHandler<GetColorListRequest, GetColorListResult>
    {
        private readonly IMapper _mapper;
        private readonly IQueryContext _context;

        public GetColorListHandler(IMapper mapper, IQueryContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<GetColorListResult> Handle(GetColorListRequest request, CancellationToken cancellationToken)
        {
            var query = _context
                .Colors
                .AsNoTracking()
                .ApplyIsDeletedFilter(request.IsDeleted)
                .AsQueryable();

            var entities = await query.ToListAsync(cancellationToken);

            var dtos = _mapper.Map<List<GetColorListDto>>(entities);

            return new GetColorListResult
            {
                Data = dtos
            };
        }


    }


}
