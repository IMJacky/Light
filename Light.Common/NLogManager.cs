using NLog;
using NLog.Config;
using NLog.Targets;
using System;
using System.IO;

namespace Light.Common
{
    /// <summary>
    /// Nlog日志配置
    /// </summary>
    public class NLogManager
    {

        private static Logger logger = LogManager.GetCurrentClassLogger();

        static NLogManager()
        {
            //初始化配置日志
            LogManager.Configuration = new XmlLoggingConfiguration(Path.Combine(Directory.GetCurrentDirectory(), "Config", "NLog.config"));
        }

        /// <summary>
        /// 追踪日志
        /// </summary>
        /// <param name="log"></param>
        public static void LogTrace(string log)
        {
            logger.Trace(log);
        }

        /// <summary>
        /// 调试日志
        /// </summary>
        /// <param name="log"></param>
        public static void LogDebug(string log)
        {
            logger.Debug(log);
        }

        /// <summary>
        /// 信息日志
        /// </summary>
        /// <param name="log"></param>
        public static void LogInfo(string log)
        {
            logger.Info(log);
        }

        /// <summary>
        /// 警告日志
        /// </summary>
        /// <param name="log"></param>
        public static void LogWarn(string log)
        {
            logger.Warn(log);
        }

        /// <summary>
        /// 错误日志
        /// </summary>
        /// <param name="log"></param>
        public static void LogError(string log)
        {
            logger.Error(log);
        }

        /// <summary>
        /// 重大日志
        /// </summary>
        /// <param name="log"></param>
        public static void LogFatal(string log)
        {
            logger.Fatal(log);
        }
    }
}
