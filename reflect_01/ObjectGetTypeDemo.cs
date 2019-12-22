// ObjectGetTypeDemo.cs 
using System;
namespace Reflection
{
    public class ObjectGetTypeDemo
    {
        public static void MainZZ1(string[] args)
        {
            Car c = new Car();
            Type t = c.GetType();
            Console.WriteLine(t.FullName);
            Console.ReadLine();
        }
    }
}