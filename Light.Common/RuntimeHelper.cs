using Light.Model.EnumModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Light.Common
{
    /// <summary>
    /// 运行时所需的变量汇总
    /// </summary>
    public class RuntimeHelper : SingleTon<RuntimeHelper>
    {
        /// <summary>
		/// 部署环境变量(Development/Staging/Production)
		/// </summary>
		public static string EnvironmentName;

        static RuntimeHelper()
        {
            IDictionary environmentVariables = Environment.GetEnvironmentVariables();
            if (environmentVariables.Contains("ASPNETCORE_ENVIRONMENT"))
            {
                EnvironmentName = environmentVariables["ASPNETCORE_ENVIRONMENT"].ToString();
            }
            else
            {
                EnvironmentName = EnvironmentEnum.Production.ToString();
            }
        }
    }
}
