using System;
using System.Collections.Generic;
using UnityEngine;

    [DefaultExecutionOrder(-100000)]
    public class ServiceLocator : MonoBehaviour
    { 
        static ServiceLocator _instance { get; set;} = null;
        readonly Dictionary<Type, object> SingletonServices = new Dictionary<Type, object>();
        private readonly Dictionary<SerLocID, object> Services = new Dictionary<SerLocID, object>();

        void Awake()
        {
            if (_instance != null)
            {
                Debug.LogError($"Found duplicate {nameof(ServiceLocator)} on {gameObject.name}");
                Destroy(gameObject);
                Debug.Break();
                return;
            }

            _instance = this;
        }
        public static void Register<TService>(TService service) where TService : class, new()
        {
            if (_instance.SingletonServices.TryGetValue(service.GetType(), out object srv))
            {
                if (IsNullOrDestroyed(srv))
                {
                    _instance.SingletonServices[service.GetType()] = service;
                }
                else
                {
                    Debug.Break();
                    throw new ServiceLocatorException($"{service.GetType()} has already registered as singleton. " +
                                                      $"Multiple registration of a singleton object is not allowed");
                }
            }
            else
            {
                _instance.SingletonServices[service.GetType()] = service;
            }
        }
        
        public static void Register<TService>(TService service, SerLocID id) where TService : Component
        {
            if (_instance.Services.ContainsKey(id))
            {
                if (IsNullOrDestroyed(_instance.Services[id]))
                {
                    _instance.Services[id] = service;
                }
                else
                {
                    Debug.Break();
                    throw new ServiceLocatorException(
                        $"You are trying to use same id for different objects. The id is <color=red>\"{id}\"</color>. The object you assigned with this id is <color=red>\"{(TService)_instance.Services[id]}\"</color> <color=yellow> You are trying to assign another object with this id. This is forbidden.</color> <color=green> Click to the error message see other object you are trying to assign.</color>");
                }
            }
            else
            {
                _instance.Services[id] = service;
            }
        }
        
        static bool IsNullOrDestroyed(System.Object obj) 
        {
            if (ReferenceEquals(obj, null)) 
                return true;
            
            if (obj is UnityEngine.Object) 
                return (obj as UnityEngine.Object) == null;
            
            return false;
        }

        public static TService Get<TService>() where TService : class, new()
        {
            if (_instance.SingletonServices.TryGetValue(typeof(TService), out object srv))
            {
                return (TService)srv;
            }
            Debug.Break();
            throw new ServiceLocatorException($"{typeof(TService)} hasn't been registered.");
        }
        
        public static TService Get<TService>(SerLocID id) where TService : Component
        {
            if (_instance.Services.TryGetValue(id, out object srv))
            {
                return (TService)srv;
            }
            Debug.Break();
            throw new ServiceLocatorException($"An Instance of {typeof(TService)} with the <color=red>\"{id}\"</color> id hasn't been registered.");
        }
    }

    public class ServiceLocatorException : Exception
    {
        public ServiceLocatorException(string message) : base(message) { }
    }
