using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Shared;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;

public class FlatCityCallback:IFlatCityCallback
{
    private int _flatId;
    private readonly string _callback = "flat-city";
    private readonly IRepository<Flat> _flatRepository;
    private readonly IRepository<City> _cityRepository;
    private readonly IKeyboardService _keyboardService;

    public FlatCityCallback(
        IRepository<Flat> flatRepository,
        IKeyboardService keyboardService, 
        IRepository<City> cityRepository)
    {
        _flatRepository = flatRepository;
        _keyboardService = keyboardService;
        _cityRepository = cityRepository;
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        var cities = await _cityRepository.ListAsync();
        await client.TryEditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            "Виберіть місто зі списку:",
            callbackQuery.Message,
            replyMarkup: (InlineKeyboardMarkup) _keyboardService.GetKeys(new CityListKeyboardCommand(cities,_flatId))
        );
    }

    public bool Contains(string callbackData)
    {
        var msg = callbackData.Split();
        if (msg.Length !=2) 
            return false;
        if (callbackData.Split()[0]==_callback)
        {
            _flatId = Int32.Parse(msg[1]);
            return true;
        }

        return false;
    }
}