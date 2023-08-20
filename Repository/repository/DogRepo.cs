using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Repository.repository
{
    public class DogRepository : BaseRepository<Dog>
    {
        public DogRepository() { }

        //get all including include DogType

        public IQueryable<Dog> GetAllIncludingDogType()
        {
            try
            {
                return _dbContext.Set<Dog>().Include(d => d.DogType);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting entities including DogType: {ex.Message}", ex);
            }
        }
    }
}
