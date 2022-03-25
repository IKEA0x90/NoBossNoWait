using BepInEx;
using RoR2;
using System;
namespace NoBossNoWait
{
    [BepInDependency(R2API.R2API.PluginGUID)]
    [BepInPlugin(PluginGUID, PluginName, PluginVersion)]
	
	public class NoBossNoWait : BaseUnityPlugin
	{
        public const string PluginGUID = PluginAuthor + "." + PluginName;
        public const string PluginAuthor = "IKEA";
        public const string PluginName = "NoBossNoWait";
        public const string PluginVersion = "1.0.0";

        public void Awake()
        {
            On.RoR2.TeleporterInteraction.UpdateMonstersClear += (orig, self) =>
            {
                orig(self);
                if (self.monstersCleared && self.holdoutZoneController && self.activationState == TeleporterInteraction.ActivationState.Charging && self.chargeFraction > 0.02f)
                {
                    int displayChargePercent = TeleporterInteraction.instance.holdoutZoneController.displayChargePercent;
                    float runStopwatch = Run.instance.GetRunStopwatch();
                    int num = Math.Min(Util.GetItemCountForTeam(self.holdoutZoneController.chargingTeam, RoR2Content.Items.FocusConvergence.itemIndex, true, true), 3);
                    float num2 = (100f - (float)displayChargePercent) / 100f * (TeleporterInteraction.instance.holdoutZoneController.baseChargeDuration / (1f + 0.3f * (float)num));
                    num2 = (float)Math.Round((double)num2, 2);
                    float runStopwatch2 = runStopwatch + (float)Math.Round((double)num2, 2);
                    Run.instance.SetRunStopwatch(runStopwatch2);
                    TeleporterInteraction.instance.holdoutZoneController.FullyChargeHoldoutZone();
                    //Chat.AddMessage("Added " + num2.ToString() + " seconds to the game timer.");
                }
            };
        }


    }
}
