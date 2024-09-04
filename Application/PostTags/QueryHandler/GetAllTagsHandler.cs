using Application.Enums;
using Application.Exceptions.TagExceptions;
using Application.Models;
using Application.PostTags.DTOs;
using Application.PostTags.Query;
using AutoMapper;
using Domain.Entities;
using Infrastracture;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.PostTags.QueryHandler;

public class GetAllTagsHandler(ApplicationDbContext dbContext, IMapper mapper)
    : IRequestHandler<GetAllTagsQuery, OperationResult<IReadOnlyList<TagDto>>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private OperationResult<IReadOnlyList<TagDto>> _result = new();

    public async Task<OperationResult<IReadOnlyList<TagDto>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var tags = await _dbContext.Tags
                .AsNoTracking()
                .ToListAsync(cancellationToken);
            if (tags.Count == 0)
            {
                _result.AddError(ErrorCode.NotFound, "Tags is empty");
            }
            _result.Payload = _mapper.Map<List<TagDto>>(tags);
        }
        catch (GetTagsEx e)
        {
            _result.AddError(ErrorCode.UnknownError, "Something went wrong");
        }

        return _result;
    }
}