using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OMS.Core.DTOs;
using OMS.Core.Entities;
using OMS.Core.Services;

namespace OMS.API.Filters;

public class NotFoundUpdateFilter<T> : IAsyncActionFilter where T : BaseEntity
{
    private readonly IGenericService<T> _service;

    public NotFoundUpdateFilter(IGenericService<T> service)
    {
        _service = service;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var objectUpdate = context.ActionArguments.Values.FirstOrDefault();
        
        if (objectUpdate == null)
        {
            await next.Invoke();
            return;
        }
        
        var id = (int)objectUpdate.GetType().GetProperty("Id")?.GetValue(objectUpdate, null)!;
        
        var anyEntity = await _service.AnyAsync(x => x.Id == id);

        if (anyEntity)
        {
            await next.Invoke();
            return;
        }
        
        context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, $"{typeof(T).Name} id {id} not found"));
    }
}