using HarmonyLib;
using UnityEngine;

namespace JollyLethal;

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
        if (enemy is null || enemy.isEnemyDead) {
            yield break;
        }

        var (bonePath, posOffset, rotOffset, scale) = PlaceHatsOnEnemiesPatch.GetEnemyHatConfig("Masked");
        if (bonePath is null)
        {
            yield break;
        }
        Transform targetBone = PlaceHatsOnEnemiesPatch.GetHatPlaceTransform(enemy.transform, bonePath);
        PlaceHatsOnEnemiesPatch.SpawnSantaHatOnEnemy(targetBone, posOffset, rotOffset, scale); 
    }
}
