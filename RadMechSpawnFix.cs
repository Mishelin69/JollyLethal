using HarmonyLib;
using UnityEngine;

namespace JollyLethal;

[HarmonyPatch(typeof(RoundManager))]
internal class PutHatOnDormantOldBird
{
    [HarmonyPatch("SpawnNestObjectForOutsideEnemy")]
    [HarmonyPostfix]
    private static void AddHatToDormantOldBird(RoundManager __instance)
    {
        foreach (EnemyAINestSpawnObject netSpawn in __instance.enemyNestSpawnObjects)
        {
            string enemyName = netSpawn.enemyType.enemyName;
            JollyLethal.PluginLogInfoWithPrefix($"NestSpawn: {enemyName}");
            switch (enemyName)
            {
                case "RadMech":
                    PlaceHatOnDormantOldBird(netSpawn);
                    break;
            }
        } 
    }

    private static void PlaceHatOnDormantOldBird(EnemyAINestSpawnObject enemy)
    {
        JollyLethal.PluginLogInfoWithPrefix("Dormant birdo");
        PlaceHatsOnEnemiesPatch.PlaceHatOnEnemyTransform(enemy.transform, enemy.enemyType);
    }
}
