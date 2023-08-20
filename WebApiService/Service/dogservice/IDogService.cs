using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiService.DTO.response;
using Repository.Models;

namespace WebApiService.Service.dogservice
{
    public interface IDogService
    {
        //creating service for implementing in the 
        //adding
        bool AddDog(Repository.Models.Dog  dog);
        //retrieving 
        //all
        List<DogInfoResponse> GetAllDogs();
        //by name
        List<DogInfoResponse> GettAllDogsByName(string name);

        // //updating
        // Task<bool> UpdateDog(Repository.Models.Dog dog);
  

        // //deleting
        // Task<bool> DeleteDog(Repository.Models.Dog  dog);
    }
}