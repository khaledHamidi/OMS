using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using OMS.Core.DTOs;
using OMS.Core.Entities;
using OMS.Core.Services;

namespace OMS.API.Filters
{
    public class NotFoundIdFilter<T> : IAsyncActionFilter where T : BaseEntity
    {

        private readonly IGenericService<T> _service;

        public NotFoundIdFilter(IGenericService<T> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue = context.ActionArguments.Values.FirstOrDefault();

            if (idValue == null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x => x.Id == id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(StatusCodes.Status404NotFound, $"{typeof(T).Name} id {id} not found"));

        }
    }
}
