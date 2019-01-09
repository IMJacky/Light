using FluentValidation;
using Light.Model.TableModel.LightAuthority;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Light.AuthorityApi.Validation
{
    /// <summary>
    /// 角色验证
    /// </summary>
    public class RoleValidation : AbstractValidator<Role>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public RoleValidation()
        {
            RuleFor(m => m.RoleName).NotEmpty().MaximumLength(50);
        }
    }
}
