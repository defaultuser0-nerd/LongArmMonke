using BepInEx;
using BepInEx.Configuration;
using Photon.Voice.Unity;
using System;
using System.Collections.Generic;
using System.Text;

namespace LongarmMonke
{
    [BepInPlugin("default.LongArmMonke", "LongArmMonke", "1.0.1")]
    public class Mod : BaseUnityPlugin 
    {
        public static ConfigEntry<bool> Arms;

        void Start() =>
            Arms = Config.Bind("Settings", "LongArms Enabled", false);
    }
}
