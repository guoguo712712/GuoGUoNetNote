using guoguo.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace guoguo.Domain.ApplicationService
{
    public class BasicUserRepository : IBasicUserRepository
    {
        //private NoteDbContext _NoteContext;
        public IServiceScopeFactory _ServiceScopeFactory;

        public BasicUserRepository(IServiceScopeFactory serviceScopeFactory)
        {
            _ServiceScopeFactory = serviceScopeFactory;
        }
        public bool Validate(string userName, string password)
        {
            using (var scope = _ServiceScopeFactory.CreateScope())
            {
                NoteDbContext context = scope.ServiceProvider.GetService<NoteDbContext>();
                BasicUser user= context.BasicUsers.Where(uu => uu.UserName == userName && uu.PassWord == password).FirstOrDefault();
                return user != null;

            }


        }
    }
}
  