using System.Diagnostics;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aspects.AutoFac.Performance
{
    public class PerformanceAspect : MethodInterception
    {
        private int _interval;
        private Stopwatch _stopWatch;

        public PerformanceAspect(int _interval)
        {
            this._interval = _interval;
            _stopWatch = ServiceTool.ServiceProvider.GetService<Stopwatch>();

        }
        protected override void OnBefore(IInvocation invocation)
        {
            _stopWatch.Start();
        }

        protected override void OnAfter(IInvocation invocation)
        {
            if (_stopWatch.Elapsed.TotalSeconds > _interval)
            {
                //istenirse mail atılabilir
                Debug.WriteLine($"Performance: {invocation.Method.DeclaringType.FullName}.{invocation.Method.Name}-->{_stopWatch.Elapsed.TotalSeconds}");
            }
            _stopWatch.Reset();
        }
    }
}