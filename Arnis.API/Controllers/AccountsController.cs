using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Arnis.API.Repositiories;
using Arnis.API.Models;
using Arnis.Documents;
using Microsoft.AspNetCore.Mvc;

namespace Arnis.Web.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IWorkspaceRepository _workspaceRepository;

        public AccountsController(
            IAccountRepository accountRepository,
            IWorkspaceRepository workspaceRepository)
        {
            _accountRepository = accountRepository;
            _workspaceRepository = workspaceRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AccountCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return new StatusCodeResult((int)HttpStatusCode.BadRequest);
            }

            try
            {
                //check if account already exists
                var userName = request.Email.Substring(0, request.Email.IndexOf("@", StringComparison.Ordinal));
                var accountExists = _accountRepository
                    .GetByUserName(userName)
                    != null;

                if (!accountExists)
                {
                    //generate a new api key
                    var apiKey = "ARNIS-" + Guid.NewGuid().ToString().ToUpper().Substring(0, 4);

                    //create client account
                    var account = new Account
                    {
                        UserName = userName,
                        Email = request.Email,
                        ApiKey = apiKey
                    };
                    await _accountRepository.Create(account);

                    //create default workspace
                    var workspace = new Workspace
                    {
                        AccountId = account.Id,
                        Name = "default",
                        Description = "Default workspace",
                        Owners = new List<string> { account.UserName },
                        Solutions = new List<Solution>()
                    };
                    await _workspaceRepository.Create(workspace);

                    string baseUri = "http://arnis.azurewebsites.net";
                    string accountLocation = $"{baseUri}/{account.UserName.ToLower()}";
                    string workspaceLocation = $"{baseUri}/{account.UserName.ToLower()}/{workspace.Name.ToLower()}";

                    HttpContext.Response.StatusCode = (int)HttpStatusCode.OK;
                    HttpContext.Response.Headers.Add("Location", accountLocation);
                    var responseDto = new
                    {
                        userName = request.UserName,
                        email = request.Email,
                        apiKey,
                        accountUri = accountLocation,
                        workspaceUri = workspaceLocation
                    };

                    return new OkObjectResult(responseDto);
                }
                else
                {
                    HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return new ObjectResult(new
                    {
                        errorMessage = "Username already exists."
                    });
                }
            }
            catch (Exception ex)
            {
                HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return new ObjectResult(new
                {
                    errorMessage = ex.Message,
                    errorDetails = ex.ToString()
                });
            }
        }

    }
}