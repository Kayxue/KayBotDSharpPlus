using System;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;

namespace Discord_Bot_CSharp.Commands
{
    public class Info : BaseCommandModule
    {
        [Command("test")]
        public async Task test_command(CommandContext ctx)
        {
            await ctx.RespondAsync("test");
        }
        
        [Command("ping")]
        public async Task ping_command(CommandContext ctx)
        {
            await ctx.RespondAsync(ctx.Client.Ping.ToString());
        }
    }
}