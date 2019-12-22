using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarUtilities.LogUtility;
using log4net;

namespace DynaLibrary
{
    public class DynaClass
    {
        public DynaClass()
        {
            HarLogger.GetLogger(typeof(DynaClass)).Debug($"INSIDE    CONSTRUCTOR  !!!!");
        }

        public string MethodToInvoke()
        {
            HarLogger.GetLogger(typeof(DynaClass)).Debug($"INSIDE    MethodToInvoke  !!!!");
            return "Hello World! (no args)";
        }

        public string MethodToInvokeWithArgs(int i1, double d2)
        {
            HarLogger.GetLogger(typeof(DynaClass)).Debug($"INSIDE    MethodToInvokeWithArgs  ARG1: {i1}  ARG2: {d2}");
            return "Hello World! (two args)";
        }
    }
}
