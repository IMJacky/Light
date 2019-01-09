using Light.Common;
using Light.EFRespository;
using Light.EFRespository.LightAuthority;
using Light.IService.LightAuthority;
using Light.Model.CommonModel;
using Light.Model.TableModel.LightAuthority;
using Light.Model.ViewModel.LightAuthority;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Light.Service.Authority
{
    /// <summary>
    /// 权限相关服务实现
    /// </summary>
    public class AuthorityService : IAuthorityService
    {
        private readonly IUnitOfWork<LightAuthorityContext> _unitOfWork;

        public AuthorityService(IUnitOfWork<LightAuthorityContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 获取用户所有的权限信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        public async Task<ResultModel<UserAuthorityDetail>> GetUserAuthorityDetail(int userId)
        {
            var roleRepository = _unitOfWork.GetRepository<Role>();
            var applicationUserRepository = _unitOfWork.GetRepository<ApplicationUser>();
            var userRoleRepository = _unitOfWork.GetRepository<UserRole>();
            var moduleSystemRepository = _unitOfWork.GetRepository<ModuleSystem>();
            var roleModuleRepository = _unitOfWork.GetRepository<RoleModule>();
            ResultModel<UserAuthorityDetail> resultModel = new ResultModel<UserAuthorityDetail>();
            var user = await applicationUserRepository.GetSingleAsync(m => new { m.Id, m.UserName, m.CreateDate }, m => m.Id == userId);
            if (user != null)
            {
                resultModel.Result.UserId = user.Id;
                resultModel.Result.UserName = user.UserName;
                resultModel.Result.CreateDate = user.CreateDate;
                var userRoleList = await userRoleRepository.GetListAsync(m => new { m.RoleId }, m => m.UserId == userId);
                if (userRoleList != null && userRoleList.Count > 0)
                {
                    var roleIdList = userRoleList.Select(m => m.RoleId).ToList();
                    var roleList = await roleRepository.GetListAsync(m => new { m.RoleName }, m => roleIdList.Contains(m.Id));
                    if (roleIdList != null && roleIdList.Count > 0)
                    {
                        resultModel.Result.RoleNameList = roleList.Select(m => m.RoleName).ToList();
                        var roleModuleList = await roleModuleRepository.GetListAsync(m => new { m.ModuleId }, m => roleIdList.Contains(m.RoleId));
                        if (roleModuleList != null && roleModuleList.Count > 0)
                        {
                            var moduleIdList = roleModuleList.Select(m => m.ModuleId).ToList();
                            var moduleList = await moduleSystemRepository.GetListAsyncCurrent(m => moduleIdList.Contains(m.Id));
                            if (moduleList != null && moduleList.Count > 0)
                            {
                                resultModel.Result.UserModuleDetailList = new List<UserModuleDetail>();
                                resultModel.Result.UserModuleDetailList = GetUserModuleDetailList(moduleList, resultModel.Result.UserModuleDetailList);
                                resultModel.IsSuccess = true;
                            }
                            else
                            {
                                resultModel.Message = MessageEnum.UserAuthority.ModuleIsNotValid.GetDescription();
                            }
                        }
                        else
                        {
                            resultModel.Message = MessageEnum.UserAuthority.RoleNotHasModule.GetDescription();
                        }
                    }
                    else
                    {
                        resultModel.Message = MessageEnum.UserAuthority.RoleIsNotValid.GetDescription();
                    }
                }
                else
                {
                    resultModel.Message = MessageEnum.UserAuthority.UserNoRole.GetDescription();
                }
            }
            else
            {
                resultModel.Message = MessageEnum.UserAuthority.UserIsNotValid.GetDescription();
            }
            return resultModel;
        }

        /// <summary>
        /// 递归处理用户的系统模块权限
        /// </summary>
        /// <param name="moduleSystems"></param>
        /// <returns></returns>
        private List<UserModuleDetail> GetUserModuleDetailList(List<ModuleSystem> moduleSystems, List<UserModuleDetail> userModuleDetails, long parentId = 0)
        {
            foreach (var moduleSystem in moduleSystems.OrderByDescending(m => m.Sort).Where(m => m.ParentId == parentId))
            {
                UserModuleDetail userModuleDetail = new UserModuleDetail
                {
                    ModuleId = moduleSystem.Id,
                    ModuleName = moduleSystem.ModuleName,
                    ParentId = moduleSystem.ParentId,
                    RouteUrl = moduleSystem.RouteUrl,
                    Sort = moduleSystem.Sort,
                    SubUserModuleDetailList = new List<UserModuleDetail>()
                };
                userModuleDetails.Add(userModuleDetail);
                userModuleDetail.SubUserModuleDetailList = GetUserModuleDetailList(moduleSystems, userModuleDetail.SubUserModuleDetailList, userModuleDetail.ModuleId);
            }
            return userModuleDetails;
        }
    }
}
