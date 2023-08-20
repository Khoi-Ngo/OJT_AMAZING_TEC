using Microsoft.EntityFrameworkCore;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.repository
{
    public class BaseRepository<T> where T : class
    {
        /*
         * 
        IQueryable<T>:

IQueryable<T> is an interface in C# that represents a queryable collection of data, 
        typically used for querying data from a data source (like a database) in a flexible and composable manner.
The GetAll() method returns an IQueryable<T> instance representing the entities of 
        type T in the _dbContext. This allows you to perform further operations like filtering,
        sorting, and projecting on the returned data before executing the query.
T? (Nullable Reference Type):


         */

        protected readonly DogSlot01AmazingContext _dbContext;
        public BaseRepository()
        {
            _dbContext = new DogSlot01AmazingContext();
        }

        // Concurency code -> Query ...
        public IQueryable<T> GetAll()
        {
            try
            {
                return _dbContext.Set<T>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting entity: {ex.Message}", ex);
            }
        }
        public async Task<T?> GetByIntId(int id)
        {
            try
            {
                var entity = await _dbContext.Set<T>().FindAsync(id);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting by id : {ex.Message}", ex);
            }
        }
        //LOOKOUT THIS FUNCTION : e => EF.Property<string>(e, "Name") ??
        public IQueryable<T> GetByName(string namePattern)
        {
            try
            {
                var entities = _dbContext.Set<T>().Where(e => EF.Property<string>(e, "Name").Contains(namePattern));
                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error getting entities by name : {ex.Message}", ex);
            }
        }


        //CREATE
        public async Task Add(T entity)
        {
            try
            {
                await _dbContext.Set<T>().AddAsync(entity);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error while creating in database : {ex.Message}", ex);
            }
        }

        //UPDATE
        public async Task Update(T entity)
        {
            try
            {
                //in this  circumstance the EF will recieve an exist entity and update it 
                //the validation should be included in the service layer including ?exist ?validation input
                //await _dbContext.Set<T>().UpdateAsync(entity);
                //in the EF there is no direct UpdateAsync
                _dbContext.Set<T>().Update(entity);
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error updating entity in the database: {ex.Message}");
            }
        }

        //DELETE
        public async Task Delete(T entity)
        {
            try
            {
                _dbContext.Set<T>().Remove(entity);
                await _dbContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new Exception($"Error at deleting in the database: {ex.Message}", ex);
            }
        }

    }
}
