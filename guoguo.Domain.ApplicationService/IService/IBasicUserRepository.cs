using System;
using System.Collections.Generic;
using System.Text;

namespace guoguo.Domain.ApplicationService
{
   public interface IBasicUserRepository
    {
        bool Validate(string userName, string password);

    }
}
