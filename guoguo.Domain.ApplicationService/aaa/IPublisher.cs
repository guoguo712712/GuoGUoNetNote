using System;
using System.Collections.Generic;
using System.Text;

namespace guoguo.Domain.ApplicationService.aaa
{
    public delegate void MsgArrived(string msg);

    interface IPublisher
    {
      event  MsgArrived OnMsgArrived;
    }
}
