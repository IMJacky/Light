using Light.IRepository;
using Light.Repository;
using Light.Repository.MySQL;
using Microsoft.Extensions.DependencyInjection;

namespace Light.DependencyInjection
{
    /// <summary>
    /// 注入仓储层
    /// </summary>
    public class RepositoryInjection
    {
        public static void ConfigureRepository(IServiceCollection services)
        {
            //services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IUserRepository, UserRepositoryMySql>();
        }
    }
}
