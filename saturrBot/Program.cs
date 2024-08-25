using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using saturrBot.commands;
using saturrBot.config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saturrBot
{
    internal class Program
    {
        private static DiscordClient Client { get; set; }
        private static CommandsNextExtension Commands { get; set; }


        static async Task Main(string[] args)
        {

            var jsonReader = new JSONReader();
            await jsonReader.ReadJson();

            var discordConfig = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = jsonReader.token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,

            };
            Client = new DiscordClient(discordConfig);

            Client.Ready += Client_Ready;

            Client.GuildMemberAdded += OnGuildMemberAdded;

            var commandsConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { jsonReader.prefix },
                EnableMentionPrefix = true,
                EnableDms =true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<manageCommands>(); 

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private static Task Client_Ready(DiscordClient sender, DSharpPlus.EventArgs.ReadyEventArgs args)
        {
            return Task.CompletedTask;
        }

        private static async Task OnGuildMemberAdded(DiscordClient sender, GuildMemberAddEventArgs e)
        {
            string roleName = "Member";
            var role = e.Guild.Roles.Values.FirstOrDefault(r => r.Name.Equals(roleName, StringComparison.OrdinalIgnoreCase));

            if (role != null)
            {
                await e.Member.GrantRoleAsync(role);
                Console.WriteLine($"Роль {roleName} была автоматически выдана {e.Member.DisplayName}.");
            }
            else
            {
                Console.WriteLine($"Роль с именем {roleName} не найдена.");
            }
        }
    }
}

