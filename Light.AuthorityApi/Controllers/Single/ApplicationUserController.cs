using Light.Extension;
using Light.Model.CommonModel;
using Light.Model.TableModel.LightAuthority;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Light.EFRespository;
using Light.Service.Event;
using Light.Service.Event.CustomerEventArgs;
using Light.Model.RequestModel.LightAuthority;
using Light.Common;
using Light.EFRespository.LightAuthority;
using Microsoft.AspNetCore.Authorization;
using Light.Model.EnumModel;
using Light.Model.ResponseModel.LightAuthority;
using AutoMapper;

namespace Light.AuthorityApi.Controllers.Single
{
    /// <summary>
    /// 角色权限控制器
    /// </summary>
    [Produces("application/json")]
    [Route("auth/user")]
    public class ApplicationUserController : BaseController
    {
        private readonly IUnitOfWork<LightAuthorityContext> _unitOfWork;
        private readonly IMapper _mapper;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="mapper"></param>
        public ApplicationUserController(IUnitOfWork<LightAuthorityContext> unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        #region ApplicationUser
        /// <summary>
        /// 所有用户列表
        /// </summary>
        /// <returns></returns>
        //[HttpGet("all/{d:int:range(1,3)}")]
        [HttpGet("all")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _unitOfWork.GetRepository<ApplicationUser>().GetListAsync(m => _mapper.Map<ApplicationUser, ApplicationUserResponse>(m)));
        }

        /// <summary>
        /// 分页所有用户列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("page")]
        public async Task<IActionResult> GetAllUserByPage(PageRequest pageRequest)
        {
            return Ok(await _unitOfWork.GetRepository<ApplicationUser>().GetPagedListAsync(m => _mapper.Map<ApplicationUser, ApplicationUserResponse>(m), pageIndex: pageRequest.PageIndex, pageSize: pageRequest.PageSize));
        }

        /// <summary>
        /// 根据Id获取一个用户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return Ok(await _unitOfWork.GetRepository<ApplicationUser>().GetSingleAsyncCurrent(m => m.Id == id));
        }

        /// <summary>
        /// 添加一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddUser([FromBody]ApplicationUser user)
        {
            await _unitOfWork.GetRepository<ApplicationUser>().AddAsync(user);
            var publisher = new PublisherGenerics<LoginEventArgs>();
            publisher.Notify(new LoginEventArgs { UserName = user.UserName, IP = "127.0.0.1" });

            var publisherRegister = new PublisherGenerics<RegisterEventArgs>();
            publisherRegister.Notify(new RegisterEventArgs { UserName = user.UserName, IP = "127.0.0.1" });
            return Ok(await _unitOfWork.SaveChangesAsync());
        }

        /// <summary>
        /// 更新一个用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody]ApplicationUser user)
        {
            var userExist = await _unitOfWork.GetRepository<ApplicationUser>().GetSingleAsyncCurrent(m => m.Id == user.Id);
            if (userExist == null)
            {
                return NotFound();
            }
            _unitOfWork.GetRepository<ApplicationUser>().Update(user);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }
        /// <summary>
        /// 删除一个用户
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var userExist = await _unitOfWork.GetRepository<ApplicationUser>().GetSingleAsyncCurrent(m => m.Id == id);
            if (userExist == null)
            {
                return NotFound();
            }
            _unitOfWork.GetRepository<ApplicationUser>().Delete(userExist);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }
        #endregion

        /// <summary>
        /// 用户登录
        /// </summary>
        /// <param name="userLogin"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> UserLogin([FromBody]UserLoginRequest userLogin)
        {
            ResultModel<ApplicationUser> result = new ResultModel<ApplicationUser> { Message = MessageEnum.UserMessageEnum.UserNameOrPasswordError.GetDescription() };
            var userExist = await _unitOfWork.GetRepository<ApplicationUser>().GetSingleAsyncCurrent(m => m.UserName == userLogin.UserName && m.Password == userLogin.Password);
            result.Result = userExist;
            if (userExist != null)
            {
                result.Message = MessageEnum.UserMessageEnum.LoginSuccess.GetDescription();
                result.IsSuccess = true;
            }
            return Ok(result);
        }
    }
}