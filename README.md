# 简单介绍
基于.Net Core，我会一直会保持升级到最新版。
项目主要包括API网关，Asp.Net Core Web API，EF Core，Identity4，都是一些练手的简单项目，各位仅供参考，有问题找我。

# 怎么运行
想运行起来很简单，全文搜索“10.154.5.185”，然后替换成你自己的IP地址就可以了，运行之后系统会自动生成所有的表，支持Mysql和SQL Server

2019.11.05更新

因为加入了IdentityServer4和ApiGateway，所以如果想要真正运行起来，这两个项目也要提前运行。
然后Postman先获取token，然后再用token去请求接口数据

![](https://github.com/IMJacky/picturestore/blob/master/%E4%BC%81%E4%B8%9A%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_15730038295612.png)
![](https://github.com/IMJacky/picturestore/blob/master/%E4%BC%81%E4%B8%9A%E5%BE%AE%E4%BF%A1%E6%88%AA%E5%9B%BE_15730038472866.png)

# NLog日志配置
和appsettings.json的连接字符串保持一致即可：
```
<target name="log4database" xsi:type="Database" dbProvider="MySql.Data.MySqlClient.MySqlConnection, MySql.Data">
  <connectionString>
	Server=localhost;DataBase=LightLog;User=root;password=Wjg50058
  </connectionString>
  <commandText>
	INSERT INTO Log
	(CreateDate, Message, Level) 
	VALUES(@createDate, @message, @level)
  </commandText>
  <parameter name="@createDate" layout="${date}" />
  <parameter name="@level" layout="${level}" />
  <parameter name="@message" layout="${message}" />
</target>
```
# 服务和接口使用RegisterServiceAttribute，然后统一注册所有服务
```
    /// <summary>
    /// ServiceCollection扩展方法
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllService(this IServiceCollection services, params string[] assemblyNames)
        {
            foreach (var assemblyName in assemblyNames)
            {
                Assembly assemblie = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName + ".dll"));
                var typesInfos = assemblie.DefinedTypes.Where(x => x.GetCustomAttributes().Any(a => a is RegisterServiceAttribute)).ToList();
                foreach (var type in typesInfos)
                {
                    var registerServiceAttribute = type.GetCustomAttribute<RegisterServiceAttribute>();
                    services.Add(new ServiceDescriptor(registerServiceAttribute.IServiceType, type, registerServiceAttribute.ServiceLifetime));
                }
            }
        }
    }
```
