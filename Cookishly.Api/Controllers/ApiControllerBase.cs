using System.Web.Http;
using Cookishly.Services.Results;

namespace Cookishly.Api.Controllers
{
    public class ApiControllerBase : ApiController
    {
        protected IHttpActionResult GetErrorResult(IResult serviceResult)
        {
            if (serviceResult.FailureType.HasValue)
            {
                switch (serviceResult.FailureType.Value)
                {
                    case FailureType.BadRequest:
                        return BadRequest(serviceResult.Message);
                    default:
                        return InternalServerError();
                }
            }

            return InternalServerError();
        }
    }
}