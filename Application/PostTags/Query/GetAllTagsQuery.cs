using Application.Models;
using Application.PostTags.DTOs;
using Domain.Entities;
using MediatR;

namespace Application.PostTags.Query;

public class GetAllTagsQuery : IRequest<OperationResult<List<TagDto>>>
{
    
}