using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Filters
{
    public class TransactionFilter : ServiceFilterAttribute
    {
        public TransactionFilter() : base(typeof(TransactionAsyncActionFilter)) { }
    }
}
