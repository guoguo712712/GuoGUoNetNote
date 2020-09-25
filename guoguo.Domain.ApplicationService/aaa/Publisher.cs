using System;
using System.Collections.Generic;
using System.Text;

namespace guoguo.Domain.ApplicationService.aaa
{
    class Publisher 
    {
        private List<ISubscriber> _SubscriberList = new List<ISubscriber>();

        public List<ISubscriber> SubscriberList
        {
            get => _SubscriberList;
            private set => _SubscriberList = value;
        }

        public void InvokeEvent(string msg)
        {
            if (_SubscriberList != null && _SubscriberList.Count > 0)
                _SubscriberList.ForEach(ss => ss.ResolveMsg(msg));
        }

       
    }
}
