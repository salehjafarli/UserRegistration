using DAL.Abstract;
using DAL.Concrete.Postgre.Repositories;
using DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Concrete.Postgre
{
    public class PostgreUnitOfWork : BaseUnitOfWork
    {
        public PostgreUnitOfWork(string ConString) : base(ConString)
        {

        }
        public override IRepository<User> Users => new PostgreSqlUserRepository(ConString);
    }
}
