using JetBrains.Annotations;
using NuGet.Protocol.Plugins;
using Rocket.API;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.SocialPlatforms;

namespace tpa
{
    public class Config : IRocketPluginConfiguration
    {
        public string Author;
        public float TimeForAccept;
        
        public void LoadDefaults()
        {
            Author = "MVP_Wendi";
            TimeForAccept = 5;
        }
    }
}
