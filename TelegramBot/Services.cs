using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot
{
    public static class Services
    {
        public static InlineKeyboardMarkup buttons()
        {

            InlineKeyboardMarkup inlineKeyboard = new(new[]
            {
                // first row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "С", callbackData: "C"),
                    InlineKeyboardButton.WithCallbackData(text: "<", callbackData: "<"),
                    InlineKeyboardButton.WithCallbackData(text: "%", callbackData: "%"),
                    InlineKeyboardButton.WithCallbackData(text: "/", callbackData: "/"),
                },
                // second row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "7", callbackData: "7"),
                    InlineKeyboardButton.WithCallbackData(text: "8", callbackData: "8"),
                    InlineKeyboardButton.WithCallbackData(text: "9", callbackData: "9"),
                    InlineKeyboardButton.WithCallbackData(text: "*", callbackData: "*"),
                },
                // third row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "4", callbackData: "4"),
                    InlineKeyboardButton.WithCallbackData(text: "5", callbackData: "5"),
                    InlineKeyboardButton.WithCallbackData(text: "6", callbackData: "6"),
                    InlineKeyboardButton.WithCallbackData(text: "-", callbackData: "-"),
                },
                // fourth row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "1", callbackData: "1"),
                    InlineKeyboardButton.WithCallbackData(text: "2", callbackData: "2"),
                    InlineKeyboardButton.WithCallbackData(text: "3", callbackData: "3"),
                    InlineKeyboardButton.WithCallbackData(text: "+", callbackData: "+"),
                },
                // fifth row
                new []
                {
                    InlineKeyboardButton.WithCallbackData(text: "_", callbackData: "_"),
                    InlineKeyboardButton.WithCallbackData(text: "0", callbackData: "0"),
                    InlineKeyboardButton.WithCallbackData(text: ".", callbackData: "."),
                    InlineKeyboardButton.WithCallbackData(text: "=", callbackData: "="),
                },

            }
            );
            return inlineKeyboard;
        }
    }
}
