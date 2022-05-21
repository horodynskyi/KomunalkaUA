using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Specifications.FlatSpec;
using KomunalkaUA.Shared;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace KomunalkaUA.Domain.Services;

public class FlatService:IFlatService
{
    private readonly IRepository<State> _stateRepository;
    private readonly IRepository<Flat> _flatRepository;
    private readonly IRepository<FlatMeter> _flatMeterRepository;

    public FlatService(
        IRepository<State> stateRepository, 
        IRepository<Flat> flatRepository, 
        IRepository<FlatMeter> flatMeterRepository)
    {
        _stateRepository = stateRepository;
        _flatRepository = flatRepository;
        _flatMeterRepository = flatMeterRepository;
    }

    public async Task SetAddressAsync(ITelegramBotClient client, Update update, State state)
    {
        var addressMsg = update.Message.Text.Split();
        var text = $"Адрес записано\n{ReplyTextService.GetGasStateMessage()}";
        (string street, string building, string flatNumber) = (addressMsg[0], addressMsg[1], addressMsg[2]);
        var flat = JsonConvert.DeserializeObject<Flat>(state.Value);
        flat.Address = new()
        {
                Building = building,
                Street = street,
                FlatNumber = flatNumber
            };
        
        state.StateType = StateType.GasMeter;
        var entity = await _flatRepository.AddAsync(flat);
        state.Value = JsonConvert.SerializeObject(entity.Id);
        await _stateRepository.UpdateAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id,
            text);
    }
    public async Task SetGasMeterAsync(ITelegramBotClient client, Update update, State state)
    {
        
        var numberMsg = update.Message.Text.Split();
        var text = "Введіть номер рахунку і показник води формат:" +
                  "\nНомер рахунку показник";
        if (numberMsg.Length != 2)
        {
            await client.SendTextMessageAsync(
                update.Message.Chat.Id,
                $"Введено некоретно дані!\n{ReplyTextService.GetGasStateMessage()}");
            return;
        }
        (string number, string value) = (numberMsg[0],  numberMsg[1]);
        var flatId = JsonConvert.DeserializeObject<int>(state.Value);
        var meter = new Meter
        {
            Number = number,
            Value = Int32.Parse(value),
            MeterType = MeterType.Gas
        };
        await _flatMeterRepository.AddAsync(new FlatMeter
        {
            FlatId = flatId,
            Meter = meter
        });
        state.StateType = StateType.WatterMeter;
        await _stateRepository.UpdateAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id,
            text);
    }
    public async Task SetWatterMeterAsync(ITelegramBotClient client, Update update, State state)
    {
        var numberMsg = update.Message.Text.Split();
        var text = "Введіть номер рахунку і показник світла формат:" +
                   "\nНомер рахунку показник";
        if (numberMsg.Length != 2)
        {
            await client.SendTextMessageAsync(
                update.Message.Chat.Id,
                $"Введено некоретно дані!\n{ReplyTextService.GetWatterStateMessage()}");
            return;
        }
        (string number, string value) = (numberMsg[0],  numberMsg[1]);
        var flatId = JsonConvert.DeserializeObject<int>(state.Value);
        var meter = new Meter
        {
            Number = number,
            Value = Int32.Parse(value),
            MeterType = MeterType.Watter
        };
        await _flatMeterRepository.AddAsync(new FlatMeter
        {
            FlatId = flatId,
            Meter = meter
        });
        state.StateType = StateType.ElectricMeter;
        await _stateRepository.UpdateAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id,
            text);
    }
    public async Task SetElectricMeterAsync(ITelegramBotClient client, Update update, State state)
    {
        var numberMsg = update.Message.Text.Split();
        var text = "Показники збережено! Квартиру додано до вашого списку!";
        if (numberMsg.Length != 2)
        {
            await client.SendTextMessageAsync(
                update.Message.Chat.Id,
                $"Введено некоретно дані!\n{ReplyTextService.GetElectricStateMessage()}");
            return;
        }
        (string number, string value) = (numberMsg[0],  numberMsg[1]);
        var flatId = JsonConvert.DeserializeObject<int>(state.Value);
        var meter = new Meter
        {
            Number = number,
            Value = Int32.Parse(value),
            MeterType = MeterType.Electric
        };
        await _flatMeterRepository.AddAsync(new FlatMeter
        {
            FlatId = flatId,
            Meter = meter
        });
        state.StateType = StateType.None;
        await _stateRepository.DeleteAsync(state);
        await client.SendTextMessageAsync(
            update.Message.Chat.Id,
            text,
            replyMarkup:KeyboardService.GetStartOwnerButtons());
        
    }

    private async Task FlatDetailAsync(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        var msg = callbackQuery.Data.Split();
        if (msg.Length !=2) 
            return;
        Int32.TryParse(msg[1],out var flatId);
        var flat = await _flatRepository.GetBySpecAsync(new FlatGetByIdIncludeAddressSpec(flatId));
        var text = $"Квартира:{flat.Address.Street}\nЩо ви хочете зробити з квартирою?";
        await client.EditMessageTextAsync(
            callbackQuery.From.Id,
            callbackQuery.Message.MessageId,
            text:text,
            replyMarkup:KeyboardService.CreateFlatDetailInlineKeyboardMarkup(flat)
        );
    }

    public async Task ProccessCallbackAsync(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        switch (callbackQuery.Data.Split())
        {
            case { } a when a.Contains("flat-detail"):
                await FlatDetailAsync(client,callbackQuery);
                await client.AnswerCallbackQueryAsync(callbackQuery.Id);
                break;
            case { } a when a.Contains("flat-list"):
                await FlatListAsync(client,callbackQuery);
                await client.AnswerCallbackQueryAsync(callbackQuery.Id);
                break;
            case { } a when a.Contains("flat-edit"):
                await FlatEditAsync(client,callbackQuery);
                await client.AnswerCallbackQueryAsync(callbackQuery.Id);
                break;
        }
    }

    private async Task FlatEditAsync(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
        
    }

    private async Task FlatListAsync(ITelegramBotClient client, CallbackQuery callbackQuery)
    {
       var text = $"Квартири {callbackQuery.From.Username}:";
       var flats = await _flatRepository.ListAsync(new FlatGetByOwnerIdIncludeAddressSpec(callbackQuery.From.Id));
       var replyMarkup = KeyboardService.CreateListFlatInlineKeyboardMarkup(flats);
       await client.EditMessageTextAsync(
           callbackQuery.From.Id,
           callbackQuery.Message.MessageId,
           text,
           replyMarkup: replyMarkup);
    }
}