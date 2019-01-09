using log4net;
using log4net.Config;
using System.IO;
using System.Reflection;

namespace Light.Common
{
    /// <summary>
    /// Nlog日志配置
    /// </summary>
    public class Log4NetManager
    {

        private static readonly ILog logger = LogManager.GetLogger(typeof(Log4NetManager));

        static Log4NetManager()
        {
            var logRepository = LogManager.CreateRepository(Assembly.GetEntryAssembly(), typeof(log4net.Repository.Hierarchy.Hierarchy));
            //初始化配置日志
            XmlConfigurator.Configure(logRepository, File.OpenRead(Path.Combine(Directory.GetCurrentDirectory(), "Config", "log4net.config")));
        }

        /// <summary>
        /// 追踪日志
        /// </summary>
        /// <param name="log"></param>
        public static void LogTrace(string log)
        {
            logger.Debug(log);
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
