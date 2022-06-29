using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks;
using KomunalkaUA.Domain.Services.CallbackServices.FlatCallbacks.Interfaces;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.Interfaces;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.OwnerCallback;
using KomunalkaUA.Domain.Services.CallbackServices.UserCallback.TenantCallback;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.Lists;

public class ListCallback
{
    private readonly List<ICallback> _callbacks;
    private ICallback _currentCallback;
    public ListCallback(
        IFlatDetailCallback flatDetailCallback,
        IFlatListCallback flatListCallback,
        IFlatEditCallback flatEditCallback,
        IFlatRentCallback flatRentCallback,
        IFlatRentAddCallback flatRentAddCallback,
        IFlatCardCallback flatCardCallback,
        ITenantRequestToOwnerAllowCallback tenantRequestToOwnerCallback,
        IOwnerChooseFlatToAutorizeCallback ownerChooseFlatToAutorizeCallback,
        ITenantRequestToOwnerDeniedCallback tenantRequestToOwnerDeniedCallback,
        ITenantStartCallback tenantStartCallback,
        ITenantRequestCardCallBack tenantRequestCardCallBack,
        ITenantRequestCardIfEmptyCallBack tenantRequestCardIfEmptyCallBack,
        IOwnerAddCardCallback ownerAddCardCallback,
        IFlatCityListCallback flatCityListCallback,
        IFlatGasMeterCallback flatGasMeterCallback,
        IFlatChooseProviderCallback flatChooseProviderCallback,
        IFlatAddressCallback flatAddressCallback,
        IFlatStreetCallback flatStreetCallback,
        IFlatStreetEditCallback flatStreetEditCallback,
        IFlatBuildingCallback flatBuildingCallback,
        IFlatBuildingEditCallback flatBuildingEditCallback,
        IFlatFlatNumberCallback flatFlatNumberCallback,
        IFlatFlatNumberEditCallback flatFlatNumberEditCallback,
        IFlatCityCallback flatCityCallback,
        IFlatMetersCallback flatMetersCallback,
        IFlatWatterMeterCallback flatWatterMeterCallback,
        IFlatElectricalMeterCallback flatElectricalMeterCallback,
        IFlatMeterEditCallback flatMeterEditCallback,
        IFlatMeterValueEditCallback flatMeterValueEditCallback,
        ITenantSendMetersCallback tenantSendMetersCallback,
        ITenantSendGasMeterCallback tenantSendGasMeterCallback,
        ITenantSendWatterMeterCallback tenantSendWatterMeterCallback,
        ITenantSendElectricalMeterCallback tenantSendElectricalMeterCallback,
        IOwnerAccessPreMetersCheckoutCallback ownerAccessPreMetersCheckoutCallback,
        IFlatListCheckoutsCallback flatListCheckoutsCallback,
        IFlatCheckoutCallback checkoutDetailCallback,
        IFlatCheckoutDetailCallback flatCheckoutDetailCallback,
        ITenantListCheckoutCallback tenantListCheckoutCallback,
        ITenantCheckoutCallback tenantCheckoutCallback,
        ITenantCheckoutDetailCallback tenantCheckoutDetailCallback,
        IFlatAddCardCallback flatAddCardCallback)
    {
        _callbacks = new List<ICallback>()
        {
            flatDetailCallback,
            flatListCallback,
            flatEditCallback,
            flatRentCallback,
            flatRentAddCallback,
            flatCardCallback,
            tenantRequestToOwnerCallback,
            ownerChooseFlatToAutorizeCallback,
            tenantRequestToOwnerDeniedCallback,
            tenantStartCallback,
            tenantRequestCardCallBack,
            tenantRequestCardIfEmptyCallBack,
            ownerAddCardCallback,
            flatCityListCallback,
            flatGasMeterCallback,
            flatChooseProviderCallback,
            flatAddressCallback,
            flatStreetCallback,
            flatStreetEditCallback,
            flatBuildingCallback,
            flatBuildingEditCallback,
            flatFlatNumberCallback,
            flatFlatNumberEditCallback,
            flatCityCallback,
            flatMetersCallback,
            flatWatterMeterCallback,
            flatElectricalMeterCallback,
            flatMeterEditCallback,
            flatMeterValueEditCallback,
            tenantSendMetersCallback,
            tenantSendGasMeterCallback,
            tenantSendWatterMeterCallback,
            tenantSendElectricalMeterCallback,
            ownerAccessPreMetersCheckoutCallback,
            flatListCheckoutsCallback,
            checkoutDetailCallback,
            flatCheckoutDetailCallback,
            tenantListCheckoutCallback,
            tenantCheckoutCallback,
            tenantCheckoutDetailCallback,
            flatAddCardCallback
        };
    }

    public async Task ExecuteAsync(CallbackQuery callbackQuery, ITelegramBotClient client)
    {
        await _currentCallback.ExecuteAsync(callbackQuery, client);
    }
    
    public bool Contains(string callbackData)
    {
        foreach (var callback in _callbacks)
        {
            try
            {
                if (callback.Contains(callbackData))
                {
                    _currentCallback = callback;
                    return true;
                }
            }
            catch (NotImplementedException e)
            {
                Console.WriteLine(e);
               
            }
        }
        return false;
    }
}