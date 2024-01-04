using System;
using HarmonyLib;
using UnityEngine;

namespace NoRBerserkMod
{
    internal class PlayerStatusPatch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::PlayerStatus), "_atk_speed", global::HarmonyLib.MethodType.Getter)]
        [global::HarmonyLib.HarmonyPostfix]
        private static void IncreaseAttackSpeed(global::PlayerStatus __instance, ref float __result)
        {
            float LostHp = 1 - (__instance.Hp / __instance.AllMaxHP());
            __result *= global::UnityEngine.Mathf.Lerp(1, Plugin.AtkSpeedMult.Value, LostHp);
        }

        [global::HarmonyLib.HarmonyPatch(typeof(global::PlayerStatus), "_ATK", global::HarmonyLib.MethodType.Getter)]
        [global::HarmonyLib.HarmonyPostfix]
        private static void DecreaseAttackDamage(global::PlayerStatus __instance, ref float __result)
        {
            float LostHp = 1 - (__instance.Hp / __instance.AllMaxHP());
            __result *= global::UnityEngine.Mathf.Lerp(1, Plugin.DamageMult.Value, LostHp);
        }

        public PlayerStatusPatch()
        {
        }
    }
}
