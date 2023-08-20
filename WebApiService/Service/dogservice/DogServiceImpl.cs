using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Repository.Models;
using Repository.repository;
using WebApiService.DTO.response;
using Microsoft.EntityFrameworkCore;

namespace WebApiService.Service.dogservice
{
    public class DogServiceImpl : IDogService
    {

        //Apply SingleTon pattern + Dependency Injection
        private static DogServiceImpl _instance = null; // creating singleton
        private static readonly object _lockInstance = new object();
        private readonly IMapper _mapper;
        private readonly DogRepository _dogRepository;

        public DogServiceImpl(IMapper mapper, DogRepository dogRepository)
        {
            _mapper = mapper;
            _dogRepository = dogRepository;
        }

        public DogServiceImpl()
        {

        }






        //===================================
        public bool AddDog(Repository.Models.Dog dog)
        {
            //after recieve from API_Controller

            try
            {
                //check duplicate id
                if (dog == null)
                {
                    return false;
                }
                ICollection<Repository.Models.Dog> checkDuplicate = (ICollection<Repository.Models.Dog>)_dogRepository.GetAll();
                checkDuplicate.Where(e => e.DogId == dog.DogId);

                if (checkDuplicate.Count > 0)
                {
                    //DUPLICATE CASE
                    return false;


                }


                //call repo
                _dogRepository.Add(dog);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error service adding dog: {ex.Message}", ex);
                // return false;
            }
        }

        // public async Task<bool> DeleteDog(Repository.Models.Dog dog)
        // {
        //     //after recieve Dog from controller
        //     try
        //     {
        //         //call repo
        //         _dogRepository.Delete(dog);
        //         return true;
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception($"Error service Deleting: {ex.Message}", ex);
        //         // return false;
        //     }
        // }

        public List<DogInfoResponse> GetAllDogs()
        {
            try
            {
                //call repo
                //return (ICollection<Dog>)_dogRepository.GetAll(); // have to cast from data set to icollection
                // var dogs = _dogRepository.GetAll().Include(d => d.DogType).ToList();

                var dogs = _dogRepository.GetAll();
                var dogs2 = _dogRepository.GetAllIncludingDogType();

                var noUsingMapperDogReses = new List<DogInfoResponse>();

                foreach (var dog in dogs2)
                {
                    var dogInfo = new DogInfoResponse
                    {
                        DogId = dog.DogId,
                        DogName = dog.DogName,
                        DogDescription = dog.DogDescription,
                        DogAge = dog.DogAge,
                        DogTypeId = dog.DogType?.Id ?? 0,
                        DogTypeName = dog.DogType?.DogTypeName
                    };

                    noUsingMapperDogReses.Add(dogInfo);
                }

                // var dogInfoResponses = _mapper.Map<ICollection<DogInfoResponse>>(dogs2).ToList();
                return noUsingMapperDogReses;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error service get al~l entities: {ex.Message}", ex);
            }
        }

        public List<DogInfoResponse> GettAllDogsByName(string? name)
        {
            try
            {
                if (name == null) name = "";
                //call repo
                var dogsByName = _dogRepository.GetByName(name);
                var dogInfoResponses = _mapper.Map<ICollection<DogInfoResponse>>(dogsByName).ToList();
                return dogInfoResponses;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        // public async Task<bool> UpdateDog(Repository.Models.Dog dog)
        // {
        //     try
        //     {
        //         if (dog == null)
        //         {
        //             return false;
        //         }
        //         //check exist

        //         var dogExist = _dogRepository.GetByIntId(dog.DogId);

        //         _dogRepository.Update(dog);
        //         return true;

        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception(ex.Message, ex);
        //         // return false;
        //     }
        // }


    }
}
