using Light.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Light.Extension
{

#if !DEBUG
    [Authorize]
#endif
    public class BaseController : ControllerBase
    {
    }
}
