﻿using KomunalkaUA.Domain.Interfaces;
using KomunalkaUA.Domain.Services.CommandService.Interfaces;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using User = KomunalkaUA.Domain.Models.User;

namespace KomunalkaUA.Domain.Services.CommandService;

public class TenantCommand:ITenantCommand
{
    private readonly string _name = "Орендувальник";
    private readonly IRepository<User> _userRepository;

    public TenantCommand(
        IRepository<User> flatRepository)
    {
        _userRepository = flatRepository;
    }

    public async Task Execute(Message message, ITelegramBotClient client)
    {
        
    }
    
    public bool Contains(Message message)
    {
        if (message.Type != MessageType.Text)
            return false;
        return message.Text.Contains(_name);
    }
}