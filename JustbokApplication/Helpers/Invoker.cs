using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JustbokApplication.Helpers
{
    public static class Invoker
    {
        /// <summary>
        ///  creating instance for class and calling required method
        /// </summary>
        /// <param name="typeName">name of class</param>
        /// <param name="constructorArgs">class constructor parameters</param>
        /// <param name="methodName">method name in class</param>
        /// <param name="methodArgs">parameters of method</param>
        /// <returns></returns>
        public static object CreateAndInvoke(string typeName, object[] constructorArgs, string methodName, object[] methodArgs)
        {
            Type type = Type.GetType(typeName);
            object instance = Activator.CreateInstance(type, constructorArgs);

            MethodInfo method = type.GetMethod(methodName);
            return method.Invoke(instance, methodArgs);
        }

        /// <summary>
        /// creating instance for class by class name as parameter
        /// </summary>
        /// <param name="typeName">name of class</param>
        /// <param name="constructorArgs"> class constructor parameters</param>
        /// <returns></returns>
        public static object CreateAndInvoke(string typeName, object[] constructorArgs)
        {
            Type type = Type.GetType(typeName);
            return Activator.CreateInstance(type, constructorArgs);
        }
    }
}
