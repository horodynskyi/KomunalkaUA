using KomunalkaUA.Shared;
using Microsoft.Extensions.DependencyInjection;

namespace KomunalkaUA.Infrastracture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastracture(this IServiceCollection service)
    {
        service.AddTransient(typeof(IRepository<>), typeof(EfRepository<>));
        return service;
    }
}