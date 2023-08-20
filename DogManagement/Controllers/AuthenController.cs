using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Repository.Models;
using WebApiService.DTO.request;
using WebApiService.Service.authenservice;

namespace DogManagement.Controllers
{
    [Route("api/haha/nata/[controller]")]
    [ApiController]
    public class AuthenController : Controller
    {
        private readonly ILogger<AuthenController> _logger;
        private IConfiguration _config;
        private readonly AuthenService _authenService;
        private static readonly string jwtSecretKey = "khoi@12";
        private static readonly string jwtIssuer = "http://localhost:5293";
        private static readonly string jwtAudience = "http://localhost:5293";

        public AuthenController(ILogger<AuthenController> logger, IConfiguration config, AuthenService authenService)
        {
            _logger = logger;
            _config = config;
            _authenService = authenService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        // [AllowAnonymous]
        [Route("/login")]
        [HttpPost]
        public IActionResult Login([FromBody] LoginRequest _logingRequest)
        {

            var check = _authenService.checkLogin(_logingRequest,jwtIssuer, jwtAudience, jwtSecretKey);
            if(check == null){
                // return NotFound("Error login");
                return Ok("nothing nothing");
            }else{
                return Ok(check);
            }

        }

     
    }
}