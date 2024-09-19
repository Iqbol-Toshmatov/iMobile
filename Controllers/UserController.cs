using iMobile.Object.ViewModels.User;
using iMobile.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace iMobile.Controllers
{
    [ApiController]
    [Route("api[controller]")]
    public class UserController : Controller
    {
        private readonly IUserService service;
        public UserController(IUserService userService)
        {
            service = userService;
        }

        [HttpGet]
        [Route("/GetUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetUsers()
        {
            var response=await service.GetUsers();
            if (response.StatusCode==iMobile.Object.Enum.StatusCode.Ok)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.Description);
        }

        [HttpDelete]
        [Route("/DeleteUser")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var response=await service.DeleteUser(id);
            if (response.StatusCode==iMobile.Object.Enum.StatusCode.Ok)
            {
                return Ok(response.Description);
            }
            return BadRequest(response.Description);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("/AddUser")]
        public async Task<IActionResult> Save(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var response = await service.Create(user);
                if (response.StatusCode==iMobile.Object.Enum.StatusCode.Ok)
                {
                    return Json(new { description = response.Data });
                }
                return BadRequest(response.Data);
            }
            var errorMessage=ModelState.Values
                .SelectMany(x=>x.Errors.Select(x=>x.ErrorMessage)).ToList();
            return StatusCode(StatusCodes.Status500InternalServerError, new { errorMessage });
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
