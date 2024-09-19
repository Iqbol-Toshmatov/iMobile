using iMobile.DAL.Interfaces;
using iMobile.Object.Entity;
using iMobile.Object.Response;
using iMobile.Object.ViewModels.Phone;
using iMobile.Service.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iMobile.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PhoneController : Controller
    {
        private readonly IPhoneService _phoneService;

        public PhoneController(IPhoneService phoneService)
        {
            _phoneService = phoneService;
        }

        [HttpGet]
        [Route("/GetPhones")]
        public async Task<IActionResult> GetPhones()
        {
            var response= await _phoneService.GetPhones();
            if (response.StatusCode == Object.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        [Route("/GetPhone")]
        public async Task<IActionResult> GetPhone(int id)
        {
            var response=await _phoneService.GetPhone(id);

            if (response.StatusCode == Object.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");

        }
        [HttpDelete]
        [Route("/DeletePhone")]
        [Authorize(Roles ="Admin")]

        public async Task<IActionResult> DeletePhone(int id)
        {
            var response = await _phoneService.DeletePhone(id);

            if (response.StatusCode == Object.Enum.StatusCode.Ok)
            {
                return RedirectToAction("GetPhones");
            }
            return RedirectToAction("Error");

        }

        [HttpGet]
        [Route("/Save")]
        public async Task<IActionResult> Save(int id)
        {
            if (id==0)
            {
                return View();
            }

            var response = await _phoneService.DeletePhone(id);

            if (response.StatusCode == Object.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return RedirectToAction("Error");

        }

        [HttpPost]
        [Route("/UpdatePhone")]
        public async Task<IActionResult> UpdatePhone(PhoneViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                if (model.Id==0)
                {
                    await _phoneService.CreatePhone(model);
                }

                return View(model);
            }

            else
            {
                await _phoneService.Edit(model.Id,model);
            }

            return RedirectToAction("GetPhones");  
        }

    }
}
