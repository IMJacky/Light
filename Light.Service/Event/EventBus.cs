using Light.Service.Event.CustomerEventArgs;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Light.Service.Event
{
    public static class EventBus
    {
        private static readonly Dictionary<Type, object> dicEventBus = new Dictionary<Type, object>();

        public static void InitEventBus()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            Type[] types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(Subscribe<LoginEventArgs>)))
                {
                    Subscribe<LoginEventArgs> subscribe = assembly.CreateInstance(type.FullName) as Subscribe<LoginEventArgs>;
                    ADdSubscribe(subscribe);
                }

                if (type.IsSubclassOf(typeof(Subscribe<RegisterEventArgs>)))
                {
                    Subscribe<RegisterEventArgs> subscribe = assembly.CreateInstance(type.FullName) as Subscribe<RegisterEventArgs>;
                    ADdSubscribe(subscribe);
                }
            }
        }

        //public static void InitEventBusTemp()
        //{
        //    Assembly assembly = Assembly.GetExecutingAssembly();
        //    Type[] types = assembly.GetTypes();
        //    foreach (Type publisherEventArgs in types)
        //    {
        //        if (publisherEventArgs.IsSubclassOf(typeof(EventArgs)))
        //        {
        //            foreach (var type in types)
        //            {
        //                if (type.IsSubclassOf(typeof(Subscribe<publisherEventArgs>)))
        //                {
        //                    Subscribe<publisherEventArgs> subscribe = assembly.CreateInstance(type.FullName) as Subscribe<publisherEventArgs>;
        //                    ADdSubscribe(subscribe);
        //                }
        //            }
        //        }
        //    }
        //}

        //public static void InitEventBusTemp()
        //{
        //    Assembly assembly = Assembly.GetExecutingAssembly();
        //    Type[] types = assembly.GetTypes();
        //    foreach (Type publisherEventArgs in types)
        //    {
        //        if (publisherEventArgs.IsSubclassOf(typeof(EventArgs)))
        //        {
        //            //var args = (EventArgs)Activator.CreateInstance(publisherEventArgs);
        //            List<object> subscribes = new List<object>();
        //            foreach (var type in types)
        //            {
        //                if (type.IsSubclassOf(typeof(Subscribe<>)))
        //                {
        //                    //Subscribe<EventArgs> subscribe = assembly.CreateInstance(type.FullName) as Subscribe<EventArgs>;
        //                    subscribes.Add(assembly.CreateInstance(type.FullName));
        //                }
        //            }
        //            dicEventBus.Add(publisherEventArgs, subscribes);
        //        }
        //    }
        //}

        private static void ADdSubscribe<T>(Subscribe<T> subscribe) where T : EventArgs
        {
            if (dicEventBus.ContainsKey(typeof(T)))
            {
                var subscribes = dicEventBus[typeof(T)] as List<Subscribe<T>>;
                subscribes.Add(subscribe);
            }
            else
            {
                List<Subscribe<T>> subscribes = new List<Subscribe<T>>();
                subscribes.Add(subscribe);
                dicEventBus.Add(typeof(T), subscribes);
            }
        }

        public static List<Subscribe<T>> GetSubscribes<T>() where T : EventArgs
        {
            var type = typeof(T);
            var subscribeList = dicEventBus[type] as List<Subscribe<T>>;
            return subscribeList;
        }
    }
}
