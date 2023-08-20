using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using WebApiService.DTO.response;
using Repository.Models;

//mapper model Response - Entity


namespace Service.mapper
{
    public class autoMapperConfig : Profile
    {
        //add all mapper in the constructor default
        public autoMapperConfig()
        {
            MapDog();
            MapDogType();
            MapOwner(); 
        }



        private void MapDog()
        {
            CreateMap<Dog, DogInfoResponse>().ReverseMap();
        }
        private void MapOwner()
        {
            CreateMap<Owner, OwnerResponse>().ReverseMap();
        }

        private void MapDogType()
        {
            CreateMap<DogType, DogTypeResponse>().ReverseMap();
        }
    }




}
