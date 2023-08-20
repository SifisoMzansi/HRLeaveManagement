using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly LeaveManagementDBContext _dBContext;

        public GenericRepository(LeaveManagementDBContext dBContext)
        {
            _dBContext = dBContext;
        }
        public async Task Add(T entity)
        { 
            await _dBContext.AddAsync(entity);
          //  await _dBContext.SaveChangesAsync();         
         }

        public async Task Delete(T entity)
        {
            _dBContext.Set<T>().Remove(entity);
       //     await _dBContext.SaveChangesAsync();
        }

        public async Task<bool> Exists(int id)
        {
            var  Entity = await Get(id);
            return Entity != null;

        }

        public async Task<T> Get(int id)
        {
            return await _dBContext.Set<T>().FindAsync(id );
        }

        public async Task<IReadOnlyList<T>> GetAll()
        {
            return await _dBContext.Set<T>().ToListAsync();
        }

        public async Task Update(T entity)
        {
            try
            {
                _dBContext.Entry(entity).State = EntityState.Modified;
                _dBContext.Set<T>().Update(entity);

               // await _dBContext.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
            }
        }
    }
}
