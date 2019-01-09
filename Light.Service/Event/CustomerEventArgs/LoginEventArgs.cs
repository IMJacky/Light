using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Service.Event.CustomerEventArgs
{
    public class LoginEventArgs : EventArgs
    {
        public string UserName { get; set; }

        public string IP { get; set; }
    }
}
