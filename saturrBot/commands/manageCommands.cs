using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saturrBot.commands
{
    internal class manageCommands : BaseCommandModule
    {
        [Command("sendMassage")]
        public async Task sendMassage(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Hi guys");
        }

        
    }
}

