using System;
using System.Text.Json;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using Newtonsoft.Json;
using RabbitMQCommon.PubSub;
using RabbitMQCommon.PubSub.Impl;

namespace RabbitMQCommon
{
    public static class EnvironmentContainer
    {
        private static bool _isInited;
        private static object _syncObject = new object();

        public static IWindsorContainer Container;

        public static void InitContainer()
        {
            if (_isInited)
            {
                return;
            }

            lock (_syncObject)
            {
                if (_isInited)
                {
                    return;
                }
                
                
                Container ??= new WindsorContainer();
                Register();
            }

            JsonConvert.DefaultSettings = () => new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            _isInited = true;
        }

        private static void Register()
        {
            Container.RegisterTransient<IMessagePublishService, MessagePublishService>();
            Container.RegisterTransient<IMessageSubscriberService, MessageSubscriberService>();
        }
        
        public static TService Resolve<TService>()
        {
            return Container.Resolve<TService>();
        }
        
        public static void Release(object service)
        {
            Container.Release(service);
        }

        private static void RegisterTransient<TService, TImp>(this IWindsorContainer container)
            where TService : class
            where TImp : TService
        {
            container.Register(
                new ComponentRegistration<TService>()
                    .ImplementedBy<TImp>()
                    .LifestyleTransient());
        }
        
        private static void RegisterScoped<TService, TImp>(this IWindsorContainer container)
            where TService : class
            where TImp : TService
        {
            container.Register(
                new ComponentRegistration<TService>()
                    .ImplementedBy<TImp>()
                    .LifestyleScoped());
        }
        
        private static void RegisterSingleton<TService, TImp>(this IWindsorContainer container)
            where TService : class
            where TImp : TService
        {
            container.Register(
                new ComponentRegistration<TService>()
                    .ImplementedBy<TImp>()
                    .LifestyleSingleton());
        }
    }
}