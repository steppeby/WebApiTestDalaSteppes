using DataAccess.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Data.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly WebApiDbContext _db;
        public UserRepository(WebApiDbContext db) : base(db)
        { 
            _db = db;
        }

    }
}
