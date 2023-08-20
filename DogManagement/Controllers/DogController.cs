using Microsoft.AspNetCore.Mvc;
using Repository.Models;
// using Service.service.dog;
using WebApiService.Service.dogservice;


namespace DogManagement.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class DogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly IDogService _dogService;

        public DogController(IDogService dogService)
        {
            _dogService = dogService;
        }


        //CRUD Controller here...


        [HttpGet] //api / Dog
        public IActionResult GetAllDogs()
        {
            try
            {
                // DogServiceImpl _dogServiceImpl = new DogServiceImpl();
                var dogResponses = _dogService.GetAllDogs();
                return Ok(dogResponses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");

            }
        }



        // GET: api/Dog/{name}
        [HttpGet("{name}")]
        public IActionResult GetDogsByName(string name)
        {
            try
            {
                var dogResponses = _dogService.GettAllDogsByName(name);
                return Ok(dogResponses);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // POST: api/Dog
        [HttpPost]
        public  IActionResult AddDog([FromBody] Dog dog)
        {
            try
            {
                var success =  _dogService.AddDog(dog);
                if (success)
                {
                    return Ok("Dog added successfully.");
                }
                return BadRequest("Failed to add dog.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // [HttpDelete("{id}")]
        // public async Task<IActionResult> DeleteDog(int id)
        // {
        //     try
        //     {
        //         var dog = new Dog { DogId = id }; // Create a dummy Dog with ID to delete
        //         var success = await _dogService.DeleteDog(dog);
        //         if (success)
        //         {
        //             return Ok("Dog deleted successfully.");
        //         }
        //         return BadRequest("Failed to delete dog.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"An error occurred: {ex.Message}");
        //     }
        // }
        // [HttpPut("{id}")]
        // public async Task<IActionResult> UpdateDog(int id, [FromBody] Dog dog)
        // {
        //     try
        //     {
        //         dog.DogId = id; // Set the DogId to match the route parameter
        //         var success = await _dogService.UpdateDog(dog);
        //         if (success)
        //         {
        //             return Ok("Dog updated successfully.");
        //         }
        //         return BadRequest("Failed to update dog.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"An error occurred: {ex.Message}");

        //     }
        // }
    }
}