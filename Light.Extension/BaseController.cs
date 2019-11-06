using Light.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Light.Extension
{

    [Authorize]
    public class BaseController : ControllerBase
    {
    }
}
