using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace Discord_Bot_CSharp.Commands
{
    public class Moderation : BaseCommandModule
    {
        [Command("clear")]
        [RequireUserPermissions(Permissions.ManageMessages)]
        public async Task clear_command(CommandContext ctx, int message_count)
        {
            if (message_count >= 100)
            {
                await ctx.RespondAsync("訊息數量超過100！");
            }

            List<DiscordMessage> messages = (await ctx.Channel.GetMessagesAsync(message_count)).ToList();
            await ctx.Channel.DeleteMessagesAsync(messages);
            
            DiscordEmbedBuilder discordEmbedBuilder=new DiscordEmbedBuilder()
            {
                Title = "已成功刪除訊息",
                Description = $"已成功刪除{messages.Count.ToString()}則訊息！"
            }.WithColor(0x01afef);

            await ctx.RespondAsync(embed: discordEmbedBuilder);
        }

        [Command("ban")]
        public async Task ban_command(CommandContext ctx,DiscordUser userToBan)
        {
            await ctx.Guild.BanMemberAsync(userToBan.Id);
            await ctx.RespondAsync("成功！");
        }

        [Command("kick")]
        public async Task kick_command(CommandContext ctx, DiscordUser userToKick)
        {
            DiscordMember memberToKick=await ctx.Guild.GetMemberAsync(userToKick.Id);
            await memberToKick.RemoveAsync();
            await ctx.RespondAsync($"成功將{userToKick.Username}#{userToKick.Discriminator}踢出伺服器！");
        }
    }
}