using System;
using Castle.DynamicProxy;

namespace Core.Utilities.Interceptors
{
    public class MethodInterception:MethodInterceptionBaseAttribute
    {
        protected virtual void OnBefore(IInvocation invocation){}
        protected virtual void OnAfter(IInvocation invocation){}
        protected virtual void OnException(IInvocation invocation){}
        protected virtual void OnSuccess(IInvocation invocation){}
       
        public override void Intercept(IInvocation invocation)
        {
            var isSucess=true;
            OnBefore(invocation);
            try
            {
                invocation.Proceed();
            }
            catch (Exception)
            {
                isSucess=false;
                OnException(invocation);
                throw;
            }
            finally
            {
                if(isSucess)
                {
                    OnSuccess(invocation);
                }
            }

            OnAfter(invocation);
        }
    }
}