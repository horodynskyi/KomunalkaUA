using System.Globalization;
using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Extensions;
using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.KeyboardServices;
using KomunalkaUA.Domain.Services.KeyboardServices.KeyboardCommands;
using KomunalkaUA.Domain.Specifications.FlatMeterSpec;
using KomunalkaUA.Domain.Specifications.PreMeterCheckoutSpec;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services.StateServices.UserState.TenantState;

public class TenantSetPreMeterCheckoutState:ITenantSetPreMeterCheckoutState
{
    private readonly IRepository<State> _stateRepository;
    private readonly IRepository<PreMeterCheckout> _preMeterCheckoutRepository;
    private readonly IKeyboardService _keyboardService;
    private readonly IRepository<FlatMeter> _meterRepository;
    private readonly IRepository<Flat> _flatRepository;
    private readonly IRepository<User> _userRepository;

    public TenantSetPreMeterCheckoutState(
        IRepository<State> stateRepository, 
        IRepository<PreMeterCheckout> preMeterCheckoutRepository, 
        IKeyboardService keyboardService, 
        IRepository<FlatMeter> meterRepository, 
        IRepository<Flat> flatRepository, 
        IRepository<User> userRepository)
    {
        _stateRepository = stateRepository;
        _preMeterCheckoutRepository = preMeterCheckoutRepository;
        _keyboardService = keyboardService;
        _meterRepository = meterRepository;
        _flatRepository = flatRepository;
        _userRepository = userRepository;
    }

    public async Task ExecuteAsync(ITelegramBotClient client, Update update, State state)
    {
        var preMeterCheckout = JsonConvert.DeserializeObject<PreMeterCheckout>(state.Value);
        preMeterCheckout.EndValue = Int32.Parse(update.Message.Text);
        await _preMeterCheckoutRepository.AddAsync(preMeterCheckout);
        var preMeterCheckouts =
            await _preMeterCheckoutRepository.ListAsync(
                new PreMeterCheckoutGetByTenantIdIncludeMeterIsNotApproved((long) preMeterCheckout.TenantId));
        if (preMeterCheckouts.Count == 3)
        {
            await client.SendTextMessageAsync(
                update.Message.From.Id,
                $"Показники відправлені власнику:",
                replyMarkup: _keyboardService.GetKeys(new TenantCardKeyboardCommand(preMeterCheckout.TenantId))
            );
            var meters =
                await _meterRepository.ListAsync(
                    new FlatMeterGetByFlatIdIncludeMeterProviderSpec((int) preMeterCheckout.FlatId));
            var gasMeter = meters.FirstOrDefault(x => x.Meter.Provider.MeterTypeId == (int) MeterTypeEnum.Gas).Meter;
            var gasPreCheckout = preMeterCheckouts.FirstOrDefault(x => x.MeterId == gasMeter.Id);
           
            var (gasPayment,gasSum) = gasMeter.GetPaymentString(gasPreCheckout);
            
            var watterMeter = meters.FirstOrDefault(x => x.Meter.Provider.MeterTypeId == (int) MeterTypeEnum.Watter).Meter;
            var watterPreCheckout = preMeterCheckouts.FirstOrDefault(x => x.MeterId == watterMeter.Id);
            var (watterPayment,watterSum) = watterMeter.GetPaymentString(watterPreCheckout);
            
            var elecMeter = meters.FirstOrDefault(x => x.Meter.Provider.MeterTypeId == (int) MeterTypeEnum.Electric).Meter;
            var elecPreCheckout = preMeterCheckouts.FirstOrDefault(x => x.MeterId == elecMeter.Id);
            var (elecPayment,elecSum) = elecMeter.GetPaymentString(elecPreCheckout);
            var tenant = await _userRepository.GetByIdAsync(update.Message.From.Id);
            var flat = await _flatRepository.GetByIdAsync(meters.FirstOrDefault().FlatId);
            await client.SendTextMessageAsync(
                flat.OwnerId,
                $"\nОрендар, {tenant.FirstName} {tenant.SecondName} відправив показники:" +
                $"\n{gasPayment}" +
                $"\n{watterPayment}" +
                $"\n{elecPayment}" +
                $"\n<b>До сплати</b> <code>{gasSum+watterSum+elecSum} грн.</code>",
                ParseMode.Html,
                replyMarkup:_keyboardService.GetKeys(new FlatApprovePreMeterCheckoutsKeyboardCommand(tenant.Id))
            );
        }
        else
        {
            await client.SendTextMessageAsync(
                update.Message.From.Id,
                $"Показник збережено!",
                replyMarkup: _keyboardService.GetKeys(new TenantMetersKeyboardCommand((long) preMeterCheckout.TenantId,preMeterCheckouts))
            );
        }
       
        await _stateRepository.DeleteAsync(state);
    }

    public bool Contains(StateType stateType)
    {
        if (stateType == StateType.PreMeterCheckout)
            return true;
        return false;
    }
}