# 简单介绍
基于.Net Core，我会一直会保持升级到最新版。
项目主要包括API网关，Asp.Net Core Web API，EF Core，Identity4，还有引入了RabbitMQ，都是一些练手的简单项目，各位仅供参考，有问题找我。

# 怎么运行
想运行起来很简单，只要把appsettings.json里边的数据库连接配置弄好就可以了，运行之后系统会自动生成所有的表，支持Mysql和SQL Server

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
#服务和接口分别以Service结尾，作为规范，然后统一注册所有服务
```
/// <summary>
/// 注册所有服务
/// </summary>
/// <param name="services"></param>
public static void AddAllServices(this IServiceCollection services)
{
	string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Light.Service.dll");
	Assembly assembly = Assembly.LoadFrom(path);
	Type[] types = assembly.GetTypes();

	string pathIService = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Light.IService.dll");
	Assembly assemblyIService = Assembly.LoadFrom(pathIService);
	Type[] typesIService = assemblyIService.GetTypes();
	foreach (var type in types)
	{
		if (type.Name.Contains("Service"))
		{
			var iService = "I" + type.Name;
			foreach (var typeIService in typesIService)
			{
				if (typeIService.Name.Equals(iService))
				{
					services.AddScoped(typeIService, type);
				}
			}
		}
	}
}
```