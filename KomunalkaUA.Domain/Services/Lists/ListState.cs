using KomunalkaUA.Domain.Enums;
using KomunalkaUA.Domain.Models;
using KomunalkaUA.Domain.Services.StateServices;
using KomunalkaUA.Domain.Services.StateServices.FlatState;
using KomunalkaUA.Domain.Services.StateServices.FlatState.Interfaces;
using KomunalkaUA.Domain.Services.StateServices.UserState;
using KomunalkaUA.Domain.Services.StateServices.UserState.Interfaces;
using KomunalkaUA.Domain.Services.StateServices.UserState.OwnerState.Interfaces;
using KomunalkaUA.Domain.Services.StateServices.UserState.TenantState;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace KomunalkaUA.Domain.Services.Lists;

public class ListState
{
    private readonly List<IState> _states;
    private IState _currentState;
    public ListState(
        IUserRegistrationState userRegistrationState,
        IUserChooseRoleState chooseRoleState,
        IUserAddPhoneNumberState userAddPhoneNumberState,
        IFlatSetAddressState flatSetAddressState,
        IFlatSetWatterState flatSetWatterState,
        IFlatSetElectricMeterState flatSetElectricMeterState,
        IFlatSetMeterState flatSetGasMeterState,
        ITenantAutorizeFlatState tenantAutorizeFlatState,
        IOwnerAddCardState ownerAddCardState,
        IFlatRentState flatRentState,
        IFlatSetStreetState flatSetStreetState,
        IFlatSetBuildingState flatSetBuildingState,
        IFlatSetFlatNumberState flatSetFlatNumberState,
        IFlatValueMeterState flatValueMeterState
    ) 
    {
        _states = new List<IState>()
        {
            userRegistrationState,
            chooseRoleState,
            userAddPhoneNumberState,
            flatSetAddressState,
            flatSetWatterState,
            flatSetElectricMeterState,
            flatSetGasMeterState,
            tenantAutorizeFlatState,
            ownerAddCardState,
            flatRentState,
            flatSetStreetState,
            flatSetBuildingState,
            flatSetFlatNumberState,
            flatValueMeterState
        };
    }

    public async Task ExecuteAsync(Update update, ITelegramBotClient client,State state)
    {
        await _currentState.ExecuteAsync( client, update, state);
    }

    public bool Contains(StateType stateType)
    {
        foreach (var state in _states)
        {
            if (state.Contains(stateType))
            {
                _currentState = state;
                return true;
            }
        }
        return false;
    }
}