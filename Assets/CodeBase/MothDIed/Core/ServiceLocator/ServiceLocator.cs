using System;
using System.Collections.Generic;

namespace MothDIed.ServiceLocators
{
    public class ServiceLocator<TServiceBase> : IServiceLocator
        where TServiceBase : class
    {
        private readonly Dictionary<Type, TServiceBase> services = new ();
        private readonly List<TServiceBase> servicesList = new ();

        public TServiceBase[] GetAll() => servicesList.ToArray();
        
        public bool Contains<TService>() where TService : class
            => Contains(typeof(TService));
        
        public TService Get<TService>() where TService : class, TServiceBase
        {
            return Get(typeof(TService)) as TService;
        }

        public bool Contains(Type serviceType)
        {
            return services.ContainsKey(serviceType);
        }

        public object Get(Type extensionType)
        {
            if (!services.ContainsKey(extensionType))
                throw new Exception($"No service of type {extensionType}");
            
            return services[extensionType];
        }

        public T[] GetAllOfType<T>() 
            where T : class
        {
            List<T> result = new List<T>();

            foreach (var service in services)
            {
                if (service.Key.IsSubclassOf(typeof(T)) || service.Key == typeof(T))
                {
                    result.Add(service.Value as T);
                }
            }

            return result.ToArray();
        }

        public bool TryGet<TService>(out TService serviceEnquire)
            where TService : class, TServiceBase
        {
            services.TryGetValue(typeof(TService), out var outService);
            serviceEnquire = outService as TService;

            return serviceEnquire != null;
        }

        public ServiceLocator<TServiceBase>Register<TService>(TService service)
            where TService : TServiceBase
        {
            if(services.TryAdd(service.GetType(), service))
                servicesList.Add(service);

            return this;
        }

        public void UnregisterAll()
        {
            var keys = new List<Type>(services.Keys);
            
            while (services.Count > 0)
            {
                Unregister(keys[0]);
                keys.RemoveAt(0);
            }
        }
        
        public bool Unregister(Type serviceType)
        {
            if (!serviceType.IsSubclassOf(typeof(TServiceBase)))
                return false;
            
            if (services.TryGetValue(serviceType, out TServiceBase service))
            {
                services.Remove(serviceType);
                servicesList.Remove(service);
                
                return true;
            }

            return false;
        }
        
        public bool Unregister<TService>()
            where TService : TServiceBase
        {
            if (services.TryGetValue(typeof(TService), out TServiceBase service))
            {
                services.Remove(typeof(TService));
                servicesList.Remove(service);
                
                return true;
            }

            return false;
        }
        
        public bool Unregister<TService>(out TService returnService)
            where TService : class, TServiceBase
        {
            if (services.TryGetValue(typeof(TService), out TServiceBase service))
            {
                services.Remove(typeof(TService));
                servicesList.Remove(service);
                
                returnService = service as TService;

                return true;
            }

            returnService = null;
            return false;
        }
    }
}