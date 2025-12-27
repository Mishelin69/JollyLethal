using HarmonyLib;
using UnityEngine;
using System.Linq;

namespace JollyLethal;

[HarmonyPatch(typeof(RoundManager))]
internal class EnemyDumper
{
    [HarmonyPatch("Start")]
    [HarmonyPostfix]
    private static void DumpEveryEnemy()
    {
        JollyLethal.PluginLogInfoWithPrefix("=== STARTING ENEMY NAME DUMP ===");

        EnemyType[] allEnemies = Resources.FindObjectsOfTypeAll<EnemyType>();

        var distinctEnemies = allEnemies
            .Select(e => e.enemyName)
            .Distinct()
            .OrderBy(n => n);

        foreach (string name in distinctEnemies)
        {
            JollyLethal.PluginLogInfoWithPrefix($"Found Enemy Name: \"{name}\"");
        }

        JollyLethal.PluginLogInfoWithPrefix("=== DUMP FINISHED ===");
    }
}
