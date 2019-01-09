using Light.Service.Event;
using Light.Service.Event.CustomerEventArgs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Extension
{
    public static class ConfigureExtend
    {
        public static void AddEventBus(this IServiceCollection services)
        {
            //services.AddSingleton<IEventBus, EventBus>();
            //var eventBus = new EventBus();
            EventBus.InitEventBus();
        }
    }
}
