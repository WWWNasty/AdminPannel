using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Serilog.Context;
using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace Admin.Panel.Web.Logging
{
    public class UserNameEnricher : ILogEventEnricher
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public UserNameEnricher(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        
        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            var identityName = _contextAccessor.HttpContext.User.Identity.Name;
            
            if (identityName != null)
            {
                logEvent.AddPropertyIfAbsent(propertyFactory.CreateProperty("UserName", identityName));
            }
        }
    }
}