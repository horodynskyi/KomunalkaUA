using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;

public class TenantMetersKeyboardCommand:IKeyboardCommand
{
    private readonly long _tenantId;
    private readonly List<PreMeterCheckout> _preMeterCheckouts;
    private readonly Dictionary<int, InlineKeyboardButton> _inlineKeyboardButtons;

    public TenantMetersKeyboardCommand(long tenantId, List<PreMeterCheckout> preMeterCheckouts)
    {
        _tenantId = tenantId;
        _inlineKeyboardButtons = new Dictionary<int, InlineKeyboardButton>();
        _inlineKeyboardButtons.Add((int) MeterTypeEnum.Gas,InlineKeyboardButton.WithCallbackData($"Газ", $"tenant-send-gas-meter {_tenantId}"));
        _inlineKeyboardButtons.Add((int) MeterTypeEnum.Watter,InlineKeyboardButton.WithCallbackData($"Вода", $"tenant-send-watter-meter {_tenantId}"));
        _inlineKeyboardButtons.Add((int) MeterTypeEnum.Electric,InlineKeyboardButton.WithCallbackData($"Світло", $"tenant-send-electrical-meter {_tenantId}"));
        _preMeterCheckouts = preMeterCheckouts;
    }

    public IReplyMarkup Get()
    {
        List<InlineKeyboardButton> buttons = new ();
        if (!_preMeterCheckouts.Any())
        {
            return new InlineKeyboardMarkup(new[]
            {
                 _inlineKeyboardButtons.Select(d => d.Value)
            });
        }
        var keys = new List<InlineKeyboardButton>();
     
        if (!_preMeterCheckouts.Exists(x => x.Meter.Provider.MeterTypeId == (int) MeterTypeEnum.Gas))
            keys.Add(_inlineKeyboardButtons.FirstOrDefault(x => x.Key==(int) MeterTypeEnum.Gas).Value);
        if (!_preMeterCheckouts.Exists(x => x.Meter.Provider.MeterTypeId == (int) MeterTypeEnum.Watter))
            keys.Add(_inlineKeyboardButtons.FirstOrDefault(x => x.Key==(int) MeterTypeEnum.Watter).Value);
        if (!_preMeterCheckouts.Exists(x => x.Meter.Provider.MeterTypeId == (int) MeterTypeEnum.Electric))
            keys.Add(_inlineKeyboardButtons.FirstOrDefault(x => x.Key==(int) MeterTypeEnum.Electric).Value);
        return new InlineKeyboardMarkup(new[]
        {
            keys
        });

    }
}