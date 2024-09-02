using Application.Enums;
using Application.Exceptions.TagExceptions;
using Application.Models;
using Application.PostTags.Command;
using Application.PostTags.DTOs;
using AutoMapper;
using Domain.Entities;
using Infrastracture;
using MediatR;

namespace Application.PostTags.CommandHandler;

public class CreateTagHandler(ApplicationDbContext dbContext, IMapper mapper)
    : IRequestHandler<CreateTagCommand, OperationResult<TagDto>>
{
    private readonly ApplicationDbContext _dbContext = dbContext;
    private readonly IMapper _mapper = mapper;
    private readonly OperationResult<TagDto> _result = new();

    public async Task<OperationResult<TagDto>> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var tag = Tag.CreateTag(request.TagName);
            await _dbContext.Tags.AddAsync(tag, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);
            _result.Payload = _mapper.Map<TagDto>(tag);
        }
        catch (CreateTagEx e)
        {
            _result.AddError(ErrorCode.TagCreationFailed, e.Message);
        }

        return _result;
    }
}