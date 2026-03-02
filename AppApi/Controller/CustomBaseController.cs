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
        if (result.statusCode == HttpStatusCode.NoContent)
        {
            return new ObjectResult(null) { StatusCode = (int)result.statusCode };
        }

        return new ObjectResult(result.Data) { StatusCode = (int)result.statusCode };
    }

  //Update ve Delete işlemlerinde ise bunu kullanacaz
    [NonAction]
    public IActionResult CreateActionResult(ServiceResult result)
    {
        if (result.statusCode == HttpStatusCode.NoContent)
        {
            return new ObjectResult(null) { StatusCode = (int)result.statusCode };
        }

        if (result.IsFailed)
        {
            return new ObjectResult(result.ErrorMessages) { StatusCode = (int)result.statusCode };
            
        }

        return new ObjectResult(null) { StatusCode = (int)result.statusCode };
    }
}