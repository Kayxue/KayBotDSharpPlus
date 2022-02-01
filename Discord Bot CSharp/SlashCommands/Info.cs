using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;

namespace Discord_Bot_CSharp.SlashCommands
{
    public class Info : ApplicationCommandModule
    {
        [SlashCommand("ping", "Check ping of the bot")]
        public async Task ping(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource,
                new DiscordInteractionResponseBuilder().WithContent($"Ping:{ctx.Client.Ping.ToString()}"));
        }
    }
}