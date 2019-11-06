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
# CI/CD
另外我这边还配置了ci/cd，可以完全托管于docker，包括jenkins，mysql，nginx，以及各个api项目，实现了全自动化自动编译，自动部署，具体的脚本可以参考各个项目下的ci文件
```
echo 'dotnet start'

echo '1、env'
pwd
ls
whoami
which dotnet
dotnet --info
dotnet --version

echo '2、cd Light.AuthorityApi'
cd ./Light.AuthorityApi

echo '3、dotnet restore Light.AuthorityApi'
dotnet restore

echo '4、delete and add directory Light_AuthorityApi_Publish'
rm -rf $WORKSPACE/Light_AuthorityApi_Publish
mkdir $WORKSPACE/Light_AuthorityApi_Publish

echo '5、dotnet publish Light.AuthorityApi'
dotnet publish -c:Release -o $WORKSPACE/Light_AuthorityApi_Publish

echo 'dotnet end'

echo '6、cd Light_AuthorityApi_Publish'
cd $WORKSPACE/Light_AuthorityApi_Publish

echo 'docker start'

echo '7、add Dockerfile'
touch Dockerfile
echo "FROM mcr.microsoft.com/dotnet/core/aspnet" >> Dockerfile
echo "WORKDIR /app" >> Dockerfile
echo "COPY . ." >> Dockerfile
echo "EXPOSE 5001" >> Dockerfile
echo "ENV ASPNETCORE_URLS http://*:5001" >> Dockerfile
echo "ENV ASPNETCORE_ENVIRONMENT Production" >> Dockerfile
echo "ENTRYPOINT [\"dotnet\", \"Light.AuthorityApi.dll\"]" >> Dockerfile

echo '8、stop container light.authorityapi'
sudo docker stop $(sudo docker ps -a -q  --filter=ancestor=light.authorityapi) || :

echo '9、delete container light.authorityapi'
sudo docker rm $(sudo docker ps -a -q --filter=ancestor=light.authorityapi) || :

echo '10、delete image light.authorityapi'
sudo docker rmi light.authorityapi || :

echo '11、build image light.authorityapi'
sudo docker build -t light.authorityapi .

echo '12、run container light.authorityapi'
sudo docker run -p 5001:5001 -d --name light.authorityapi light.authorityapi

echo 'docker end'
```
