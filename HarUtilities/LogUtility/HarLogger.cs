using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;

//[assembly: log4net.Config.XmlConfigurator(ConfigFileExtension = "log4net", Watch = true)]
[assembly: log4net.Config.XmlConfigurator(ConfigFile ="log4net.config", Watch = true)]
namespace HarUtilities.LogUtility
{
    public class HarLogger
    {
        //private readonly log4net.ILog _log;

        private static ILog logger = LogManager.GetLogger("HarLogger");

        //public HarLogger()
        //{
        //    _log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        //}

        public static ILog GetAppLogger()
        {
            return logger;
        }

        public static ILog GetLogger(Type type)
        {
            return log4net.LogManager.GetLogger(type);
        }
    }
}
