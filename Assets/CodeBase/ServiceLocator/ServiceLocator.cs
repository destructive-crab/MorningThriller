using System;
using System.Collections.Generic;

namespace destructive_code.ServiceLocators
{
    public class ServiceLocator<TService>
        where TService : class
    {
        private readonly Dictionary<Type, TService> services = new ();

        public bool Contains<TServiceEnquire>() where TServiceEnquire : class => services.ContainsKey(typeof(TServiceEnquire));
        
        public TServiceEnquire Get<TServiceEnquire>() where TServiceEnquire : class => services[typeof(TServiceEnquire)] as TServiceEnquire;
        
        public bool TryGet<TServiceEnquire>(out TServiceEnquire serviceEnquire)
            where TServiceEnquire : class 
        {
            serviceEnquire = services[typeof(TServiceEnquire)] as TServiceEnquire;
            return serviceEnquire != null;
        }

        public ServiceLocator<TService> Register(TService service)
        {
            services.TryAdd(service.GetType(), service);
            
            return this;
        }
        
        public ServiceLocator<TService> Unregister(TService service)
        {
            if (services.ContainsKey(service.GetType()))
                services.Remove(service.GetType());
            
            return this;
        }
    }
}