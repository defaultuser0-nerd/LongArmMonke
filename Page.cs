using System;
using System.Collections.Generic;
using System.Text;
using BananaOS;
using BananaOS.Pages;
using UnityEngine;
using GorillaNetworking;
using Unity.Burst.Intrinsics;
using Player = GorillaLocomotion.Player;
using BepInEx;

namespace LongarmMonke
{
    [BepInPlugin("default.LongarmMonke", "LongarmMonke", "1.0.0")]
    public class Mod : BaseUnityPlugin { }
    public class Page : WatchPage
    {
        private const string PageTitle = "<i><color=#fd5c63>Long Arm Monke</color></i>";

        public override bool DisplayOnMainMenu => true;
        public bool arms;
        public override string Title => PageTitle;
        public override void OnPostModSetup() => selectionHandler.maxIndex = 0;
         
        public override string OnGetScreenContent()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<size=0.3>    </size>");
            stringBuilder.AppendLine("   <i><color=#fd5c63>Long Arm Monke:</color></i>");
            stringBuilder.AppendLine("<size=0.25>    </size>");
            if (NetworkSystem.Instance.InRoom && NetworkSystem.Instance.GameModeString.Contains("MODDED"))
            {
                if (arms)
                    stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "<size=0.57><color=#00FF40>Enabled</color>"));
                else
                    stringBuilder.AppendLine(selectionHandler.GetOriginalBananaOSSelectionText(0, "<size=0.57><color=#AA0000>Disabled</color>"));
            }
            else
            {
                stringBuilder.AppendLine("   <size=0.57>But mod is not enabled, due to the\n    user not being in a modded lobby"); // tis is what rainwave said it said if u werent in a private/moded
            }

            return stringBuilder.ToString();
        }

        public override void OnButtonPressed(WatchButtonType buttonType)
        {
            switch (buttonType)
            {
                case WatchButtonType.Up:
                    selectionHandler.MoveSelectionUp();
                    break;

                case WatchButtonType.Down:
                    selectionHandler.MoveSelectionDown();
                    break;

                case WatchButtonType.Enter:
                    arms = !arms;
                    break;

                case WatchButtonType.Back:
                    ReturnToMainMenu();
                    break;
            }
        }
        void Update()
        {
            if (arms && NetworkSystem.Instance.InRoom && NetworkSystem.Instance.GameModeString.Contains("MODDED")) 
            {
                Player.Instance.leftControllerTransform.transform.position = GorillaTagger.Instance.leftHandTransform.position + (GorillaTagger.Instance.leftHandTransform.forward * 0.35f);
                Player.Instance.rightControllerTransform.transform.position = GorillaTagger.Instance.rightHandTransform.position + (GorillaTagger.Instance.rightHandTransform.forward * 0.35f);
            }
        }
    }
}
