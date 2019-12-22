using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarUtilities.LogUtility;
using HarUtilities.Reflection;
using log4net;

namespace reflect_01
{
    public class ProgramDynamic
    {
        public static void Main(string[] argsin)
        {
            HarLogger.GetLogger(typeof(ProgramDynamic)).Debug($"Inside  Main  !!!");

            string dllPath = string.Empty;
            string className = string.Empty;
            string methodName = string.Empty;

            var libraryConfigSection = ConfigurationManager.GetSection("DynamicConfigSection") as DynamicConfigSection;

            if (libraryConfigSection == null || libraryConfigSection.Members == null || libraryConfigSection.Members.Count < 1)
            {
                HarLogger.GetLogger(typeof(ProgramDynamic)).Debug($"CONFIG SECTION DOES NOT EXIST !!!!!!!!!!!!!");
            }

            foreach (var obj in libraryConfigSection.Members)
            {
                HarLogger.GetLogger(typeof(ProgramDynamic)).Debug($"dllPath :: {((DynamicElement)obj).Value1}");
                HarLogger.GetLogger(typeof(ProgramDynamic)).Debug($"className :: {((DynamicElement)obj).Value2}");
                HarLogger.GetLogger(typeof(ProgramDynamic)).Debug($"methodName :: {((DynamicElement)obj).Value3}");
                dllPath = ((DynamicElement)obj).Value1;
                className = ((DynamicElement)obj).Value2;
                methodName = ((DynamicElement)obj).Value3;
            }

            Object[] args = { };
            Object Result = HarUtilities.Reflection.DynamicInvoke.InvokeMethodSlow(dllPath, className, methodName, args);
            //Object Result = HarUtilities.Reflection.DynamicInvoke.InvokeMethod(dllPath, className, methodName, args);


            System.Console.ReadLine();
        }
    }
}
