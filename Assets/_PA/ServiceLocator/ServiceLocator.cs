using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    [DefaultExecutionOrder(-100000)]
    public class ServiceLocator : MonoBehaviour
    {
        public static ServiceLocator Instance { get; private set;} = null;
        readonly Dictionary<Type, object> Services = new Dictionary<Type, object>();

        void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError($"Found duplicate {nameof(ServiceLocator)} on {gameObject.name}");
                Destroy(gameObject);
                Debug.Break();
                return;
            }

            Instance = this;
        }
        public void Register<TService>(TService service, bool safe = true) /*where TService : class, new()*/
        {
            if (IsRegistered(service) && safe)
            {
                throw new ServiceLocatorException($"{service.GetType().Name} has been already registered.");
            }
            Services[service.GetType()] = service;
        }

        public TService Get<TService>() where TService : class, new()
        {
            var serviceType = typeof(TService);
            if (IsRegistered<TService>())
            {
                return (TService)Services[serviceType];
            }

            throw new ServiceLocatorException($"{serviceType.Name} hasn't been registered.");
        }

        bool IsRegistered(object o)
        {
            return Services.ContainsKey(o.GetType());
        }

        bool IsRegistered<TService>()
        {
            return Services.ContainsKey(typeof(TService));
        }
    }

    public class ServiceLocatorException : Exception
    {
        public ServiceLocatorException(string message) : base(message) { }
    }

}
