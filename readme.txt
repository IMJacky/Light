1、使用VS自带的发布系统时，如果时API项目，需要在pubxml文件中加入以下节点，否则会找不到xml文件
<GenerateDocumentationFile>true</GenerateDocumentationFile>
<Target Name="CopyDocumentationFile" AfterTargets="ComputeFilesToPublish">
    <ItemGroup>
      <ResolvedFileToPublish Include="@(FinalDocFile)" RelativePath="@(FinalDocFile->'%(Filename)%(Extension)')" />
    </ItemGroup>
</Target>
2、webapi不需要view，或者当web项目需要view页面的时候而不是DLL的时候，添加如下配置
<MvcRazorCompileOnPublish>false</MvcRazorCompileOnPublish>
如果不想生成refs（视图预编译）文件夹，添加如下配置
<PreserveCompilationContext>false</PreserveCompilationContext>
3、使用EFCore迁移必须添加GenerateRuntimeConfigurationFiles，不行的话在加一个<RuntimeFrameworkVersion>2.0.3</RuntimeFrameworkVersion>
<PropertyGroup>
    <TargetFramework>netcoreapp2.0</TargetFramework>
    <GenerateRuntimeConfigurationFiles>true</GenerateRuntimeConfigurationFiles>
</PropertyGroup>