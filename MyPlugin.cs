using Rocket.API;
using Rocket.Core.Plugins;
using SDG.Unturned;
using System.Collections.Generic;
using UnityEngine;
using Rocket.Unturned.Player;
using System;
using Rocket.API.Collections;
using Rocket.Unturned.Chat;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Unturned;
using Rocket.Core.Steam;
using Rocket.Unturned.Extensions;
using MoreLinq;
using System.Linq;
using Rocket.Core;
using System.ServiceModel.Configuration;
using MoreLinq.Extensions;
using Rocket.Unturned.Permissions;
using System.Collections;
using System.Web.UI.WebControls;
using Rocket.Unturned.Events;

namespace tpa
{
    public class MyPlugin : RocketPlugin<Config>
    {

        public static MyPlugin Instance;
        public class TpaPlayer
        {
            public UnturnedPlayer fromUplayer;
            public UnturnedPlayer toUplayer;
        }

        public List<TpaPlayer> PlayersTpaList = new List<TpaPlayer>();
        protected override void Load()
        {
            base.Load();
            PlayersTpaList.Clear();
            Instance = this;
            EffectManager.onEffectButtonClicked += onClicked;

        }
        public void StartC(UnturnedPlayer upla)
        {
            StartCoroutine(DestroyUI(upla));
        }
        public IEnumerator DestroyUI(UnturnedPlayer player)
        {
            yield return new WaitForSeconds(MyPlugin.Instance.Configuration.Instance.TimeForAccept);
            EffectManager.askEffectClearByID(14100, player.CSteamID);           
            var Players = PlayersTpaList.Find(p => p.toUplayer.CharacterName == player.CharacterName);
            if (Players != null)
            {
                PlayersTpaList.Remove(Players);
                UnturnedChat.Say(Players.fromUplayer.CSteamID, "Игрок не ответил на запрос");
            }
        }
        public void onClicked(Player player, string button)
        {  
            UnturnedPlayer uplayer = UnturnedPlayer.FromPlayer(player);
            var Players = PlayersTpaList.Find(p => p.toUplayer.CharacterName == uplayer.CharacterName);
            if (button == "A_Button")
            {
                UnturnedChat.Say(Players.fromUplayer.CSteamID, "Игрок принял запрос");
                Players.fromUplayer.Teleport(Players.toUplayer.Position, 0);
                PlayersTpaList.Remove(Players);
                EffectManager.askEffectClearByID(14100, Players.toUplayer.CSteamID);
            }
            if (button == "D_Button")
            {
                UnturnedChat.Say(Players.fromUplayer.CSteamID, "Игрок отклонил запрос");
                PlayersTpaList.Remove(Players);
                EffectManager.askEffectClearByID(14100, Players.toUplayer.CSteamID);
            }
        }
        protected override void Unload()
        {
            base.Unload();
            Instance = null;
            EffectManager.onEffectButtonClicked -= onClicked;
        }
    }
}