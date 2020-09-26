using DDD.Domain.UserAggregate;
using DDD.Infrastructure.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace DDD.Infrastructure.Repositories
{
    class UserRepository : Repository<User, long, DomainContext>
    {
        public UserRepository(DomainContext context) : base(context)
        {
        }
    }
}
