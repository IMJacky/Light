using Light.Business;
using Light.IBusiness;
using Light.Model.TableModel;
using Microsoft.Extensions.DependencyInjection;

namespace Light.DependencyInjection
{
    /// <summary>
    /// 注入业务逻辑层
    /// </summary>
    public class BusinessInjection
    {
        public static void ConfigureBusiness(IServiceCollection services)
        {
            services.AddSingleton<IUserBusiness, UserBusiness>();
        }
    }
}
