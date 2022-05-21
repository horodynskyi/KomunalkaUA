using KomunalkaUA.Domain.Commands;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KomunalkaUA.Domain;

public static class DependencyInjection
{
    public static IServiceCollection AddDomain(this IServiceCollection service)
    {

        return service
            .AddTransient<IListCommand, ListCommand>()
            .AddTransient<IStateService, StateService>()
            .AddTransient<IUserService, UserService>()
            .AddTransient<IFlatService, FlatService>()
            .AddTransient<ICallBackService,CallBackService>();

    }
}