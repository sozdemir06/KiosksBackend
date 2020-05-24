using System.Transactions;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors;

namespace Core.Aspects.AutoFac.Transaction
{
    public class TransactionAspect : MethodInterception
    {
       public override void Intercept(IInvocation invocation)
        {
            using (TransactionScope transactionScope = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transactionScope.Complete();
                }
                catch (System.Exception)
                {

                    transactionScope.Dispose();
                    throw;
                }
            }
        }
    }
}