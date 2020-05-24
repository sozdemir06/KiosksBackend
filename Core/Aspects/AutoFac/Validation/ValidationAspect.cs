using System;
using System.Linq;
using System.Net;
using Castle.DynamicProxy;
using Core.CrossCuttingConcerns.Validation.FluentValidation;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.Messages;
using FluentValidation;

namespace Core.Aspects.AutoFac.Validation
{
    public class ValidationAspect : MethodInterception
    {
        private Type _validatortype;
        public ValidationAspect(Type validatortype)
        {
            
            if (!typeof(IValidator).IsAssignableFrom(validatortype))
            {
                throw new RestException(HttpStatusCode.BadRequest, new { WronType = AspectMessages.WrongType });
            }
            
            _validatortype = validatortype;
        }
        protected override void OnBefore(IInvocation invocation)
        {
            var validator=(IValidator)Activator.CreateInstance(_validatortype);

            var entitiyType=_validatortype.BaseType.GetGenericArguments()[0];
            var entities=invocation.Arguments.Where(t=>t.GetType()==entitiyType);
            foreach (var entity in entities)
            {
                ValidationTool.Validate(validator,entity);
            }
        }
    }
}