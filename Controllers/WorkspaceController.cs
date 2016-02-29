using System.Collections.Generic;
using Microsoft.AspNet.Mvc;
using Arnis.API.Repositiories;
using Arnis.API.Models;
using MongoDB.Bson;

namespace Arnis.Web.ApiControllers
{
    [Route("api/[controller]")]
    public class WorkspaceController : Controller
    {
        private readonly IWorkspaceRepository _workspaceRepository;
        private readonly IAccountRepository _accountRepository;

        public WorkspaceController(
            IWorkspaceRepository workspaceRepository, 
            IAccountRepository accountRepository)
        {
            _workspaceRepository = workspaceRepository;
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IEnumerable<Workspace> GetAll()
        {
            var workspaces = _workspaceRepository.All();
            return workspaces;
        }

        [HttpGet("{id:length(24)}", Name = "GetByIdRoute")]
        public IActionResult GetById(string id)
        {
            var workspaceDbo = _workspaceRepository.GetById(new ObjectId(id));
            if (workspaceDbo == null)
            {
                return HttpNotFound();
            }

            return new ObjectResult(workspaceDbo);
        }

        [HttpPost]
        public void Create([FromBody] WorkspaceRequest workspaceDto)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;  //400 Bad Request
            }
            else
            {
                //validate api key
                var account = _accountRepository.GetByApiKey(workspaceDto.ApiKey);
                if(account!= null)
                {
                    //update workspace
                    var workspaceDbo = new Workspace
                    {
                        AccountId = account.Id,
                        Solutions = workspaceDto.Solutions
                    };
                    _workspaceRepository.Update(workspaceDbo);

                    HttpContext.Response.StatusCode = 200;
                    string workspaceLocation = $"{Request.Scheme}://{Request.Host}/workspaces/{account.UserName.ToLower()}";
                    HttpContext.Response.Headers.Add("Location", workspaceLocation);
                }
                else
                {
                    HttpContext.Response.StatusCode = 400;  //400 Bad Request
                }
            }
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            if (_workspaceRepository.Remove(new ObjectId(id)))
            {
                return new HttpStatusCodeResult(204);   // 204 No Content
            }
            return HttpNotFound();
        }
    }
}