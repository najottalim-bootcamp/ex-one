using System.Runtime.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot;

var botClient = new TelegramBotClient("6023621119:AAHQMzlIsp63leprBkqEqZpy2RSpfChzvEc");

using CancellationTokenSource cts = new();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
ReceiverOptions receiverOptions = new()
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
};

botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    if(update.CallbackQuery != null)
    {
        CallbackQuery callbackQuery = update.CallbackQuery;
        string data = callbackQuery.Data;
        User user = callbackQuery.From;
        if(callbackQuery.Message.Text == "Start")
        {
            callbackQuery.Message.Text = "";
        }
        
       Message sentMessage1 = await botClient.EditMessageTextAsync(
        messageId: callbackQuery.Message.MessageId,
        chatId: user.Id,
        text: callbackQuery.Message.Text+data,
        replyMarkup : Services.buttons(),
        cancellationToken: cancellationToken);

        


    }
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");

 
    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "Start",
        replyMarkup: Services.buttons(),
        cancellationToken: cancellationToken);
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}