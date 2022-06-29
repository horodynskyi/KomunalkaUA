using System.Reflection;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.CallbackServices;
using KomunalkaUA.Domain.Services.CommandService;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.Lists;
using KomunalkaUA.Domain.Services.StateServices;
using Microsoft.Extensions.DependencyInjection;


namespace KomunalkaUA.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection service)
    {

        service.AddTransient<IListCommand, ListCommand>();
        service.AddTransient<IStateService, StateService>();
        service.AddTransient<ICallBackService, CallBackService>();
        service.AddTransient<IKeyboardService, KeyboardService>();
        service.AddTransient<ICommandService, CommandService>();
        service.AddTransient<ListCallback>();
        service.AddTransient<ListState>();
        service
            .AddState()
            .AddCallback()
            .AddCommand();
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
                    {
                        service.AddTransient(i,c);
                    }
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
           i ==typeof(ITelegramCommand))).ToList();
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