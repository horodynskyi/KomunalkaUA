using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatMeterSpec;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Domain.Specifications.ProviderSpec;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatWatterMeterCallback:IFlatWatterMeterCallback
{
    private readonly string _callback = "flat-watter-meter";
    private int _flatId;
    private readonly IRepository<Provider> _providerRepository;
    private readonly IRepository<Flat> _flatRepository;
    private readonly IKeyboardService _keyboardService;
    private readonly IRepository<FlatMeter> _flatMeterRepository;

    public FlatWatterMeterCallback(
        IRepository<Provider> providerRepository,
        IKeyboardService keyboardService, 
        IRepository<Flat> flatRepository, 
        IRepository<FlatMeter> flatMeterRepository)
    {
        _providerRepository = providerRepository;
        _keyboardService = keyboardService;
        _flatRepository = flatRepository;
        _flatMeterRepository = flatMeterRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var meters = await _flatMeterRepository.ListAsync(new FlatMeterGetByFlatIdIncludeMeterProviderSpec(_flatId));
        if (meters.Any(x => x.Meter.Provider.MeterTypeId == (int?) MeterTypeEnum.Watter && x.FlatId==_flatId))
        {
            var meter = meters.FirstOrDefault(x => x.Meter.Provider.MeterTypeId == (int?) MeterTypeEnum.Watter && x.FlatId==_flatId);
            await client.TryEditMessageTextAsync(
                callbackQuery.From.Id,
                callbackQuery.Message.MessageId,
                "<b>Вода</b>:" +
                $"\n<b>Постачальник</b>:<code> {meter.Meter.Provider.Name}</code>" +
                $"\n<b>Тариф:</b><code> {meter.Meter.Provider.Rate} грн</code>" +
                $"\n<b>Номер рахунку:</b><code> {meter.Meter.Number}</code>" +
                $"\n<b>Показник:</b>:<code> {meter.Meter.Value??0} m³</code>",
                callbackQuery.Message,
                ParseMode.Html,
                replyMarkup:(InlineKeyboardMarkup)_keyboardService.GetKeys(new MeterInfoKeyboardCommand((int) meter.MetterId,_flatId))
            );
            return;
        }
        var flat =await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressSpec(_flatId));
        var providers = await _providerRepository.ListAsync(new ProviderGetByCityIdAndMeterTypeIdSpec((int)flat.Address.CityId,(int)MeterTypeEnum.Watter));
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            "Виберіть зі списку постачальника води:",
            replyMarkup:(InlineKeyboardMarkup)_keyboardService.GetKeys(new ProviderListKeyboardCommand(providers,_flatId))
        );
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (msg[0]==_callback)
        {
            _flatId = Int32.Parse(msg[1]);
            return true;
        }

        return false;
    }
}