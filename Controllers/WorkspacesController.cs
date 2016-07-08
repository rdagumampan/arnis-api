using System;
using System.Net;
using Microsoft.AspNet.Mvc;
using Arnis.API.Repositiories;
using Arnis.API.Models;
using Arnis.Documents;

namespace Arnis.Web.ApiControllers
{
    [Route("api/[controller]")]
    public class WorkspacesController : Controller
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IAccountRepository _accountRepository;

        public WorkspacesController(
            IWorkspaceRepository workspaceRepository,
            IAccountRepository accountRepository)
        {
            _workspaceRepository = workspaceRepository;
            _accountRepository = accountRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] WorkspaceCreateRequest workspaceDto)
        {
            if (!ModelState.IsValid)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }

            //validate api key
            var account = _accountRepository
                .GetByApiKey(workspaceDto.ApiKey);

            if (null != account)
            {
                //find the workspace
                var workspace = _workspaceRepository.GetByName(account.Id, workspaceDto.Name);

                //create new workspace
                if (null == workspace)
                {
                    workspace = new Workspace
                    {
                        AccountId = account.Id,

                        Name = workspaceDto.Name,
                        Description = workspaceDto.Description,
                        Owners = workspaceDto.Owners,
                        Solutions = workspaceDto.Solutions,
                        Logs = workspaceDto.Logs
                    };
                    _workspaceRepository.Create(workspace);
                }
                //update existing workspace
                else
                {
                    workspace.Solutions = workspaceDto.Solutions;
                    workspace.Logs = workspace.Logs;
                    workspace.DateUpdated = DateTime.UtcNow;
                    _workspaceRepository.Update(workspace);
                }

                string baseUri = "http://arnis.azurewebsites.net";
                string workspaceLocation = $"{baseUri}/{account.UserName.ToLower()}/{workspace.Name.ToLower()}";
                HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                HttpContext.Response.Headers.Add("Location", workspaceLocation);

                var responseDto = new
                {
                    workspace = workspace.Name,
                    workspaceUri = workspaceLocation
                };

                return new HttpOkObjectResult(responseDto);
            }
            else
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }

            return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
        }
    }
}