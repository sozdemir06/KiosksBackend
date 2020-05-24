using System.Net;
using Business.Constants;
using Castle.DynamicProxy;
using Core.Extensions;
using Core.Utilities.Interceptors;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace BusinessAspects.AutoFac
{
    public class SecuredOperation : MethodInterception
  {
    private readonly IHttpContextAccessor _httpContextAccessor;
    private string[] _roles;
    public SecuredOperation(string roles)
    {
      _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
      _roles=roles.Split(',');
    }

    protected override void OnBefore(IInvocation invocation)
    {
      var roleClaims = _httpContextAccessor.HttpContext.User.ClaimRoles();
      foreach (var role in _roles)
      {
          if(roleClaims.Contains(role))
          {
              return;
          }
      }

      throw new RestException(HttpStatusCode.Unauthorized, new{AuthorizedDenied=Messages.AuthorizationsDenied});
    }
  }
}