using System.Net;
using Microsoft.AspNetCore.Mvc;
using Services;
namespace AppApi.Controller;

[ApiController]
//Classlama icin Controller kullanırız yani burdaki api/Products olur 
[Route("api/[Controller]")]

public class CustomBaseController : ControllerBase
{
    [NonAction] 
    
    //Ekleme işlemlerini yaparken biz bunu kullanırızz//
    public IActionResult CreateActionResult<T>(ServiceResult<T> result)
    {
        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            return new ObjectResult(null) { StatusCode = (int)result.StatusCode };
        }

        if (result.StatusCode==HttpStatusCode.Created)
        {
            return new ObjectResult(result.Data) { StatusCode = (int)result.StatusCode,};
        }

        return new ObjectResult(result.Data) { StatusCode = (int)result.StatusCode };
    }

  //Update ve Delete işlemlerinde ise bunu kullanacaz
    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        if (result.StatusCode == HttpStatusCode.NoContent)
        {
            return new ObjectResult(null) { StatusCode = (int)result.StatusCode };
        }

        if (result.IsFailed)
        {
            return new ObjectResult(result.ErrorMessages) { StatusCode = (int)result.StatusCode };
        }

        return new ObjectResult(null) { StatusCode = (int)result.StatusCode };
    }
}