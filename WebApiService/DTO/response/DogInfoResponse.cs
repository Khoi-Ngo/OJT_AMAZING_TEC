using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.DTO.response
{
    public class DogInfoResponse
    {
        public int DogId { get; set; }

        public string? DogName { get; set; }

        public string? DogDescription { get; set; }

        public int? DogAge { get; set; }
        public int DogTypeId { get; set; } // New property for DogType Id
        public string? DogTypeName { get; set; } // New property for DogType Name


    }
}