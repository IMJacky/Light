using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Common
{
    public class SingleTon<T> where T : class, new()
    {
        private static T t;
        private static readonly object obj = new object();
        protected static T Instance
        {
            get
            {
                if (t == null)
                {
                    lock (obj)
                    {
                        if (t == null)
                        {
                            t = new T();
                        }
                    }
                }
                return t;
            }
        }
    }
}
