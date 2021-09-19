using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public abstract class BaseRepository<T> : IRepository<T>
    {
        public string ConString { get; set; }
        public BaseRepository(string ConString)
        {
            this.ConString = ConString;
        }

        public abstract Task<T> GetByIdAsync(int id);

        public abstract Task<List<T>> GetAllAsync();

        public abstract Task<bool> CreateAsync(T Entity);

        public abstract Task<bool> DeleteAsync(int id);
    }
}
