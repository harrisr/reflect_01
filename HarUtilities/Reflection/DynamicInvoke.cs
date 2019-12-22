using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarUtilities.LogUtility;

namespace HarUtilities.Reflection
{
    public class DynamicInvoke
    {
        // this way of invoking a function is slower when making multiple calls
        // because the assembly is being instantiated each time. But this code is clearer as to what is going on

        public static Object InvokeMethodSlow(string AssemblyName, string ClassName, string MethodName, Object[] args)
        {
            HarLogger.GetLogger(typeof(DynamicInvoke)).Debug("INSIDE INVOKE SLOW  !!!");

            Assembly assembly = Assembly.LoadFrom(AssemblyName);

            // SPIN through types in the assembly looking for the injected class
            foreach (Type type in assembly.GetTypes())
            {
                if (type.IsClass == true)
                {
                    // doing it this way means that you don't have to specify the full namespace and class (just the class)
                    if (type.FullName.EndsWith("." + ClassName))
                    {
                        object ClassObj = Activator.CreateInstance(type);

                        object Result = type.InvokeMember(MethodName,
                          BindingFlags.Default | BindingFlags.InvokeMethod,
                               null,
                               ClassObj,
                               args);
                        return (Result);
                    }
                }
            }
            throw (new System.Exception("Could not invoke method"));
        }

        // ---------------------------------------------
        // now do it the efficient way by holding references to the assembly and class

        // this is an inner class which holds the class instance info
        public class DynamicClassInfo
        {
            public Type type;
            public Object ClassObject;

            public DynamicClassInfo()
            {
            }

            public DynamicClassInfo(Type t, Object c)
            {
                type = t;
                ClassObject = c;
            }
        }


        public static Hashtable AssemblyReferences = new Hashtable();
        public static Hashtable ClassReferences = new Hashtable();

        public static DynamicClassInfo GetClassReference(string AssemblyName, string ClassName)
        {
            if (ClassReferences.ContainsKey(AssemblyName) == false)
            {
                Assembly assembly;
                if (AssemblyReferences.ContainsKey(AssemblyName) == false)
                {
                    AssemblyReferences.Add(AssemblyName, assembly = Assembly.LoadFrom(AssemblyName));
                }
                else
                {
                    assembly = (Assembly)AssemblyReferences[AssemblyName];
                }

                // SPIN through types in the assembly looking for the injected class
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.IsClass == true)
                    {
                        // doing it this way means that you don't have to specify the full namespace and class (just the class)
                        if (type.FullName.EndsWith("." + ClassName))
                        {
                            DynamicClassInfo ci = new DynamicClassInfo(type, Activator.CreateInstance(type));
                            ClassReferences.Add(AssemblyName, ci);
                            return (ci);
                        }
                    }
                }
                throw (new System.Exception("could not instantiate class"));
            }
            return ((DynamicClassInfo)ClassReferences[AssemblyName]);
        }

        public static Object InvokeMethod(DynamicClassInfo ci, string MethodName, Object[] args)
        {
            Object Result = ci.type.InvokeMember(MethodName,
              BindingFlags.Default | BindingFlags.InvokeMethod,
                   null,
                   ci.ClassObject,
                   args);
            return (Result);
        }

        // This is the method that is invoked from external code
        public static Object InvokeMethod(string AssemblyName, string ClassName, string MethodName, Object[] args)
        {
            HarLogger.GetLogger(typeof(DynamicInvoke)).Debug($"INSIDE InvokeMethod  !!! {AssemblyName}  {ClassName}  {MethodName}");
            foreach(var obj in args)
            {
                HarLogger.GetLogger(typeof(DynamicInvoke)).Debug($"INSIDE InvokeMethod  ARG: {obj}");
            }

            DynamicClassInfo ci = GetClassReference(AssemblyName, ClassName);
            return (InvokeMethod(ci, MethodName, args));
        }
    }
}
