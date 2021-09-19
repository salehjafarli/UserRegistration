using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Abstract
{
    public abstract class BaseUnitOfWork : IUnitOfWork
    {
        public string ConString { get; set; }
        public BaseUnitOfWork(string ConString)
        {
            this.ConString = ConString;
        }
        public abstract IRepository<User> Users { get; }
    }
}
