using HarmonyLib;
using UnityEngine;

namespace JollyLethal.HatActions;

[HarmonyPatch(typeof(MaskedPlayerEnemy))]
internal class PlaceHatOnMaskedPatch
{
    const float myWaitForSeconds = 1.5f;
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void GiveHatToMasked(MaskedPlayerEnemy __instance)
    {
        __instance.StartCoroutine(GiveHatToMaskedCoroutine(__instance));
    }

    private static System.Collections.IEnumerator GiveHatToMaskedCoroutine(MaskedPlayerEnemy enemy)
    {
        yield return new WaitForSeconds(myWaitForSeconds);

        JollyLethal.PluginLogInfoWithPrefix("Placing santa hat on masked");
        PlaceHatsOnEnemiesPatch.PlaceHatOnEnemyTransform(enemy.transform, enemy.enemyType);
    }
}
