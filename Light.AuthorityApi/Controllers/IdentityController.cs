using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityModel.Client;
using Light.Extension;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Light.AuthorityApi.Controllers
{
    /// <summary>
    /// 身份认证
    /// </summary>
    [Route("auth/identity")]
    public class IdentityController : ControllerBase
    {
        /// <summary>
        /// 所有用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("test")]
        [Authorize]
        public IActionResult Test()
        {
            return Ok(from c in User.Claims select new { c.Type, c.Value });
        }

        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <returns></returns>
        [HttpGet("token")]
        public async Task<IActionResult> GetToken()
        {
            // discover endpoints from metadata
            var disco = await DiscoveryClient.GetAsync("http://localhost:5000");
            if (disco.IsError)
            {
                return BadRequest(disco.Error);
            }

            // request token
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                return BadRequest(tokenResponse.Error);
            }
            return Ok(tokenResponse.Json);
        }
    }
}