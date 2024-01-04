using System;
using UnityEngine;

namespace NoRBerserkMod
{
    internal class EnemyDatePatch
    {
        [global::HarmonyLib.HarmonyPatch(typeof(global::EnemyDate), "Nakadasi")]
        [global::HarmonyLib.HarmonyPostfix]
        private static void OnCreampie(global::EnemyDate __instance, global::PlayerStatus ___playerstatus)
        {
            ___playerstatus.Sp += UnityEngine.Random.Range((0.1f * ___playerstatus.AllMaxSP()), (1f * ___playerstatus.AllMaxSP()));
        }

        [global::HarmonyLib.HarmonyPatch(typeof(global::EnemyDate), "WeaponDamage")]
        [global::HarmonyLib.HarmonyPatch(typeof(global::EnemyDate), "StabDamage")]
        [global::HarmonyLib.HarmonyPrefix]
        private static void SkipPleasureOnHit(global::PlayerStatus ___playerstatus)
        {
            // Pleasure skip multiplier
            float LostHp = 1 - (___playerstatus.Hp / ___playerstatus.AllMaxHP());
            if (___playerstatus._BadstatusVal[0] > 0)
            {
                ___playerstatus._BadstatusVal[0] -= (___playerstatus.Sp * 0.1f) * UnityEngine.Mathf.Lerp(1, Plugin.PleasureSkipMult.Value, LostHp);
                if (___playerstatus._BadstatusVal[0] < 0)
                {
                    ___playerstatus._BadstatusVal[0] = 0;
                }
            }

        }
    }
}
