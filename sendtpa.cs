using Rocket.API;
using Rocket.Core;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using UnityEngine;
using UnityEngine.Bindings;
using UnityEngine.Scripting;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.Xml;

namespace tpa
{
    public class TPA : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Player;

        public string Name => "sendtpa";

        public string Help => "";

        public string Syntax => "";

        public List<string> Aliases => new List<string> { "sendtpa" };

        public List<string> Permissions => new List<string> { "sendtpa" };
        public UnturnedPlayer uplayer;
        public void Execute(IRocketPlayer caller, string[] command)
        {
            uplayer = (UnturnedPlayer)caller;
            if(command.Length==1)
            {
                UnturnedPlayer uplayer2 = UnturnedPlayer.FromName(command[0]);
                if (uplayer2 != null)
                {

                    MyPlugin.Instance.PlayersTpaList.Add(new MyPlugin.TpaPlayer { fromUplayer = uplayer, toUplayer = uplayer2 });
                    EffectManager.sendUIEffect(14100, 1, uplayer2.CSteamID, true);
                    EffectManager.sendUIEffectText(1, uplayer2.CSteamID, true, "PlayerName", uplayer.CharacterName);
                    EffectManager.sendUIEffectImageURL(1, uplayer2.CSteamID, true, "Profile", Convert.ToString(uplayer.SteamProfile.AvatarFull));
                    UnturnedChat.Say(uplayer.CSteamID, "Запрос отправлен", UnityEngine.Color.green);
                    MyPlugin.Instance.StartC(uplayer2);
                }
                else
                {
                    UnturnedChat.Say(uplayer.CSteamID, "Игрок не найден", UnityEngine.Color.red);
                }
            }
            else
            {
                UnturnedChat.Say(uplayer.CSteamID, "Неправильно написана команда", UnityEngine.Color.red);
            }
        }
    }
}
