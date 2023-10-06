using System.Runtime.CompilerServices;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot;

var botClient = new TelegramBotClient("6347696138:AAH-V2cP_VAsUNzrLD-4Mn2aYf_dK9g5Xw0");

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
        if(callbackQuery.Message.Text == "Start" || callbackQuery.Message.Text == "divide zero error")
        {
            callbackQuery.Message.Text = "";
        }
        string s = callbackQuery.Message.Text;
        if (Services.Opers.Contains(data))
        {
            if (Services.oper !=null && s.FirstOrDefault(Services.oper[0]) == null) 
            {
                Services.oper = null;
            }

            if(Services.oper == null)
            {
                Services.oper = data;
                s += data;
            }
           
        }


        if(Services.Numbers.Contains(data))
        {
            s += data;
        }
        if (data.Equals("C"))
        {
            s = "Start";

        }
        if(data .Equals("<")) 
        {
            s =  s.Remove(s.Length - 1);
        }
        else if (data.Equals("="))
        {
            s = s.Trim();
            
          try
            {
                if(Services.oper != null && s.IndexOf(Services.oper)!= s.Length-1) 
                {

                    var s1 = s.Split(Services.oper[0]).ToList();
                    var first = Convert.ToDouble(s1[0]);
                    var second = Convert.ToDouble(s1[1]);
                    if(Services.oper == "-") 
                    {
                        s = Services.Sub(first, second).ToString();
                    }
                    else if (Services.oper == "+")
                    {
                        s = Services.Add(first, second).ToString();
                    }
                    else if (Services.oper == "*")
                    {
                        s = Services.Mul(first, second).ToString();
                    }
                    else if (Services.oper == "/")
                    {
                        if(second != 0)
                        {
                            s =  Services.Div(first, second).ToString();
                        }
                        else
                        {
                            s = "divide zero error";
                        }
                    }
                    Services.oper = null;
                }
            }
            catch
            {
               
            }
            



        }
        else if(data .Equals ("%")) 
        {
            var s1 = s.Split(Services.oper[0]).ToList();
            var first = Convert.ToDouble(s1[0]);
            var second = Convert.ToDouble(s1[1]);
            second %= 100;
            s = first.ToString() + Services.oper+second.ToString();
        }




       Message sentMessage1 = await botClient.EditMessageTextAsync(
        messageId: callbackQuery.Message.MessageId,
        chatId: user.Id,
        text: s,
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