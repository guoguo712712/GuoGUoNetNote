using System;
using System.Collections.Generic;
using System.Text;

namespace guoguo.Domain.ApplicationService.aaa
{
    interface ISubscriber
    {
        void ResolveMsg(string msg);

    }
}
