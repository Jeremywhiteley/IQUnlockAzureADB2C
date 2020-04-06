using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace GetUserPermissionsFunction
{
    public class GetUserPermissions
    {
        [FunctionName("GetUserPermissions")]
        public IActionResult Run(
            [HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequest httpRequest, ILogger log)
        {
            IActionResult result;

            // Get user object ID from request
            var userObjectId = Guid.Parse(httpRequest.Query["objectId"]);

            // Get the application group that the user is coming through
            var groupObjectId = (string)httpRequest.Query["groupId"];

            // If a user has just signed-up
            if (IsUserNew(httpRequest))
            {
                // Here we can insert the new user object id somewhere
                // into our own systems to have its Azure AD user reference
            }

            // Based on the GroupId (application) and UserId
            // we can fetch permissions from our own systems/databases/etc.
            // var permissions = GetPermissionsFromDatabase(groupObjectId, userObjectId);

            // For now, I'll just return a static permission set for all logged-in users

            // Serialize
            var model = new
            {
                UserPermissions = JsonConvert.SerializeObject(new
                {
                    Application = groupObjectId,
                    Permissions = new List<string>
                    {
                        "CanSeeDashboard",
                        "CanEditProfile",
                        "CanApproveProjects",
                        "CanEditProjects"
                    }
                })
            };

            result = new OkObjectResult(model);

            return result;
        }

        private bool IsUserNew(HttpRequest httpRequest)
        {
            var isNew = false;

            try
            {
                isNew = bool.Parse(httpRequest.Query["newUser"]);
            }
            catch { }

            return isNew;
        }
    }
}