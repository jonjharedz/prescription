using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Webtest.Provider
{
    public class BasicHttpAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext httpActionContext)
        {
            var authHeader = httpActionContext.Request.Headers.Authorization;
            if (authHeader == null)
                return false;

            string[] authDefinition = authHeader.ToString().Split(' ');
            if (authDefinition == null || authDefinition.Count() < 2)
            {
                return false;
            }

            // Get the header value
            AuthenticationHeaderValue auth = new AuthenticationHeaderValue(authDefinition[0], authDefinition[1]);
            // ensure its schema is correct
            if (auth != null && string.Compare(auth.Scheme, "Basic", StringComparison.OrdinalIgnoreCase) == 0)
            {
                // get the credientials
                string credentials = UTF8Encoding.UTF8.GetString(Convert.FromBase64String(auth.Parameter));
                int separatorIndex = credentials.IndexOf(':');
                if (separatorIndex >= 0)
                {
                    // get user and password
                    string passedUserName = credentials.Substring(0, separatorIndex);
                    string passedPassword = credentials.Substring(separatorIndex + 1);
                    string ValidUsername = ConfigurationManager.AppSettings["BasicAuth_Username"];
                    string ValidPassword = ConfigurationManager.AppSettings["BasicAuth_Password"];

                    if (ValidUsername == null || ValidPassword == null)
                    {
                        return false;
                    }

                    // validate
                    if (passedUserName == ValidUsername && passedPassword == ValidPassword)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext filterContext)
        {
            throw new HttpException(403, "Access denied.");
        }
    }
}