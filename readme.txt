1��ʹ��VS�Դ��ķ���ϵͳʱ�����ʱAPI��Ŀ����Ҫ��pubxml�ļ��м������½ڵ㣬������Ҳ���xml�ļ�
<GenerateDocumentationFile>true</GenerateDocumentationFile>
<Target Name="CopyDocumentationFile" AfterTargets="ComputeFilesToPublish">
    <ItemGroup>
      <ResolvedFileToPublish Include="@(FinalDocFile)" RelativePath="@(FinalDocFile->'%(Filename)%(Extension)')" />
    </ItemGroup>
</Target>
2��webapi����Ҫview�����ߵ�web��Ŀ��Ҫviewҳ���ʱ�������DLL��ʱ�������������
<MvcRazorCompileOnPublish>false</MvcRazorCompileOnPublish>
�����������refs����ͼԤ���룩�ļ��У������������
<PreserveCompilationContext>false</PreserveCompilationContext>
3��ʹ��EFCoreǨ�Ʊ������GenerateRuntimeConfigurationFiles�����еĻ��ڼ�һ��<RuntimeFrameworkVersion>2.0.3</RuntimeFrameworkVersion>
<PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
    <GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
</PropertyGroup>
4��Mysql8.0m����ģʽ�޸��Ĳ���
mysql -u root -p
use mysql
alter user 'root'@'10.181.24.30' identified by 'your pwd' password expire never
alter user 'root'@'10.181.24.30' identified with mysql_native_password by 'your pwd'
flush privileges