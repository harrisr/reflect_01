using System;
using System.Reflection;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflect_01
{
    public class TestMethodInfo
    {
        public static void MainZZZ11()
        {
            // Get the constructor and create an instance of MagicClass

            Type magicType = Type.GetType("MagicClass");
            ConstructorInfo magicConstructor = magicType.GetConstructor(Type.EmptyTypes);
            object magicClassObject = magicConstructor.Invoke(new object[] { });

            // Get the ItsMagic method and invoke with a parameter value of 100

            MethodInfo magicMethod = magicType.GetMethod("ItsMagic");
            object magicValue = magicMethod.Invoke(magicClassObject, new object[] { 100 });

            Console.WriteLine("MethodInfo.Invoke() Example\n");
            Console.WriteLine("MagicClass.ItsMagic() returned: {0}", magicValue);
        }
    }

    // The example program gives the following output:
    //
    // MethodInfo.Invoke() Example
    //
    // MagicClass.ItsMagic() returned: 900
}
