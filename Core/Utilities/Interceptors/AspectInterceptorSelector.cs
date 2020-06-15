using System;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
   public class AspectInteerceptorSelector : IInterceptorSelector
    {
        public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
        {
            var classAttribute=type.GetCustomAttributes<MethodInterceptionBaseAttribute>(true).ToList();
            var methodAttributes=type.GetMethod(method.Name).GetCustomAttributes<MethodInterceptionBaseAttribute>(true);
            classAttribute.AddRange(methodAttributes);

            return classAttribute.OrderBy(x=>x.Priority).ToArray();
        }
    }
}