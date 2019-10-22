using Light.Model.TableModel.LightAuthority;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Light.EFRespository;
using Light.Model.RequestModel.LightAuthority;
using Light.EFRespository.LightAuthority;
using Light.Model.EnumModel;
using Light.Common;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using System;
using Microsoft.AspNetCore.Http;

namespace Light.IdentityServer
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    public class AccountController : Controller
    {
        private readonly IUnitOfWork<LightAuthorityContext> _unitOfWork;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IEventService _events;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public AccountController(IUnitOfWork<LightAuthorityContext> unitOfWork,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            IIdentityServerInteractionService interaction)
        {
            _unitOfWork = unitOfWork;
            _interaction = interaction;
            _clientStore = clientStore;
            _schemeProvider = schemeProvider;
            _events = events;
        }

        /// <summary>
        /// 主页
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            await Task.CompletedTask;
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await Task.CompletedTask;
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Login([FromForm]UserLoginRequest userLogin)
        {
            ViewBag.ReturnUrl = userLogin.ReturnUrl;
            if (ModelState.IsValid)
            {
                var userExist = await _unitOfWork.GetRepository<ApplicationUser>().GetSingleAsyncCurrent(m => m.UserName == userLogin.UserName && m.Password == userLogin.Password);
                if (userExist != null)
                {
                    userLogin.Message = MessageEnum.UserMessageEnum.LoginSuccess.GetDescription();
                    userLogin.IsSuccess = true;
                    AuthenticationProperties props = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(1))
                    };
                    await HttpContext.SignInAsync(userExist.Id.ToString(), userExist.UserName, props);
                    if (!string.IsNullOrWhiteSpace(userLogin.ReturnUrl))
                    {
                        return Redirect(userLogin.ReturnUrl);
                    }
                    return View(userLogin);
                }
                else
                {
                    userLogin.Message = MessageEnum.UserMessageEnum.UserNameOrPasswordError.GetDescription();
                }
            }
            return View(userLogin);
        }
    }
}