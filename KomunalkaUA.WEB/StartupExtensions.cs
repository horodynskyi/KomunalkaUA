﻿using Newtonsoft.Json;
using Telegram.Bot;

namespace KomunalkaUA.WEB;

public static class StartupExtensions
{
    public static IApplicationBuilder UseTelegramBotWebhook(this IApplicationBuilder applicationBuilder)
    {
        var services = applicationBuilder.ApplicationServices;
        var lifetime = services.GetRequiredService<IHostApplicationLifetime>();
        lifetime.ApplicationStarted.Register(
            () =>
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                var address = services.GetRequiredService<AppSettings>().WebHookAddress;

                async Task ResetWebHook()
                {
                    logger.LogInformation("Removing webhook");
                    await services.GetRequiredService<ITelegramBotClient>().DeleteWebhookAsync();
                    //await services.GetRequiredService<ITelegramBotClient>().SendInv
                    logger.LogInformation($"Setting webhook to {address}");
                    await services.GetRequiredService<ITelegramBotClient>()
                        .SetWebhookAsync(address);
                    logger.LogInformation($"Webhook is set to {address}");

                    var webhookInfo = await services.GetRequiredService<ITelegramBotClient>().GetWebhookInfoAsync();
                    logger.LogInformation($"Webhook info: {JsonConvert.SerializeObject(webhookInfo)}");
                }

                _ = ResetWebHook();
            });

        lifetime.ApplicationStopping.Register(
            () =>
            {
                var logger = services.GetService<ILogger<Program>>();

                services.GetRequiredService<ITelegramBotClient>().DeleteWebhookAsync().Wait();
                logger.LogInformation("Webhook removed");
            });
        return applicationBuilder;
    }
}