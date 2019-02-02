using AutoMapper;
using Light.Model.ResponseModel.LightAuthority;
using Light.Model.TableModel.LightAuthority;
using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model
{
    /// <summary>
    /// 实体映射
    /// </summary>
    public class ModelMappingProfile : Profile
    {
        /// <summary>
        /// 构造
        /// </summary>
        public ModelMappingProfile()
        {
            CreateMap<ApplicationUser, ApplicationUserResponse>();
        }
    }
}
