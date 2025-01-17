using Mango.Services.ProductAPI.Data;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Mango.Services.ProductAPI.Filters
{
    public class TransactionAsyncActionFilter : IAsyncActionFilter
    {
        private readonly AppDbContext _repositoryContext;
        public TransactionAsyncActionFilter(AppDbContext repositoryContext)
        {
            _repositoryContext = repositoryContext;
        }
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using(var trans = await _repositoryContext.Database.BeginTransactionAsync())
            {
                try
                {
                    var resultContext = await next();

                    if(resultContext.Exception == null)
                    {
                        trans.Commit();
                    }
                    else
                    {
                        trans.Rollback();
                    }
                }
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }
    }
}
