using System;
using System.Collections.Generic;
using System.Net;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Logging;
using Core.CrossCuttingConcerns.Logging.NLog;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;

namespace Core.Aspects.AutoFac.Logging
{
    public class LogAspect : MethodInterception
    {
        private readonly LoggerServiceBase _loggerServiceBase;
        public LogAspect(Type loggerService)
        {
            if (loggerService.BaseType != typeof(LoggerServiceBase))
            {
                throw new RestException(HttpStatusCode.BadRequest, new { WrongLoggingType = AspectMessages.WrongLoggerType });
            }
        
            _loggerServiceBase = (LoggerServiceBase)Activator.CreateInstance(loggerService);
        }

         protected override void OnBefore(IInvocation invocation)
        {
            _loggerServiceBase.Info(GetLogDetail(invocation));
        }

        private LogDetail GetLogDetail(IInvocation invocation)
        {
            var logParameters = new List<LogParameter>();
            for (int i = 0; i < invocation.Arguments.Length; i++)
            {
                logParameters.Add(new LogParameter
                {
                    Name = invocation.GetConcreteMethod().GetParameters()[i].Name,
                    Value = invocation.Arguments[i],
                    Type = invocation.Arguments[i].GetType().Name

                });
            }
            var logDetail = new LogDetail
            {
                MethodName = invocation.Method.Name,
                Parameters = logParameters,

            };
            return logDetail;
        }

    }
}