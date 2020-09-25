﻿using System;
using System.Collections.Generic;
using System.Text;

namespace guoguo.Domain.ApplicationService.bbbb
{
    //定义发布事件
    public delegate void PublishHandle(string str);
    //定义发布接口
    public interface IPublish
    {
        event PublishHandle PublishEvent;

        void Notify(string str);
    }
}
