using Light.Model.TableModel.LightAuthority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.EFRespository.LightAuthority
{
    public class LightAuthorityDataSeed
    {
        /// <summary>
        /// 初始化数据库数据
        /// </summary>
        /// <param name="lightContext"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public static async Task SeedAsync(LightAuthorityContext lightContext, IUnitOfWork<LightAuthorityContext> unitOfWork)
        {
            var roleRepository = unitOfWork.GetRepository<Role>();
            var applicationUserRepository = unitOfWork.GetRepository<ApplicationUser>();
            var userRoleRepository = unitOfWork.GetRepository<UserRole>();
            var moduleSystemRepository = unitOfWork.GetRepository<ModuleSystem>();
            var roleModuleRepository = unitOfWork.GetRepository<RoleModule>();

            if (lightContext.Database.EnsureCreated())
            {
                roleRepository.Add(new List<Role>{ new Role
                    {
                        RoleName = "系统管理员",
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    },new Role
                    {
                        RoleName = "人事专员",
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    } });
                await unitOfWork.SaveChangesAsync();
                applicationUserRepository.Add(new List<ApplicationUser> {
                        new ApplicationUser
                        {
                            Email="871834898@qq.com",
                            UserName="wangjieguang",
                            Password="wangjieguang",
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new ApplicationUser
                        {
                            Email="hbjieguang@hotmail.com",
                            UserName="wjg",
                            Password="wjg",
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        }
                    });
                await unitOfWork.SaveChangesAsync();
                for (int i = 0; i < 100; i++)
                {
                    applicationUserRepository.Add(new ApplicationUser
                    {
                        Email = $"Email{i}@qq.com",
                        UserName = $"UserName{i}",
                        Password = $"Password{i}",
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    });
                }
                await unitOfWork.SaveChangesAsync();
                userRoleRepository.Add(new List<UserRole> {
                        new UserRole
                        {
                            UserId = 1,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new UserRole
                        {
                            UserId = 2,
                            RoleId = 2,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        }
                    });
                await unitOfWork.SaveChangesAsync();
                moduleSystemRepository.Add(new List<ModuleSystem>{ new ModuleSystem
                    {
                        ModuleName = "报表管理",
                        RouteUrl = string.Empty,
                        ParentId = 0,
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    },new ModuleSystem
                    {
                        ModuleName = "人事管理",
                        RouteUrl = string.Empty,
                        ParentId = 0,
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    },new ModuleSystem
                    {
                        ModuleName = "系统管理",
                        RouteUrl = string.Empty,
                        ParentId = 0,
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    },new ModuleSystem
                    {
                        ModuleName = "系统用户",
                        RouteUrl = string.Empty,
                        ParentId = 3,
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    }, new ModuleSystem
                    {
                        ModuleName = "用户列表",
                        ParentId = 4,
                        RouteUrl = "users",
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    },new ModuleSystem
                    {
                        ModuleName = "业绩报表",
                        RouteUrl = string.Empty,
                        ParentId = 1,
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    }, new ModuleSystem
                    {
                        ModuleName = "个人业绩",
                        ParentId = 6,
                        RouteUrl = "userachieve",
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    },new ModuleSystem
                    {
                        ModuleName = "员工管理",
                        RouteUrl = string.Empty,
                        ParentId = 2,
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    }, new ModuleSystem
                    {
                        ModuleName = "员工列表",
                        ParentId = 8,
                        RouteUrl = "employees",
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    },new ModuleSystem
                    {
                        ModuleName = "系统角色",
                        RouteUrl = string.Empty,
                        ParentId = 3,
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    }, new ModuleSystem
                    {
                        ModuleName = "角色列表",
                        ParentId = 10,
                        RouteUrl = "roles",
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    },new ModuleSystem
                    {
                        ModuleName = "新增用户",
                        ParentId = 4,
                        RouteUrl = "users/add",
                        UpdaterId = 1,
                        UpdaterName = "王杰光",
                        UpdateDate = DateTime.Now,
                        CreaterId = 1,
                        CreaterName = "王杰光",
                        CreateDate = DateTime.Now
                    } });
                await unitOfWork.SaveChangesAsync();
                roleModuleRepository.Add(new List<RoleModule> {
                        new RoleModule
                        {
                            ModuleId = 1,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 2,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 3,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 4,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 5,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 6,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 7,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 8,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 9,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 10,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 11,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 12,
                            RoleId = 1,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 2,
                            RoleId = 2,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 8,
                            RoleId = 2,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        },
                        new RoleModule
                        {
                            ModuleId = 9,
                            RoleId = 2,
                            UpdaterId = 1,
                            UpdaterName = "王杰光",
                            UpdateDate = DateTime.Now,
                            CreaterId = 1,
                            CreaterName = "王杰光",
                            CreateDate = DateTime.Now
                        }
                    });
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
