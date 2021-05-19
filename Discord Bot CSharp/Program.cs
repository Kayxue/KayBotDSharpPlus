using System;
using Microsoft.Extensions.Logging;
using DSharpPlus;
using DSharpPlus.EventArgs;
using DSharpPlus.CommandsNext;
using Discord_Bot_CSharp.Commands;
using System.Threading.Tasks;
using DSharpPlus.Lavalink;
using Microsoft.Extensions.Logging.Console;

namespace Discord_Bot_CSharp
{
    public class Program
    {
        private static DiscordClient? BotClient { get; set; }
        private static DiscordIntents? intents { get; } = DiscordIntents.All;
        private static CommandsNextExtension? botCommand { get; set; }

        private static void Main(string[] args)
        {
            new Program().RunBotAsync().GetAwaiter().GetResult();
        }

        private async Task RunBotAsync()
        {
            DiscordConfiguration configuration = new DiscordConfiguration
            {
                Token = Config.TOKEN,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug,
                Intents = intents
            };

            CommandsNextConfiguration commandsNextConfiguration = new CommandsNextConfiguration
            {
                CaseSensitive = true,
                StringPrefixes = new[] { "drmc!" },
                EnableMentionPrefix = true,
                EnableDefaultHelp = false
            };

            LavalinkConfiguration lavalinkConfiguration = new LavalinkConfiguration()
            {
                ResumeTimeout = 5
            };

            BotClient = new DiscordClient(configuration);
            BotClient.Ready += BotClient_On_Ready;

            botCommand = BotClient.UseCommandsNext(commandsNextConfiguration);
            botCommand.RegisterCommands<Info>();
            botCommand.RegisterCommands<Moderation>();
            botCommand.CommandExecuted += commandExecuted;
            botCommand.CommandErrored += commandError;
            await BotClient.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task BotClient_On_Ready(DiscordClient discordClient, ReadyEventArgs readyEventArgs)
        {
            discordClient.Logger.LogInformation(
                "機器人已上線",
                DateTime.Now
            );

            return Task.CompletedTask;
        }

        private static Task commandExecuted(CommandsNextExtension extension, CommandExecutionEventArgs args)
        {
            extension.Client.Logger.LogInformation(
                $"{args.Context.User.Username}使用了指令{args.Command.QualifiedName}",
                DateTime.Now
            );
            return Task.CompletedTask;
        }

        private static Task commandError(CommandsNextExtension extension, CommandErrorEventArgs args)
        {
            try
            {
                extension.Client.Logger.LogError(
                    $"指令{args.Command.Name}發生錯誤！錯誤原因：{args.Exception.Message}",
                    DateTime.Now
                );
            }
            catch (Exception e)
            {
                extension.Client.Logger.LogError(
                    $"發生錯誤！錯誤原因：{args.Exception.Message}",
                    DateTime.Now
                );
            }

            return Task.CompletedTask;
        }
    }
}