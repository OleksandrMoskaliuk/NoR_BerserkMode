using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoRBerserkMod
{
    internal class PlayerConPatch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "Start")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void StartFunctionPatch(global::playercon __instance, PlayerStatus ___playerstatus)
        {
            // Get all components for update
            Plugin.mPlayerstatus = ___playerstatus;
            Plugin.mPlayercon = __instance;
        }

        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "step_fun")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void PlayerMoveAndDashSpeed(global::playercon __instance, ref float ___MOVESPD)
        {
            // Longer dash on low hp
            float LostHp = 1 - (Plugin.mPlayerstatus.Hp / Plugin.mPlayerstatus.AllMaxHP());
            __instance.movespeed = UnityEngine.Mathf.Lerp(3f, (3f * Plugin.DashMult.Value), LostHp);
        }

        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::playercon), "fun_damage_Improvement")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void DamageResist(ref float getatk, ref int kickbackkind, global::PlayerStatus ___playerstatus)
        {
            // Increase damage resist on low hp
            float LostHp =  1 - (___playerstatus.Hp / ___playerstatus.AllMaxHP());
            getatk *= global::UnityEngine.Mathf.Lerp(1f, (1f / Plugin.DamageResistMult.Value), LostHp);
        }
    }

    



}