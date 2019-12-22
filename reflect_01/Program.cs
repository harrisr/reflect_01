using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HarUtilities.LogUtility;
using HarUtilities.Reflection;
using log4net;
using System.Collections.Specialized;


namespace reflect_01
{
    public class Program
    {
        private static readonly string DLL_PATH_KEY = "dllPath";
        private static readonly string CLASS_NAME_KEY = "className";
        private static readonly string METHOD_NAME_KEY = "methodName";

        public static void MainZZ1(string[] argsin)
        {
            HarLogger.GetLogger(typeof(Program)).Debug($"Inside  Main  !!!");



            var libraryConfigSection = ConfigurationManager.GetSection("DynamicConfigSection") as DynamicConfigSection;

            if (libraryConfigSection == null ||
            libraryConfigSection.Members == null || libraryConfigSection.Members.Count < 1)
            {
                System.Console.WriteLine("CONFIG SECTION DOES NOT EXIST !!!!!!!!!!!!!");
            }

            foreach (var obj in libraryConfigSection.Members)
            {
                System.Console.WriteLine(((DynamicElement)obj).Value1);
                System.Console.WriteLine(((DynamicElement)obj).Value2);
                System.Console.WriteLine(((DynamicElement)obj).Value3);
            }
            System.Console.ReadLine();


            /*
            var libraryConfigSection = ConfigurationManager.GetSection("DynamicLibrarySection") as DynamicLibrariesConfigSection;

            if (libraryConfigSection == null ||
            libraryConfigSection.Members == null || libraryConfigSection.Members.Count < 1)
            {
                System.Console.WriteLine("CONFIG SECTION DOES NOT EXIST !!!!!!!!!!!!!");
            }

            foreach (var obj in libraryConfigSection.Members)
            {
                System.Console.WriteLine(((DynamicLibrary)obj).DllPath);
                System.Console.WriteLine(((DynamicLibrary)obj).ClassName);
                System.Console.WriteLine(((DynamicLibrary)obj).MethodName);
            }
            System.Console.ReadLine();
            */

            /*
            var employeesConfigSection = ConfigurationManager.GetSection("employeeCollectionSection") as EmployeesConfigSection;

            if (employeesConfigSection == null ||
            employeesConfigSection.Members == null || employeesConfigSection.Members.Count < 1)
            {

            }

            foreach (var obj in employeesConfigSection.Members)
            {
                System.Console.WriteLine(((Employee)obj).Id);
                System.Console.WriteLine(((Employee)obj).HomeAddress);
            }
            */
            System.Console.ReadLine();



            var PostSetting = ConfigurationManager.GetSection("BlogGroup/PostSetting") as NameValueCollection;
            if (PostSetting.Count == 0)
            {
                Console.WriteLine("Post Settings are not defined");
            }
            else
            {
                foreach (var key in PostSetting.AllKeys)
                {
                    Console.WriteLine(key + " = " + PostSetting[key]);
                }
            }


            var LibrarySetting = ConfigurationManager.GetSection("DynamicLibraries/Library") as NameValueCollection;
            if (LibrarySetting.Count == 0)
            {
                Console.WriteLine("Library Settings are not defined");
            }
            else
            {
                foreach (var key in LibrarySetting.AllKeys)
                {
                    Console.WriteLine(key + " = " + LibrarySetting[key]);
                }
            }

            System.Console.ReadLine();




            // Create an object array consisting of the parameters to the method.
            // Make sure you get the types right or the underlying
            // InvokeMember will not find the right method


            string dllPath = ConfigurationManager.AppSettings[DLL_PATH_KEY];
            string className = ConfigurationManager.AppSettings[CLASS_NAME_KEY];
            string methodName = ConfigurationManager.AppSettings[METHOD_NAME_KEY];

            HarLogger.GetAppLogger().Debug($"dllPath : {dllPath}");
            HarLogger.GetAppLogger().Debug($"className : {className}");
            HarLogger.GetAppLogger().Debug($"methodName : {methodName}");


            //Object[] args = { 1, 3.0 };

            Object[] args = { };
            Object Result = HarUtilities.Reflection.DynamicInvoke.InvokeMethod(dllPath, className, methodName, args);

            //Object Result = HarUtilities.Reflection.DynaInvoke.InvokeMethod("DynaLibrary.dll", "DynaClass", "MethodToInvokeWithArgs", args);
            //Object Result = HarUtilities.Reflection.DynaInvoke.InvokeMethod("C:\\vsprojects\\reflect_01\\reflect_01\\bin\\Debug\\DynaLibrary.dll", "DynaClass", "MethodToInvokeWithArgs", args);

            //Object Result = HarUtilities.Reflection.DynaInvoke.InvokeMethodSlow("C:\\vsprojects\\reflect_01\\reflect_01\\bin\\Debug\\DynaLibrary.dll", "DynaClass", "MethodToInvokeWithArgs", args);

            System.Console.WriteLine(Result);
            // cast the result to the type that the method actually returned.
            



            /*
            Object[] args = { };

            Object Result = DynaInvoke.InvokeMethod("C:\\vsprojects\\reflect_01\\reflect_01\\bin\\Debug\\DynaLibrary.dll", "DynaClass", "MethodToInvoke", args);

            System.Console.WriteLine(Result);
            */



            /*
            // create instance of class DateTime
            DateTime dateTime1 = (DateTime) Activator.CreateInstance(typeof(DateTime));


            // create instance of DateTime, use constructor with parameters (year, month, day)
            DateTime dateTime2 = (DateTime) Activator.CreateInstance(typeof(DateTime), new object[] { 2008, 7, 4 });

            System.Console.WriteLine(dateTime1);

            System.Console.WriteLine(dateTime2);
            */

            System.Console.ReadLine();
        }
    }
}
