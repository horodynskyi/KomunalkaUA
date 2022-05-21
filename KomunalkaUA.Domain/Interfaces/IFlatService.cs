using KomunalkaUA.Domain.Models;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Interfaces;

public interface IFlatService
{
    Task SetAddressAsync(ITelegramBotClient client, Update update, State state);
    Task SetWatterMeterAsync(ITelegramBotClient client, Update update, State state);
    Task SetGasMeterAsync(ITelegramBotClient client, Update update, State state);
    Task SetElectricMeterAsync(ITelegramBotClient client, Update update, State state);
    Task ProccessCallbackAsync(ITelegramBotClient client, CallbackQuery callbackQuery);
}