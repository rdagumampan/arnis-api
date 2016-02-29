using System;
using System.Collections.Generic;
using Arnis.API.Repositiories;
using Arnis.API.Models;
using Microsoft.AspNet.Mvc;

namespace Arnis.Web.Controllers
{
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public AccountController(
            IAccountRepository accountRepository, 
            IWorkspaceRepository workspaceRepository)
        {
            _accountRepository = accountRepository;
            _workspaceRepository = workspaceRepository;
        }

        [HttpPost]
        public IActionResult Create([FromBody] AccountRequest request)
        {
            if (!ModelState.IsValid)
            {
                HttpContext.Response.StatusCode = 400;  //400 Bad Request
            }
            else
            {
                //check if account already exists
                var accountExists = _accountRepository.GetByUserName(request.UserName) != null;

                if (!accountExists)
                {
                    //generate a new api key
                    var apiKey = "ARNIS-" + Guid.NewGuid().ToString().ToUpper().Substring(0,6);
                    var accountDbo = new Account
                    {
                        UserName = request.UserName,
                        ApiKey = apiKey
                    };

                    //create client account
                    _accountRepository.Add(accountDbo);
                        
                    //create default workspace
                    var workspaceDbo = new Workspace
                    {
                        AccountId = accountDbo.Id,
                        Name = "default",
                        Description = "This is your default workspace",
                        Owners = new List<string> { accountDbo.UserName }
                    };
                    _workspaceRepository.Add(workspaceDbo);

                    string workspaceLocation = $"{Request.Scheme}://{Request.Host}/workspaces/{request.UserName.ToLower()}";
                    var responseDto = new
                    {
                        userName = request.UserName,
                        apiKey = apiKey,
                        workspace = workspaceDbo.Name,
                        location =  workspaceLocation
                    };

                    return new HttpOkObjectResult(responseDto);
                }
                else
                {
                    HttpContext.Response.StatusCode = 400;  //400 Bad Request
                    return new ObjectResult(new
                    {
                        errorMessage = "Account with desired username already exists."
                    });

                }
            }

            return new HttpStatusCodeResult(400);
        }

    }
}