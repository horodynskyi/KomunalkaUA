using System.Reflection;
using KomunalkaUA.Domain.Commands;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services;
using KomunalkaUA.Domain.Services.Callback.FlatCallbacks;
using KomunalkaUA.Domain.Services.Callback.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.Lists;
using KomunalkaUA.Domain.Services.StateServices;
using KomunalkaUA.Domain.Services.StateServices.FlatState;
using KomunalkaUA.Domain.Services.StateServices.FlatState.Interfaces;
using KomunalkaUA.Domain.Services.StateServices.PhotoState;
using KomunalkaUA.Domain.Services.StateServices.PhotoState.Interfaces;
using KomunalkaUA.Domain.Services.StateServices.UserState;
using KomunalkaUA.Domain.Services.StateServices.UserState.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace KomunalkaUA.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection service)
    {

        service.AddTransient<IListCommand, ListCommand>();
        service.AddTransient<IStateService, StateService>();
        service.AddTransient<ICallBackService, CallBackService>();
        
        service.AddTransient<ListCallbackServices>();
        service.AddTransient<ListCallbackServices>();
        service.AddTransient<ListFlatCallback>();
        service.AddTransient<ListState>();
        service.AddTransient<IFlatCallBackService, FlatCallbackService>();
        service.AddState();
        service.AddCallback();
     
        

        return service;
    }

    public static IServiceCollection AddState(this IServiceCollection service)
    {
        var typesWithState  = Assembly
            .GetAssembly(typeof(DependencyInjection))
            ?.GetTypes().Where(t => 
                t.GetInterfaces()
                    .Any(i => 
           i ==typeof(IState))).ToList();
        if (typesWithState != null)
        {
            var interfaces = typesWithState.Where(x => x.IsInterface).ToList();
            var classTypesWithState = typesWithState.Where(x => x.IsClass).ToList();
            foreach (var i in interfaces)
            {
                foreach (var c in classTypesWithState)
                {
                    if (i.IsAssignableFrom(c))
                        service.AddTransient(i,c);
                }
            }
        }

        return service;
    }   
    public static IServiceCollection AddCommand(this IServiceCollection service)
    {
        var typesWithState  = Assembly
            .GetAssembly(typeof(DependencyInjection))
            ?.GetTypes().Where(t => 
                t.GetInterfaces()
                    .Any(i => 
           i ==typeof(IState))).ToList();
        if (typesWithState != null)
        {
            var interfaces = typesWithState.Where(x => x.IsInterface).ToList();
            var classTypesWithState = typesWithState.Where(x => x.IsClass).ToList();
            foreach (var i in interfaces)
            {
                foreach (var c in classTypesWithState)
                {
                    if (i.IsAssignableFrom(c))
                        service.AddTransient(i,c);
                }
            }
        }

        return service;
    }  
    public static IServiceCollection AddCallback(this IServiceCollection service)
    {
        var typesWithState  = Assembly
            .GetAssembly(typeof(DependencyInjection))
            ?.GetExportedTypes().Where(t => 
                t.GetInterfaces()
                    .Any(i => 
           i ==typeof(ICallback))).ToList();
        if (typesWithState != null)
        {
            var interfaces = typesWithState.Where(x => x.IsInterface).ToList();
            var classTypesWithState = typesWithState.Where(x => x.IsClass).ToList();
            foreach (var i in interfaces)
            {
                foreach (var c in classTypesWithState)
                {
                    if (i.IsAssignableFrom(c))
                        service.AddTransient(i,c);
                }
            }
        }
        return service;
    }
}