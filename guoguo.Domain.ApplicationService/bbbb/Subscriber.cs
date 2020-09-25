﻿using System;
using System.Collections.Generic;
using System.Text;

namespace guoguo.Domain.ApplicationService.bbbb
{
    public class Subscriber
    {
        private string _subscriberName;

        public Subscriber(string subscriberName)
        {
            this._subscriberName = subscriberName;
        }

        public ISubscribe AddSubscribe { set { value.SubscribeEvent += Show; } }
        public ISubscribe RemoveSubscribe { set { value.SubscribeEvent -= Show; } }

        private void Show(string str)
        {
            Console.WriteLine(string.Format("我是{0}，我收到订阅的消息是:{1}", _subscriberName, str));
        }
    }
}
