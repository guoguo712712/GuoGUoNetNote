using System;
using System.Collections.Generic;
using System.Text;

namespace guoguo.Domain.ApplicationService.bbbb
{
    public delegate void SubscribeHandle(string str);
    //定义订阅接口
    public interface ISubscribe
    {
        event SubscribeHandle SubscribeEvent;
    }
}
